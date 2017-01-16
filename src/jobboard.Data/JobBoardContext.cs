using jobboard.Model.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Metadata;
using System.ComponentModel.DataAnnotations.Schema;

namespace jobboard.Data
{
    public class JobBoardContext : DbContext
    {
        public DbSet<Skill> Skills { set; get; }
        public DbSet<Job> Jobs { set; get; }
        public DbSet<JobSkill> JobSkills { set; get; }

        public JobBoardContext(DbContextOptions options)
            :base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Skill>()
                .ToTable("Skill");
            modelBuilder.Entity<Skill>()
                .Property(s => s.Id);
            modelBuilder.Entity<Skill>()
                .Property(s => s.KeyWords)
                .IsRequired();
            modelBuilder.Entity<Skill>()
                .Property(s => s.IsReg)
                .IsRequired();
            modelBuilder.Entity<Skill>()
                .HasIndex(s => s.Name);
            modelBuilder.Entity<Skill>()
                .HasMany(s => s.JobSkills)
                .WithOne(js => js.Skill)
                .HasForeignKey(js => js.SkillId);

            modelBuilder.Entity<Job>()
                .ToTable("Job");
            modelBuilder.Entity<Job>()
                .Property(j => j.Title)
                .IsRequired();
            modelBuilder.Entity<Job>()
                .Property(j => j.Employer)
                .IsRequired();
            modelBuilder.Entity<Job>()
                .Property(j => j.PostAt)
                .IsRequired();
            modelBuilder.Entity<Job>()
                .Property(j => j.ReadAt)
                .IsRequired();
            modelBuilder.Entity<Job>()
                .Property(j => j.Url)
                .IsRequired();
            modelBuilder.Entity<Job>()
                .Property(j => j.Analyzed)
                .HasDefaultValue(false);
            modelBuilder.Entity<Job>()
                .HasOne(j => j.Content)
                .WithOne(c => c.Job)
                .HasForeignKey<Content>(c => c.JobId);
            modelBuilder.Entity<Job>()
                .HasIndex(j => j.PostAt);
            modelBuilder.Entity<Job>()
               .HasMany(j => j.JobSkills)
               .WithOne(js => js.Job)
               .HasForeignKey(js => js.JobId);

            modelBuilder.Entity<Content>()
                .ToTable("Content");
            modelBuilder.Entity<Content>()
                .Property(c => c.Text)
                .HasColumnType("text")
                .HasMaxLength(100000)
                .IsRequired();

            modelBuilder.Entity<JobSkill>()
                .ToTable("JobSkill");
            modelBuilder.Entity<JobSkill>()
                .HasIndex(js => new { js.JobId, js.SkillId });
        }
    }
}
