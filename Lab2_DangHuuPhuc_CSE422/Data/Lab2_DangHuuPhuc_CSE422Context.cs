using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Lab2_DangHuuPhuc_CSE422.Models;

namespace Lab2_DangHuuPhuc_CSE422.Data
{
    public class Lab2_DangHuuPhuc_CSE422Context : DbContext
    {
        public Lab2_DangHuuPhuc_CSE422Context (DbContextOptions<Lab2_DangHuuPhuc_CSE422Context> options)
            : base(options)
        {
        }

        public DbSet<Lab2_DangHuuPhuc_CSE422.Models.Device> Device { get; set; } = default!;
        public DbSet<Lab2_DangHuuPhuc_CSE422.Models.Category> Category { get; set; } = default!;
        public DbSet<Lab2_DangHuuPhuc_CSE422.Models.User> User { get; set; } = default!;
    }
}
