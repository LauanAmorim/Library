using AutoMapper;
using FluentAssertions;
using Library.Application.UseCases.Commands.CreateBook;
using Library.Domain.Entities;
using Library.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Tests.Unit.src.Library.Application.UseCases.Commands
{
    public class CreateBookMapperTests
    {
        private readonly IMapper _mapper;

        public CreateBookMapperTests()
        {
            var configuration = new MapperConfiguration(cfg => cfg.AddProfile<CreateBookMapper>());
            _mapper = configuration.CreateMapper();
        }

        [Fact]
        public void Map_CreateBookRequestToBook_ShouldMapCorrectly()
        {
            // Arrange
            var request = new CreateBookRequest("Sample Title", "Sample Author", new ISBN("1234567890123"), DateTime.Now);

            // Act
            var result = _mapper.Map<Book>(request);

            // Assert
            result.Title.Should().Be(request.title);
            result.Author.Should().Be(request.author);
            result.Isbn.Should().Be(request.isbn);
            result.ReleaseDate.Should().Be(request.releaseDate);
        }

        [Fact]
        public void Map_BookToCreateBookResponse_ShouldMapCorrectly()
        {
            // Arrange
            var book = new Book { Id = Guid.NewGuid(), Title = "Sample Title", Author = "Sample Author", Isbn = new ISBN("1234567890123"), ReleaseDate = DateTime.Now };

            // Act
            var result = _mapper.Map<CreateBookResponse>(book);

            // Assert
            result.Id.Should().Be(book.Id);
            result.Title.Should().Be(book.Title);
            result.Author.Should().Be(book.Author);
            result.Isbn.Should().Be(book.Isbn);
            result.ReleaseDate.Should().Be(book.ReleaseDate);
        }
    }
}
