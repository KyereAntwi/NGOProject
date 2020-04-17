using AppModels.DTO;

namespace AppModels.ViewModels
{
    public class BlogDetailsViewModel
    {
        public Blog Blog { get; set; }

        public string SubTitle { get; set; }
        public string SubDetails { get; set; }
    }
}
