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
        => optionsBuilder.UseSqlServer("Data Source=EMRECAN;Initial Catalog=Database1;Integrated Security=True;Encrypt=False");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Author>(entity =>
        {
            entity.HasKey(e => e.AuthorId).HasName("PK__Author__70DAFC144335C0D4");

            entity.ToTable("Author");

            entity.Property(e => e.AuthorId)
                .ValueGeneratedNever()
                .HasColumnName("AuthorID");
            entity.Property(e => e.Name).HasMaxLength(100);
            entity.Property(e => e.PersonId).HasColumnName("PersonID");

            entity.HasOne(d => d.Person).WithMany(p => p.Authors)
                .HasForeignKey(d => d.PersonId)
                .HasConstraintName("FK__Author__PersonID__4D94879B");
        });

        modelBuilder.Entity<CoSupervisor>(entity =>
        {
            entity.HasKey(e => e.CoSupervisorId).HasName("PK__CoSuperv__845F095FE0CE6513");

            entity.ToTable("CoSupervisor");

            entity.Property(e => e.CoSupervisorId)
                .ValueGeneratedNever()
                .HasColumnName("CoSupervisorID");
            entity.Property(e => e.PersonId).HasColumnName("PersonID");

            entity.HasOne(d => d.Person).WithMany(p => p.CoSupervisors)
                .HasForeignKey(d => d.PersonId)
                .HasConstraintName("FK__CoSupervi__Perso__534D60F1");
        });

        modelBuilder.Entity<Institute>(entity =>
        {
            entity.HasKey(e => e.InstituteId).HasName("PK__Institut__09EC0D9B3E70CB0F");

            entity.ToTable("Institute");

            entity.Property(e => e.InstituteId)
                .ValueGeneratedNever()
                .HasColumnName("InstituteID");
            entity.Property(e => e.Name).HasMaxLength(100);
            entity.Property(e => e.UniversityId).HasColumnName("UniversityID");

            entity.HasOne(d => d.University).WithMany(p => p.Institutes)
                .HasForeignKey(d => d.UniversityId)
                .HasConstraintName("FK__Institute__Unive__5812160E");
        });

        modelBuilder.Entity<Keyword>(entity =>
        {
            entity.HasKey(e => e.KeywordId).HasName("PK__Keyword__37C135C1FDD15F31");

            entity.ToTable("Keyword");

            entity.Property(e => e.KeywordId)
                .ValueGeneratedNever()
                .HasColumnName("KeywordID");
            entity.Property(e => e.KeywordText)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Person>(entity =>
        {
            entity.HasKey(e => e.PersonId).HasName("PK__Person__AA2FFB85EA6A0FBB");

            entity.ToTable("Person");

            entity.Property(e => e.PersonId)
                .ValueGeneratedNever()
                .HasColumnName("PersonID");
            entity.Property(e => e.Name).HasMaxLength(50);
            entity.Property(e => e.Role).HasMaxLength(50);
        });

        modelBuilder.Entity<SubjectTopic>(entity =>
        {
            entity.HasKey(e => e.TopicId).HasName("PK__SubjectT__022E0F7D25059D96");

            entity.ToTable("SubjectTopic");

            entity.Property(e => e.TopicId)
                .ValueGeneratedNever()
                .HasColumnName("TopicID");
            entity.Property(e => e.TopicName).HasMaxLength(100);
        });

        modelBuilder.Entity<Supervisor>(entity =>
        {
            entity.HasKey(e => e.SupervisorId).HasName("PK__Supervis__6FAABDAFF45176E0");

            entity.ToTable("Supervisor");

            entity.Property(e => e.SupervisorId)
                .ValueGeneratedNever()
                .HasColumnName("SupervisorID");
            entity.Property(e => e.PersonId).HasColumnName("PersonID");

            entity.HasOne(d => d.Person).WithMany(p => p.Supervisors)
                .HasForeignKey(d => d.PersonId)
                .HasConstraintName("FK__Superviso__Perso__5070F446");
        });

        modelBuilder.Entity<Thesis>(entity =>
        {
            entity.HasKey(e => e.ThesisNumber).HasName("PK__Thesis__BA8B77A65AAB033A");

            entity.ToTable("Thesis");

            entity.Property(e => e.ThesisNumber).ValueGeneratedNever();
            entity.Property(e => e.Abstract).HasMaxLength(4000);
            entity.Property(e => e.AuthorId).HasColumnName("AuthorID");
            entity.Property(e => e.CosupervisorId).HasColumnName("CosupervisorID");
            entity.Property(e => e.InstituteId).HasColumnName("InstituteID");
            entity.Property(e => e.KeywordId).HasColumnName("KeywordID");
            entity.Property(e => e.Language).HasMaxLength(50);
            entity.Property(e => e.SupervisorId).HasColumnName("SupervisorID");
            entity.Property(e => e.Title).HasMaxLength(500);
            entity.Property(e => e.Type).HasMaxLength(50);
            entity.Property(e => e.UniversityId).HasColumnName("UniversityID");

            entity.HasOne(d => d.Author).WithMany(p => p.Theses)
                .HasForeignKey(d => d.AuthorId)
                .HasConstraintName("FK__Thesis__AuthorID__5EBF139D");

            entity.HasOne(d => d.Cosupervisor).WithMany(p => p.Theses)
                .HasForeignKey(d => d.CosupervisorId)
                .HasConstraintName("FK__Thesis__Cosuperv__5DCAEF64");

            entity.HasOne(d => d.Institute).WithMany(p => p.Theses)
                .HasForeignKey(d => d.InstituteId)
                .HasConstraintName("FK__Thesis__Institut__619B8048");

            entity.HasOne(d => d.Keyword).WithMany(p => p.Theses)
                .HasForeignKey(d => d.KeywordId)
                .HasConstraintName("FK__Thesis__KeywordI__5FB337D6");

            entity.HasOne(d => d.Supervisor).WithMany(p => p.Theses)
                .HasForeignKey(d => d.SupervisorId)
                .HasConstraintName("FK__Thesis__Supervis__5CD6CB2B");

            entity.HasOne(d => d.University).WithMany(p => p.Theses)
                .HasForeignKey(d => d.UniversityId)
                .HasConstraintName("FK__Thesis__Universi__60A75C0F");

            entity.HasMany(d => d.Keywords).WithMany(p => p.ThesisNumbers)
                .UsingEntity<Dictionary<string, object>>(
                    "ThesisKeyword",
                    r => r.HasOne<Keyword>().WithMany()
                        .HasForeignKey("KeywordId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__ThesisKey__Keywo__693CA210"),
                    l => l.HasOne<Thesis>().WithMany()
                        .HasForeignKey("ThesisNumber")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__ThesisKey__Thesi__68487DD7"),
                    j =>
                    {
                        j.HasKey("ThesisNumber", "KeywordId").HasName("PK__ThesisKe__B9F764FA46397070");
                        j.ToTable("ThesisKeyword");
                        j.IndexerProperty<int>("KeywordId").HasColumnName("KeywordID");
                    });

            entity.HasMany(d => d.Topics).WithMany(p => p.ThesisNumbers)
                .UsingEntity<Dictionary<string, object>>(
                    "ThesisSubjectTopic",
                    r => r.HasOne<SubjectTopic>().WithMany()
                        .HasForeignKey("TopicId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__ThesisSub__Topic__656C112C"),
                    l => l.HasOne<Thesis>().WithMany()
                        .HasForeignKey("ThesisNumber")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__ThesisSub__Thesi__6477ECF3"),
                    j =>
                    {
                        j.HasKey("ThesisNumber", "TopicId").HasName("PK__ThesisSu__7AA997517F0957F7");
                        j.ToTable("ThesisSubjectTopic");
                        j.IndexerProperty<int>("TopicId").HasColumnName("TopicID");
                    });
        });

        modelBuilder.Entity<University>(entity =>
        {
            entity.HasKey(e => e.UniversityId).HasName("PK__Universi__9F19E19C17F94C80");

            entity.ToTable("University");

            entity.Property(e => e.UniversityId)
                .ValueGeneratedNever()
                .HasColumnName("UniversityID");
            entity.Property(e => e.Name).HasMaxLength(100);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
