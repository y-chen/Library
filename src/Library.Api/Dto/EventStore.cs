namespace Library.Dto
{
    public class EventStore : DtoBase
    {
        public Guid StreamId { get; set; }

        public string StreamName { get; set; }

        public string EventType { get; set; }

        public object Data { get; set; }

        public long Revision { get; set; }
    }
}
