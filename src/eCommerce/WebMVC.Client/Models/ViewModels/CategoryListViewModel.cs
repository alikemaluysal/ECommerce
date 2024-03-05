namespace WebMVC.Client.Models.ViewModels
{
    public class CategoryListViewModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = null!;
        public string Color { get; set; } = null!;
        public string IconCssClass { get; set; } = null!;

    }
}
