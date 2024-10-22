using AutoMapper;
using Library.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Application.UseCases.Commands.UpdateBook
{
    public sealed class UpdateBookMapper : Profile
    {
        public UpdateBookMapper()
        {
            CreateMap<UpdateBookRequest, Book>().ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
            CreateMap<Book, UpdateBookResponse>();
        }
    }
}
