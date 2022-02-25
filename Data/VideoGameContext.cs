using eCommerce.Models;
using Microsoft.EntityFrameworkCore;

namespace eCommerce.Data
{
    public class VideoGameContext : DbContext
    {
        public VideoGameContext(DbContextOptions<VideoGameContext> option) : base(option)
        {
             
        }

        public DbSet<Game> Games { get; set; }
    }
}
