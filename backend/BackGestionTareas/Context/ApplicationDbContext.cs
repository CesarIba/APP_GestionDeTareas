using BackGestionTareas.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackGestionTareas.Context
{
    public class ApplicationDbContext: DbContext
    {
        public DbSet<TareaDto> Tarea  { get; set; } 
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options) { }
    }
}
