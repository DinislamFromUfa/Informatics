﻿using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Reflection.Emit;
using WebApplication1.Configurations;
using WebApplication1.Models;

namespace WebApplication1.Data

{
    public class DataBaseContext: DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=MainDataOnTeaching.db");

        }
        public DataBaseContext()
        {
            //Database.EnsureDeleted();
            Database.EnsureCreated();
        }

        public DbSet<Teacher> Teachers { get; set; }

        public DbSet<Student> Students { get; set; }

        public DbSet<Course> Courses { get; set; }

        public DbSet<Lesson> Lessons { get; set; }

        public DbSet<Homework> Homeworks { get; set; }

        public DbSet<ValueOfHomework> ValuesOfHomework { get; set; }

        public DbSet<ReviewOnCourse> ReviewOnCourses { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new TeacherConfiguration());
            modelBuilder.ApplyConfiguration(new CourseConfiguration());
            modelBuilder.ApplyConfiguration(new StudentConfiguration());
            modelBuilder.ApplyConfiguration(new LessonConfiguration());
            modelBuilder.ApplyConfiguration(new HomeworkConfiguration());
            modelBuilder.ApplyConfiguration(new ValueOfHomeworkConfiguration());
            modelBuilder.ApplyConfiguration(new ReviewOnCourseConfiguration());
            base.OnModelCreating(modelBuilder);
        }
    }
}
