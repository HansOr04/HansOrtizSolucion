using System.ComponentModel.DataAnnotations;

namespace HansOrtizWebContactos.Models
{
    public class HO_Contactos
    {
        [Key]
        public int IdHO_Contactos { get; set; }
        [Required]
        public string? FirstName { get; set; }
        [Required]
        public string? LastName { get; set; }
        [Required]
        public string? PhoneNumber { get; set; }
        [Required]
        public string? Email { get; set; }
    }
}
