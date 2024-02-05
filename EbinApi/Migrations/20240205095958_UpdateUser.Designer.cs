﻿// <auto-generated />
using System;
using EbinApi.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace EbinApi.Migrations
{
    [DbContext(typeof(EbinContext))]
    [Migration("20240205095958_UpdateUser")]
    partial class UpdateUser
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.1")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("AppCompany", b =>
                {
                    b.Property<long>("AppsId")
                        .HasColumnType("bigint");

                    b.Property<long>("CompaniesId")
                        .HasColumnType("bigint");

                    b.HasKey("AppsId", "CompaniesId");

                    b.HasIndex("CompaniesId");

                    b.ToTable("AppCompany");
                });

            modelBuilder.Entity("AppUser", b =>
                {
                    b.Property<long>("AppsId")
                        .HasColumnType("bigint");

                    b.Property<long>("UsersId")
                        .HasColumnType("bigint");

                    b.HasKey("AppsId", "UsersId");

                    b.HasIndex("UsersId");

                    b.ToTable("AppUser");
                });

            modelBuilder.Entity("EbinApi.Models.Db.Account", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<bool>("DarkTheme")
                        .HasColumnType("boolean");

                    b.Property<bool>("PushInstall")
                        .HasColumnType("boolean");

                    b.Property<bool>("PushUpdate")
                        .HasColumnType("boolean");

                    b.Property<long>("UserId")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("UserId")
                        .IsUnique();

                    b.ToTable("Accounts");
                });

            modelBuilder.Entity("EbinApi.Models.Db.App", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<string>("Description")
                        .HasColumnType("varchar(500)")
                        .HasColumnName("description");

                    b.Property<string>("Developer")
                        .IsRequired()
                        .HasColumnType("varchar(50)")
                        .HasColumnName("developer");

                    b.Property<string>("Icon")
                        .HasColumnType("varchar(100)")
                        .HasColumnName("icon");

                    b.Property<string>("Images")
                        .HasColumnType("varchar(500)")
                        .HasColumnName("images");

                    b.Property<float?>("MinAndroid")
                        .HasColumnType("real");

                    b.Property<byte?>("MinIos")
                        .HasColumnType("smallint");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("varchar(100)")
                        .HasColumnName("name");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasColumnType("varchar(70)")
                        .HasColumnName("status");

                    b.HasKey("Id");

                    b.ToTable("Apps");
                });

            modelBuilder.Entity("EbinApi.Models.Db.AuthCode", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<string>("Code")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("code");

                    b.Property<string>("Phone")
                        .IsRequired()
                        .HasColumnType("varchar(10)")
                        .HasColumnName("phone");

                    b.HasKey("Id");

                    b.ToTable("Auth_codes");
                });

            modelBuilder.Entity("EbinApi.Models.Db.Company", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("varchar(100)")
                        .HasColumnName("name");

                    b.HasKey("Id");

                    b.ToTable("Companies");
                });

            modelBuilder.Entity("EbinApi.Models.Db.Review", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<long>("AppId")
                        .HasColumnType("bigint");

                    b.Property<long>("Date")
                        .HasColumnType("bigint")
                        .HasColumnName("date");

                    b.Property<string>("Description")
                        .HasColumnType("varchar(500)")
                        .HasColumnName("description");

                    b.Property<bool>("IsViewed")
                        .HasColumnType("boolean");

                    b.Property<byte>("Rating")
                        .HasColumnType("smallint")
                        .HasColumnName("rating");

                    b.Property<long>("UserId")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("AppId");

                    b.HasIndex("UserId");

                    b.ToTable("Reviews");
                });

            modelBuilder.Entity("EbinApi.Models.Db.Update", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<long>("AppId")
                        .HasColumnType("bigint");

                    b.Property<long>("Date")
                        .HasColumnType("bigint")
                        .HasColumnName("date");

                    b.Property<string>("Description")
                        .HasColumnType("varchar(500)")
                        .HasColumnName("description");

                    b.Property<string>("FilePath")
                        .HasColumnType("varchar(100)")
                        .HasColumnName("file_path");

                    b.Property<string>("TestFlight")
                        .HasColumnType("varchar(150)")
                        .HasColumnName("testflight");

                    b.Property<string>("Version")
                        .IsRequired()
                        .HasColumnType("varchar(20)")
                        .HasColumnName("version");

                    b.HasKey("Id");

                    b.HasIndex("AppId");

                    b.ToTable("Updates");
                });

            modelBuilder.Entity("EbinApi.Models.Db.User", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<long>("CompanyId")
                        .HasColumnType("bigint");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("varchar(30)")
                        .HasColumnName("last_name");

                    b.Property<string>("MiddleName")
                        .HasColumnType("varchar(50)")
                        .HasColumnName("middle_name");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("varchar(30)")
                        .HasColumnName("name");

                    b.Property<string>("Phone")
                        .IsRequired()
                        .HasColumnType("varchar(10)")
                        .HasColumnName("phone");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasColumnType("varchar(70)")
                        .HasColumnName("status");

                    b.HasKey("Id");

                    b.HasIndex("CompanyId");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("EbinApi.Models.Db.UserApp", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<long>("AppId")
                        .HasColumnType("bigint");

                    b.Property<string>("AppVersion")
                        .IsRequired()
                        .HasColumnType("varchar(20)")
                        .HasColumnName("app_version");

                    b.Property<long>("UserId")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("AppId");

                    b.HasIndex("UserId");

                    b.ToTable("User_apps");
                });

            modelBuilder.Entity("AppCompany", b =>
                {
                    b.HasOne("EbinApi.Models.Db.App", null)
                        .WithMany()
                        .HasForeignKey("AppsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("EbinApi.Models.Db.Company", null)
                        .WithMany()
                        .HasForeignKey("CompaniesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("AppUser", b =>
                {
                    b.HasOne("EbinApi.Models.Db.App", null)
                        .WithMany()
                        .HasForeignKey("AppsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("EbinApi.Models.Db.User", null)
                        .WithMany()
                        .HasForeignKey("UsersId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("EbinApi.Models.Db.Account", b =>
                {
                    b.HasOne("EbinApi.Models.Db.User", "User")
                        .WithOne("Account")
                        .HasForeignKey("EbinApi.Models.Db.Account", "UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("EbinApi.Models.Db.Review", b =>
                {
                    b.HasOne("EbinApi.Models.Db.App", "App")
                        .WithMany("Reviews")
                        .HasForeignKey("AppId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("EbinApi.Models.Db.User", "User")
                        .WithMany("Reviews")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("App");

                    b.Navigation("User");
                });

            modelBuilder.Entity("EbinApi.Models.Db.Update", b =>
                {
                    b.HasOne("EbinApi.Models.Db.App", "App")
                        .WithMany("Updates")
                        .HasForeignKey("AppId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("App");
                });

            modelBuilder.Entity("EbinApi.Models.Db.User", b =>
                {
                    b.HasOne("EbinApi.Models.Db.Company", "Company")
                        .WithMany("Users")
                        .HasForeignKey("CompanyId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Company");
                });

            modelBuilder.Entity("EbinApi.Models.Db.UserApp", b =>
                {
                    b.HasOne("EbinApi.Models.Db.App", "App")
                        .WithMany("UserApps")
                        .HasForeignKey("AppId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("EbinApi.Models.Db.User", "User")
                        .WithMany("UserApps")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("App");

                    b.Navigation("User");
                });

            modelBuilder.Entity("EbinApi.Models.Db.App", b =>
                {
                    b.Navigation("Reviews");

                    b.Navigation("Updates");

                    b.Navigation("UserApps");
                });

            modelBuilder.Entity("EbinApi.Models.Db.Company", b =>
                {
                    b.Navigation("Users");
                });

            modelBuilder.Entity("EbinApi.Models.Db.User", b =>
                {
                    b.Navigation("Account");

                    b.Navigation("Reviews");

                    b.Navigation("UserApps");
                });
#pragma warning restore 612, 618
        }
    }
}
