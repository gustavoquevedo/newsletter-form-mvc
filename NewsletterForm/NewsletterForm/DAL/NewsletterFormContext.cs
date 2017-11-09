using NewsletterForm.Models;
using System.Data.Entity;

namespace NewsletterForm.DAL
{
    public class NewsletterFormContext : DbContext
    {
        public DbSet<Subscription> Subscriptions { get; set; }
    }
}