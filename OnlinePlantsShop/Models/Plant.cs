namespace OnlinePlantsShop_.Models
{
  
    public class Plant
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public required string ImageUrl { get; set; }
        public int Price { get; set; }  
        public required string Description { get; set; }
        public required string Type { get; set; }
        public List<review> Reviews { get; set; } = new List<review>();
    }
}
