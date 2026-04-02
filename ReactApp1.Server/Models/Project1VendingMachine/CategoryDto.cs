using System.ComponentModel.DataAnnotations;

namespace ReactApp1.Server.Models.Project1VendingMachine
{
    public class UpdateCategoryDto
    {
        [Required]
        public string Name { get; set; }
    }
}
