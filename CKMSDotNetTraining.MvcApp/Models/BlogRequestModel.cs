namespace CKMSDotNetTraining.MvcApp.Models
{
    public class BlogRequestModel
    {
        public int id { get; set; }
        public string Title { get; set; } = null!;

        public string Author { get; set; } = null!;

        public string Content { get; set; } = null!;
    }
}
