using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Konecta.Tools.CCaptureClient.Core.DbEntities;

public partial class CCaptureDbContext : DbContext
{
    public CCaptureDbContext()
    {
    }

    public CCaptureDbContext(DbContextOptions<CCaptureDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Batch> Batches { get; set; }

    public virtual DbSet<BatchClass> BatchClasses { get; set; }

    public virtual DbSet<BatchClass1> BatchClasses1 { get; set; }

    public virtual DbSet<BatchField> BatchFields { get; set; }

    public virtual DbSet<BatchFieldDef> BatchFieldDefs { get; set; }

    public virtual DbSet<BatchState> BatchStates { get; set; }

    public virtual DbSet<Document> Documents { get; set; }

    public virtual DbSet<DocumentClass> DocumentClasses { get; set; }

    public virtual DbSet<DocumentField> DocumentFields { get; set; }

    public virtual DbSet<Field> Fields { get; set; }

    public virtual DbSet<FieldType> FieldTypes { get; set; }

    public virtual DbSet<Group> Groups { get; set; }

    public virtual DbSet<Page> Pages { get; set; }

    public virtual DbSet<PageType> PageTypes { get; set; }

    public virtual DbSet<PageType1> PageTypes1 { get; set; }

    public virtual DbSet<Signature> Signatures { get; set; }

    public virtual DbSet<Submission> Submissions { get; set; }

    public virtual DbSet<VerificationDocument> VerificationDocuments { get; set; }

    public virtual DbSet<VerificationDocumentClass> VerificationDocumentClasses { get; set; }

    public virtual DbSet<VerificationResponse> VerificationResponses { get; set; }

//    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
//#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
//        => optionsBuilder.UseSqlServer("Server=ANDREW-SAMY\\MSSQLSERVER2;Database=CCaptureClientDB;User Id=sa;Password=123;TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Batch>(entity =>
        {
            entity.HasKey(e => e.BatchId).HasName("PK__Batches__5D55CE586B4142EB");

            entity.HasIndex(e => e.BatchClassId, "IX_Batches_BatchClassId");

            entity.Property(e => e.CloseDate).HasColumnType("datetime");
            entity.Property(e => e.CreationDate).HasColumnType("datetime");
            entity.Property(e => e.Name).HasMaxLength(255);

            entity.HasOne(d => d.BatchClass).WithMany(p => p.Batches)
                .HasForeignKey(d => d.BatchClassId)
                .HasConstraintName("FK__Batches__BatchCl__5812160E");
        });

        modelBuilder.Entity<BatchClass>(entity =>
        {
            entity.HasKey(e => e.IdBatchClass).HasName("PK__batch_cl__16F99ACEB4291FE6");

            entity.ToTable("batch_class");

            entity.Property(e => e.IdBatchClass).HasColumnName("id_batch_class");
            entity.Property(e => e.Name)
                .HasMaxLength(255)
                .HasColumnName("name");
        });

        modelBuilder.Entity<BatchClass1>(entity =>
        {
            entity.HasKey(e => e.BatchClassId).HasName("PK__BatchCla__574C7EBD7F8FFD99");

            entity.ToTable("BatchClasses");

            entity.Property(e => e.Name).HasMaxLength(255);
        });

        modelBuilder.Entity<BatchField>(entity =>
        {
            entity.HasKey(e => e.BatchFieldId).HasName("PK__BatchFie__D90DB39DC4F20D61");

            entity.HasIndex(e => e.BatchId, "IX_BatchFields_BatchId");

            entity.Property(e => e.Name).HasMaxLength(255);

            entity.HasOne(d => d.Batch).WithMany(p => p.BatchFields)
                .HasForeignKey(d => d.BatchId)
                .HasConstraintName("FK__BatchFiel__Batch__59063A47");
        });

        modelBuilder.Entity<BatchFieldDef>(entity =>
        {
            entity.HasKey(e => e.IdBatchFieldDef).HasName("PK__batch_fi__A379CE941B474CFE");

            entity.ToTable("batch_field_def");

            entity.Property(e => e.IdBatchFieldDef).HasColumnName("id_batch_field_def");
            entity.Property(e => e.FieldName)
                .HasMaxLength(255)
                .HasColumnName("field_name");
            entity.Property(e => e.IdBatchClass).HasColumnName("id_batch_class");
            entity.Property(e => e.IdFieldType).HasColumnName("id_field_type");

            entity.HasOne(d => d.IdBatchClassNavigation).WithMany(p => p.BatchFieldDefs)
                .HasForeignKey(d => d.IdBatchClass)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__batch_fie__id_ba__71D1E811");

            entity.HasOne(d => d.IdFieldTypeNavigation).WithMany(p => p.BatchFieldDefs)
                .HasForeignKey(d => d.IdFieldType)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_batch_field_def_field_type");
        });

        modelBuilder.Entity<BatchState>(entity =>
        {
            entity.HasKey(e => e.BatchStateId).HasName("PK__BatchSta__5C426D5A9953D6A0");

            entity.HasIndex(e => e.BatchId, "IX_BatchStates_BatchId");

            entity.Property(e => e.TrackDate).HasColumnType("datetime");
            entity.Property(e => e.Value).HasMaxLength(255);
            entity.Property(e => e.Workstation).HasMaxLength(255);

            entity.HasOne(d => d.Batch).WithMany(p => p.BatchStates)
                .HasForeignKey(d => d.BatchId)
                .HasConstraintName("FK__BatchStat__Batch__59FA5E80");
        });

        modelBuilder.Entity<Document>(entity =>
        {
            entity.HasKey(e => e.DocumentId).HasName("PK__Document__1ABEEF0F7CA1810A");

            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.FileName).HasMaxLength(100);
            entity.Property(e => e.FilePath).HasMaxLength(500);
            entity.Property(e => e.PageType).HasMaxLength(50);

            entity.HasOne(d => d.Submission).WithMany(p => p.Documents)
                .HasForeignKey(d => d.SubmissionId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Documents__Submi__5BE2A6F2");
        });

        modelBuilder.Entity<DocumentClass>(entity =>
        {
            entity.HasKey(e => e.IdDocumentClass).HasName("PK__document__D16E5DA07DF43BBC");

            entity.ToTable("document_class");

            entity.Property(e => e.IdDocumentClass).HasColumnName("id_document_class");
            entity.Property(e => e.IdBatchClass).HasColumnName("id_batch_class");
            entity.Property(e => e.Name)
                .HasMaxLength(255)
                .HasColumnName("name");

            entity.HasOne(d => d.IdBatchClassNavigation).WithMany(p => p.DocumentClasses)
                .HasForeignKey(d => d.IdBatchClass)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__document___id_ba__76969D2E");
        });

        modelBuilder.Entity<DocumentField>(entity =>
        {
            entity.HasKey(e => e.DocumentFieldId).HasName("PK__Document__BA65001E396519CB");

            entity.HasIndex(e => e.VerificationDocumentId, "IX_DocumentFields_VerificationDocumentId");

            entity.Property(e => e.Name).HasMaxLength(255);

            entity.HasOne(d => d.VerificationDocument).WithMany(p => p.DocumentFields)
                .HasForeignKey(d => d.VerificationDocumentId)
                .HasConstraintName("FK__DocumentF__Verif__5AEE82B9");
        });

        modelBuilder.Entity<Field>(entity =>
        {
            entity.HasKey(e => e.FieldId).HasName("PK__Fields__C8B6FF07A40F0E3E");

            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.FieldName).HasMaxLength(100);
            entity.Property(e => e.FieldType).HasMaxLength(50);
            entity.Property(e => e.FieldValue).HasMaxLength(500);

            entity.HasOne(d => d.Submission).WithMany(p => p.Fields)
                .HasForeignKey(d => d.SubmissionId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Fields__Submissi__5CD6CB2B");
        });

        modelBuilder.Entity<FieldType>(entity =>
        {
            entity.HasKey(e => e.IdFieldType).HasName("PK__field_ty__53500C37984A333F");

            entity.ToTable("field_type");

            entity.Property(e => e.IdFieldType).HasColumnName("id_field_type");
            entity.Property(e => e.TypeName)
                .HasMaxLength(255)
                .HasColumnName("type_name");
        });

        modelBuilder.Entity<Group>(entity =>
        {
            entity.HasKey(e => e.GroupId).HasName("PK__Groups__149AF36A5102A98D");

            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.GroupName).HasMaxLength(50);
        });

        modelBuilder.Entity<Page>(entity =>
        {
            entity.HasKey(e => e.PageId).HasName("PK__Pages__C565B104B4EBBFF6");

            entity.HasIndex(e => e.VerificationDocumentId, "IX_Pages_VerificationDocumentId");

            entity.Property(e => e.FileName).HasMaxLength(255);

            entity.HasOne(d => d.VerificationDocument).WithMany(p => p.Pages)
                .HasForeignKey(d => d.VerificationDocumentId)
                .HasConstraintName("FK__Pages__Verificat__5DCAEF64");
        });

        modelBuilder.Entity<PageType>(entity =>
        {
            entity.HasKey(e => e.IdPageType).HasName("PK__page_typ__B6ABAE0B2CF434C0");

            entity.ToTable("page_type");

            entity.Property(e => e.IdPageType).HasColumnName("id_page_type");
            entity.Property(e => e.IdDocumentClass).HasColumnName("id_document_class");
            entity.Property(e => e.Name)
                .HasMaxLength(255)
                .HasColumnName("name");

            entity.HasOne(d => d.IdDocumentClassNavigation).WithMany(p => p.PageTypes)
                .HasForeignKey(d => d.IdDocumentClass)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__page_type__id_do__797309D9");
        });

        modelBuilder.Entity<PageType1>(entity =>
        {
            entity.HasKey(e => e.PageTypeId).HasName("PK__PageType__33FA9A45A64746F6");

            entity.ToTable("PageTypes");

            entity.HasIndex(e => e.PageId, "IX_PageTypes_PageId");

            entity.Property(e => e.Name).HasMaxLength(255);

            entity.HasOne(d => d.Page).WithMany(p => p.PageType1s)
                .HasForeignKey(d => d.PageId)
                .HasConstraintName("FK__PageTypes__PageI__5EBF139D");
        });

        modelBuilder.Entity<Signature>(entity =>
        {
            entity.HasKey(e => e.SignatureId).HasName("PK__Signatur__3DCA57A9C976BFF8");

            entity.HasIndex(e => e.VerificationDocumentId, "IX_Signatures_VerificationDocumentId");

            entity.HasOne(d => d.VerificationDocument).WithMany(p => p.Signatures)
                .HasForeignKey(d => d.VerificationDocumentId)
                .HasConstraintName("FK__Signature__Verif__5FB337D6");
        });

        modelBuilder.Entity<Submission>(entity =>
        {
            entity.HasKey(e => e.SubmissionId).HasName("PK__Submissi__449EE125A869F1FD");

            entity.HasIndex(e => e.RequestGuid, "UQ__Submissi__27CC2CAA2E291F11").IsUnique();

            entity.Property(e => e.AuthToken).HasMaxLength(500);
            entity.Property(e => e.BatchClassName).HasMaxLength(100);
            entity.Property(e => e.Channel).HasMaxLength(50);
            entity.Property(e => e.CheckedGuid).HasColumnName("Checked_GUID");
            entity.Property(e => e.InteractionDateTime).HasColumnType("datetime");
            entity.Property(e => e.MessageId).HasMaxLength(50);
            entity.Property(e => e.RequestGuid).HasMaxLength(36);
            entity.Property(e => e.SessionId).HasMaxLength(50);
            entity.Property(e => e.SourceSystem).HasMaxLength(50);
            entity.Property(e => e.SubmittedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.UserCode).HasMaxLength(50);

            entity.HasOne(d => d.Group).WithMany(p => p.Submissions)
                .HasForeignKey(d => d.GroupId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Submissio__Group__60A75C0F");
        });

        modelBuilder.Entity<VerificationDocument>(entity =>
        {
            entity.HasKey(e => e.VerificationDocumentId).HasName("PK__Verifica__D3A0DFE7E5798E16");

            entity.HasIndex(e => e.BatchId, "IX_VerificationDocuments_BatchId");

            entity.HasIndex(e => e.DocumentClassId, "IX_VerificationDocuments_DocumentClassId");

            entity.Property(e => e.Name).HasMaxLength(255);

            entity.HasOne(d => d.Batch).WithMany(p => p.VerificationDocuments)
                .HasForeignKey(d => d.BatchId)
                .HasConstraintName("FK__Verificat__Batch__619B8048");

            entity.HasOne(d => d.DocumentClass).WithMany(p => p.VerificationDocuments)
                .HasForeignKey(d => d.DocumentClassId)
                .HasConstraintName("FK__Verificat__Docum__628FA481");
        });

        modelBuilder.Entity<VerificationDocumentClass>(entity =>
        {
            entity.HasKey(e => e.DocumentClassId).HasName("PK__Verifica__96CD938B5E7966CE");

            entity.Property(e => e.Name).HasMaxLength(255);
        });

        modelBuilder.Entity<VerificationResponse>(entity =>
        {
            entity.HasKey(e => e.VerificationResponseId).HasName("PK__Verifica__ABD28C682F3C64B2");

            entity.HasIndex(e => e.BatchId, "IX_VerificationResponses_BatchId");

            entity.Property(e => e.Channel).HasMaxLength(50);
            entity.Property(e => e.ExecutionDate).HasColumnType("datetime");
            entity.Property(e => e.InteractionDateTime).HasColumnType("datetime");
            entity.Property(e => e.MessageId).HasMaxLength(100);
            entity.Property(e => e.RequestGuid).HasMaxLength(36);
            entity.Property(e => e.SessionId).HasMaxLength(100);
            entity.Property(e => e.SourceSystem).HasMaxLength(50);
            entity.Property(e => e.UserCode).HasMaxLength(50);

            entity.HasOne(d => d.Batch).WithMany(p => p.VerificationResponses)
                .HasForeignKey(d => d.BatchId)
                .HasConstraintName("FK__Verificat__Batch__6383C8BA");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
