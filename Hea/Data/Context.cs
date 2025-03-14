using Microsoft.EntityFrameworkCore;
using Hea.Models;

namespace Hea.Data
{
    public class Context : DbContext
    {
        public Context(DbContextOptions<Context> options) : base(options) { }
        public DbSet<User> Users { get; set; }
        public DbSet<Appointment> Appointments { get; set; }
        public DbSet<Consultation> Consultations { get; set; }
        public DbSet<DocAvailability> DocAvailabilities { get; set; }
        public DbSet<Notification> Notifications { get; set; }
        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    // User Table
        //    modelBuilder.Entity<User>()
        //        .HasKey(u => u.UserId);
        //    modelBuilder.Entity<User>()
        //        .Property(u => u.Name)
        //        .IsRequired()
        //        .HasMaxLength(100);
        //    modelBuilder.Entity<User>()
        //        .Property(u => u.Role)
        //        .IsRequired();
        //    modelBuilder.Entity<User>()
        //        .Property(u => u.Email)
        //        .IsRequired();
        //    modelBuilder.Entity<User>()
        //        .Property(u => u.Password)
        //        .IsRequired();
            // Appointment Table
            //modelBuilder.Entity<Appointment>()
            //    .HasKey(a => a.AppointmentId);
            //modelBuilder.Entity<Appointment>()
            //    .Property(a => a.Status)
            //    .IsRequired();
            //modelBuilder.Entity<Appointment>()
            //    .HasOne<User>()
            //    .WithMany()
            //    .HasForeignKey(a => a.PatientId)
            //    .OnDelete(DeleteBehavior.Restrict);
            //modelBuilder.Entity<Appointment>()
            //    .HasOne<User>()
            //    .WithMany()
            //    //.HasForeignKey(a => a.DoctorId)
            //    .OnDelete(DeleteBehavior.Restrict);
            // Consultation Table
        //    modelBuilder.Entity<Consultation>()
        //        .HasKey(c => c.ConsultationId);
        //    modelBuilder.Entity<Consultation>()
        //        .Property(c => c.Notes)
        //        .IsRequired();
        //    modelBuilder.Entity<Consultation>()
        //        .HasOne<Appointment>()
        //        .WithMany()
        //        .HasForeignKey(c => c.AppointmentId)
        //        .OnDelete(DeleteBehavior.Cascade);
        //    modelBuilder.Entity<Consultation>()
        //        .HasOne<User>()
        //        .WithMany()
        //        .HasForeignKey(c => c.DoctorId)
        //        .OnDelete(DeleteBehavior.Restrict);
        //    // Doctor Availability Table
        //    modelBuilder.Entity<DocAvailability>()
        //        .HasKey(d => d.SessionId);
        //    modelBuilder.Entity<DocAvailability>()
        //        .Property(d => d.AvailableDate)
        //        .IsRequired();
        //    modelBuilder.Entity<DocAvailability>()
        //        .Property(d => d.StartTime)
        //        .IsRequired();
        //    modelBuilder.Entity<DocAvailability>()
        //        .Property(d => d.EndTime)
        //        .IsRequired();
        //    modelBuilder.Entity<DocAvailability>()
        //        .Property(d => d.Location)
        //        .IsRequired();
        //    modelBuilder.Entity<DocAvailability>()
        //        .HasOne<User>()
        //        .WithMany()
        //        .HasForeignKey(d => d.DoctorId)
        //        .OnDelete(DeleteBehavior.Cascade);
        //    // Notification Table
        //    modelBuilder.Entity<Notification>()
        //        .HasKey(n => n.NotificationId);
        //    modelBuilder.Entity<Notification>()
        //        .Property(n => n.Message)
        //        .IsRequired();
        //    modelBuilder.Entity<Notification>()
        //        .Property(n => n.Status)
        //        .IsRequired();
        //    modelBuilder.Entity<Notification>()
        //        .HasOne<User>()
        //        .WithMany()
        //        .HasForeignKey(n => n.UserId)
        //        .OnDelete(DeleteBehavior.Cascade);
        //}
    }
}