namespace PortfolioMVC5v3.Models
{
    public class Image
    {
        public int ImageId { get; set; }
        public string FileName { get; set; }
        public int ProjectId { get; set; }
        public int ImageType { get; set; }
        public bool Favorite { get; set; }
        public string Guid { get; set; }
    }
}