using Library.Domain.Enums;
using Library.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Application.UseCases.Commands.CreateBook
{
    public sealed record CreateBookResponse
    {
        public Guid Id { get; set; }
        public string? Title { get; set; }
        public string? Author { get; set; }
        public ISBN Isbn { get; set; }
        public EntityStatus Status { get; set; } = EntityStatus.Active;
        public DateTime ReleaseDate { get; set; }
    }
}
