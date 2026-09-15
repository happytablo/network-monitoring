using a_webapi.Identity;
using a_webapi.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace a_webapi.Data;

public class AppDbContext(DbContextOptions<AppDbContext> options) : IdentityDbContext<ApplicationUser>(options)
{
    public DbSet<Device> Devices { get; set; }
    public DbSet<DeviceStatusHistory> DeviceStatusHistories { get; set; }
}