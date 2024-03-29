﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using E_market.Core.Domain.Common;
using E_market.Core.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using E_market.Core.Application.Helpers;
using Microsoft.AspNetCore.Http;
using E_market.Core.Application.ViewModels.Users;

namespace E_market.Infrastructure.Persistence.Contexts
{
    public class ApplicationContext : DbContext
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly UserViewModel _userViewModel;
        public ApplicationContext(DbContextOptions<ApplicationContext> options, IHttpContextAccessor httpContextAccessor) : base(options)
        {
            _httpContextAccessor = httpContextAccessor;
            _userViewModel = _httpContextAccessor.HttpContext.Session.Get<UserViewModel>("user");
        }

        public DbSet<Article> Articles { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<User> Users { get; set; }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            foreach (var entry in ChangeTracker.Entries<AuditableBaseEntity>())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.Entity.Created = DateTime.Now;
                        entry.Entity.CreatedBy = _userViewModel.UserName;
                        break;
                    case EntityState.Modified:
                        entry.Entity.LastTimeModified = DateTime.Now;
                        entry.Entity.LastModified = _userViewModel.UserName;
                        break;
                }
            }

            return base.SaveChangesAsync(cancellationToken);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            #region tables
            modelBuilder.Entity<Article>().ToTable("Articles");
            modelBuilder.Entity<User>().ToTable("Users");
            modelBuilder.Entity<Category>().ToTable("Categories");
            #endregion

            #region "primary keys"
            modelBuilder.Entity<Article>().HasKey(h => h.Id);
            modelBuilder.Entity<User>().HasKey(h => h.Id);
            modelBuilder.Entity<Category>().HasKey(h => h.Id);
            #endregion

            #region "Relationships"
            modelBuilder.Entity<Category>()
                .HasMany<Article>(s => s.Articles)
                .WithOne(g => g.Category)
                .HasForeignKey(s => s.CategoryId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<User>()
                .HasMany<Article>(s => s.Articles)
                .WithOne(g => g.User)
                .HasForeignKey(s => s.UserId)
                .OnDelete(DeleteBehavior.Cascade);
            #endregion

            #region Configurations
            #region Articles
            modelBuilder.Entity<Article>()
                .Property(article => article.Name)
                .IsRequired()
                .HasMaxLength(75);

            modelBuilder.Entity<Article>()
                .Property(article => article.Price)
                .IsRequired();

            modelBuilder.Entity<Article>()
                .Property(article => article.Description)
                .IsRequired();
            #endregion

            #region Categories
            modelBuilder.Entity<Category>()
                .Property(category => category.Name)
                .IsRequired()
                .HasMaxLength(50);

            modelBuilder.Entity<Category>()
                .Property(category => category.Description)
                .IsRequired();
            #endregion

            #region Users
            modelBuilder.Entity<User>()
                .Property(user => user.Name)
                .IsRequired()
                .HasMaxLength(35);

            modelBuilder.Entity<User>()
                .Property(user => user.LastName)
                .IsRequired()
                .HasMaxLength(35);

            modelBuilder.Entity<User>()
                .Property(user => user.Email)
                .IsRequired();

            modelBuilder.Entity<User>()
                .Property(user => user.Phone)
                .IsRequired();

            modelBuilder.Entity<User>()
                .Property(user => user.UserName)
                .IsRequired();

            modelBuilder.Entity<User>()
                .Property(user => user.Password)
                .IsRequired();
            #endregion
            #endregion
        }
    }
}
