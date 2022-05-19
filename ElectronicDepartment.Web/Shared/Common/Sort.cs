using System.ComponentModel.DataAnnotations;

namespace ElectronicDepartment.Web.Shared.Common
{
    public class SortingRequest
    {
        /// <summary>
        /// Sorting Column name (or null if none set)
        /// </summary>
        public string SortColumn { get; set; }

        /// <summary>
        /// Sorting Order ("ASC", "DESC" or null if none set)
        /// </summary>
        public string SortOrder { get; set; } = "ASC";
    }
}
