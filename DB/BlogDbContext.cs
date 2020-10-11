using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using blogAfonina.Model;
using blogAfonina.Model.Common;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace blogAfonina.DB
{
    public class BlogDbContext : IdentityDbContext<User, IdentityRole<int>, int>
    {
        public BlogDbContext(DbContextOptions<BlogDbContext> options) : 
            base(options)
        {
            Database.EnsureCreated();
        }

        /// <summary>
        /// users
        /// </summary>
        public override DbSet<User> Users { get; set; }

        /// <summary>
        /// profiles
        /// </summary>
        public DbSet<Profile> Profiles { get; private set; }

        /// <summary>
        /// blog post
        /// </summary>
        public DbSet<BlogPost> BlogPosts { get; private set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<User>(x =>
            {
                x.HasOne(y => y.Profile)
                .WithOne()
                .HasForeignKey<User>("ProfileId")
                .IsRequired(true);
                x.HasIndex("ProfileId").IsUnique(true);
            });

            #region Profile
            builder.Entity<Profile>(b =>
            {
                b.ToTable("Profiles");
                ProfileId(b);
                b.Property(x => x.FirstName)
                    .HasColumnName("FirstName")
                    .IsRequired();
                b.Property(x => x.Surname)
                    .HasColumnName("Surname")
                    .IsRequired(); 
                b.Ignore(x => x.FullName);
            });
            #endregion

            #region BlogPost
            builder.Entity<BlogPost>(b =>
            {
                b.ToTable("BlogPosts");
                ProfileId(b);
                b.Property(x => x.Created)
                    .HasColumnName("Created")
                    .IsRequired();
                b.Property(x => x.Title)
                    .HasColumnName("Title")
                    .IsRequired();
                b.Property(x => x.Data)
                    .HasColumnName("Data")
                    .IsRequired();
                b.HasOne(x => x.Author)
                    .WithMany()
                    .IsRequired();
            });
            #endregion
        }

        /// <summary>
        /// model entity identifier description
        /// </summary>
        /// <typeparam name="TEntity">entity type</typeparam>
        /// <param name="builder">data model builder</param>
        private static void ProfileId<TEntity>(EntityTypeBuilder<TEntity> builder)
            where TEntity : Entity
        {
            builder.Property(x => x.Id)
                .HasColumnName("Id")
                .IsRequired();
            builder.HasKey(x => x.Id)
                .HasAnnotation("Npgsql:Serial", true);
        }
    }
}
