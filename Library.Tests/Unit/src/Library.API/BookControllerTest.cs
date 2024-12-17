using FluentAssertions;
using Library.API.Controllers;
using Library.Application.UseCases.Commands.CreateBook;
using Library.Application.UseCases.Commands.DeleteBook;
using Library.Application.UseCases.Commands.RestoreBook;
using Library.Application.UseCases.Commands.UpdateBook;
using Library.Domain.Enums;
using Library.Domain.ValueObjects;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Tests.Unit.src.Library.API
{
    public class BookControllerTest
    {
        private readonly Mock<IMediator> _mediatorMock;
        private readonly BookController _controller;

        public BookControllerTest()
        {
            _mediatorMock = new Mock<IMediator>();
            _controller = new BookController(_mediatorMock.Object);
        }

        [Fact]
        public async Task Create_Should_Return_Ok_With_CreateBookResponse()
        {
            // Arrange
            var request = new CreateBookRequest
            (
                "Teste Tiulo",
                "Teste Autor",
                new ISBN("1234567890"),
                DateTime.Now
            );

            var expectedResponse = new CreateBookResponse
            {
                Id = Guid.NewGuid(),
                Title = "Teste Titulo",
                Author = "Teste Autor",
                Isbn = new ISBN("1234567890"),
                ReleaseDate = DateTime.Now,
                Status = EntityStatus.Active,
            };

            _mediatorMock.Setup(m => m.Send(It.IsAny<CreateBookRequest>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(expectedResponse);

            // Act
            var result = await _controller.Create(request, CancellationToken.None);

            // Assert
            result.Result.Should().BeOfType<OkObjectResult>();

            var okResult = result.Result as OkObjectResult;
            okResult?.Value.Should().BeEquivalentTo(expectedResponse);
            _mediatorMock.Verify(m => m.Send(It.Is<CreateBookRequest>(r => r == request), It.IsAny<CancellationToken>()), Times.Once);
        }
        [Fact]
        public async Task Create_Should_Return_BadRequest_With_CreateBookRequest_NoTitle()
        {
            // Arrange
            var request = new CreateBookRequest
            (
                "",
                "Teste Autor",
                new ISBN("1234567890"),
                DateTime.Now
            );

            // Act
            var result = await _controller.Create(request, CancellationToken.None);

            // Assert
            var badRequestResult = result.Result as BadRequestObjectResult;
            badRequestResult.Should().NotBeNull();
            badRequestResult?.StatusCode.Should().Be(400);
        }
        [Fact]
        public async Task Create_InvalidRequest_ShouldReturnBadRequest()
        {
            // Arrange
            var request = new CreateBookRequest("", "Valid Author", new ISBN("1234567890123"), DateTime.Now);

            // Act
            var result = await _controller.Create(request, CancellationToken.None);

            // Assert
            var badRequestResult = result.Result as BadRequestObjectResult;
            badRequestResult.Should().NotBeNull();
            badRequestResult!.StatusCode.Should().Be(400);
            badRequestResult.Value.Should().Be("Invalid request data");
        }

        [Fact]
        public async Task Create_MediatorThrowsException_ShouldThrowException()
        {
            // Arrange
            var request = new CreateBookRequest("Valid Title", "Valid Author", new ISBN("1234567890123"), DateTime.Now);

            _mediatorMock.Setup(m => m.Send(request, It.IsAny<CancellationToken>())).ThrowsAsync(new Exception("Mediator failed"));

            // Act & Assert
            await Assert.ThrowsAsync<Exception>(() => _controller.Create(request, CancellationToken.None));
        }

        [Fact]
        public async Task Delete_Should_Return_Ok_With_Response_When_BookIsDeleted()
        {
            // Arrange
            var bookId = Guid.NewGuid();
            var response = new DeleteBookResponse
            {
                Id = bookId,
                Title = "Sample Title",
                Author = "Sample Author",
                Isbn = new ISBN("1234567890"),
                Status = EntityStatus.Active,
                ReleaseDate = DateTime.Now
            };

            _mediatorMock.Setup(m => m.Send(It.IsAny<DeleteBookRequest>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(response);

            // Act
            var result = await _controller.Delete(bookId, CancellationToken.None);

            // Assert
            result.Should().BeOfType<OkObjectResult>();
            var okResult = result as OkObjectResult;
            okResult!.Value.Should().BeEquivalentTo(response);

            _mediatorMock.Verify(m => m.Send(It.Is<DeleteBookRequest>(r => r.Id == bookId), It.IsAny<CancellationToken>()), Times.Once);
        }

        [Fact]
        public async Task Delete_Should_Return_BadRequest_When_IdIsNull()
        {
            // Arrange
            Guid? bookId = null;

            // Act
            var result = await _controller.Delete(bookId, CancellationToken.None);

            // Assert
            result.Should().BeOfType<BadRequestResult>();
            _mediatorMock.Verify(m => m.Send(It.IsAny<DeleteBookRequest>(), It.IsAny<CancellationToken>()), Times.Never);
        }

        [Fact]
        public async Task Delete_Should_Return_NotFound_When_BookDoesNotExist()
        {
            // Arrange
            var bookId = Guid.NewGuid();

            _mediatorMock.Setup(m => m.Send(It.IsAny<DeleteBookRequest>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync((DeleteBookResponse?)null);

            // Act
            var result = await _controller.Delete(bookId, CancellationToken.None);

            // Assert
            result.Should().BeOfType<NotFoundResult>();
            _mediatorMock.Verify(m => m.Send(It.Is<DeleteBookRequest>(r => r.Id == bookId), It.IsAny<CancellationToken>()), Times.Once);
        }

        [Fact]
        public async Task Update_Should_Return_BadRequest_When_Id_Does_Not_Match_Request()
        {
            // Arrange
            var id = Guid.NewGuid();
            var request = new UpdateBookRequest(Guid.NewGuid(), "Title", "Author", DateTime.UtcNow);

            // Act
            var result = await _controller.Update(id, request, CancellationToken.None);

            // Assert
            result.Result.Should().BeOfType<BadRequestResult>();
        }

        [Fact]
        public async Task Update_Should_Return_Ok_When_Book_Is_Updated()
        {
            // Arrange
            var id = Guid.NewGuid();
            var request = new UpdateBookRequest(id, "Title", "Author", DateTime.UtcNow);
            var response = new UpdateBookResponse
            {
                Id = id,
                Title = "Updated Title",
                Author = "Updated Author",
                ReleaseDate = DateTime.UtcNow
            };

            _mediatorMock
                .Setup(m => m.Send(request, It.IsAny<CancellationToken>()))
                .ReturnsAsync(response);

            // Act
            var result = await _controller.Update(id, request, CancellationToken.None);

            // Assert
            result.Result.Should().BeOfType<OkObjectResult>()
                .Which.Value.Should().BeEquivalentTo(response);
        }

        [Fact]
        public async Task Update_Should_Return_NotFound_When_Book_Does_Not_Exist()
        {
            // Arrange
            var id = Guid.NewGuid();
            var request = new UpdateBookRequest(id, "Title", "Author", DateTime.UtcNow);

            _mediatorMock
                .Setup(m => m.Send(request, It.IsAny<CancellationToken>()))
                .ReturnsAsync((UpdateBookResponse)null);

            // Act
            var result = await _controller.Update(id, request, CancellationToken.None);

            // Assert
            result.Result.Should().BeOfType<NotFoundResult>();
        }

        [Fact]
        public async Task Restore_Should_Return_Ok_When_Book_Is_Restored()
        {
            // Arrange
            var bookId = Guid.NewGuid();
            var response = new RestoreBookResponse
            {
                Id = bookId,
                Title = "Restored Book",
                Author = "Restored Author",
                Isbn = new ISBN("1234567890"),
                Status = EntityStatus.Active,
                ReleaseDate = DateTime.Now
            };

            _mediatorMock.Setup(m => m.Send(It.IsAny<RestoreBookRequest>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(response);

            // Act
            var result = await _controller.Restore(bookId, CancellationToken.None);

            // Assert
            result.Should().BeOfType<OkObjectResult>();
            var okResult = result as OkObjectResult;
            okResult!.Value.Should().BeEquivalentTo(response);

            _mediatorMock.Verify(m => m.Send(It.Is<RestoreBookRequest>(r => r.Id == bookId), It.IsAny<CancellationToken>()), Times.Once);
        }

        [Fact]
        public async Task Restore_Should_Return_NotFound_When_Book_Does_Not_Exist()
        {
            // Arrange
            var bookId = Guid.NewGuid();

            _mediatorMock.Setup(m => m.Send(It.IsAny<RestoreBookRequest>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync((RestoreBookResponse?)null);

            // Act
            var result = await _controller.Restore(bookId, CancellationToken.None);

            // Assert
            result.Should().BeOfType<NotFoundResult>();
            _mediatorMock.Verify(m => m.Send(It.Is<RestoreBookRequest>(r => r.Id == bookId), It.IsAny<CancellationToken>()), Times.Once);
        }

    }
}
