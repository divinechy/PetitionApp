using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace PetitionApi.Models
{
    public partial class PetitionDbContext : DbContext
    {
        public PetitionDbContext(DbContextOptions<PetitionDbContext> options): base(options)
        {
        }

        /// <summary>
        /// These includes all the database table/ schema
        /// </summary>
        public virtual DbSet<Petition> Petitions { get; set; }
        public virtual DbSet<Role> Roles { get; set; }
        public virtual DbSet<User> Users { get; set; } 
        public virtual DbSet<UserPetition> UserPetitions { get; set; } 
    }
}
