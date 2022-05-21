using ElectronicDepartment.Web.Shared.Common;

namespace ElectronicDepartment.Web.Shared
{
    public partial class CafedraController
    {
        public class GetApiBodyRequest
        {
            public int PageIndex { get; set; }

            /// <summary>
            /// Number of items contained in each page.
            /// </summary>
            public int PageSize { get; set; }

            public IEnumerable<SortingRequest> SortingRequests { get; set; } = new List<SortingRequest>();

            public IEnumerable<FilterRequest> FilterRequests { get; set; } = new List<FilterRequest >();
        }
    }
}