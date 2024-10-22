using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Application.UseCases.Commands.RestoreBook
{
    public sealed record RestoreBookRequest(Guid Id) : IRequest<RestoreBookResponse>;
}
