using System.ComponentModel.DataAnnotations;

namespace ReactApp1.Server.Models.Project2Exercise.Dto
{
    public class RegisterRequestDto
    {
        [Required]
        public string Email {  get; set; }=string.Empty;
        [Required]

        public string Password {  get; set; }=string.Empty;
        [Required]

        public string Name {  get; set; }=string.Empty;
        public string Role {  get; set; }=string.Empty;
    }
}
