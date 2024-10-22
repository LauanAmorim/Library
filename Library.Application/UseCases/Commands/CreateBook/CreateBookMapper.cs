using AutoMapper;
using Library.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Application.UseCases.Commands.CreateBook
{
    public sealed class CreateBookMapper : Profile
    {
        public CreateBookMapper()
        {
            CreateMap<CreateBookRequest, Library.Domain.Entities.Book>();
            CreateMap<Library.Domain.Entities.Book, CreateBookResponse>();
        }
    }
}
