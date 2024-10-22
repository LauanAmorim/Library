using AutoMapper;
using Library.Application.UseCases.Commands.DeleteBook;
using Library.Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Application.UseCases.Commands.RestoreBook
{
    public class RestoreBookHandler : IRequestHandler<RestoreBookRequest, RestoreBookResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IBookRepository _bookRepository;
        private readonly IMapper _mapper;
        public RestoreBookHandler(IUnitOfWork unitOfWork, IBookRepository bookRepository, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _bookRepository = bookRepository;
            _mapper = mapper;
        }
        public async Task<RestoreBookResponse> Handle(RestoreBookRequest request, CancellationToken cancellationToken)
        {
            var book = await _bookRepository.Get(request.Id, cancellationToken);
            if (book == null) return default;
            _bookRepository.Restore(book);
            await _unitOfWork.Commit(cancellationToken);
            return _mapper.Map<RestoreBookResponse>(book);
        }
    }
}
