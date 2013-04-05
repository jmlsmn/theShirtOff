namespace DomainModel.Abstract.Entities
{
    public interface IImage
    {
        int ImageID { get; set; }
        string ImageLocation { get; set; }
        string ImageThumbnailLocation { get; set; }
        string ImageMimeType { get; set; }
    }
}
