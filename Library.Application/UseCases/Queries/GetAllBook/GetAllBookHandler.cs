using AutoMapper;
using Library.Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Application.UseCases.Queries.GetAllBook
{
    public sealed class GetAllBookHandler : IRequestHandler<GetAllBookRequest, List<GetAllBookResponse>>
    {
        private readonly IBookRepository _bookRepository;
        private readonly IMapper _mapper;

        public GetAllBookHandler (IBookRepository bookRepository, IMapper mapper)
        {
            _bookRepository = bookRepository;
            _mapper = mapper;
        }
        public async Task<List<GetAllBookResponse>> Handle(GetAllBookRequest request, CancellationToken cancellationToken)
        {
            var books = await _bookRepository.GetAll(cancellationToken);
            return _mapper.Map<List<GetAllBookResponse>>(books);
        }
    }
}
