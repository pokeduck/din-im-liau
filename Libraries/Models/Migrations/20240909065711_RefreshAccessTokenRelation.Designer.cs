﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Models.DataModels;

#nullable disable

namespace Models.Migrations
{
    [DbContext(typeof(DataContext))]
    [Migration("20240909065711_RefreshAccessTokenRelation")]
    partial class RefreshAccessTokenRelation
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            MySqlModelBuilderExtensions.AutoIncrementColumns(modelBuilder);

            modelBuilder.Entity("Models.DataModels.AccessToken", b =>
                {
                    b.Property<Guid>("Token")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<int>("AccountId")
                        .HasColumnType("int");

                    b.Property<long>("CreateTime")
                        .HasColumnType("bigint");

                    b.Property<long>("ExpireTime")
                        .HasColumnType("bigint");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.Property<long>("UpdateTime")
                        .HasColumnType("bigint");

                    b.HasKey("Token");

                    b.HasIndex("AccountId");

                    b.ToTable("AccessToken");
                });

            modelBuilder.Entity("Models.DataModels.Account", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("AccessToken")
                        .HasColumnType("longtext");

                    b.Property<long>("CreateTime")
                        .HasColumnType("bigint");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)");

                    b.Property<int>("EmailValidStatus")
                        .HasColumnType("int");

                    b.Property<string>("GoogleOpenId")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)");

                    b.Property<string>("HashPassword")
                        .HasColumnType("longtext");

                    b.Property<string>("NickName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)");

                    b.Property<int>("PermissionId")
                        .HasColumnType("int");

                    b.Property<string>("Salt")
                        .HasColumnType("longtext");

                    b.Property<string>("ThumbnailUrl")
                        .HasMaxLength(200)
                        .HasColumnType("varchar(200)");

                    b.Property<long>("UpdateTime")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("PermissionId");

                    b.ToTable("Account");
                });

            modelBuilder.Entity("Models.DataModels.Drink", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("Id"));

                    b.Property<long>("CreateTime")
                        .HasColumnType("bigint");

                    b.Property<string>("Memo")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("varchar(30)");

                    b.Property<string>("MenuLink")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("varchar(200)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("varchar(20)");

                    b.Property<int>("Price")
                        .HasColumnType("int");

                    b.Property<int>("ToppingLowerLimit")
                        .HasColumnType("int");

                    b.Property<int>("ToppingUpperLimit")
                        .HasColumnType("int");

                    b.Property<long>("UpdateTime")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.ToTable("Drink");
                });

            modelBuilder.Entity("Models.DataModels.DrinkIceRelation", b =>
                {
                    b.Property<int>("DrinkId")
                        .HasColumnType("int");

                    b.Property<int>("IceId")
                        .HasColumnType("int");

                    b.HasKey("DrinkId", "IceId");

                    b.HasIndex("IceId");

                    b.ToTable("DrinkIceRelation");
                });

            modelBuilder.Entity("Models.DataModels.DrinkSuggerRelation", b =>
                {
                    b.Property<int>("DrinkId")
                        .HasColumnType("int");

                    b.Property<int>("SuggerId")
                        .HasColumnType("int");

                    b.HasKey("DrinkId", "SuggerId");

                    b.HasIndex("SuggerId");

                    b.ToTable("DrinkSuggerRelation");
                });

            modelBuilder.Entity("Models.DataModels.DrinkToppingRelation", b =>
                {
                    b.Property<int>("DrinkId")
                        .HasColumnType("int");

                    b.Property<int>("ToppingId")
                        .HasColumnType("int");

                    b.HasKey("DrinkId", "ToppingId");

                    b.HasIndex("ToppingId");

                    b.ToTable("DrinkToppingRelation");
                });

            modelBuilder.Entity("Models.DataModels.EmailVerificationToken", b =>
                {
                    b.Property<Guid>("Token")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<int>("AccountId")
                        .HasColumnType("int");

                    b.Property<long>("CreateTime")
                        .HasColumnType("bigint");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.Property<long>("UpdateTime")
                        .HasColumnType("bigint");

                    b.HasKey("Token");

                    b.ToTable("EmailVerificationToken");
                });

            modelBuilder.Entity("Models.DataModels.Ice", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("varchar(20)");

                    b.Property<int>("Ratio")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Ice");
                });

            modelBuilder.Entity("Models.DataModels.Order", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("AdminId")
                        .HasColumnType("int");

                    b.Property<long>("CreateTime")
                        .HasColumnType("bigint");

                    b.Property<long>("EndTime")
                        .HasColumnType("bigint");

                    b.Property<int>("StoreId")
                        .HasColumnType("int");

                    b.Property<int>("TotalPrice")
                        .HasColumnType("int");

                    b.Property<long>("UpdateTime")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("AdminId");

                    b.HasIndex("StoreId");

                    b.ToTable("Order");
                });

            modelBuilder.Entity("Models.DataModels.OrderRecord", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("Id"));

                    b.Property<long>("CreateTime")
                        .HasColumnType("bigint");

                    b.Property<int>("CustomerId")
                        .HasColumnType("int");

                    b.Property<string>("DrinkName")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("varchar(20)");

                    b.Property<string>("Extra")
                        .IsRequired()
                        .HasMaxLength(300)
                        .HasColumnType("varchar(300)");

                    b.Property<int>("OrderId")
                        .HasColumnType("int");

                    b.Property<int>("Quantity")
                        .HasColumnType("int");

                    b.Property<int>("TotalPrice")
                        .HasColumnType("int");

                    b.Property<long>("UpdateTime")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("CustomerId");

                    b.HasIndex("OrderId");

                    b.ToTable("OrderRecord");
                });

            modelBuilder.Entity("Models.DataModels.Permission", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("Id"));

                    b.Property<long>("CreateTime")
                        .HasColumnType("bigint");

                    b.Property<bool>("IsAdmin")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("varchar(20)");

                    b.Property<long>("UpdateTime")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.ToTable("Permission");
                });

            modelBuilder.Entity("Models.DataModels.RefreshToken", b =>
                {
                    b.Property<Guid>("Token")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<int>("AccountId")
                        .HasColumnType("int");

                    b.Property<long>("CreateTime")
                        .HasColumnType("bigint");

                    b.Property<long>("ExpireTime")
                        .HasColumnType("bigint");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.Property<long>("UpdateTime")
                        .HasColumnType("bigint");

                    b.HasKey("Token");

                    b.HasIndex("AccountId");

                    b.ToTable("RefreshToken");
                });

            modelBuilder.Entity("Models.DataModels.ServingSize", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(5)
                        .HasColumnType("varchar(5)");

                    b.Property<int>("PriceGap")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("ServingSize");
                });

            modelBuilder.Entity("Models.DataModels.Store", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)");

                    b.Property<string>("Memo")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("varchar(30)");

                    b.Property<string>("MenuLink")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("varchar(100)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("varchar(20)");

                    b.Property<string>("Telephone")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("varchar(20)");

                    b.HasKey("Id");

                    b.ToTable("Store");
                });

            modelBuilder.Entity("Models.DataModels.StoreDrinkRelation", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("DrinkId")
                        .HasColumnType("int");

                    b.Property<int>("StoreId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("StoreDrinkRelation");
                });

            modelBuilder.Entity("Models.DataModels.Sugger", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("varchar(20)");

                    b.Property<int>("Ratio")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Sugger");
                });

            modelBuilder.Entity("Models.DataModels.Topping", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("varchar(20)");

                    b.Property<int>("Price")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Topping");
                });

            modelBuilder.Entity("Models.DataModels.AccessToken", b =>
                {
                    b.HasOne("Models.DataModels.Account", "Account")
                        .WithMany("AccessTokens")
                        .HasForeignKey("AccountId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Account");
                });

            modelBuilder.Entity("Models.DataModels.Account", b =>
                {
                    b.HasOne("Models.DataModels.Permission", "Permission")
                        .WithMany("Accounts")
                        .HasForeignKey("PermissionId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("Permission");
                });

            modelBuilder.Entity("Models.DataModels.DrinkIceRelation", b =>
                {
                    b.HasOne("Models.DataModels.Drink", null)
                        .WithMany("IceRelations")
                        .HasForeignKey("DrinkId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Models.DataModels.Ice", null)
                        .WithMany("Drinks")
                        .HasForeignKey("IceId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Models.DataModels.DrinkSuggerRelation", b =>
                {
                    b.HasOne("Models.DataModels.Drink", null)
                        .WithMany("DrinkRelations")
                        .HasForeignKey("DrinkId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Models.DataModels.Sugger", null)
                        .WithMany("Drinks")
                        .HasForeignKey("SuggerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Models.DataModels.DrinkToppingRelation", b =>
                {
                    b.HasOne("Models.DataModels.Drink", null)
                        .WithMany("ToppingRelations")
                        .HasForeignKey("DrinkId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Models.DataModels.Topping", null)
                        .WithMany("Drinks")
                        .HasForeignKey("ToppingId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Models.DataModels.Order", b =>
                {
                    b.HasOne("Models.DataModels.Account", "Admin")
                        .WithMany("Orders")
                        .HasForeignKey("AdminId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("Models.DataModels.Store", null)
                        .WithMany("Orders")
                        .HasForeignKey("StoreId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Admin");
                });

            modelBuilder.Entity("Models.DataModels.OrderRecord", b =>
                {
                    b.HasOne("Models.DataModels.Account", "Customer")
                        .WithMany("OrderRecords")
                        .HasForeignKey("CustomerId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("Models.DataModels.Order", "order")
                        .WithMany("orderRecords")
                        .HasForeignKey("OrderId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Customer");

                    b.Navigation("order");
                });

            modelBuilder.Entity("Models.DataModels.RefreshToken", b =>
                {
                    b.HasOne("Models.DataModels.Account", "Account")
                        .WithMany("RefreshTokens")
                        .HasForeignKey("AccountId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Account");
                });

            modelBuilder.Entity("Models.DataModels.Account", b =>
                {
                    b.Navigation("AccessTokens");

                    b.Navigation("OrderRecords");

                    b.Navigation("Orders");

                    b.Navigation("RefreshTokens");
                });

            modelBuilder.Entity("Models.DataModels.Drink", b =>
                {
                    b.Navigation("DrinkRelations");

                    b.Navigation("IceRelations");

                    b.Navigation("ToppingRelations");
                });

            modelBuilder.Entity("Models.DataModels.Ice", b =>
                {
                    b.Navigation("Drinks");
                });

            modelBuilder.Entity("Models.DataModels.Order", b =>
                {
                    b.Navigation("orderRecords");
                });

            modelBuilder.Entity("Models.DataModels.Permission", b =>
                {
                    b.Navigation("Accounts");
                });

            modelBuilder.Entity("Models.DataModels.Store", b =>
                {
                    b.Navigation("Orders");
                });

            modelBuilder.Entity("Models.DataModels.Sugger", b =>
                {
                    b.Navigation("Drinks");
                });

            modelBuilder.Entity("Models.DataModels.Topping", b =>
                {
                    b.Navigation("Drinks");
                });
#pragma warning restore 612, 618
        }
    }
}
