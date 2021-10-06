using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ReactCRUDAPI.Models
{
    public class AppDB:DbContext
    {
        public AppDB(DbContextOptions<AppDB> options):base(options){}

        public DbSet<Client> Client { get; set; }
    }

    public class Client
    {
        [Key]
        public int ClientId { get; set; }

        public string Name { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
    }

    public partial class User
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }

        public string Token { get; set; }
    }
}
