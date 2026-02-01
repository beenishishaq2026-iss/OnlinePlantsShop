namespace OnlinePlantsShop_.Models
{
    public class PlaceOrderViewModel

    {
        public int PlantId { get; set; }    
        public string PlantName { get; set; } 
        public decimal PlantPrice { get; set; } 
        public int Quantity { get; set; }
        public string Address { get; set; }

        public int OrderId { get; set; } 


    }
}