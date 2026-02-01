using System;

namespace OnlinePlantsShop_.Models
{
    public class Order
    {
        public int Id { get; set; }
        public DateTime OrderDate { get; set; }
        public string Address { get; set; }
        public string OrderStatus { get; set; }
        public string PlantNames { get; set; }     // Comma-separated: "Rose,Aloe Vera"
        public string Quantities { get; set; }     // Comma-separated: "1,2"
        public string Prices { get; set; }         // Comma-separated: "10,15"
        public decimal TotalAmount { get; set; }
    }
}
