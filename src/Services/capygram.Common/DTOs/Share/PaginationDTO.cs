namespace capygram.Common.DTOs.Share
{
    public class PaginationDTO<T>
    {
        public List<T> Data { get; set; }
        public long total { get;set; }
        public int limit { get; set; }
        public int? page { get; set; }
    }
}
