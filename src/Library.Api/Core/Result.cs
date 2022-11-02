namespace Library.Core
{
    public class Result<T>
    {
        public IEnumerable<T> Items { get; set; }

        public int Count { get; set; }

        public Result(IEnumerable<T> items, int count)
        {
            Items = items;
            Count = count;
        }
    }
}
