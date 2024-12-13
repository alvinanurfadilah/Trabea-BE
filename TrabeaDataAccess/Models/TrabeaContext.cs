using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace TrabeaDataAccess.Models
{
    public partial class TrabeaContext : DbContext
    {
        public TrabeaContext()
        {
        }

        public TrabeaContext(DbContextOptions<TrabeaContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Employee> Employees { get; set; } = null!;
        public virtual DbSet<PartTimeEmployee> PartTimeEmployees { get; set; } = null!;
        public virtual DbSet<Role> Roles { get; set; } = null!;
        public virtual DbSet<User> Users { get; set; } = null!;
        public virtual DbSet<WorkSchedule> WorkSchedules { get; set; } = null!;
        public virtual DbSet<WorkShift> WorkShifts { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Employee>(entity =>
            {
                entity.Property(e => e.FirstName)
                    .HasMaxLength(25)
                    .IsUnicode(false);

                entity.Property(e => e.LastName)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.WorkEmail)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.HasOne(d => d.WorkEmailNavigation)
                    .WithMany(p => p.Employees)
                    .HasForeignKey(d => d.WorkEmail)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Employees__WorkE__2D27B809");
            });

            modelBuilder.Entity<PartTimeEmployee>(entity =>
            {
                entity.HasIndex(e => e.PersonalEmail, "UQ__PartTime__7B5B59A53A20EDAD")
                    .IsUnique();

                entity.HasIndex(e => e.PersonalPhoneNumber, "UQ__PartTime__91CFA0ED60329F1E")
                    .IsUnique();

                entity.Property(e => e.Address)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.FirstName)
                    .HasMaxLength(25)
                    .IsUnicode(false);

                entity.Property(e => e.JoinDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.LastEducation)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.LastName)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.OnGoingEducation)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.PersonalEmail)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.PersonalPhoneNumber)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.ResignDate).HasColumnType("datetime");

                entity.Property(e => e.WorkEmail)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.HasOne(d => d.WorkEmailNavigation)
                    .WithMany(p => p.PartTimeEmployees)
                    .HasForeignKey(d => d.WorkEmail)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__PartTimeE__WorkE__32E0915F");
            });

            modelBuilder.Entity<Role>(entity =>
            {
                entity.HasIndex(e => e.Name, "UQ__Roles__737584F6FE192CBD")
                    .IsUnique();

                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(e => e.Email)
                    .HasName("PK__Users__A9D10535020350CD");

                entity.Property(e => e.Email)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Password)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.HasMany(d => d.Roles)
                    .WithMany(p => p.Users)
                    .UsingEntity<Dictionary<string, object>>(
                        "UserRole",
                        l => l.HasOne<Role>().WithMany().HasForeignKey("RoleId").OnDelete(DeleteBehavior.ClientSetNull).HasConstraintName("FK__UserRoles__RoleI__2A4B4B5E"),
                        r => r.HasOne<User>().WithMany().HasForeignKey("UserId").OnDelete(DeleteBehavior.ClientSetNull).HasConstraintName("FK__UserRoles__UserI__29572725"),
                        j =>
                        {
                            j.HasKey("UserId", "RoleId").HasName("PK__UserRole__AF2760AD4AB4C9E4");

                            j.ToTable("UserRoles");

                            j.IndexerProperty<string>("UserId").HasMaxLength(100).IsUnicode(false);
                        });
            });

            modelBuilder.Entity<WorkSchedule>(entity =>
            {
                entity.Property(e => e.WorkDate).HasColumnType("date");

                entity.HasOne(d => d.Manager)
                    .WithMany(p => p.WorkSchedules)
                    .HasForeignKey(d => d.ManagerId)
                    .HasConstraintName("FK__WorkSched__Manag__37A5467C");

                entity.HasOne(d => d.PartTimeEmployee)
                    .WithMany(p => p.WorkSchedules)
                    .HasForeignKey(d => d.PartTimeEmployeeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__WorkSched__PartT__38996AB5");

                entity.HasOne(d => d.Shift)
                    .WithMany(p => p.WorkSchedules)
                    .HasForeignKey(d => d.ShiftId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__WorkSched__Shift__398D8EEE");
            });

            modelBuilder.Entity<WorkShift>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
