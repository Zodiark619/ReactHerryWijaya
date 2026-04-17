using System.ComponentModel.DataAnnotations;

namespace ReactApp1.Server.Models.Project2Exercise.Dto
{
    public class OrderDetailsUpdateDTO
    {
        [Required]
        public int OrderDetailId { get; set; }
        
        [Required]
        [Range(1,5)]
        public int Rating { get; set; }
    }
}
