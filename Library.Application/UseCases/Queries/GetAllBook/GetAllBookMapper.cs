using AutoMapper;
using Library.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Application.UseCases.Queries.GetAllBook
{
    public sealed class GetAllBookMapper : Profile
    {
        public GetAllBookMapper()
        {
            CreateMap<Book, GetAllBookResponse>();
        }
    }
}
