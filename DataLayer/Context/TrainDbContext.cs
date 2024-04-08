using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Domain.Products;
using Domain.Shop;
using Domain.Users;
using Microsoft.EntityFrameworkCore;

namespace DataLayer.Context
{
    public class TrainDbContext : DbContext
    {
        public TrainDbContext(DbContextOptions<TrainDbContext> option) : base(option)
        {

        }

        #region User Entities
        public DbSet<MyRole> MyRoles { get; set; }
        public DbSet<MyUser> MyUsers { get; set; }

        public DbSet<MyProduct> MyProducts { get; set; }
        public DbSet<Level> Levels { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Item> Items { get; set; }

        public DbSet<Visit> visits { get; set; }

        #endregion

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            #region seed data

            modelBuilder.Entity<MyRole>().HasData(new MyRole
            {
                MyRoleID = 1,
                RoleName = "user",
                RoleTitle = "کاربر",
                IsDelete = false,
                CreateDate = DateTime.Now,
                ModifiedDate = DateTime.Now,
            },
            new MyRole
            {
                MyRoleID = 2,
                RoleName = "Admin",
                RoleTitle = "مدیر فروشگاه",
                IsDelete = false,
                CreateDate = DateTime.Now,
                ModifiedDate = DateTime.Now,
            }
            );


            modelBuilder.Entity<Category>().HasData(new Category
            {
                MyCategoryID = 1,
                description = "گرافیک",
                Createdate = DateTime.Now,
                ModifiedDate = DateTime.Now,
                Isdelete = false,
            },
            new Category
            {
                MyCategoryID = 2,
                description = "برنامه نویسی",
                Createdate = DateTime.Now,
                ModifiedDate = DateTime.Now,
                Isdelete = false,
            }, new Category
            {
                MyCategoryID = 3,
                description = "شبکه",
                Createdate = DateTime.Now,
                ModifiedDate = DateTime.Now,
                Isdelete = false,
            }, new Category
            {
                MyCategoryID = 4,
                description = "دیجیتال مارکتینگ",
                Createdate = DateTime.Now,
                ModifiedDate = DateTime.Now,
                Isdelete = false,
            });


            modelBuilder.Entity<Level>().HasData(new Level
            {
                MYlevelID = 1,
                Description = "مبتدی"

            }, new Level
            {
                MYlevelID = 2,
                Description = "پیشرفته"
            }
            );


            modelBuilder.Entity<MyUser>().HasData(new MyUser
            {
                MYUserID = 1,
                UserName = "ادمین",
                Mobile = "9199149932",
                Avatar = "download.png",
                IsDelete = false,
                CreateDate = DateTime.Now,
                ModifiedDate= DateTime.Now,
                MyRoleID = 2,

            });

            #endregion

            base.OnModelCreating(modelBuilder);
        }
    }
}
