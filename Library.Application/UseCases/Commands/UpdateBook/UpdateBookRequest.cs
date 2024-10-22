using Library.Domain.ValueObjects;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Application.UseCases.Commands.UpdateBook
{
    public sealed record UpdateBookRequest(Guid Id, string? Title, string? Author, DateTime? ReleaseDate) : IRequest<UpdateBookResponse>;
}
