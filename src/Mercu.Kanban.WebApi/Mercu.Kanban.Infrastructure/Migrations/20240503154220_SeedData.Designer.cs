﻿// <auto-generated />
using System;
using Mercu.Kanban.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Mercu.Kanban.Infrastructure.Migrations
{
    [DbContext(typeof(DatabaseContext))]
    [Migration("20240503154220_SeedData")]
    partial class SeedData
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.28")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("Mercu.Kanban.Domain.Entities.Candidate", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("CandidateStatus")
                        .IsRequired()
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(100)
                        .HasColumnType("varchar(100)")
                        .HasDefaultValue("Applied");

                    b.Property<DateTime>("CreateDate")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime(6)")
                        .HasDefaultValueSql("CURRENT_TIMESTAMP(6)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("varchar(255)");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("varchar(255)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("varchar(255)");

                    b.Property<string>("PhoneNumber")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("varchar(255)");

                    b.HasKey("Id");

                    b.ToTable("Candidates");
                });

            modelBuilder.Entity("Mercu.Kanban.Domain.Entities.CandidateInterviewerRelation", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("CandidateId")
                        .HasColumnType("int");

                    b.Property<int>("InterviewerId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("CandidateId");

                    b.HasIndex("InterviewerId");

                    b.ToTable("CandidateInterviewerRelations");
                });

            modelBuilder.Entity("Mercu.Kanban.Domain.Entities.CandidateJobRelation", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("CandidateId")
                        .HasColumnType("int");

                    b.Property<int>("JobId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("CandidateId");

                    b.HasIndex("JobId");

                    b.ToTable("CandidateJobRelations");
                });

            modelBuilder.Entity("Mercu.Kanban.Domain.Entities.Interviewer", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(256)
                        .HasColumnType("varchar(256)");

                    b.HasKey("Id");

                    b.ToTable("Interviewers");
                });

            modelBuilder.Entity("Mercu.Kanban.Domain.Entities.Job", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("JobDescription")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("JobTitle")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("varchar(255)");

                    b.HasKey("Id");

                    b.ToTable("Jobs");
                });

            modelBuilder.Entity("Mercu.Kanban.Domain.Entities.CandidateInterviewerRelation", b =>
                {
                    b.HasOne("Mercu.Kanban.Domain.Entities.Candidate", "Candidate")
                        .WithMany("CandidateInterviewerRelations")
                        .HasForeignKey("CandidateId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Mercu.Kanban.Domain.Entities.Interviewer", "Interviewer")
                        .WithMany("CandidateInterviewerRelations")
                        .HasForeignKey("InterviewerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Candidate");

                    b.Navigation("Interviewer");
                });

            modelBuilder.Entity("Mercu.Kanban.Domain.Entities.CandidateJobRelation", b =>
                {
                    b.HasOne("Mercu.Kanban.Domain.Entities.Candidate", "Candidate")
                        .WithMany("CandidateJobRelations")
                        .HasForeignKey("CandidateId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Mercu.Kanban.Domain.Entities.Job", "Job")
                        .WithMany("CandidateJobRelations")
                        .HasForeignKey("JobId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Candidate");

                    b.Navigation("Job");
                });

            modelBuilder.Entity("Mercu.Kanban.Domain.Entities.Candidate", b =>
                {
                    b.Navigation("CandidateInterviewerRelations");

                    b.Navigation("CandidateJobRelations");
                });

            modelBuilder.Entity("Mercu.Kanban.Domain.Entities.Interviewer", b =>
                {
                    b.Navigation("CandidateInterviewerRelations");
                });

            modelBuilder.Entity("Mercu.Kanban.Domain.Entities.Job", b =>
                {
                    b.Navigation("CandidateJobRelations");
                });
#pragma warning restore 612, 618
        }
    }
}