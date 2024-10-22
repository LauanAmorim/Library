using FluentValidation;
using Library.Domain.Enums;
using Library.Domain.Entities;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Application.UseCases.Queries.GetAllBook
{
    public class GetAllBookValidator : AbstractValidator<GetAllBookRequest>
    {
        public GetAllBookValidator()
        {
            // None
        }
    }
}
