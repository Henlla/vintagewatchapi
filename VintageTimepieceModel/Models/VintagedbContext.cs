using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace VintageTimepieceModel.Models;

public partial class VintagedbContext : DbContext
{
    public VintagedbContext()
    {
    }

    public VintagedbContext(DbContextOptions<VintagedbContext> options)
        : base(options)
    {
    }
    public virtual DbSet<Brand> Brands { get; set; }
    public virtual DbSet<Category> Categories { get; set; }
    public virtual DbSet<Evaluation> Evaluations { get; set; }
    public virtual DbSet<FeedbacksUser> FeedbacksUsers { get; set; }
    public virtual DbSet<Order> Orders { get; set; }
    public virtual DbSet<OrdersDetail> OrdersDetails { get; set; }
    public virtual DbSet<RatingsTimepiece> RatingsTimepieces { get; set; }
    public virtual DbSet<Role> Roles { get; set; }
    public virtual DbSet<SupportTicket> SupportTickets { get; set; }
    public virtual DbSet<Timepiece> Timepieces { get; set; }
    public virtual DbSet<TimepieceCategory> TimepieceCategories { get; set; }
    public virtual DbSet<TimepieceEvaluation> TimepieceEvaluations { get; set; }
    public virtual DbSet<TimepieceImage> TimepieceImages { get; set; }
    public virtual DbSet<Transactions> Transactions { get; set; }
    public virtual DbSet<RefundTransaction> RefundTransactions { get; set; }
    public virtual DbSet<User> Users { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Brand>(entity =>
        {
            entity.HasKey(e => e.BrandId).HasName("PK__brand__06B77299A0CDC1D9");

            entity.ToTable("brand");

            entity.Property(e => e.BrandId).HasColumnName("brandId");
            entity.Property(e => e.BrandDescription)
                .HasMaxLength(200)
                .IsUnicode(false)
                .HasColumnName("brandDescription");
            entity.Property(e => e.BrandName)
                .HasMaxLength(200)
                .IsUnicode(false)
                .HasColumnName("brandName");
            entity.Property(e => e.IsDel).HasColumnName("isDel");
        });

        modelBuilder.Entity<Category>(entity =>
        {
            entity.HasKey(e => e.CategoryId).HasName("PK__categori__23CAF1D8814D4FA1");

            entity.ToTable("categories");

            entity.Property(e => e.CategoryId).HasColumnName("categoryId");
            entity.Property(e => e.CategoryName)
                .HasMaxLength(200)
                .IsUnicode(false)
                .HasColumnName("categoryName");
            entity.Property(e => e.IsDel)
                .HasDefaultValue(false)
                .HasColumnName("isDel");
        });

        modelBuilder.Entity<Evaluation>(entity =>
        {
            entity.HasKey(e => e.EvaluationId).HasName("PK__evaluati__053C90BB24472B39");

            entity.ToTable("evaluation");

            entity.Property(e => e.EvaluationId).HasColumnName("evaluationId");
            entity.Property(e => e.AccuracyStatus)
                .HasMaxLength(200)
                .IsUnicode(false)
                .HasColumnName("accuracyStatus");
            entity.Property(e => e.BraceletStatus)
                .HasMaxLength(200)
                .IsUnicode(false)
                .HasColumnName("braceletStatus");
            entity.Property(e => e.BuckleStatus)
                .HasMaxLength(200)
                .IsUnicode(false)
                .HasColumnName("buckleStatus");
            entity.Property(e => e.CaseDiameterStatus)
                .HasMaxLength(200)
                .IsUnicode(false)
                .HasColumnName("caseDiameterStatus");
            entity.Property(e => e.CaseMaterialStatus)
                .HasMaxLength(200)
                .IsUnicode(false)
                .HasColumnName("caseMaterialStatus");
            entity.Property(e => e.CrystalTypeStatus)
                .HasMaxLength(200)
                .IsUnicode(false)
                .HasColumnName("crystalTypeStatus");
            entity.Property(e => e.DialStatus)
                .HasMaxLength(200)
                .IsUnicode(false)
                .HasColumnName("dialStatus");
            entity.Property(e => e.EvaluatorId).HasColumnName("evaluatorId");
            entity.Property(e => e.HandsStatus)
                .HasMaxLength(200)
                .IsUnicode(false)
                .HasColumnName("handsStatus");
            entity.Property(e => e.IsDel)
                .HasDefaultValue(false)
                .HasColumnName("isDel");
            entity.Property(e => e.MovementStatus)
                .HasMaxLength(200)
                .IsUnicode(false)
                .HasColumnName("movementStatus");
            entity.Property(e => e.ValueExtimatedStatus)
                .HasColumnType("decimal(18, 2)")
                .HasColumnName("valueExtimatedStatus");
            entity.Property(e => e.WaterResistanceStatus)
                .HasMaxLength(200)
                .IsUnicode(false)
                .HasColumnName("waterResistanceStatus");

            entity.HasOne(d => d.Evaluator).WithMany(p => p.Evaluations)
                .HasForeignKey(d => d.EvaluatorId)
                .HasConstraintName("FK_UserEvaluation");
        });

        modelBuilder.Entity<FeedbacksUser>(entity =>
        {
            entity.HasKey(e => e.FeedbackUsersId).HasName("PK__feedback__5587998174A1FE69");

            entity.ToTable("feedbacks_users");

            entity.Property(e => e.FeedbackUsersId).HasColumnName("feedbackUsersId");
            entity.Property(e => e.Comment)
                .HasColumnType("text")
                .HasColumnName("comment");
            entity.Property(e => e.CreatedDate)
                .HasColumnType("datetime")
                .HasColumnName("createdDate");
            entity.Property(e => e.FeedbackTargetId).HasColumnName("feedbackTargetId");
            entity.Property(e => e.IsDel)
                .HasDefaultValue(false)
                .HasColumnName("isDel");
            entity.Property(e => e.RatingStar).HasColumnName("ratingStar");
            entity.Property(e => e.UserId).HasColumnName("userId");

            entity.HasOne(d => d.FeedbackTarget).WithMany(p => p.FeedbacksUserFeedbackTargets)
                .HasForeignKey(d => d.FeedbackTargetId)
                .HasConstraintName("FK_FeedbackTarget");

            entity.HasOne(d => d.User).WithMany(p => p.FeedbacksUserUsers)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK_FeedbackUsers");
        });

        modelBuilder.Entity<Order>(entity =>
        {
            entity.HasKey(e => e.OrderId).HasName("PK__orders__0809335DC4280D50");

            entity.ToTable("orders");

            entity.Property(e => e.OrderId).HasColumnName("orderId");
            entity.Property(e => e.Status)
            .HasMaxLength(50)
            .HasDefaultValue(false)
            .HasColumnName("status");
            entity.Property(e => e.IsDel)
                .HasDefaultValue(false)
                .HasColumnName("isDel");
            entity.Property(e => e.OrderDate)
                .HasColumnType("datetime")
                .HasColumnName("orderDate");
            entity.Property(e => e.TotalPrice)
                .HasColumnType("decimal(18, 2)")
                .HasColumnName("totalPrice");
            entity.Property(e => e.UserId).HasColumnName("userId");

            entity.HasOne(d => d.User).WithMany(p => p.Orders)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK_OrderUser");
        });

        modelBuilder.Entity<OrdersDetail>(entity =>
        {
            entity.HasKey(e => e.OrderDetailId).HasName("PK__orders_d__E4FEDE4AD5CAD8FA");

            entity.ToTable("orders_detail");

            entity.Property(e => e.OrderDetailId).HasColumnName("orderDetailId");
            entity.Property(e => e.IsDel)
                .HasDefaultValue(false)
                .HasColumnName("isDel");
            entity.Property(e => e.OrderId).HasColumnName("orderId");
            entity.Property(e => e.Quantity).HasColumnName("quantity");
            entity.Property(e => e.TimepieceId).HasColumnName("timepieceId");
            entity.Property(e => e.UnitPrice)
                .HasColumnType("decimal(18, 2)")
                .HasColumnName("unitPrice");

            entity.HasOne(d => d.Order).WithMany(p => p.OrdersDetails)
                .HasForeignKey(d => d.OrderId)
                .HasConstraintName("FK_OrderOrderDetail");

            entity.HasOne(d => d.Timepiece).WithMany(p => p.OrdersDetails)
                .HasForeignKey(d => d.TimepieceId)
                .HasConstraintName("FK_OrderTimepieceDetail");
        });

        modelBuilder.Entity<RatingsTimepiece>(entity =>
        {
            entity.HasKey(e => e.RatingId).HasName("PK__ratings___2D290CA997836043");

            entity.ToTable("ratings_timepiece");

            entity.Property(e => e.RatingId).HasColumnName("ratingId");
            entity.Property(e => e.FeedbackContent)
                .HasMaxLength(200)
                .HasColumnName("feedbackContent");
            entity.Property(e => e.IsDel)
                .HasDefaultValue(false)
                .HasColumnName("isDel");
            entity.Property(e => e.RatingDate)
                .HasColumnType("datetime")
                .HasColumnName("ratingDate");
            entity.Property(e => e.RatingStar).HasColumnName("ratingStar");
            entity.Property(e => e.TimepieceId).HasColumnName("timepieceId");
            entity.Property(e => e.UserId).HasColumnName("userId");

            entity.HasOne(d => d.Timepiece).WithMany(p => p.RatingsTimepieces)
                .HasForeignKey(d => d.TimepieceId)
                .HasConstraintName("FK_RatingTimepiece");

            entity.HasOne(d => d.User).WithMany(p => p.RatingsTimepieces)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK_UserRatingTimepiece");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.RoleId).HasName("PK__roles__CD98462A3F599601");

            entity.ToTable("roles");

            entity.Property(e => e.RoleId).HasColumnName("roleId");
            entity.Property(e => e.IsDel)
                .HasDefaultValue(false)
                .HasColumnName("isDel");
            entity.Property(e => e.RoleName)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("roleName");
        });

        modelBuilder.Entity<SupportTicket>(entity =>
        {
            entity.HasKey(e => e.TicketId).HasName("PK__support___3333C610BD8071C6");

            entity.ToTable("support_ticket");

            entity.Property(e => e.TicketId).HasColumnName("ticketId");
            entity.Property(e => e.CreatedDate)
                .HasColumnType("datetime")
                .HasColumnName("createdDate");
            entity.Property(e => e.Desciption)
                .HasColumnType("text")
                .HasColumnName("desciption");
            entity.Property(e => e.IsDel)
                .HasDefaultValue(false)
                .HasColumnName("isDel");
            entity.Property(e => e.IsResovle).HasColumnName("isResovle");
            entity.Property(e => e.ResovleDate)
                .HasColumnType("datetime")
                .HasColumnName("resovleDate");
            entity.Property(e => e.Status)
                .HasMaxLength(200)
                .IsUnicode(false)
                .HasColumnName("status");
            entity.Property(e => e.SupportAgentId).HasColumnName("supportAgentId");
            entity.Property(e => e.UserId).HasColumnName("userId");

            entity.HasOne(d => d.SupportAgent).WithMany(p => p.SupportTicketSupportAgents)
                .HasForeignKey(d => d.SupportAgentId)
                .HasConstraintName("FK_AgentSupport");

            entity.HasOne(d => d.User).WithMany(p => p.SupportTicketUsers)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK_UserSupport");
        });

        modelBuilder.Entity<Timepiece>(entity =>
        {
            entity.HasKey(e => e.TimepieceId).HasName("PK__timepiec__A78C7F237AB3FB9E");

            entity.ToTable("timepieces");

            entity.Property(e => e.TimepieceId).HasColumnName("timepieceId");
            entity.Property(e => e.BrandId).HasColumnName("brandId");
            entity.Property(e => e.CaseDiameter)
                .HasMaxLength(200)
                .HasColumnName("caseDiameter");
            entity.Property(e => e.CaseMaterial)
                .HasMaxLength(200)
                .HasColumnName("caseMaterial");
            entity.Property(e => e.CaseThickness)
                .HasMaxLength(200)
                .HasColumnName("caseThickness");
            entity.Property(e => e.Crystal)
                .HasMaxLength(200)
                .HasColumnName("crystal");
            entity.Property(e => e.DatePost)
                .HasColumnType("datetime")
                .HasColumnName("datePost");
            entity.Property(e => e.Description)
                .HasColumnType("text")
                .HasColumnName("description");
            entity.Property(e => e.IsDel)
                .HasDefaultValue(false)
                .HasColumnName("isDel");
            entity.Property(e => e.IsBuy)
                .HasDefaultValue(false)
                .HasColumnName("isBuy");
            entity.Property(e => e.Movement)
                .HasMaxLength(200)
                .IsUnicode(false)
                .HasColumnName("movement");
            entity.Property(e => e.Price)
                .HasColumnType("decimal(18, 2)")
                .HasColumnName("price");
            entity.Property(e => e.StrapMaterial)
                .HasMaxLength(200)
                .HasColumnName("strapMaterial");
            entity.Property(e => e.StrapWidth)
                .HasMaxLength(200)
                .HasColumnName("strapWidth");
            entity.Property(e => e.Style)
                .HasMaxLength(200)
                .HasColumnName("style");
            entity.Property(e => e.TimepieceName)
                .HasMaxLength(200)
                .IsUnicode(false)
                .HasColumnName("timepieceName");
            entity.Property(e => e.UserId).HasColumnName("userId");
            entity.Property(e => e.WaterResistance)
                .HasMaxLength(200)
                .HasColumnName("waterResistance");

            entity.HasOne(d => d.Brand).WithMany(p => p.Timepieces)
                .HasForeignKey(d => d.BrandId)
                .HasConstraintName("FK_TimepieceBrand");

            entity.HasOne(d => d.User).WithMany(p => p.Timepieces)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK_UserTimepiece");
        });

        modelBuilder.Entity<TimepieceCategory>(entity =>
        {
            entity.HasKey(e => e.TimepieceCategoryId).HasName("PK__timepiec__BD9500C98895CA4D");

            entity.ToTable("timepiece_category");

            entity.Property(e => e.TimepieceCategoryId).HasColumnName("timepieceCategoryId");
            entity.Property(e => e.CategoryId).HasColumnName("categoryId");
            entity.Property(e => e.IsDel)
                .HasDefaultValue(false)
                .HasColumnName("isDel");
            entity.Property(e => e.TimepieceId).HasColumnName("timepieceId");

            entity.HasOne(d => d.Category).WithMany(p => p.TimepieceCategories)
                .HasForeignKey(d => d.CategoryId)
                .HasConstraintName("FK_TimepieceCategory");

            entity.HasOne(d => d.Timepiece).WithMany(p => p.TimepieceCategories)
                .HasForeignKey(d => d.TimepieceId)
                .HasConstraintName("FK_CategoryTimepiece");
        });

        modelBuilder.Entity<TimepieceEvaluation>(entity =>
        {
            entity.HasKey(e => e.TimepieceEvaluationId).HasName("PK__timepiec__C49F7B362D6BC195");

            entity.ToTable("timepiece_evaluation");

            entity.Property(e => e.TimepieceEvaluationId).HasColumnName("timepieceEvaluationId");
            entity.Property(e => e.EvaluationDate)
                .HasColumnType("datetime")
                .HasColumnName("evaluationDate");
            entity.Property(e => e.EvaluationId).HasColumnName("evaluationId");
            entity.Property(e => e.IsDel)
                .HasDefaultValue(false)
                .HasColumnName("isDel");
            entity.Property(e => e.TimepieceId).HasColumnName("timepieceId");

            entity.HasOne(d => d.Evaluation).WithMany(p => p.TimepieceEvaluations)
                .HasForeignKey(d => d.EvaluationId)
                .HasConstraintName("FK_EvaluationTimepieceEvaluation");

            entity.HasOne(d => d.Timepiece).WithMany(p => p.TimepieceEvaluations)
                .HasForeignKey(d => d.TimepieceId)
                .HasConstraintName("FK_TimepieceTimepieceEvaluation");
        });

        modelBuilder.Entity<TimepieceImage>(entity =>
        {
            entity.HasKey(e => e.TimepieceImageId).HasName("PK__timepiec__624BDFFF3402087E");

            entity.ToTable("timepiece_image");

            entity.Property(e => e.TimepieceImageId).HasColumnName("timepieceImageId");
            entity.Property(e => e.ImageName)
                .HasMaxLength(200)
                .HasColumnName("imageName");
            entity.Property(e => e.ImageUrl)
                .IsUnicode(false)
                .HasColumnName("imageUrl");
            entity.Property(e => e.IsDel)
                .HasDefaultValue(false)
                .HasColumnName("isDel");
            entity.Property(e => e.TimepieceId).HasColumnName("timepieceId");

            entity.HasOne(d => d.Timepiece).WithMany(p => p.TimepieceImages)
                .HasForeignKey(d => d.TimepieceId)
                .HasConstraintName("FK_Timepiece_image");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("PK__users__CB9A1CFF74E579DE");

            entity.ToTable("users");

            entity.Property(e => e.UserId).HasColumnName("userId");
            entity.Property(e => e.Address)
                .HasMaxLength(200)
                .IsUnicode(false)
                .HasColumnName("address");
            entity.Property(e => e.Avatar)
                .IsUnicode(false)
                .HasColumnName("avatar");
            entity.Property(e => e.DateJoined)
                .HasColumnType("datetime")
                .HasColumnName("dateJoined");
            entity.Property(e => e.Email)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("email");
            entity.Property(e => e.FirstName)
                .HasMaxLength(100)
                .HasColumnName("firstName");
            entity.Property(e => e.IsDel)
                .HasDefaultValue(false)
                .HasColumnName("isDel");
            entity.Property(e => e.LastName)
                .HasMaxLength(100)
                .HasColumnName("lastName");
            entity.Property(e => e.Password)
                .IsUnicode(false)
                .HasColumnName("password");
            entity.Property(e => e.PhoneNumber)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("phoneNumber");
            entity.Property(e => e.RoleId).HasColumnName("roleId");

            entity.HasOne(d => d.Role)
            .WithMany(p => p.Users)
                .HasForeignKey(d => d.RoleId)
                .HasConstraintName("FK_User_Role");
        });

        modelBuilder.Entity<Transactions>(entity =>
        {
            entity.HasKey(e => e.TransactionId).HasName("PK__transact__9B57CF72DD5ACA9C");

            entity.Property(e => e.TransactionId).HasColumnName("transactionId");
            entity.Property(e => e.OrderId).HasColumnName("orderId");


            entity.Property(e => e.PaymentMethod)
            .HasMaxLength(100)
            .HasColumnName("paymentMethod");

            entity.Property(e => e.TransactionDate)
            .HasColumnType("datetime")
            .HasColumnName("transactionDate");

            entity.Property(e => e.Amount)
            .HasColumnType("decimal(18,2)")
            .HasColumnName("amount");

            entity.Property(e => e.TransactionStatus)
            .HasMaxLength(50)
            .HasColumnName("transactionStatus");

            entity.Property(e => e.BankCode)
            .HasMaxLength(200)
            .HasColumnName("bankCode");

            entity.Property(e => e.Description)
            .HasMaxLength(200)
            .HasColumnName("description");

            entity.Property(e => e.IsDel)
            .HasDefaultValue(false)
            .HasColumnName("isDel");

            entity.HasOne(e => e.Order)
            .WithOne(p => p.Transaction)
            .HasForeignKey<Transactions>(e => e.OrderId);

            entity.HasOne(e => e.RefundTransaction)
            .WithOne(p => p.Transaction)
            .HasForeignKey<Transactions>(e => e.RefundId);

        });

        modelBuilder.Entity<RefundTransaction>(entity =>
        {
            entity.ToTable("refund_transaction");

            entity.HasKey(e => e.RefundId).HasName("PK__refund_t__B219848FCBD1B3AE");
            entity.Property(e => e.RefundId).HasColumnName("refundId");
            entity.Property(e => e.RefundBankCode).HasMaxLength(100).HasColumnName("refundBankCode");
            entity.Property(e => e.RefundAmount).HasColumnType("decimal(18,2)").HasColumnName("refundAmount");
            entity.Property(e => e.RefundType).HasMaxLength(200).HasColumnName("refundType");
            entity.Property(e => e.RefundInfo).HasMaxLength(100).HasColumnName("refundInfo");
            entity.Property(e => e.RefundDate).HasColumnType("datetime").HasColumnName("refundDate");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
