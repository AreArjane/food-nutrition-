﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace FoodRegisterationToolSub1.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20241115034546_data3-date-create")]
    partial class data3datecreate
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("FoodRegisterationToolSub1.Models.datasets.FoodCategory", b =>
                {
                    b.Property<int>("Id")
                        .HasColumnType("integer")
                        .HasColumnName("id");

                    b.Property<string>("Code")
                        .IsRequired()
                        .HasMaxLength(10)
                        .HasColumnType("character varying(10)")
                        .HasColumnName("code");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)")
                        .HasColumnName("description");

                    b.HasKey("Id");

                    b.ToTable("FoodCategories");
                });

            modelBuilder.Entity("FoodRegisterationToolSub1.Models.meals.Meal", b =>
                {
                    b.Property<int>("MealId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("meal_id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("MealId"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)")
                        .HasColumnName("name");

                    b.Property<int>("UserId")
                        .HasColumnType("integer")
                        .HasColumnName("user_id");

                    b.HasKey("MealId");

                    b.HasIndex("UserId");

                    b.ToTable("Meal");
                });

            modelBuilder.Entity("FoodRegisterationToolSub1.Models.meals.MealFood", b =>
                {
                    b.Property<int>("MealFoodId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("meal_food_id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("MealFoodId"));

                    b.Property<int>("FoodId")
                        .HasColumnType("integer")
                        .HasColumnName("food_id");

                    b.Property<int?>("MealId")
                        .HasColumnType("integer");

                    b.Property<double>("Quantity")
                        .HasColumnType("double precision")
                        .HasColumnName("quantity");

                    b.HasKey("MealFoodId");

                    b.HasIndex("FoodId");

                    b.HasIndex("MealId");

                    b.ToTable("MealFood");
                });

            modelBuilder.Entity("FoodRegisterationToolSub1.Models.permissions.Permission", b =>
                {
                    b.Property<int>("PermissionId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("PermissionId"));

                    b.Property<int>("PermissionType")
                        .HasColumnType("integer");

                    b.Property<int?>("UserId")
                        .HasColumnType("integer");

                    b.HasKey("PermissionId");

                    b.HasIndex("UserId");

                    b.ToTable("Permissions");
                });

            modelBuilder.Entity("FoodRegisterationToolSub1.Models.users.User", b =>
                {
                    b.Property<int>("UserId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("UserId"));

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(60)
                        .HasColumnType("character varying(60)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("PhoneNr")
                        .IsRequired()
                        .HasMaxLength(17)
                        .HasColumnType("character varying(17)");

                    b.Property<int>("UserType")
                        .HasColumnType("integer");

                    b.HasKey("UserId");

                    b.ToTable("Users", (string)null);

                    b.UseTptMappingStrategy();
                });

            modelBuilder.Entity("FoodRegistrationToolSub1.Models.datasets.Food", b =>
                {
                    b.Property<int>("FoodId")
                        .HasColumnType("integer")
                        .HasColumnName("fdc_id");

                    b.Property<string>("DataType")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)")
                        .HasColumnName("data_type");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)")
                        .HasColumnName("description");

                    b.Property<int?>("FoodCategoryId")
                        .HasColumnType("integer")
                        .HasColumnName("food_category_id");

                    b.Property<string>("PublicationDate")
                        .HasColumnType("text")
                        .HasColumnName("publication_date");

                    b.HasKey("FoodId");

                    b.HasIndex("FoodCategoryId");

                    b.ToTable("Food");
                });

            modelBuilder.Entity("FoodRegistrationToolSub1.Models.datasets.FoodNutrient", b =>
                {
                    b.Property<int>("Id")
                        .HasColumnType("integer")
                        .HasColumnName("id");

                    b.Property<long?>("Amount")
                        .HasColumnType("bigint")
                        .HasColumnName("amount");

                    b.Property<string>("DataPoint")
                        .HasColumnType("text")
                        .HasColumnName("data_points");

                    b.Property<string>("DerivationId")
                        .HasColumnType("text")
                        .HasColumnName("derivation_id");

                    b.Property<int>("FdcId")
                        .HasColumnType("integer")
                        .HasColumnName("fdc_id");

                    b.Property<string>("Footnote")
                        .HasMaxLength(2555)
                        .HasColumnType("character varying(2555)")
                        .HasColumnName("footnote");

                    b.Property<string>("Max")
                        .HasColumnType("text")
                        .HasColumnName("max");

                    b.Property<string>("Median")
                        .HasColumnType("text")
                        .HasColumnName("median");

                    b.Property<string>("Min")
                        .HasColumnType("text")
                        .HasColumnName("min");

                    b.Property<string>("MinYearAcquired")
                        .HasColumnType("text")
                        .HasColumnName("min_year_acquired");

                    b.Property<int>("NutrientId")
                        .HasColumnType("integer")
                        .HasColumnName("nutrient_id");

                    b.HasKey("Id");

                    b.HasIndex("FdcId");

                    b.HasIndex("NutrientId");

                    b.ToTable("FoodNutrient");
                });

            modelBuilder.Entity("FoodRegistrationToolSub1.Models.datasets.Nutrient", b =>
                {
                    b.Property<int>("Id")
                        .HasColumnType("integer")
                        .HasColumnName("id");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)")
                        .HasColumnName("name");

                    b.Property<string>("NutrientNbr")
                        .HasColumnType("text")
                        .HasColumnName("nutrient_nbr");

                    b.Property<string>("Rank")
                        .HasColumnType("text")
                        .HasColumnName("rank");

                    b.Property<string>("UnitName")
                        .IsRequired()
                        .HasMaxLength(10)
                        .HasColumnType("character varying(10)")
                        .HasColumnName("unit_name");

                    b.HasKey("Id");

                    b.ToTable("Nutrient");
                });

            modelBuilder.Entity("FoodRegisterationToolSub1.Models.users.AdminUser", b =>
                {
                    b.HasBaseType("FoodRegisterationToolSub1.Models.users.User");

                    b.Property<string>("NationalIdenityNumber")
                        .IsRequired()
                        .HasMaxLength(11)
                        .HasColumnType("character varying(11)");

                    b.Property<string>("OfficeAddress")
                        .HasMaxLength(25)
                        .HasColumnType("character varying(25)");

                    b.Property<string>("WorkPhoneNr")
                        .HasColumnType("text");

                    b.ToTable("AdminUsers", (string)null);
                });

            modelBuilder.Entity("FoodRegisterationToolSub1.Models.users.NormalUser", b =>
                {
                    b.HasBaseType("FoodRegisterationToolSub1.Models.users.User");

                    b.Property<string>("Email")
                        .HasColumnType("text");

                    b.Property<string>("HomeAddress")
                        .HasMaxLength(25)
                        .HasColumnType("character varying(25)");

                    b.Property<string>("LastName")
                        .HasColumnType("text");

                    b.Property<string>("PostalCode")
                        .HasMaxLength(8)
                        .HasColumnType("character varying(8)");

                    b.ToTable("NormalUsers", (string)null);
                });

            modelBuilder.Entity("FoodRegisterationToolSub1.Models.users.PendingSuperUser", b =>
                {
                    b.HasBaseType("FoodRegisterationToolSub1.Models.users.User");

                    b.Property<string>("DateOfBirth")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Email")
                        .HasColumnType("text");

                    b.Property<DateTime>("datacreated")
                        .HasColumnType("timestamp with time zone");

                    b.Property<bool>("isApproved")
                        .HasColumnType("boolean");

                    b.Property<string>("verificationcode")
                        .IsRequired()
                        .HasColumnType("text");

                    b.ToTable("PendingSuperUser", (string)null);
                });

            modelBuilder.Entity("FoodRegisterationToolSub1.Models.users.SuperUser", b =>
                {
                    b.HasBaseType("FoodRegisterationToolSub1.Models.users.User");

                    b.Property<string>("DateOfBirth")
                        .HasColumnType("text");

                    b.Property<string>("Email")
                        .HasColumnType("text");

                    b.ToTable("SuperUsers", (string)null);
                });

            modelBuilder.Entity("FoodRegisterationToolSub1.Models.meals.Meal", b =>
                {
                    b.HasOne("FoodRegisterationToolSub1.Models.users.User", "User")
                        .WithMany("Meals")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("FoodRegisterationToolSub1.Models.meals.MealFood", b =>
                {
                    b.HasOne("FoodRegistrationToolSub1.Models.datasets.Food", "Food")
                        .WithMany()
                        .HasForeignKey("FoodId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("FoodRegisterationToolSub1.Models.meals.Meal", null)
                        .WithMany("MealFoods")
                        .HasForeignKey("MealId");

                    b.Navigation("Food");
                });

            modelBuilder.Entity("FoodRegisterationToolSub1.Models.permissions.Permission", b =>
                {
                    b.HasOne("FoodRegisterationToolSub1.Models.users.User", null)
                        .WithMany("Permissions")
                        .HasForeignKey("UserId");
                });

            modelBuilder.Entity("FoodRegistrationToolSub1.Models.datasets.Food", b =>
                {
                    b.HasOne("FoodRegisterationToolSub1.Models.datasets.FoodCategory", "FoodCategory")
                        .WithMany("Foods")
                        .HasForeignKey("FoodCategoryId");

                    b.Navigation("FoodCategory");
                });

            modelBuilder.Entity("FoodRegistrationToolSub1.Models.datasets.FoodNutrient", b =>
                {
                    b.HasOne("FoodRegistrationToolSub1.Models.datasets.Food", "Food")
                        .WithMany("FoodNutrients")
                        .HasForeignKey("FdcId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("FoodRegistrationToolSub1.Models.datasets.Nutrient", "Nutrient")
                        .WithMany("FoodNutrients")
                        .HasForeignKey("NutrientId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Food");

                    b.Navigation("Nutrient");
                });

            modelBuilder.Entity("FoodRegisterationToolSub1.Models.users.AdminUser", b =>
                {
                    b.HasOne("FoodRegisterationToolSub1.Models.users.User", null)
                        .WithOne()
                        .HasForeignKey("FoodRegisterationToolSub1.Models.users.AdminUser", "UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("FoodRegisterationToolSub1.Models.users.NormalUser", b =>
                {
                    b.HasOne("FoodRegisterationToolSub1.Models.users.User", null)
                        .WithOne()
                        .HasForeignKey("FoodRegisterationToolSub1.Models.users.NormalUser", "UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("FoodRegisterationToolSub1.Models.users.PendingSuperUser", b =>
                {
                    b.HasOne("FoodRegisterationToolSub1.Models.users.User", null)
                        .WithOne()
                        .HasForeignKey("FoodRegisterationToolSub1.Models.users.PendingSuperUser", "UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("FoodRegisterationToolSub1.Models.users.SuperUser", b =>
                {
                    b.HasOne("FoodRegisterationToolSub1.Models.users.User", null)
                        .WithOne()
                        .HasForeignKey("FoodRegisterationToolSub1.Models.users.SuperUser", "UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("FoodRegisterationToolSub1.Models.datasets.FoodCategory", b =>
                {
                    b.Navigation("Foods");
                });

            modelBuilder.Entity("FoodRegisterationToolSub1.Models.meals.Meal", b =>
                {
                    b.Navigation("MealFoods");
                });

            modelBuilder.Entity("FoodRegisterationToolSub1.Models.users.User", b =>
                {
                    b.Navigation("Meals");

                    b.Navigation("Permissions");
                });

            modelBuilder.Entity("FoodRegistrationToolSub1.Models.datasets.Food", b =>
                {
                    b.Navigation("FoodNutrients");
                });

            modelBuilder.Entity("FoodRegistrationToolSub1.Models.datasets.Nutrient", b =>
                {
                    b.Navigation("FoodNutrients");
                });
#pragma warning restore 612, 618
        }
    }
}
