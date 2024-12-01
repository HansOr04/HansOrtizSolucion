using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace APPConsultasHansOrtiz.Models
{
    public class HO_Contacto
    {
        [JsonPropertyName("idHO_Contactos")]
        public int IdHO_Contactos { get; set; }

        [JsonPropertyName("firstName")]
        public string FirstName { get; set; }

        [JsonPropertyName("lastName")]
        public string LastName { get; set; }

        [JsonPropertyName("phoneNumber")]
        public string PhoneNumber { get; set; }

        [JsonPropertyName("email")]
        public string Email { get; set; }

        public HO_Contacto()
        {
            FirstName = string.Empty;
            LastName = string.Empty;
            PhoneNumber = string.Empty;
            Email = string.Empty;
        }
    }
}
