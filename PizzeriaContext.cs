using Microsoft.EntityFrameworkCore;
using la_mia_pizzeria_static.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

public class PizzeriaContext : IdentityDbContext<IdentityUser>
{
    public DbSet<Pizza> Pizzas { get; set; }
    public DbSet<Location> Locations { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<Ingredient> Ingredients { get; set; }
    public DbSet<Message> Messages { get; set; }
    public PizzeriaContext()
    {
    }
    public PizzeriaContext(DbContextOptions<PizzeriaContext> options) : base(options)
    {

    }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer("Data Source=localhost;Initial Catalog=pizze-db;Integrated Security=True");
    }
}