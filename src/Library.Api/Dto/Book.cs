namespace Library.Dto
{
    public class Book : DtoBase
    {
        public string Title { get; set; }

        public string Description { get; set; }

        public DateTime PublishDate { get; set; }

        public string[] Authors { get; set; }
    }
}
