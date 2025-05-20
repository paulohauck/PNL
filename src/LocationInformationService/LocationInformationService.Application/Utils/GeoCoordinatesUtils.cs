
namespace LocationInformationService.Application.Utils
{
    public static class GeoCoordinatesUtils
    {

        public static (double minLatitude, double maxLatitude, double minLongitude, double maxLongitude) GetCoordinatesBox(double latitude, double longitude, double maxDistanceInKm)
        {
            double latDelta = maxDistanceInKm / 111.0; // 1 degree latitude ≈ 111 km
            double lonDelta = maxDistanceInKm / (111.0 * Math.Cos(latitude * (Math.PI / 180)));

            double minLat = latitude - latDelta;
            double maxLat = latitude + latDelta;
            double minLon = longitude - lonDelta;
            double maxLon = longitude + lonDelta;

            return (minLat, maxLat, minLon, maxLon);
        }
    }
}
