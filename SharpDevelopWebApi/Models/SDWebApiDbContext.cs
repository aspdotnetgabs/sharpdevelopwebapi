using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace SharpDevelopWebApi.Models
{
    public class SDWebApiDbContext : DbContext
    {
        public SDWebApiDbContext() : base("SDWebApiDb") // name_of_dbconnection_string
        {
        }

        // Map model classes to database tables
        public DbSet<UserAccount> Users { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Leave> Leaves { get; set; }
        public DbSet<Reason> Reasons { get; set; }
        public DbSet<Status> Status { get; set; }
   
    }


}

