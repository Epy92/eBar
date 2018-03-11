using System.IO;

namespace eBarService.Models
{
    public class RestaurantModel
    {
        public string RestaurantName { get; set; }
        public string RestaurantCity { get; set; }
        public string FileBytesArray { get; set; }
        public string RestaurantType { get; set; }
        public string RestaurantDescription { get; set; }
        public string RestaurantContentType { get; set; }

        public byte[] ThumbnailStream{ get; set; }
    }
}