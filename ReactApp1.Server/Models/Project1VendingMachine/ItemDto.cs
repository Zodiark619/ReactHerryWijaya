using System.ComponentModel.DataAnnotations;

namespace ReactApp1.Server.Models.Project1VendingMachine
{
    public class CreateItemDto
    {
        [Required]

        public string Name { get; set; }
        [Range(0.01, double.MaxValue)]
        public decimal Price { get; set; }
        [Range(1, int.MaxValue)]

        public int CategoryId { get; set; }
    }
    public class GetItemDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }

        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
    }
}
