namespace ElectronicDepartment.Web.Shared
{
    public class ApiResponce<T>
    {
        public IEnumerable<T> Data { get; set; } = new List<T>();

        public int TotalPage { get; set; }

        public int PageIndex { get; set; }

        public int PageSize { get; set; }

        public bool HasNextPage { get; set; }

        public bool HasPreviewPage { get; set; }
    }
}