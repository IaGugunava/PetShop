using System.Collections.Generic;
using System.Security.Principal;
using System.Transactions;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using PetShop.Models;

namespace PetShop.Data
{
    public class PetShopContext : IdentityDbContext<IdentityUser>
    {
        public PetShopContext(DbContextOptions<PetShopContext> options) : base(options)
        {

        }
        public DbSet<Pet> Pets { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<User> Users { get; set; }
    }
}
