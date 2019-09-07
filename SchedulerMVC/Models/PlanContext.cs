using System.Data.Entity;

namespace SchedulerMVC.Models
{
    public class PlanContext : DbContext
    {
        public DbSet<Plan> Plans { get; set; }
    }
}