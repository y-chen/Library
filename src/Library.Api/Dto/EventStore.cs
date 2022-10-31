using System.Dynamic;

namespace Library.Dto
{
    public class EventStore : DtoBase
    {
        public Guid StreamId { get; set; }

        public string StreamName { get; set; }

        public string EventType { get; set; }

        public ExpandoObject Data { get; set; }

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
