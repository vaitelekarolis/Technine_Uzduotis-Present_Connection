using System.ComponentModel.DataAnnotations;

namespace TechnineUzduotis.Models
{
    public class Order
    {
        [Required]
        public Client Client { get; set; }
        [Required]
        public Provider Provider { get; set; }
        [Required]
        public double Price { get; set; }
        public Order() { }
        public Order(Client client, Provider provider, double price)
        {
            Client = client;
            Provider = provider;
            Price = price;
        }
    }
}
