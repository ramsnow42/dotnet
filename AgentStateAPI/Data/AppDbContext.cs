using Microsoft.EntityFrameworkCore;
using AgentStateAPI.Models;

namespace AgentStateAPI.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<Agent> Agents { get; set; }
        public DbSet<AgentSkill> AgentSkills { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
    }
}