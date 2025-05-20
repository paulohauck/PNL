namespace LocationInformationService.LocationDetails
{
    public class ServiceDTO
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }

    public class ProductDTO
    {
        public string Id { get; set; }  
        public string Name { get; set; }
        public string Description { get; set; }
    }

    public class LocationDTO
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public string Address { get; set; }
        public string PostCode { get; set; }

        public List<ServiceDTO> Services {get; set;}
        public List<ProductDTO> Products { get; set; }
    }
}
