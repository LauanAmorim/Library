using AutoMapper;
using FluentAssertions;
using Library.API.Controllers;
using Library.Application.UseCases.Commands.CreateBook;
using Library.Domain.Entities;
using Library.Domain.Enums;
using Library.Domain.Interfaces;
using Library.Domain.ValueObjects;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Tests.Unit.src.Library.Application.UseCases.Commands
{
    public class CreateBookHandlerTest
    {
        private readonly Mock<IUnitOfWork> _mockUnitOfWork;
        private readonly Mock<IBookRepository> _mockBookRepository;
        private readonly Mock<IMapper> _mockMapper;
        private readonly CreateBookHandler _handler;

        public CreateBookHandlerTest()
        {
            _mockUnitOfWork = new Mock<IUnitOfWork>();
            _mockBookRepository = new Mock<IBookRepository>();
            _mockMapper = new Mock<IMapper>();
            _handler = new CreateBookHandler(_mockUnitOfWork.Object, _mockBookRepository.Object, _mockMapper.Object);
        }

        [Fact]
        public async Task Handle_ValidRequest_ShouldCreateBookAndCommitTransaction()
        {
            // Arrange
            var request = new CreateBookRequest("Sample Title", "Sample Author", new ISBN("1234567890123"), DateTime.Now);
            var book = new Book(); // entidade mapeada

            _mockMapper.Setup(m => m.Map<Book>(request)).Returns(book);
            _mockMapper.Setup(m => m.Map<CreateBookResponse>(book)).Returns(new CreateBookResponse());

            // Act
            var result = await _handler.Handle(request, CancellationToken.None);

            // Assert
            _mockBookRepository.Verify(r => r.Create(It.IsAny<Book>()), Times.Once);
            _mockUnitOfWork.Verify(u => u.Commit(It.IsAny<CancellationToken>()), Times.Once);
            result.Should().NotBeNull();
            result.Should().BeOfType<CreateBookResponse>();
        }

        [Fact]
        public async Task Handle_ExceptionThrownInUnitOfWork_ShouldThrowException()
        {
            // Arrange
            var request = new CreateBookRequest("Sample Title", "Sample Author", new ISBN("1234567890123"), DateTime.Now);
            _mockUnitOfWork.Setup(u => u.Commit(It.IsAny<CancellationToken>())).ThrowsAsync(new Exception("Commit failed"));

            // Act & Assert
            await Assert.ThrowsAsync<Exception>(() => _handler.Handle(request, CancellationToken.None));
        }
    }
}
