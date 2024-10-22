using AutoMapper;
using Library.Application.UseCases.Commands.CreateBook;
using Library.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Application.UseCases.Queries.GetBookByISBN
{
    public sealed class GetBookByISBNMapper : Profile
    {
        public GetBookByISBNMapper()
        {
            CreateMap<GetBookByISBNRequest, Book>();
            CreateMap<Book, GetBookByISBNResponse>();
        }

    }
}
