using AutoMapper;
using Library.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Application.UseCases.Commands.DeleteBook
{
    public sealed class DeleteBookMapper : Profile
    {
        public DeleteBookMapper()
        {
            CreateMap<DeleteBookRequest,Book>();
            CreateMap<Book, DeleteBookResponse>();
        }
    }
}
