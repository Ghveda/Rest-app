using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestApp.DB.models;

namespace TestApp.DB
{
    public class PostgreDB : DbContext
    {
        public PostgreDB(DbContextOptions<PostgreDB> options) : base(options)
        { }
        public virtual DbSet<Country> Country{ get; set; }
        public virtual DbSet<Person> Persons{ get; set; }
        public virtual DbSet<Gender> Genders { get; set; }
    }
}
