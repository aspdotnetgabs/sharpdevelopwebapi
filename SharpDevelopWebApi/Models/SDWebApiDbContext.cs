using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace SharpDevelopWebApi.Models
{
    public class SDWebApiDbContext : DbContext
    {
        public SDWebApiDbContext() : base("DagkutaDb") // name_of_dbconnection_string
        {
        }

        // Map model classes to database tables
        public DbSet <UserAccount> Users { get; set; }
        public DbSet<Examinee> Examinees { get; set; }
        public DbSet<Question> Questions { get; set; }
        public DbSet<Exam> Exams { get; set; }
    }


}

