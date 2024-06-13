﻿using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using VintageTimepieceModel.Models;

namespace VintageTimepieceModel;

public partial class VintagedbContext : DbContext
{
    public VintagedbContext(DbContextOptions<VintagedbContext> options) : base(options)
    {
    }

    public DbSet<Category> Categories { get; set; }

    public DbSet<Evaluation> Evaluations { get; set; }

    public DbSet<FeedbacksTimepiece> FeedbacksTimepieces { get; set; }

    public DbSet<FeedbacksUser> FeedbacksUsers { get; set; }

    public DbSet<Order> Orders { get; set; }

    public DbSet<OrdersDetail> OrdersDetails { get; set; }

    public DbSet<RatingsTimepiece> RatingsTimepieces { get; set; }

    public DbSet<Role> Roles { get; set; }

    public DbSet<SupportTicket> SupportTickets { get; set; }

    public DbSet<Timepiece> Timepieces { get; set; }

    public DbSet<TimepieceCategory> TimepieceCategories { get; set; }

    public DbSet<TimepieceEvaluation> TimepieceEvaluations { get; set; }

    public DbSet<TimepieceImage> TimepieceImages { get; set; }

    public DbSet<User> Users { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Category>(entity =>
        {
            entity.HasKey(e => e.CategoryId).HasName("PK__categori__23CAF1D80E1E54EC");

            entity.ToTable("categories");

            entity.Property(e => e.CategoryId).HasColumnName("categoryId");
            entity.Property(e => e.CategoryName)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("categoryName");
            entity.Property(e => e.IsDel)
                .HasDefaultValue(false)
                .HasColumnName("isDel");
        });

        modelBuilder.Entity<Evaluation>(entity =>
        {
            entity.HasKey(e => e.EvaluationId).HasName("PK__evaluati__053C90BB21987B13");

            entity.ToTable("evaluation");

            entity.Property(e => e.EvaluationId).HasColumnName("evaluationId");
            entity.Property(e => e.Comments)
                .HasColumnType("text")
                .HasColumnName("comments");
            entity.Property(e => e.EvaluatorId).HasColumnName("evaluatorId");
            entity.Property(e => e.IsDel)
                .HasDefaultValue(false)
                .HasColumnName("isDel");
            entity.Property(e => e.ValueExtimated)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("valueExtimated");

            entity.HasOne(d => d.Evaluator).WithMany(p => p.Evaluations)
                .HasForeignKey(d => d.EvaluatorId)
                .HasConstraintName("FK_UserEvaluation");
        });

        modelBuilder.Entity<FeedbacksTimepiece>(entity =>
        {
            entity.HasKey(e => e.FeedbackId).HasName("PK__feedback__2613FD245670C714");

            entity.ToTable("feedbacks_timepiece");

            entity.Property(e => e.FeedbackId).HasColumnName("feedbackId");
            entity.Property(e => e.Comment)
                .HasMaxLength(300)
                .HasColumnName("comment");
            entity.Property(e => e.FeedbackDate)
                .HasColumnType("datetime")
                .HasColumnName("feedbackDate");
            entity.Property(e => e.IsDel)
                .HasDefaultValue(false)
                .HasColumnName("isDel");
            entity.Property(e => e.TimepieceId).HasColumnName("timepieceId");
            entity.Property(e => e.UserId).HasColumnName("userId");

            entity.HasOne(d => d.Timepiece).WithMany(p => p.FeedbacksTimepieces)
                .HasForeignKey(d => d.TimepieceId)
                .HasConstraintName("FK_UserFeebackTimepiece");

            entity.HasOne(d => d.User).WithMany(p => p.FeedbacksTimepieces)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK_FeedbackTimepiece");
        });

        modelBuilder.Entity<FeedbacksUser>(entity =>
        {
            entity.HasKey(e => e.FeedbackUsersId).HasName("PK__feedback__5587998142CF5907");

            entity.ToTable("feedbacks_users");

            entity.Property(e => e.FeedbackUsersId).HasColumnName("feedbackUsersId");
            entity.Property(e => e.Comment)
                .HasMaxLength(300)
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
            entity.HasKey(e => e.OrderId).HasName("PK__orders__0809335DDCC96CFF");

            entity.ToTable("orders");

            entity.Property(e => e.OrderId).HasColumnName("orderId");
            entity.Property(e => e.IsDel)
                .HasDefaultValue(false)
                .HasColumnName("isDel");
            entity.Property(e => e.OrderDate)
                .HasColumnType("datetime")
                .HasColumnName("orderDate");
            entity.Property(e => e.TotalPrice)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("totalPrice");
            entity.Property(e => e.UserId).HasColumnName("userId");

            entity.HasOne(d => d.User).WithMany(p => p.Orders)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK_OrderUser");
        });

        modelBuilder.Entity<OrdersDetail>(entity =>
        {
            entity.HasKey(e => e.OrderDetailId).HasName("PK__orders_d__E4FEDE4A02432553");

            entity.ToTable("orders_detail");

            entity.Property(e => e.OrderDetailId).HasColumnName("orderDetailId");
            entity.Property(e => e.IsDel)
                .HasDefaultValue(false)
                .HasColumnName("isDel");
            entity.Property(e => e.OrderId).HasColumnName("orderId");
            entity.Property(e => e.Quantity).HasColumnName("quantity");
            entity.Property(e => e.TimepieceId).HasColumnName("timepieceId");
            entity.Property(e => e.UnitPrice)
                .HasColumnType("decimal(10, 2)")
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
            entity.HasKey(e => e.RatingId).HasName("PK__ratings___2D290CA962A3F0F0");

            entity.ToTable("ratings_timepiece");

            entity.Property(e => e.RatingId).HasColumnName("ratingId");
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
            entity.HasKey(e => e.RoleId).HasName("PK__roles__CD98462A6C4A3C26");

            entity.ToTable("roles");

            entity.Property(e => e.RoleId).HasColumnName("roleId");
            entity.Property(e => e.IsDel)
                .HasDefaultValue(false)
                .HasColumnName("isDel");
            entity.Property(e => e.RoleName)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("roleName");
        });

        modelBuilder.Entity<SupportTicket>(entity =>
        {
            entity.HasKey(e => e.TicketId).HasName("PK__support___3333C61023486F1E");

            entity.ToTable("support_ticket");

            entity.Property(e => e.TicketId).HasColumnName("ticketId");
            entity.Property(e => e.CreatedDate)
                .HasColumnType("datetime")
                .HasColumnName("createdDate");
            entity.Property(e => e.Desciption)
                .HasMaxLength(300)
                .HasColumnName("desciption");
            entity.Property(e => e.IsDel)
                .HasDefaultValue(false)
                .HasColumnName("isDel");
            entity.Property(e => e.IsResovle).HasColumnName("isResovle");
            entity.Property(e => e.ResovleDate)
                .HasColumnType("datetime")
                .HasColumnName("resovleDate");
            entity.Property(e => e.Status)
                .HasMaxLength(100)
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
            entity.HasKey(e => e.TimepieceId).HasName("PK__timepiec__A78C7F230EDED731");

            entity.ToTable("timepieces");

            entity.Property(e => e.TimepieceId).HasColumnName("timepieceId");
            entity.Property(e => e.Brand)
                .HasMaxLength(100)
                .HasColumnName("brand");
            entity.Property(e => e.DatePost)
                .HasColumnType("datetime")
                .HasColumnName("datePost");
            entity.Property(e => e.Description)
                .HasMaxLength(300)
                .HasColumnName("description");
            entity.Property(e => e.IsDel)
                .HasDefaultValue(false)
                .HasColumnName("isDel");
            entity.Property(e => e.Model)
                .HasMaxLength(100)
                .HasColumnName("model");
            entity.Property(e => e.Price)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("price");
            entity.Property(e => e.TimepieceName)
                .HasMaxLength(100)
                .HasColumnName("timepieceName");
            entity.Property(e => e.UserId).HasColumnName("userId");

            entity.HasOne(d => d.User).WithMany(p => p.Timepieces)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK_UserTimepiece");
        });

        modelBuilder.Entity<TimepieceCategory>(entity =>
        {
            entity.HasKey(e => e.TimepieceCategoryId).HasName("PK__timepiec__BD9500C998B3A18A");

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
            entity.HasKey(e => e.TimepieceEvaluationId).HasName("PK__timepiec__C49F7B36AFFB746B");

            entity.ToTable("timepiece_evaluation");

            entity.Property(e => e.TimepieceEvaluationId).HasColumnName("timepieceEvaluationId");
            entity.Property(e => e.Condition)
                .HasMaxLength(200)
                .HasColumnName("condition");
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
            entity.HasKey(e => e.TimepieceImageId).HasName("PK__timepiec__624BDFFF71D44540");

            entity.ToTable("timepiece_image");

            entity.Property(e => e.TimepieceImageId).HasColumnName("timepieceImageId");
            entity.Property(e => e.ImageUrl)
                .IsUnicode(false)
                .HasColumnName("imageUrl");
            entity.Property(e => e.IsDel)
                .HasDefaultValue(false)
                .HasColumnName("isDel");
            entity.Property(e => e.TimpieceId).HasColumnName("timpieceId");

            entity.HasOne(d => d.Timpiece).WithMany(p => p.TimepieceImages)
                .HasForeignKey(d => d.TimpieceId)
                .HasConstraintName("FK_timepiece_image_timepieces");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("PK__users__CB9A1CFFDC5DCCE1");

            entity.ToTable("users");

            entity.Property(e => e.UserId).HasColumnName("userId");
            entity.Property(e => e.Address)
                .HasMaxLength(100)
                .HasColumnName("address");
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
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("password");
            entity.Property(e => e.PhoneNumber)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("phoneNumber");
            entity.Property(e => e.RoleId).HasColumnName("roleId");

            entity.HasOne(d => d.Role).WithMany(p => p.Users)
                .HasForeignKey(d => d.RoleId)
                .HasConstraintName("FK_UserRole");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
