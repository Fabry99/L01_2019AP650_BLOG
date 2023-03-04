using Microsoft.AspNetCore.Components.Web;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.SqlServer;
using Microsoft.Identity.Client;
using System.Threading.Tasks.Dataflow;


namespace L01_2019AP650.Models
{
    public class entidadesContext : DbContext
    {
        public entidadesContext(DbContextOptions<entidadesContext> options) : base (options) 
        {
            

        }
        public DbSet<usuarios> usuarios { get; set; }   
    }
}
