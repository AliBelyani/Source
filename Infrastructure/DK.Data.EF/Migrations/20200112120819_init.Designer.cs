﻿// <auto-generated />
using System;
using DK.Data.EF.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace DK.Data.EF.Migrations
{
    [DbContext(typeof(ApplicationDBContext))]
    [Migration("20200112120819_init")]
    partial class init
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.3-servicing-35854")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("DK.Domain.Entity.Security.Permission", b =>
                {
                    b.Property<long>("xID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("xAction");

                    b.Property<int>("xActionType");

                    b.Property<string>("xComment");

                    b.Property<string>("xController");

                    b.Property<string>("xName");

                    b.Property<long>("xPermissionGroupID");

                    b.HasKey("xID");

                    b.HasIndex("xPermissionGroupID");

                    b.ToTable("Permissions","Security");
                });

            modelBuilder.Entity("DK.Domain.Entity.Security.PermissionGroup", b =>
                {
                    b.Property<long>("xID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("xControllerName");

                    b.Property<string>("xName")
                        .HasMaxLength(100);

                    b.Property<long?>("xParentID");

                    b.HasKey("xID");

                    b.HasIndex("xParentID");

                    b.ToTable("PermissionGroups","Security");
                });

            modelBuilder.Entity("DK.Domain.Entity.Security.PermissionRole", b =>
                {
                    b.Property<long>("xPermissionID");

                    b.Property<long>("xRoleID");

                    b.HasKey("xPermissionID", "xRoleID");

                    b.HasIndex("xRoleID");

                    b.ToTable("PermissionRoles","Security");
                });

            modelBuilder.Entity("DK.Domain.Entity.Security.Role", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("xID")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Name")
                        .HasColumnName("xName")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256);

                    b.Property<string>("xDescription")
                        .HasMaxLength(100);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("Role","Security");
                });

            modelBuilder.Entity("DK.Domain.Entity.Security.RoleClaim", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<long>("RoleId");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("RoleClaims","Security");
                });

            modelBuilder.Entity("DK.Domain.Entity.Security.User", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("xID")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("AccessFailedCount")
                        .HasColumnName("xAccessFailedCount");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Email")
                        .HasColumnName("xEmail")
                        .HasMaxLength(256);

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnName("xEmailConfirmed");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnName("xLockoutEnabled");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnName("xLockoutEnd");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256);

                    b.Property<string>("PasswordHash")
                        .HasColumnName("xPasswordHash");

                    b.Property<string>("PhoneNumber")
                        .HasColumnName("xMobile")
                        .HasMaxLength(20);

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnName("xMobileConfirmed");

                    b.Property<string>("SecurityStamp")
                        .HasColumnName("xSecurityStamp");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnName("xTwoFactorEnabled");

                    b.Property<string>("UserName")
                        .HasColumnName("xUsername")
                        .HasMaxLength(256);

                    b.Property<bool>("xIsActive");

                    b.Property<DateTime>("xRegisterDate");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.ToTable("Users","Security");
                });

            modelBuilder.Entity("DK.Domain.Entity.Security.UserClaim", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("xID")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ClaimType")
                        .HasColumnName("xClaimType");

                    b.Property<string>("ClaimValue")
                        .HasColumnName("xClaimValue");

                    b.Property<long>("UserId")
                        .HasColumnName("xUserID");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("UserClaims","Security");
                });

            modelBuilder.Entity("DK.Domain.Entity.Security.UserLogin", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasColumnName("xLoginProvider");

                    b.Property<string>("ProviderKey")
                        .HasColumnName("xProviderKey");

                    b.Property<long>("UserId")
                        .HasColumnName("xUserID");

                    b.Property<string>("ProviderDisplayName");

                    b.HasKey("LoginProvider", "ProviderKey", "UserId");

                    b.HasAlternateKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("UserLogins","Security");
                });

            modelBuilder.Entity("DK.Domain.Entity.Security.UserRole", b =>
                {
                    b.Property<long>("UserId")
                        .HasColumnName("xUserID");

                    b.Property<long>("RoleId")
                        .HasColumnName("xRoleID");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("UserRoles","Security");
                });

            modelBuilder.Entity("DK.Domain.Entity.Security.UserToken", b =>
                {
                    b.Property<long>("UserId");

                    b.Property<string>("LoginProvider");

                    b.Property<string>("Name");

                    b.Property<string>("Value");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("UserTokens","Security");
                });

            modelBuilder.Entity("DK.Domain.Entity.Security.Permission", b =>
                {
                    b.HasOne("DK.Domain.Entity.Security.PermissionGroup", "xPermissionGroup")
                        .WithMany("xPermissions")
                        .HasForeignKey("xPermissionGroupID")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("DK.Domain.Entity.Security.PermissionGroup", b =>
                {
                    b.HasOne("DK.Domain.Entity.Security.PermissionGroup", "xPermissionGroup")
                        .WithMany("xPermissionGroups")
                        .HasForeignKey("xParentID");
                });

            modelBuilder.Entity("DK.Domain.Entity.Security.PermissionRole", b =>
                {
                    b.HasOne("DK.Domain.Entity.Security.Permission", "xPermission")
                        .WithMany("xPermissionRoles")
                        .HasForeignKey("xPermissionID")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("DK.Domain.Entity.Security.Role", "xRole")
                        .WithMany("xPermissionRoles")
                        .HasForeignKey("xRoleID")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("DK.Domain.Entity.Security.RoleClaim", b =>
                {
                    b.HasOne("DK.Domain.Entity.Security.Role")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("DK.Domain.Entity.Security.UserClaim", b =>
                {
                    b.HasOne("DK.Domain.Entity.Security.User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("DK.Domain.Entity.Security.UserLogin", b =>
                {
                    b.HasOne("DK.Domain.Entity.Security.User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("DK.Domain.Entity.Security.UserRole", b =>
                {
                    b.HasOne("DK.Domain.Entity.Security.Role", "xRole")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("DK.Domain.Entity.Security.User", "xUser")
                        .WithMany("xUserRoles")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("DK.Domain.Entity.Security.UserToken", b =>
                {
                    b.HasOne("DK.Domain.Entity.Security.User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
