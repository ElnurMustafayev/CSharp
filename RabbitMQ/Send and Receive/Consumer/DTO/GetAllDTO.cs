namespace Consumer.DTO
{
    public class GetAllDTO<T>
    {
        public IEnumerable<T> Collection;
        public int? Count;

        public GetAllDTO(IEnumerable<T> collection)
        {
            this.Collection = collection;
            this.Count = this.Collection?.Count();
        }
    }
}