using AutoMapper;
using Library.Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Application.UseCases.Commands.DeleteBook
{
    public class DeleteBookHandler : IRequestHandler<DeleteBookRequest, DeleteBookResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IBookRepository _bookRepository;
        private readonly IMapper _mapper;
        public DeleteBookHandler( IUnitOfWork unitOfWork, IBookRepository bookRepository, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _bookRepository = bookRepository;
            _mapper = mapper;
        }
        public async Task<DeleteBookResponse> Handle(DeleteBookRequest request, CancellationToken cancellationToken)
        {
            var book = await _bookRepository.Get(request.Id, cancellationToken);
            if (book == null) return default;
            _bookRepository.Delete(book);
            await _unitOfWork.Commit(cancellationToken);
            return _mapper.Map<DeleteBookResponse>(book);
        }
    }
}
