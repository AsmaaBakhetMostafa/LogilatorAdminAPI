﻿// <auto-generated />
using System;
using BiddingEngineAPI.EFCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace BiddingEngineAPI.EFCore.Migrations
{
    [DbContext(typeof(DatabaseContext))]
    [Migration("20200308153933_beww")]
    partial class beww
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("BiddingEngineAPI.EFCore.Model.Bid", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("CreateDate")
                        .HasColumnType("datetime2");

                    b.Property<int?>("FormId")
                        .HasColumnType("int");

                    b.Property<bool?>("IsApproved")
                        .HasColumnType("bit");

                    b.Property<int>("RequestId")
                        .HasColumnType("int");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.Property<int>("UserTypeId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("FormId");

                    b.HasIndex("RequestId");

                    b.ToTable("Bids");
                });

            modelBuilder.Entity("BiddingEngineAPI.EFCore.Model.BidDetail", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("BidId")
                        .HasColumnType("int");

                    b.Property<int?>("FormFieldId")
                        .HasColumnType("int");

                    b.Property<string>("Value")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("BidId");

                    b.HasIndex("FormFieldId");

                    b.ToTable("BidDetails");
                });

            modelBuilder.Entity("BiddingEngineAPI.EFCore.Model.FieldOption", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("CreateDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("FormFiledId")
                        .HasColumnType("int");

                    b.Property<bool>("HasChild")
                        .HasColumnType("bit");

                    b.Property<bool>("HasParent")
                        .HasColumnType("bit");

                    b.Property<bool?>("IsActive")
                        .HasColumnType("bit");

                    b.Property<string>("NameAr")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NameEn")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("ParentId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("FormFiledId");

                    b.HasIndex("ParentId");

                    b.ToTable("FiledOptions");
                });

            modelBuilder.Entity("BiddingEngineAPI.EFCore.Model.FixedFiled", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<string>("NameAr")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NameEn")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("FixedFileds");
                });

            modelBuilder.Entity("BiddingEngineAPI.EFCore.Model.FixedFiledDetails", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("NameAr")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NameEn")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("ParentId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ParentId");

                    b.ToTable("FixedFiledDetails");
                });

            modelBuilder.Entity("BiddingEngineAPI.EFCore.Model.Form", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("CreateDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("FormType")
                        .HasColumnType("int");

                    b.Property<bool?>("IsActive")
                        .HasColumnType("bit");

                    b.Property<string>("NameAr")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NameEn")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("RequestType")
                        .HasColumnType("int");

                    b.Property<int>("UserType")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Forms");
                });

            modelBuilder.Entity("BiddingEngineAPI.EFCore.Model.FormField", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("CreateDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("FiledType")
                        .HasColumnType("int");

                    b.Property<int>("FormId")
                        .HasColumnType("int");

                    b.Property<bool>("HasChild")
                        .HasColumnType("bit");

                    b.Property<bool>("HasParent")
                        .HasColumnType("bit");

                    b.Property<bool?>("HasPredefined")
                        .HasColumnType("bit");

                    b.Property<bool?>("IsActive")
                        .HasColumnType("bit");

                    b.Property<bool>("IsRequired")
                        .HasColumnType("bit");

                    b.Property<string>("NameAr")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NameEn")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Order")
                        .HasColumnType("int");

                    b.Property<int?>("ParentId")
                        .HasColumnType("int");

                    b.Property<int?>("PredfinedID")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("FormId");

                    b.HasIndex("ParentId");

                    b.ToTable("FormFileds");
                });

            modelBuilder.Entity("BiddingEngineAPI.EFCore.Model.Request", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("CreateDate")
                        .HasColumnType("datetime2");

                    b.Property<int?>("FormId")
                        .HasColumnType("int");

                    b.Property<int?>("StatusId")
                        .HasColumnType("int");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("FormId");

                    b.HasIndex("StatusId");

                    b.ToTable("Requests");
                });

            modelBuilder.Entity("BiddingEngineAPI.EFCore.Model.RequestDetail", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("FormFieldId")
                        .HasColumnType("int");

                    b.Property<int>("RequestId")
                        .HasColumnType("int");

                    b.Property<string>("Value")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("FormFieldId");

                    b.HasIndex("RequestId");

                    b.ToTable("RequestDetails");
                });

            modelBuilder.Entity("BiddingEngineAPI.EFCore.Model.RequestType", b =>
                {
                    b.Property<int>("RequestType_Id")
                        .HasColumnType("int");

                    b.Property<string>("RequestType_Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("RequestType_Id");

                    b.ToTable("RequestTypes");
                });

            modelBuilder.Entity("BiddingEngineAPI.EFCore.Model.Statu", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Status");
                });

            modelBuilder.Entity("BiddingEngineAPI.EFCore.Model.User", b =>
                {
                    b.Property<int>("User_Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ActivationCode")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool?>("IsActive")
                        .HasColumnType("bit");

                    b.Property<bool>("Is_Deleted")
                        .HasColumnType("bit");

                    b.Property<bool>("Is_Online")
                        .HasColumnType("bit");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("RoleID")
                        .HasColumnType("int");

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("UserType_Id")
                        .HasColumnType("int");

                    b.HasKey("User_Id");

                    b.HasIndex("UserType_Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("BiddingEngineAPI.EFCore.Model.UserType", b =>
                {
                    b.Property<int>("UserType_Id")
                        .HasColumnType("int");

                    b.Property<string>("UserType_Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserType_Id");

                    b.ToTable("UserTypes");
                });

            modelBuilder.Entity("BiddingEngineAPI.EFCore.Model.Bid", b =>
                {
                    b.HasOne("BiddingEngineAPI.EFCore.Model.Form", "Form")
                        .WithMany()
                        .HasForeignKey("FormId");

                    b.HasOne("BiddingEngineAPI.EFCore.Model.Request", "Request")
                        .WithMany("Bids")
                        .HasForeignKey("RequestId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("BiddingEngineAPI.EFCore.Model.BidDetail", b =>
                {
                    b.HasOne("BiddingEngineAPI.EFCore.Model.Bid", "Bid")
                        .WithMany("BidDetails")
                        .HasForeignKey("BidId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("BiddingEngineAPI.EFCore.Model.FormField", "FormField")
                        .WithMany()
                        .HasForeignKey("FormFieldId");
                });

            modelBuilder.Entity("BiddingEngineAPI.EFCore.Model.FieldOption", b =>
                {
                    b.HasOne("BiddingEngineAPI.EFCore.Model.FormField", "FormFiled")
                        .WithMany("FiledOptions")
                        .HasForeignKey("FormFiledId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("BiddingEngineAPI.EFCore.Model.FieldOption", "Parent")
                        .WithMany()
                        .HasForeignKey("ParentId");
                });

            modelBuilder.Entity("BiddingEngineAPI.EFCore.Model.FixedFiledDetails", b =>
                {
                    b.HasOne("BiddingEngineAPI.EFCore.Model.FixedFiledDetails", "Parent")
                        .WithMany()
                        .HasForeignKey("ParentId");
                });

            modelBuilder.Entity("BiddingEngineAPI.EFCore.Model.FormField", b =>
                {
                    b.HasOne("BiddingEngineAPI.EFCore.Model.Form", "Form")
                        .WithMany("Fields")
                        .HasForeignKey("FormId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("BiddingEngineAPI.EFCore.Model.FormField", "Parent")
                        .WithMany()
                        .HasForeignKey("ParentId");
                });

            modelBuilder.Entity("BiddingEngineAPI.EFCore.Model.Request", b =>
                {
                    b.HasOne("BiddingEngineAPI.EFCore.Model.Form", "Form")
                        .WithMany()
                        .HasForeignKey("FormId");

                    b.HasOne("BiddingEngineAPI.EFCore.Model.Statu", "Status")
                        .WithMany("Requests")
                        .HasForeignKey("StatusId");
                });

            modelBuilder.Entity("BiddingEngineAPI.EFCore.Model.RequestDetail", b =>
                {
                    b.HasOne("BiddingEngineAPI.EFCore.Model.FormField", "FormField")
                        .WithMany()
                        .HasForeignKey("FormFieldId");

                    b.HasOne("BiddingEngineAPI.EFCore.Model.Request", "Request")
                        .WithMany("RequestDetails")
                        .HasForeignKey("RequestId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("BiddingEngineAPI.EFCore.Model.User", b =>
                {
                    b.HasOne("BiddingEngineAPI.EFCore.Model.UserType", "UserType")
                        .WithMany()
                        .HasForeignKey("UserType_Id");
                });
#pragma warning restore 612, 618
        }
    }
}
