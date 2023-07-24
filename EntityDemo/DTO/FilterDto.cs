namespace EntityDemo.DTO
{
    public class FilterDto
    {
        public List<Filter> Filters { get; set; }
        public int Page { get; set; }
        public int recordToTake { get; set; }
    }

    public class Filter
    {
        public string field { get; set; }
        public string value { get; set; }
        public string operation { get; set; }
    }
}
