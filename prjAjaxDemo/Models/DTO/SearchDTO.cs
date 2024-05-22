namespace prjAjaxDemo.Models.DTO
{
    public class SearchDTO
    {
        public int categoryId { get; set; } = 0;
        public string? keyword { get; set; }
        public string? sortby { get; set; }
        public string? sortType { get; set; }
        public int? page { get; set; }
        public int? pagesize { get; set; }
    }
}
