using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Application.UseCases.Commands.DeleteBook
{
    public sealed record DeleteBookRequest(Guid Id) : IRequest<DeleteBookResponse>;
}
