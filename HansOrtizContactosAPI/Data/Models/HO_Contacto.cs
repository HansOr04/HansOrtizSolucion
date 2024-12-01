using System;
using System.Collections.Generic;

namespace HansOrtizContactosAPI.Data.Models;

public partial class HO_Contacto
{
    public int IdHO_Contactos { get; set; }

    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public string PhoneNumber { get; set; } = null!;

    public string Email { get; set; } = null!;
}
