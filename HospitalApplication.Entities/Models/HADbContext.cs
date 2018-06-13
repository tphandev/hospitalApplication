using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace HospitalApplication.Entities.Models
{
    public class HADbContext : DbContext
    {
        public HADbContext()
            : base("name=DbConnectionString")
        {
        }
        public DbSet<bnnoitru> bnnoitru { get; set; }
        public DbSet<dmdonvi> dmdonvi { get; set; }
        public DbSet<dmbenhnhan> dmbenhnhan { get; set; }
        public DbSet<thuchi> thuchi { get; set; }
        public DbSet<dmcls> dmcls { get; set; }
        public DbSet<chidinhcls> chidinhcls { get; set; }
        public DbSet<khambenh> khambenh { get; set; }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<bnnoitru>()
                .ToTable("bnnoitru", "current").HasKey(m => new { m.maba, m.mabn, m.makb })
                .HasRequired(t => t.dmdonvi).WithMany(c => c.bnnoitru).HasForeignKey(t => t.madv).WillCascadeOnDelete(false);
                modelBuilder.Entity<bnnoitru>().HasRequired(t => t.dmbenhnhan).WithMany(c => c.bnnoitru).HasForeignKey(t => t.mabn).WillCascadeOnDelete(false);


            modelBuilder.Entity<khambenh>()
                .ToTable("khambenh", "current").HasKey(m => new {m.mabn, m.makb })
                .HasRequired(t => t.dmdonvi).WithMany(c => c.khamhbenh).HasForeignKey(t => t.madv).WillCascadeOnDelete(false);
            modelBuilder.Entity<khambenh>().HasRequired(t => t.dmbenhnhan).WithMany(c => c.khambenh).HasForeignKey(t => t.mabn).WillCascadeOnDelete(false);

            modelBuilder.Entity<dmbenhnhan>().ToTable("dmbenhnhan", "current").HasKey(m => m.mabn);
           
            modelBuilder.Entity<dmdonvi>().ToTable("dmdonvi", "current").HasKey(m => m.madv);
            modelBuilder.Entity<dmcls>().ToTable("dmcls", "current").HasKey(m => m.macls);
            modelBuilder.Entity<thuchi>().ToTable("thuchi", "current").HasKey(m => m.soct)
                .HasRequired(t=>t.dmbenhnhan).WithMany(c=>c.thuchi).HasForeignKey(t=>t.mabn);
            modelBuilder.Entity<chidinhcls>().ToTable("chidinhcls", "current").HasKey(m => m.makb)
                .HasRequired(t => t.dmcls).WithMany(c => c.chidinhclss).HasForeignKey(t => t.macls).WillCascadeOnDelete(false);
            modelBuilder.Entity<chidinhcls>().HasRequired(t => t.thuchi).WithMany(c => c.chidinhclss).HasForeignKey(t => t.soctvp).WillCascadeOnDelete(false); 

        }
    }
}

