using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Application.UseCases.Queries.GetAllBook
{
    public sealed record GetAllBookRequest : IRequest<List<GetAllBookResponse>>;
}
