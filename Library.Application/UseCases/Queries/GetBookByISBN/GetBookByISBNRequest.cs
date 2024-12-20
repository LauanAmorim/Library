﻿using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Application.UseCases.Queries.GetBookByISBN
{
    public sealed record GetBookByISBNRequest(Guid Id) : IRequest<GetBookByISBNResponse>;
}
