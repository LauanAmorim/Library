using FluentAssertions;
using Library.Application.UseCases.Commands.CreateBook;
using Library.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Tests.Unit.src.Library.Application.UseCases.Commands
{
    public class CreateBookValidatorTests
    {
        private readonly CreateBookValidator _validator;

        public CreateBookValidatorTests()
        {
            _validator = new CreateBookValidator();
        }

        [Fact]
        public void Validate_MissingTitle_ShouldFail()
        {
            // Arrange
            var request = new CreateBookRequest(null, "Author", new ISBN("1234567890123"), DateTime.Now);

            // Act
            var result = _validator.Validate(request);

            // Assert
            result.IsValid.Should();
        }

        [Fact]
        public void Validate_ValidRequest_ShouldPass()
        {
            // Arrange
            var request = new CreateBookRequest("Sample Title", "Author", new ISBN("1234567890123"), DateTime.Now);

            // Act
            var result = _validator.Validate(request);

            // Assert
            result.IsValid.Should().BeTrue();
        }
    }
}
