﻿// <auto-generated />
using System;
using MVCApplicationToDo.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace MVCApplicationToDo.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20250222010517_UpdateTaskClass")]
    partial class UpdateTaskClass
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.1")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("MVCApplicationToDo.Models.Milestone", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Code")
                        .IsRequired()
                        .HasMaxLength(10)
                        .HasColumnType("nvarchar(10)");

                    b.Property<int>("MilestoneChainId")
                        .HasColumnType("int");

                    b.Property<int>("Order")
                        .HasColumnType("int");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<decimal>("Value")
                        .HasColumnType("decimal(18, 2)");

                    b.HasKey("Id");

                    b.HasIndex("MilestoneChainId");

                    b.ToTable("Milestones");
                });

            modelBuilder.Entity("MVCApplicationToDo.Models.MilestoneChain", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Code")
                        .IsRequired()
                        .HasMaxLength(5)
                        .HasColumnType("nvarchar(5)");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.HasKey("Id");

                    b.ToTable("MilestoneChains");
                });

            modelBuilder.Entity("MVCApplicationToDo.Models.Progress", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime?>("ActualDate")
                        .HasColumnType("datetime2");

                    b.Property<decimal>("Earned")
                        .HasColumnType("decimal(18, 2)");

                    b.Property<DateTime?>("ForecastDate")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsCompleted")
                        .HasColumnType("bit");

                    b.Property<int>("MilestoneId")
                        .HasColumnType("int");

                    b.Property<DateTime?>("PlannedDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("ProjectId")
                        .HasColumnType("int");

                    b.Property<int>("TaskItemId")
                        .HasColumnType("int");

                    b.Property<decimal>("Weight")
                        .HasColumnType("decimal(18, 2)");

                    b.HasKey("Id");

                    b.HasIndex("MilestoneId");

                    b.HasIndex("ProjectId");

                    b.HasIndex("TaskItemId");

                    b.ToTable("Progresses");
                });

            modelBuilder.Entity("MVCApplicationToDo.Models.Project", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Code")
                        .IsRequired()
                        .HasMaxLength(10)
                        .HasColumnType("nvarchar(10)");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Projects");
                });

            modelBuilder.Entity("MVCApplicationToDo.Models.TaskItem", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<bool>("IsCompleted")
                        .HasColumnType("bit");

                    b.Property<int>("MilestoneChainId")
                        .HasColumnType("int");

                    b.Property<int>("ProjectId")
                        .HasColumnType("int");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<decimal>("Weight")
                        .HasColumnType("decimal(18, 2)");

                    b.HasKey("Id");

                    b.HasIndex("MilestoneChainId");

                    b.HasIndex("ProjectId");

                    b.ToTable("TaskItems");
                });

            modelBuilder.Entity("MVCApplicationToDo.Models.Milestone", b =>
                {
                    b.HasOne("MVCApplicationToDo.Models.MilestoneChain", "MilestoneChain")
                        .WithMany("Milestones")
                        .HasForeignKey("MilestoneChainId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("MilestoneChain");
                });

            modelBuilder.Entity("MVCApplicationToDo.Models.Progress", b =>
                {
                    b.HasOne("MVCApplicationToDo.Models.Milestone", "Milestone")
                        .WithMany()
                        .HasForeignKey("MilestoneId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("MVCApplicationToDo.Models.Project", "Project")
                        .WithMany("Progresses")
                        .HasForeignKey("ProjectId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("MVCApplicationToDo.Models.TaskItem", "TaskItem")
                        .WithMany("Progresses")
                        .HasForeignKey("TaskItemId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Milestone");

                    b.Navigation("Project");

                    b.Navigation("TaskItem");
                });

            modelBuilder.Entity("MVCApplicationToDo.Models.TaskItem", b =>
                {
                    b.HasOne("MVCApplicationToDo.Models.MilestoneChain", "MilestoneChain")
                        .WithMany("TaskItems")
                        .HasForeignKey("MilestoneChainId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("MVCApplicationToDo.Models.Project", "Project")
                        .WithMany("TaskItems")
                        .HasForeignKey("ProjectId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("MilestoneChain");

                    b.Navigation("Project");
                });

            modelBuilder.Entity("MVCApplicationToDo.Models.MilestoneChain", b =>
                {
                    b.Navigation("Milestones");

                    b.Navigation("TaskItems");
                });

            modelBuilder.Entity("MVCApplicationToDo.Models.Project", b =>
                {
                    b.Navigation("Progresses");

                    b.Navigation("TaskItems");
                });

            modelBuilder.Entity("MVCApplicationToDo.Models.TaskItem", b =>
                {
                    b.Navigation("Progresses");
                });
#pragma warning restore 612, 618
        }
    }
}
