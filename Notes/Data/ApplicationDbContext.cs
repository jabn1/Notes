using Microsoft.EntityFrameworkCore;
using Notes.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Notes.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
           : base(options)
        {
        }

        public required DbSet<Note> Notes { get; set; }


    }
}
