using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Application.UseCases.Commands.DeleteBook
{
    public class DeleteBookValidator : AbstractValidator<DeleteBookRequest>
    {
        public DeleteBookValidator()
        {
            RuleFor(x => x.Id).NotEmpty();
        }
    }
}
