using AutoMapper;
using Library.Domain.Entities;
using Library.Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Application.UseCases.Queries.GetBookByISBN
{
    public sealed class GetBookByISBNHandler : IRequestHandler<GetBookByISBNRequest, GetBookByISBNResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IBookRepository _bookRepository;
        private readonly IMapper _mapper;
        public GetBookByISBNHandler(IUnitOfWork unitOfWork, IBookRepository bookRepository, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _bookRepository = bookRepository;
            _mapper = mapper;
        }
        public async Task<GetBookByISBNResponse> Handle(GetBookByISBNRequest request,CancellationToken cancellationToken)
        {
            var book = await _bookRepository.Get(request.Id, cancellationToken);
            if (book == null) return default;
            return _mapper.Map<GetBookByISBNResponse>(book);
        }
    }
}
