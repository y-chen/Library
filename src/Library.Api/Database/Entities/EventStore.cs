using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Dynamic;
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
        [Column(TypeName = "JSON")]
        public ExpandoObject Data { get; set; }

        [Required]
        public long Revision { get; set; }

        public EventStore(
            Guid streamId,
            string streamName,
            string eventType,
            ExpandoObject data,
            long revision
        )
        {
            StreamId = streamId;
            StreamName = streamName;
            EventType = eventType;
            Data = data;
            Revision = revision;
        }
    }
}
