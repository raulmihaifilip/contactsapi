using ContactsApi.Core.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using Enums = ContactsApi.Core.Enums;

namespace ContactsApi.Persistence
{
    public class ContactsApiDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Contact> Contacts { get; set; }
        public DbSet<Skill> Skills { get; set; }
        public DbSet<ContactSkill> ContactSkills { get; set; }

        public ContactsApiDbContext(DbContextOptions<ContactsApiDbContext> options) : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Skill>()
                        .Property(e => e.LevelId)
                        .HasConversion<byte>();

            modelBuilder.Entity<SkillLevel>()
                        .Property(e => e.Id)
                        .HasConversion<byte>();

            modelBuilder.Entity<SkillLevel>()
                        .HasData(Enum.GetValues(typeof(Enums.SkillLevel))
                                     .Cast<Enums.SkillLevel>()
                                     .Select(e => new SkillLevel()
                                     {
                                         Id = e,
                                         Name = e.ToString()
                                     }));

            modelBuilder.Entity<ContactSkill>()
                        .HasKey(t => new { t.ContactId, t.SkillId });
        }
    }
}
