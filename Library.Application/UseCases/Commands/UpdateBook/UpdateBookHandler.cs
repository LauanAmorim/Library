using AutoMapper;
using Library.Domain.Interfaces;
using MediatR;
using MediatR.Wrappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace Library.Application.UseCases.Commands.UpdateBook
{
    public class UpdateBookHandler : IRequestHandler<UpdateBookRequest, UpdateBookResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IBookRepository _bookRepository;
        private readonly IMapper _mapper;

        public UpdateBookHandler(IUnitOfWork unitOfWork, IBookRepository bookRepository, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _bookRepository = bookRepository;
            _mapper = mapper;
        }
        public async Task<UpdateBookResponse> Handle(UpdateBookRequest request, CancellationToken cancellationToken)
        {
            var book = await _bookRepository.Get(request.Id, cancellationToken);
            if (book is null) return default;


            _mapper.Map(request, book);

            _bookRepository.Update(book);
            await _unitOfWork.Commit(cancellationToken);
            return _mapper.Map<UpdateBookResponse>(book);

        }

    }
}
