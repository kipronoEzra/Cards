using Cards.API.Models;
using Microsoft.EntityFrameworkCore;

namespace Cards.API.Data
{
    public class cardsDbContext : DbContext
    {
        public cardsDbContext(DbContextOptions options) : base(options)
        {
        }
        //Dbset
        public DbSet<Card> Cards { get; set; }
    }
}
