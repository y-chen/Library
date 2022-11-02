using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Dynamic;
using Library.Database.Core;
using Microsoft.EntityFrameworkCore;

namespace Library.Database.Entities
{
    public class EventStore : EntityBase
    {
        [Required]
        public string StreamName { get; set; }

        [Required]
        public Guid StreamId { get; set; }

        [Required]
        public string EventType { get; set; }

        [Required]
        [Column(TypeName = "JSON")]
        public ExpandoObject Data { get; set; }

        [Required]
        public long Revision { get; set; }

        public EventStore() { }

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
