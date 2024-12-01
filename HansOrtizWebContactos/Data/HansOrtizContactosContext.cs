using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using HansOrtizWebContactos.Models;

    public class HansOrtizContactosContext : DbContext
    {
        public HansOrtizContactosContext (DbContextOptions<HansOrtizContactosContext> options)
            : base(options)
        {
        }

        public DbSet<HansOrtizWebContactos.Models.HO_Contactos> HO_Contactos { get; set; } = default!;
    }
