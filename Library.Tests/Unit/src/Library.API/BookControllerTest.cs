using FluentAssertions;
using Library.API.Controllers;
using Library.Application.UseCases.Commands.CreateBook;
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
                Status = Domain.Enums.EntityStatus.Active,
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
        public async Task Create_Should_Return_BadRequest_With_CreateBookResponse()
        {
            // Arrange
            var request = new CreateBookRequest
            (
                "Teste Tiulo",
                "",
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
    }
}
