using Microsoft.EntityFrameworkCore;
using Zadatak1.Models;

namespace Zadatak1.Entities
{
    public class LeagueContext  : DbContext
    {
        public LeagueContext(DbContextOptions<LeagueContext> options) : base(options)
        {
           // Database.Migrate();
        }

        public virtual DbSet<League> Leagues { get; set; }
        public virtual DbSet<Group> Groups { get; set; }
        public virtual DbSet<GroupTeam> GroupTeams { get; set; }
        public virtual DbSet<Match> Matches { get; set; }
        public virtual DbSet<Standing> Standings { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Group>(group => 
            {
                group.HasOne(g => g.League)
                    .WithMany(l => l.Groups)
                    .HasForeignKey(g => g.LeagueId);
            });

            modelBuilder.Entity<GroupTeam>(groupTeam =>
            {
                groupTeam.HasOne(gt => gt.Group)
                    .WithMany(g => g.GroupTeams)
                    .HasForeignKey(gt => gt.GroupId);
            });
            modelBuilder.Entity<Standing>(standing =>
            {
                standing.HasOne(s => s.Team)
                    .WithMany(gt => gt.Standings)
                    .HasForeignKey(s => s.TeamId);
            });
            modelBuilder.Entity<Match>(match => 
            {
                match.HasOne(m => m.Group)
                    .WithMany(g => g.Matches)
                    .HasForeignKey(m => m.GroupId);

                match.HasOne(m => m.HomeTeam)
                    .WithMany(gt => gt.HomeMatches)
                    .HasForeignKey(m => m.HomeTeamId);

                match.HasOne(m => m.AwayTeam)
                    .WithMany(gt => gt.AwayMatches)
                    .HasForeignKey(m => m.AwayTeamId);

                match.HasIndex(m => new { m.GroupId, m.HomeTeamId, m.AwayTeamId })
                    .IsUnique();
            });
        }
    }
}
