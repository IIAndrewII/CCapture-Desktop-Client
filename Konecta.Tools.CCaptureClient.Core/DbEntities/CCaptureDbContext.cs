using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

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

    public virtual DbSet<BatchField> BatchFields { get; set; }

    public virtual DbSet<BatchState> BatchStates { get; set; }

    public virtual DbSet<Document> Documents { get; set; }

    public virtual DbSet<DocumentField> DocumentFields { get; set; }

    public virtual DbSet<Field> Fields { get; set; }

    public virtual DbSet<Group> Groups { get; set; }

    public virtual DbSet<Page> Pages { get; set; }

    public virtual DbSet<PageType> PageTypes { get; set; }

    public virtual DbSet<Signature> Signatures { get; set; }

    public virtual DbSet<Submission> Submissions { get; set; }

    public virtual DbSet<VerificationDocument> VerificationDocuments { get; set; }

    public virtual DbSet<VerificationDocumentClass> VerificationDocumentClasses { get; set; }

    public virtual DbSet<VerificationResponse> VerificationResponses { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Batch>(entity =>
        {
            entity.HasKey(e => e.BatchId).HasName("PK__Batches__5D55CE5894988B6F");

            entity.HasIndex(e => e.BatchClassId, "IX_Batches_BatchClassId");

            entity.Property(e => e.CloseDate).HasColumnType("datetime");
            entity.Property(e => e.CreationDate).HasColumnType("datetime");
            entity.Property(e => e.Name).HasMaxLength(255);

            entity.HasOne(d => d.BatchClass).WithMany(p => p.Batches)
                .HasForeignKey(d => d.BatchClassId)
                .HasConstraintName("FK__Batches__BatchCl__6FE99F9F");
        });

        modelBuilder.Entity<BatchClass>(entity =>
        {
            entity.HasKey(e => e.BatchClassId).HasName("PK__BatchCla__574C7EBD17F0D421");

            entity.Property(e => e.Name).HasMaxLength(255);
        });

        modelBuilder.Entity<BatchField>(entity =>
        {
            entity.HasKey(e => e.BatchFieldId).HasName("PK__BatchFie__D90DB39DA6B8A942");

            entity.HasIndex(e => e.BatchId, "IX_BatchFields_BatchId");

            entity.Property(e => e.Name).HasMaxLength(255);

            entity.HasOne(d => d.Batch).WithMany(p => p.BatchFields)
                .HasForeignKey(d => d.BatchId)
                .HasConstraintName("FK__BatchFiel__Batch__5AEE82B9");
        });

        modelBuilder.Entity<BatchState>(entity =>
        {
            entity.HasKey(e => e.BatchStateId).HasName("PK__BatchSta__5C426D5AF5772098");

            entity.HasIndex(e => e.BatchId, "IX_BatchStates_BatchId");

            entity.Property(e => e.TrackDate).HasColumnType("datetime");
            entity.Property(e => e.Value).HasMaxLength(255);
            entity.Property(e => e.Workstation).HasMaxLength(255);

            entity.HasOne(d => d.Batch).WithMany(p => p.BatchStates)
                .HasForeignKey(d => d.BatchId)
                .HasConstraintName("FK__BatchStat__Batch__68487DD7");
        });

        modelBuilder.Entity<Document>(entity =>
        {
            entity.HasKey(e => e.DocumentId).HasName("PK__Document__1ABEEF0F69655626");

            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.FileName).HasMaxLength(100);
            entity.Property(e => e.FilePath).HasMaxLength(500);
            entity.Property(e => e.PageType).HasMaxLength(50);

            entity.HasOne(d => d.Submission).WithMany(p => p.Documents)
                .HasForeignKey(d => d.SubmissionId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Documents__Submi__403A8C7D");
        });

        modelBuilder.Entity<DocumentField>(entity =>
        {
            entity.HasKey(e => e.DocumentFieldId).HasName("PK__Document__BA65001E9E59D64D");

            entity.HasIndex(e => e.VerificationDocumentId, "IX_DocumentFields_VerificationDocumentId");

            entity.Property(e => e.Name).HasMaxLength(255);

            entity.HasOne(d => d.VerificationDocument).WithMany(p => p.DocumentFields)
                .HasForeignKey(d => d.VerificationDocumentId)
                .HasConstraintName("FK__DocumentF__Verif__6B24EA82");
        });

        modelBuilder.Entity<Field>(entity =>
        {
            entity.HasKey(e => e.FieldId).HasName("PK__Fields__C8B6FF0740D07B46");

            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.FieldName).HasMaxLength(100);
            entity.Property(e => e.FieldType).HasMaxLength(50);
            entity.Property(e => e.FieldValue).HasMaxLength(500);

            entity.HasOne(d => d.Submission).WithMany(p => p.Fields)
                .HasForeignKey(d => d.SubmissionId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Fields__Submissi__440B1D61");
        });

        modelBuilder.Entity<Group>(entity =>
        {
            entity.HasKey(e => e.GroupId).HasName("PK__Groups__149AF36AF763853B");

            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.GroupName).HasMaxLength(50);
        });

        modelBuilder.Entity<Page>(entity =>
        {
            entity.HasKey(e => e.PageId).HasName("PK__Pages__C565B104ADD70201");

            entity.HasIndex(e => e.VerificationDocumentId, "IX_Pages_VerificationDocumentId");

            entity.Property(e => e.FileName).HasMaxLength(255);

            entity.HasOne(d => d.VerificationDocument).WithMany(p => p.Pages)
                .HasForeignKey(d => d.VerificationDocumentId)
                .HasConstraintName("FK__Pages__Verificat__628FA481");
        });

        modelBuilder.Entity<PageType>(entity =>
        {
            entity.HasKey(e => e.PageTypeId).HasName("PK__PageType__33FA9A451AD3A1D0");

            entity.HasIndex(e => e.PageId, "IX_PageTypes_PageId");

            entity.Property(e => e.Name).HasMaxLength(255);

            entity.HasOne(d => d.Page).WithMany(p => p.PageTypes)
                .HasForeignKey(d => d.PageId)
                .HasConstraintName("FK__PageTypes__PageI__656C112C");
        });

        modelBuilder.Entity<Signature>(entity =>
        {
            entity.HasKey(e => e.SignatureId).HasName("PK__Signatur__3DCA57A9408DD561");

            entity.HasIndex(e => e.VerificationDocumentId, "IX_Signatures_VerificationDocumentId");

            entity.HasOne(d => d.VerificationDocument).WithMany(p => p.Signatures)
                .HasForeignKey(d => d.VerificationDocumentId)
                .HasConstraintName("FK__Signature__Verif__6E01572D");
        });

        modelBuilder.Entity<Submission>(entity =>
        {
            entity.HasKey(e => e.SubmissionId).HasName("PK__Submissi__449EE1252B81A220");

            entity.HasIndex(e => e.RequestGuid, "UQ__Submissi__27CC2CAAE43016F1").IsUnique();

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
                .HasConstraintName("FK__Submissio__Group__3C69FB99");
        });

        modelBuilder.Entity<VerificationDocument>(entity =>
        {
            entity.HasKey(e => e.VerificationDocumentId).HasName("PK__Verifica__D3A0DFE7E4D1551E");

            entity.HasIndex(e => e.BatchId, "IX_VerificationDocuments_BatchId");

            entity.HasIndex(e => e.DocumentClassId, "IX_VerificationDocuments_DocumentClassId");

            entity.Property(e => e.Name).HasMaxLength(255);

            entity.HasOne(d => d.Batch).WithMany(p => p.VerificationDocuments)
                .HasForeignKey(d => d.BatchId)
                .HasConstraintName("FK__Verificat__Batch__5DCAEF64");

            entity.HasOne(d => d.DocumentClass).WithMany(p => p.VerificationDocuments)
                .HasForeignKey(d => d.DocumentClassId)
                .HasConstraintName("FK__Verificat__Docum__70DDC3D8");
        });

        modelBuilder.Entity<VerificationDocumentClass>(entity =>
        {
            entity.HasKey(e => e.DocumentClassId).HasName("PK__Verifica__96CD938B949B619E");

            entity.Property(e => e.Name).HasMaxLength(255);
        });

        modelBuilder.Entity<VerificationResponse>(entity =>
        {
            entity.HasKey(e => e.VerificationResponseId).HasName("PK__Verifica__ABD28C681F5C5429");

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
                .HasConstraintName("FK__Verificat__Batch__6EF57B66");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
