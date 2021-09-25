using CrudProverTech.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace CrudProverTech.Data
{
    public class ApplicationDbContext : IdentityDbContext<Usuario>
    {
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<NivelAcesso> NiveisAcesso { get; set; }
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<CrudProverTech.Models.Pessoa> Pessoa { get; set; }
        public DbSet<CrudProverTech.Models.Cargo> Cargo { get; set; }
    }
}
