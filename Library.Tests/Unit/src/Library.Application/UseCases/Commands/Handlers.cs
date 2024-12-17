using AutoMapper;
using FluentAssertions;
using Library.Application.UseCases.Commands.CreateBook;
using Library.Application.UseCases.Commands.DeleteBook;
using Library.Application.UseCases.Commands.RestoreBook;
using Library.Application.UseCases.Commands.UpdateBook;
using Library.Domain.Entities;
using Library.Domain.Enums;
using Library.Domain.Interfaces;
using Library.Domain.ValueObjects;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Tests.Unit.src.Library.Application.UseCases.Commands
{
    public class BookHandlersTests
    {
        private readonly Mock<IBookRepository> _bookRepositoryMock;
        private readonly Mock<IUnitOfWork> _unitOfWorkMock;
        private readonly IMapper _mapper;

        // Handlers
        private readonly DeleteBookHandler _deleteHandler;
        private readonly UpdateBookHandler _updateHandler;
        private readonly CreateBookHandler _createHandler;

        public BookHandlersTests()
        {
            _bookRepositoryMock = new Mock<IBookRepository>();
            _unitOfWorkMock = new Mock<IUnitOfWork>();

            var mapperConfig = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<DeleteBookMapper>();
                cfg.AddProfile<CreateBookMapper>();
                cfg.AddProfile<UpdateBookMapper>();
            });
            _mapper = mapperConfig.CreateMapper();

            _deleteHandler = new DeleteBookHandler(_unitOfWorkMock.Object, _bookRepositoryMock.Object, _mapper);
            _updateHandler = new UpdateBookHandler(_unitOfWorkMock.Object, _bookRepositoryMock.Object, _mapper);
            _createHandler = new CreateBookHandler(_unitOfWorkMock.Object, _bookRepositoryMock.Object, _mapper);
        }

        // ============================
        // TESTES PARA DeleteBookHandler
        // ============================
        [Fact]
        public async Task DeleteBookHandler_Should_Return_Null_When_Book_Not_Found()
        {
            var request = new DeleteBookRequest(Guid.NewGuid());

            _bookRepositoryMock.Setup(repo => repo.Get(request.Id, It.IsAny<CancellationToken>()))
                               .ReturnsAsync((Book)null);

            var result = await _deleteHandler.Handle(request, CancellationToken.None);

            result.Should().BeNull();
            _bookRepositoryMock.Verify(repo => repo.Delete(It.IsAny<Book>()), Times.Never);
            _unitOfWorkMock.Verify(uow => uow.Commit(It.IsAny<CancellationToken>()), Times.Never);
        }

        [Fact]
        public async Task DeleteBookHandler_Should_Delete_Book_And_Return_Response()
        {
            var book = new Book { Id = Guid.NewGuid(), Title = "Test Book" };
            var request = new DeleteBookRequest(book.Id);

            _bookRepositoryMock.Setup(repo => repo.Get(book.Id, It.IsAny<CancellationToken>()))
                               .ReturnsAsync(book);

            var result = await _deleteHandler.Handle(request, CancellationToken.None);

            result.Should().NotBeNull();
            result.Id.Should().Be(book.Id);

            _bookRepositoryMock.Verify(repo => repo.Delete(It.Is<Book>(b => b == book)), Times.Once);
            _unitOfWorkMock.Verify(uow => uow.Commit(It.IsAny<CancellationToken>()), Times.Once);
        }

        // ============================
        // TESTES PARA UpdateBookHandler
        // ============================
        [Fact]
        public async Task UpdateBookHandler_Should_Return_Null_When_Book_Not_Found()
        {
            var request = new UpdateBookRequest(Guid.NewGuid(), "New Title", "New Author", DateTime.Now);

            _bookRepositoryMock.Setup(repo => repo.Get(request.Id, It.IsAny<CancellationToken>()))
                               .ReturnsAsync((Book)null);

            var result = await _updateHandler.Handle(request, CancellationToken.None);

            result.Should().BeNull();
            _unitOfWorkMock.Verify(uow => uow.Commit(It.IsAny<CancellationToken>()), Times.Never);
        }

        [Fact]
        public async Task UpdateBookHandler_Should_Update_Book_And_Return_Response()
        {
            var book = new Book { Id = Guid.NewGuid(), Title = "Old Title" };
            var request = new UpdateBookRequest(book.Id, "New Title", "New Author", DateTime.Now);

            _bookRepositoryMock.Setup(repo => repo.Get(book.Id, It.IsAny<CancellationToken>()))
                               .ReturnsAsync(book);

            var result = await _updateHandler.Handle(request, CancellationToken.None);

            result.Should().NotBeNull();
            result.Title.Should().Be("New Title");
            result.Author.Should().Be("New Author");

            _unitOfWorkMock.Verify(uow => uow.Commit(It.IsAny<CancellationToken>()), Times.Once);
        }

        // ============================
        // TESTES PARA RestoreBookHandler
        // ============================
        [Fact]
        public async Task RestoreBookHandler_Should_Return_Null_When_Book_Not_Found()
        {
            var request = new RestoreBookRequest(Guid.NewGuid());

            _bookRepositoryMock.Setup(repo => repo.Get(request.Id, It.IsAny<CancellationToken>()))
                               .ReturnsAsync((Book)null);

            var handler = new RestoreBookHandler(_unitOfWorkMock.Object, _bookRepositoryMock.Object, _mapper);
            var result = await handler.Handle(request, CancellationToken.None);

            result.Should().BeNull();
            _unitOfWorkMock.Verify(uow => uow.Commit(It.IsAny<CancellationToken>()), Times.Never);
        }

        [Fact]
        public async Task RestoreBookHandler_Should_Restore_Book_And_Return_Response()
        {
            var book = new Book { Id = Guid.NewGuid(), Title = "Deleted Book", Status = EntityStatus.Inactive};
            var request = new RestoreBookRequest(book.Id);

            _bookRepositoryMock.Setup(repo => repo.Get(book.Id, It.IsAny<CancellationToken>()))
                               .ReturnsAsync(book);

            var handler = new RestoreBookHandler(_unitOfWorkMock.Object, _bookRepositoryMock.Object, _mapper);
            var result = await handler.Handle(request, CancellationToken.None);

            result.Should().NotBeNull();
            result.Id.Should().Be(book.Id);
            result.Status.Should().Be(EntityStatus.Active);

            _unitOfWorkMock.Verify(uow => uow.Commit(It.IsAny<CancellationToken>()), Times.Once);
        }
    }
}
