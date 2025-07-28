using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Enums;
using api.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace api.Data
{
    public class ApplicationDBContext : DbContext
    {
        public ApplicationDBContext(DbContextOptions dbContextOptions) : base(dbContextOptions)
        {

        }

        public DbSet<Problem> Problems { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<ProblemTag> ProblemTags { get; set; }
        public DbSet<TestCase> TestCases { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Problem>()
                .Property(p => p.Difficulty)
                .HasConversion(
                    new EnumToStringConverter<Difficulty>()
                 );
            modelBuilder.Entity<Problem>()
                .Property(p => p.Visibility)
                .HasConversion(
                    new EnumToStringConverter<Visibility>()
                 );
            modelBuilder.Entity<Tag>()
                .Property(t => t.TagName)
                .HasConversion(
                    new EnumToStringConverter<TagName>()
                 );
            modelBuilder.Entity<TestCase>()
                .Property(p => p.LanguageName)
                .HasConversion(
                    new EnumToStringConverter<LanguageName>()
                );
            modelBuilder.Entity<ProblemTag>()
                .HasKey(pt => new { pt.ProblemId, pt.TagId });

            modelBuilder.Entity<ProblemTag>()
                .HasOne(pt => pt.Problem)
                .WithMany(p => p.ProblemTags)
                .HasForeignKey(pt => pt.ProblemId);

            modelBuilder.Entity<ProblemTag>()
                .HasOne(pt => pt.Tag)
                .WithMany(t => t.ProblemTags)
                .HasForeignKey(pt => pt.TagId);
        }
    }
}