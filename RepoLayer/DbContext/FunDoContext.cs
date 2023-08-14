using Microsoft.EntityFrameworkCore;
using RepoLayer.Entity;
using System;
using System.Collections.Generic;
//using System.Data.Entity;
using System.Text;

namespace RepoLayer.Context
{
    public class FunDoContext : DbContext
    {
        public FunDoContext(DbContextOptions options)
            : base(options)
        {
        }
        public Microsoft.EntityFrameworkCore.DbSet<UserEntity> User { get; set; }
       

    }

}