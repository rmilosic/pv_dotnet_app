using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.Extensions.Configuration;


namespace project_management_app.Models
{
    public partial class DB_COMPANYContext : DbContext
    {
        public DB_COMPANYContext()
        {
        }

        public DB_COMPANYContext(DbContextOptions<DB_COMPANYContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Employees> Employees { get; set; }
        public virtual DbSet<ProjectCooperation> ProjectCooperation { get; set; }
        public virtual DbSet<Projects> Projects { get; set; }
        public virtual DbSet<Teams> Teams { get; set; }
        public virtual DbSet<WorkItems> WorkItems { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                //TODO use environment variables
                // #warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Server=127.0.0.1,1433;Database=DB_COMPANY; User ID=sa; Password=51r0v-Bur3k");
                // Configuration.GetConnectionString("MvcMovieContext"))
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Employees>(entity =>
            {
                entity.ToTable("employees");

                entity.HasIndex(e => e.Email)
                    .HasName("UK_employees_Email")
                    .IsUnique();

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(254);

                entity.Property(e => e.FirstName)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.LastName)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.Middlename).HasMaxLength(100);

                entity.Property(e => e.TeamId).HasColumnName("Team_Id");

                entity.HasOne(d => d.Team)
                    .WithMany(p => p.Employees)
                    .HasForeignKey(d => d.TeamId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_employees_teams");
            });

            modelBuilder.Entity<ProjectCooperation>(entity =>
            {
                entity.ToTable("project_cooperation");

                entity.HasIndex(e => new { e.ProjectId, e.TeamId })
                    .HasName("UK_project_cooperation_Project_Id_Team_Id")
                    .IsUnique();

                entity.Property(e => e.DateAssigned).HasColumnType("datetime");

                entity.Property(e => e.ProjectId).HasColumnName("Project_Id");

                entity.Property(e => e.TeamId).HasColumnName("Team_Id");

                entity.HasOne(d => d.Project)
                    .WithMany(p => p.ProjectCooperation)
                    .HasForeignKey(d => d.ProjectId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_project_cooperation_projects");

                entity.HasOne(d => d.Team)
                    .WithMany(p => p.ProjectCooperation)
                    .HasForeignKey(d => d.TeamId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_project_cooperation_teams");
            });

            modelBuilder.Entity<Projects>(entity =>
            {
                entity.ToTable("projects");

                entity.HasIndex(e => e.Name)
                    .HasName("UK_projects_Name")
                    .IsUnique();

                entity.Property(e => e.DateDue).HasColumnType("datetime");

                entity.Property(e => e.DateStart).HasColumnType("datetime");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.ProjectManagerId).HasColumnName("ProjectManager_Id");

                entity.HasOne(d => d.ProjectManager)
                    .WithMany(p => p.Projects)
                    .HasForeignKey(d => d.ProjectManagerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_projects_employees");
            });

            modelBuilder.Entity<Teams>(entity =>
            {
                entity.ToTable("teams");

                entity.HasIndex(e => e.Name)
                    .HasName("UK_teams_Name")
                    .IsUnique();

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(100);
            });

            modelBuilder.Entity<WorkItems>(entity =>
            {
                entity.ToTable("work_items");

                entity.Property(e => e.DateCreated).HasColumnType("datetime");

                entity.Property(e => e.DateDue).HasColumnType("datetime");

                entity.Property(e => e.DateFinished).HasColumnType("datetime");

                entity.Property(e => e.DateStarted).HasColumnType("datetime");

                entity.Property(e => e.Description).HasMaxLength(1000);

                entity.Property(e => e.EmployeeId).HasColumnName("Employee_Id");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.ProjectId).HasColumnName("Project_Id");

                entity.HasOne(d => d.Employee)
                    .WithMany(p => p.WorkItems)
                    .HasForeignKey(d => d.EmployeeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_work_items_employees");

                entity.HasOne(d => d.Project)
                    .WithMany(p => p.WorkItems)
                    .HasForeignKey(d => d.ProjectId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_work_items_projects");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
