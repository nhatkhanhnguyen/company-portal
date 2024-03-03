using CompanyPortal.Data.Database.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace CompanyPortal.Data.Database;

public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) 
    : IdentityDbContext<ApplicationUser>(options)
{
    public DbSet<Product> Products => Set<Product>();

    public DbSet<ProductVariant> ProductVariants => Set<ProductVariant>();

    public DbSet<Category> Categories => Set<Category>();

    public DbSet<Article> Articles => Set<Article>();

    public DbSet<Resource> Resources => Set<Resource>();

    public DbSet<ContactRequest> ContactRequests => Set<ContactRequest>();

    public DbSet<Order> Orders => Set<Order>();

    public DbSet<OrderDetail> OrderDetails => Set<OrderDetail>();
}
