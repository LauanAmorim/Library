using AutoMapper;
using Library.Application.UseCases.Commands.DeleteBook;
using Library.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Application.UseCases.Commands.RestoreBook
{
    public sealed class RestoreBookMapper : Profile
    {
        public RestoreBookMapper()
        {
            CreateMap<RestoreBookRequest, Book>();
            CreateMap<Book, RestoreBookResponse>();
        }
    }
}
