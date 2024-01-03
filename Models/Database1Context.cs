using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace DatabaseApp.Models;

public partial class Database1Context : DbContext
{
    public Database1Context()
    {
    }

    public Database1Context(DbContextOptions<Database1Context> options)
        : base(options)
    {
    }

    public virtual DbSet<Admin> Admins { get; set; }

    public virtual DbSet<Author> Authors { get; set; }

    public virtual DbSet<CoSupervisor> CoSupervisors { get; set; }

    public virtual DbSet<Institute> Institutes { get; set; }

    public virtual DbSet<Keyword> Keywords { get; set; }

    public virtual DbSet<Person> People { get; set; }

    public virtual DbSet<SubjectTopic> SubjectTopics { get; set; }

    public virtual DbSet<Supervisor> Supervisors { get; set; }

    public virtual DbSet<Thesis> Theses { get; set; }

    public virtual DbSet<University> Universities { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=.;Database=Database1;Trusted_Connection=True;TrustServerCertificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Admin>(entity =>
        {
            entity.HasKey(e => e.AdminId).HasName("PK__Admin__719FE4E8EC39603E");

            entity.ToTable("Admin");

            entity.Property(e => e.AdminId)
                .ValueGeneratedNever()
                .HasColumnName("AdminID");
            entity.Property(e => e.Password)
                .HasMaxLength(100)
                .UseCollation("Turkish_100_CS_AI");
            entity.Property(e => e.Username)
                .HasMaxLength(100)
                .UseCollation("Turkish_100_CS_AI");
        });

        modelBuilder.Entity<Author>(entity =>
        {
            entity.HasKey(e => e.AuthorId).HasName("PK__Author__70DAFC146B8BF10E");

            entity.ToTable("Author");

            entity.Property(e => e.AuthorId).HasColumnName("AuthorID");
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .UseCollation("Turkish_100_CS_AI");
            entity.Property(e => e.PersonId).HasColumnName("PersonID");

            entity.HasOne(d => d.Person).WithMany(p => p.Authors)
                .HasForeignKey(d => d.PersonId)
                .HasConstraintName("FK__Author__PersonID__3B75D760");
        });

        modelBuilder.Entity<CoSupervisor>(entity =>
        {
            entity.HasKey(e => e.CoSupervisorId).HasName("PK__CoSuperv__845F095FB477A63E");

            entity.ToTable("CoSupervisor");

            entity.Property(e => e.CoSupervisorId).HasColumnName("CoSupervisorID");
            entity.Property(e => e.PersonId).HasColumnName("PersonID");

            entity.HasOne(d => d.Person).WithMany(p => p.CoSupervisors)
                .HasForeignKey(d => d.PersonId)
                .HasConstraintName("FK__CoSupervi__Perso__412EB0B6");
        });

        modelBuilder.Entity<Institute>(entity =>
        {
            entity.HasKey(e => e.InstituteId).HasName("PK__Institut__09EC0D9B3FD30B8F");

            entity.ToTable("Institute");

            entity.Property(e => e.InstituteId).HasColumnName("InstituteID");
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .UseCollation("Turkish_100_CS_AI");
            entity.Property(e => e.UniversityId).HasColumnName("UniversityID");

            entity.HasOne(d => d.University).WithMany(p => p.Institutes)
                .HasForeignKey(d => d.UniversityId)
                .HasConstraintName("FK__Institute__Unive__45F365D3");
        });

        modelBuilder.Entity<Keyword>(entity =>
        {
            entity.HasKey(e => e.KeywordId).HasName("PK__Keyword__37C135C17900171E");

            entity.ToTable("Keyword");

            entity.Property(e => e.KeywordId).HasColumnName("KeywordID");
            entity.Property(e => e.KeywordText)
                .HasMaxLength(50)
                .IsUnicode(false)
                .UseCollation("Turkish_100_CS_AI");
        });

        modelBuilder.Entity<Person>(entity =>
        {
            entity.HasKey(e => e.PersonId).HasName("PK__Person__AA2FFB85E39D2B74");

            entity.ToTable("Person");

            entity.Property(e => e.PersonId).HasColumnName("PersonID");
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .UseCollation("Turkish_100_CS_AI");
            entity.Property(e => e.Role)
                .HasMaxLength(50)
                .UseCollation("Turkish_100_CS_AI");
        });

        modelBuilder.Entity<SubjectTopic>(entity =>
        {
            entity.HasKey(e => e.TopicId).HasName("PK__SubjectT__022E0F7D2D3CC8D2");

            entity.ToTable("SubjectTopic");

            entity.Property(e => e.TopicId).HasColumnName("TopicID");
            entity.Property(e => e.TopicName)
                .HasMaxLength(100)
                .UseCollation("Turkish_100_CS_AI");
        });

        modelBuilder.Entity<Supervisor>(entity =>
        {
            entity.HasKey(e => e.SupervisorId).HasName("PK__Supervis__6FAABDAFF002489D");

            entity.ToTable("Supervisor");

            entity.Property(e => e.SupervisorId).HasColumnName("SupervisorID");
            entity.Property(e => e.PersonId).HasColumnName("PersonID");

            entity.HasOne(d => d.Person).WithMany(p => p.Supervisors)
                .HasForeignKey(d => d.PersonId)
                .HasConstraintName("FK__Superviso__Perso__3E52440B");
        });

        modelBuilder.Entity<Thesis>(entity =>
        {
            entity.HasKey(e => e.ThesisNumber).HasName("PK__Thesis__BA8B77A6F9850505");

            entity.ToTable("Thesis");

            entity.Property(e => e.Abstract)
                .HasMaxLength(4000)
                .UseCollation("Turkish_100_CS_AI");
            entity.Property(e => e.AuthorId).HasColumnName("AuthorID");
            entity.Property(e => e.CoSupervisorId).HasColumnName("CoSupervisorID");
            entity.Property(e => e.InstituteId).HasColumnName("InstituteID");
            entity.Property(e => e.KeywordId).HasColumnName("KeywordID");
            entity.Property(e => e.Language)
                .HasMaxLength(50)
                .UseCollation("Turkish_100_CS_AI");
            entity.Property(e => e.SupervisorId).HasColumnName("SupervisorID");
            entity.Property(e => e.Title)
                .HasMaxLength(500)
                .UseCollation("Turkish_100_CS_AI");
            entity.Property(e => e.TopicId).HasColumnName("TopicID");
            entity.Property(e => e.Type)
                .HasMaxLength(50)
                .UseCollation("Turkish_100_CS_AI");
            entity.Property(e => e.UniversityId).HasColumnName("UniversityID");

            entity.HasOne(d => d.Author).WithMany(p => p.Theses)
                .HasForeignKey(d => d.AuthorId)
                .HasConstraintName("FK__Thesis__AuthorID__5165187F");

            entity.HasOne(d => d.CoSupervisor).WithMany(p => p.Theses)
                .HasForeignKey(d => d.CoSupervisorId)
                .HasConstraintName("FK__Thesis__CoSuperv__5070F446");

            entity.HasOne(d => d.Institute).WithMany(p => p.Theses)
                .HasForeignKey(d => d.InstituteId)
                .HasConstraintName("FK__Thesis__Institut__5535A963");

            entity.HasOne(d => d.Keyword).WithMany(p => p.Theses)
                .HasForeignKey(d => d.KeywordId)
                .HasConstraintName("FK__Thesis__KeywordI__52593CB8");

            entity.HasOne(d => d.Supervisor).WithMany(p => p.Theses)
                .HasForeignKey(d => d.SupervisorId)
                .HasConstraintName("FK__Thesis__Supervis__4F7CD00D");

            entity.HasOne(d => d.Topic).WithMany(p => p.Theses)
                .HasForeignKey(d => d.TopicId)
                .HasConstraintName("FK__Thesis__TopicID__534D60F1");

            entity.HasOne(d => d.University).WithMany(p => p.Theses)
                .HasForeignKey(d => d.UniversityId)
                .HasConstraintName("FK__Thesis__Universi__5441852A");
        });

        modelBuilder.Entity<University>(entity =>
        {
            entity.HasKey(e => e.UniversityId).HasName("PK__Universi__9F19E19C47CA4EB3");

            entity.ToTable("University");

            entity.Property(e => e.UniversityId).HasColumnName("UniversityID");
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .UseCollation("Turkish_100_CS_AI");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
