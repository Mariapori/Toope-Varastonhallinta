using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Toope_Varastonhallinta.Mallit;

namespace Toope_Varastonhallinta
{
    public class Tietokantakonteksti : DbContext
    {
        public DbSet<Varasto> Varastot { get; set; }
        public DbSet<Tuote> Tuotteet { get; set; }
        public DbSet<Kirjaus> Kirjaukset { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder options)
           => options.UseSqlite(@"Data Source=tietokanta.db");
    }
}
