using AutoMapper;
using FluentAssertions;
using Library.Application.UseCases.Commands.CreateBook;
using Library.Application.UseCases.Commands.DeleteBook;
using Library.Application.UseCases.Commands.UpdateBook;
using Library.Domain.Entities;
using Library.Domain.Enums;
using Library.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Tests.Unit.src.Library.Application.UseCases.Commands
{
    public class BookMappersTests
    {
        private readonly IMapper _mapper;

        public BookMappersTests()
        {
            var mapperConfig = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<DeleteBookMapper>();
                cfg.AddProfile<CreateBookMapper>();
                cfg.AddProfile<UpdateBookMapper>();
            });

            _mapper = mapperConfig.CreateMapper();
        }

        // ================================
        // TESTES PARA DeleteBookMapper
        // ================================
        [Fact]
        public void DeleteBookMapper_Should_Map_DeleteBookRequest_To_Book()
        {
            var request = new DeleteBookRequest(Guid.NewGuid());

            var result = _mapper.Map<Book>(request);

            result.Should().NotBeNull();
            result.Id.Should().Be(request.Id);
        }

        [Fact]
        public void DeleteBookMapper_Should_Map_Book_To_DeleteBookResponse()
        {
            var book = new Book
            {
                Id = Guid.NewGuid(),
                Title = "Test Title",
                Author = "Test Author",
                Isbn = new ISBN("1234567890"),
                ReleaseDate = DateTime.UtcNow
            };

            var result = _mapper.Map<DeleteBookResponse>(book);

            result.Should().NotBeNull();
            result.Id.Should().Be(book.Id);
            result.Title.Should().Be(book.Title);
            result.Author.Should().Be(book.Author);
            result.Isbn.Should().Be(book.Isbn);
            result.Status.Should().Be(book.Status);
            result.ReleaseDate.Should().Be(book.ReleaseDate);
        }

        // ================================
        // TESTES PARA CreateBookMapper
        // ================================
        [Fact]
        public void CreateBookMapper_Should_Map_CreateBookRequest_To_Book()
        {
            var request = new CreateBookRequest("Test Title", "Test Author", new ISBN("1234567890"), DateTime.Now);

            var result = _mapper.Map<Book>(request);

            result.Should().NotBeNull();
            result.Title.Should().Be(request.title);
            result.Author.Should().Be(request.author);
            result.Isbn.Should().Be(request.isbn);
        }

        [Fact]
        public void CreateBookMapper_Should_Map_Book_To_CreateBookResponse()
        {
            var book = new Book
            {
                Id = Guid.NewGuid(),
                Title = "Test Title",
                Author = "Test Author",
                Isbn = new ISBN("1234567890"),
                ReleaseDate = DateTime.UtcNow
            };

            var result = _mapper.Map<CreateBookResponse>(book);

            result.Should().NotBeNull();
            result.Id.Should().Be(book.Id);
            result.Title.Should().Be(book.Title);
            result.Author.Should().Be(book.Author);
            result.Isbn.Should().Be(book.Isbn);
            result.Status.Should().Be(book.Status);
            result.ReleaseDate.Should().Be(book.ReleaseDate);
        }

        // ================================
        // TESTES PARA UpdateBookMapper
        // ================================
        [Fact]
        public void UpdateBookMapper_Should_Map_UpdateBookRequest_To_Book()
        {
            var request = new UpdateBookRequest(Guid.NewGuid(), "Updated Title", "Updated Author", DateTime.Now);

            var result = _mapper.Map<Book>(request);

            result.Should().NotBeNull();
            result.Id.Should().Be(request.Id);
            result.Title.Should().Be(request.Title);
            result.Author.Should().Be(request.Author);
        }

        [Fact]
        public void UpdateBookMapper_Should_Map_Book_To_UpdateBookResponse()
        {
            var book = new Book
            {
                Id = Guid.NewGuid(),
                Title = "Updated Title",
                Author = "Updated Author",
                Isbn = new ISBN("1234567890"),
                ReleaseDate = DateTime.UtcNow
            };

            var result = _mapper.Map<UpdateBookResponse>(book);

            result.Should().NotBeNull();
            result.Id.Should().Be(book.Id);
            result.Title.Should().Be(book.Title);
            result.Author.Should().Be(book.Author);
            result.Isbn.Should().Be(book.Isbn);
            result.ReleaseDate.Should().Be(book.ReleaseDate);
        }
    }
}
