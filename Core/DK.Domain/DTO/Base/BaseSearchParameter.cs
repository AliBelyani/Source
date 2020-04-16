namespace DK.Domain.DTO.Base
{
    public class BaseSearchParameter
    {
        public string xSortBy { get; set; } = "xLastName";
        public string xSortType { get; set; } = "Asc";
        public int xPage { get; set; } = 1;
        public int xPageSize { get; set; } = 25;
    }
}

