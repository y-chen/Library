using Library.Dto.Core;

namespace Library.Dto
{
    public class EventStore : DtoBase
    {
        public Guid StreamId { get; set; }

        public string StreamName { get; set; }

        public string EventType { get; set; }

        public byte[] Data { get; set; }

        public long Revision { get; set; }
    }
}
