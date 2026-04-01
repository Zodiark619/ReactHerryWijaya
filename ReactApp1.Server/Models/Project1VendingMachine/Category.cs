using System.ComponentModel.DataAnnotations;

namespace ReactApp1.Server.Models.Project1VendingMachine
{
    public class Category
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public ICollection<Item>? Items { get; set; }
    }
}
