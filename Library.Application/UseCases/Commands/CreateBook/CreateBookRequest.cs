using Library.Domain.ValueObjects;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Application.UseCases.Commands.CreateBook
{
    public sealed record CreateBookRequest(string title, string author, ISBN isbn, DateTime releaseDate) : IRequest<CreateBookResponse> { }
}
