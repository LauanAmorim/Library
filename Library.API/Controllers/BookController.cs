using MediatR;
using Library.Application.UseCases.Commands.CreateBook;
using Microsoft.AspNetCore.Mvc;
using Library.Application.UseCases.Commands.DeleteBook;
using Library.Application.UseCases.Queries.GetAllBook;
using Library.Application.UseCases.Commands.RestoreBook;
using Library.Application.UseCases.Queries.GetBookByISBN;
using Library.Domain.Enums;
using Library.Application.UseCases.Commands.UpdateBook;

namespace Library.API.Controllers
{
    [Route("v1/[controller]")]
    [ApiController]
    public class BookController : Controller
    {
        private readonly IMediator _mediator;
        public BookController(IMediator mediator)
        {
            _mediator = mediator;
        }
        /// <summary>
        /// Gets a book by it's Id
        /// </summary>
        [HttpGet("{id}")]
        public async Task<ActionResult<GetBookByISBNResponse>> GetByIsbn(Guid id, CancellationToken cancellationToken)
        {
            var response = await _mediator.Send(new GetBookByISBNRequest(id), cancellationToken);
            return Ok(response);
        }
        /// <summary>
        /// Gets all books
        /// </summary>
        [HttpGet]
        public async Task<ActionResult<List<GetAllBookResponse>>> GetAll(CancellationToken cancelationToken)
        {
            var response = await _mediator.Send(new GetAllBookRequest(), cancelationToken);
            return Ok(response.Where(x => x.Status != EntityStatus.Inactive));
        }   
        /// <summary>
        /// Updates a book
        /// </summary>
        [HttpPatch("update/{id}")]
        public async Task<ActionResult<UpdateBookResponse>> Update(Guid id, UpdateBookRequest request, CancellationToken cancellationToken)
        {
            if (id != request.Id) return BadRequest();
            var response = await _mediator.Send(request, cancellationToken);
            return Ok(response);
        }
        /// <summary>
        /// Register a book
        /// </summary>
        [HttpPost]
        public async Task<ActionResult<CreateBookResponse>> Create([FromBody] CreateBookRequest request, CancellationToken cancellationToken)
        {
            if (string.IsNullOrEmpty(request.title) || string.IsNullOrEmpty(request.author) || request.isbn == null)
            {
                return BadRequest("Invalid request data");
            }

            var response = await _mediator.Send(request, cancellationToken);
            return Ok(response);
        }
        /// <summary>
        /// Soft-delete a book by it's Id
        /// </summary>
        [HttpDelete("delete/{id}")]
        public async Task<ActionResult> Delete(Guid? id, CancellationToken cancellationToken)
        {
            if (id is null) return BadRequest();
            var deleteBookRequest = new DeleteBookRequest(id.Value);
            var response = await _mediator.Send(deleteBookRequest, cancellationToken);
            return Ok(response);
        }
        /// <summary>
        /// Restores a book by its Id
        /// </summary>
        [HttpPatch("restore/{id}")]
        public async Task<ActionResult> Restore(Guid? id, CancellationToken cancellationToken)
        {
            if (id is null) return BadRequest();
            var restoreBookRequest = new RestoreBookRequest(id.Value);
            var response = await _mediator.Send(restoreBookRequest, cancellationToken);
            return Ok(response);
        }
    }
}
