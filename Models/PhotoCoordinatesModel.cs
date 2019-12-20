namespace SocialNetwork.Models
{
    public class PhotoCoordinatesModel
    {
        public double? Lat { get; set; }
        public double? Lon { get; set; }
        public string Error { get; set; }
        public bool HasValidCoordinates()
        {
            return Lat.HasValue && Lon.HasValue;
        }
    }
}