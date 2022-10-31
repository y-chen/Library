namespace Library.Dto
{
    public class EventStore : DtoBase
    {
        public Guid StreamId { get; set; }

        public string StreamName { get; set; }

        public string EventType { get; set; }

        public object Data { get; set; }

        public long Revision { get; set; }

        public EventStore(
            Guid streamId,
            string streamName,
            string eventType,
            object data,
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
