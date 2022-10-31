using System.ComponentModel.DataAnnotations;
using Library.Database.Core;
using Microsoft.EntityFrameworkCore;

namespace Library.Database.Entities
{
    [Index(nameof(StreamId), nameof(Revision), IsUnique = true)]
    public class EventStore : EntityBase
    {
        [Required]
        public Guid StreamId { get; set; }

        [Required]
        public string StreamName { get; set; }

        [Required]
        public string EventType { get; set; }

        [Required]
        public string Data { get; set; }

        [Required]
        public long Revision { get; set; }
    }
}
