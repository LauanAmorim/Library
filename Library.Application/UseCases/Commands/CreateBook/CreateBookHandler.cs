using AutoMapper;
using Library.Domain.Interfaces;
using Library.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Application.UseCases.Commands.CreateBook
{
    public class CreateBookHandler : IRequestHandler<CreateBookRequest, CreateBookResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IBookRepository _bookRepository;
        private readonly IMapper _mapper;

        public CreateBookHandler(IUnitOfWork unitOfWork, IBookRepository bookRepository, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _bookRepository = bookRepository;
            _mapper = mapper;
        }
        public async Task<CreateBookResponse> Handle(CreateBookRequest request, CancellationToken cancellationToken)
        {
            var book = _mapper.Map<Book>(request);
            _bookRepository.Create(book);
            await _unitOfWork.Commit(cancellationToken);
            return _mapper.Map<CreateBookResponse>(book);
        }
    }
}
