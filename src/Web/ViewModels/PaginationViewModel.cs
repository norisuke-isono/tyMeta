namespace Web.ViewModels
{
    public class PaginationViewModel
    {
        public int TotalItems { get; set; }

        public int PageSize { get; set; }

        public int PageIndex { get; set; }

        public int TotalPages { get; set; }

        public bool HasPrevious => PageIndex > 1;

        public bool HasNext => PageIndex < TotalPages;

        public int Range { get; set; } = 5;

        public int StartIndex
        {
            get
            {
                var startIndex = PageIndex - Range / 2;
                var endIndex = PageIndex + Range / 2;

                if (endIndex > TotalPages)
                {
                    var extra = endIndex - TotalPages;
                    startIndex = startIndex - extra;
                }

                if (startIndex < 1)
                {
                    startIndex = 1;
                }

                return startIndex;
            }
        }

        public int EndIndex
        {
            get
            {
                var startIndex = PageIndex - Range / 2;
                var endIndex = PageIndex + Range / 2;

                if (startIndex < 1)
                {
                    var extra = 1 - startIndex;
                    endIndex = endIndex + extra;
                }

                if (endIndex > TotalPages)
                {
                    endIndex = TotalPages;
                }

                return endIndex;
            }
        }
    }
}