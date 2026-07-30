using a_webapi.Models;
using Microsoft.EntityFrameworkCore;

namespace a_webapi.Data;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public DbSet<Device> Devices { get; set; }
    public DbSet<DeviceStatusHistory> DeviceStatusHistories { get; set; }
}