﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using UserManager.Dal;

namespace UserManager.Dal.Migrations
{
    [DbContext(typeof(UserDbContext))]
    partial class UserDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.2-rtm-30932")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("UserManager.Dal.Group", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name");

                    b.Property<Guid>("OrganizationId");

                    b.HasKey("Id");

                    b.HasIndex("OrganizationId");

                    b.ToTable("Groups");

                    b.HasData(
                        new { Id = new Guid("3777ec35-2393-4053-95ad-cc587d87a3e3"), Name = "Technical", OrganizationId = new Guid("c00af6d2-5c26-44cc-8414-dbb420d0f942") },
                        new { Id = new Guid("ab2ace08-2daf-4422-9242-293025aab9f6"), Name = "HR", OrganizationId = new Guid("c00af6d2-5c26-44cc-8414-dbb420d0f942") },
                        new { Id = new Guid("abba6119-b935-4870-9c06-be6b8872fb32"), Name = "SoftwareEngineer", OrganizationId = new Guid("a7bd1b7b-1110-4c6c-9fd6-f47a9cc7fbda") },
                        new { Id = new Guid("f90317a4-a87c-4800-8d24-8e7c5e84073e"), Name = "ComputerEngineer", OrganizationId = new Guid("a7bd1b7b-1110-4c6c-9fd6-f47a9cc7fbda") }
                    );
                });

            modelBuilder.Entity("UserManager.Dal.Organization", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.ToTable("Organizations");

                    b.HasData(
                        new { Id = new Guid("c00af6d2-5c26-44cc-8414-dbb420d0f942"), Name = "Rosen" },
                        new { Id = new Guid("a7bd1b7b-1110-4c6c-9fd6-f47a9cc7fbda"), Name = "UIT" }
                    );
                });

            modelBuilder.Entity("UserManager.Dal.Role", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.ToTable("Roles");

                    b.HasData(
                        new { Id = new Guid("fa83781c-c13e-4b2a-a13b-cc557cfba720"), Name = "Technical Lead" },
                        new { Id = new Guid("77817bb6-2a22-4635-8dda-b820356ed8f9"), Name = "HR Lead" },
                        new { Id = new Guid("d1eb257f-9a58-4751-8a6d-a1f0ed91b3ba"), Name = "Engineer" }
                    );
                });

            modelBuilder.Entity("UserManager.Dal.User", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Email");

                    b.Property<string>("FirstName");

                    b.Property<string>("LastName");

                    b.Property<string>("Mobile");

                    b.Property<Guid>("OrganizationId");

                    b.Property<string>("Phone");

                    b.Property<string>("ProfileImage");

                    b.HasKey("Id");

                    b.HasIndex("OrganizationId");

                    b.ToTable("Users");

                    b.HasData(
                        new { Id = "12345", Email = "{\"main\": \"em@email.com\",\"emails\": [\"em@email.com\",\"em@yahoo.com\"]}", FirstName = "Minh", LastName = "Nguyen Le", Mobile = "{\"main\": \"333444\",\"mobiles\": [\"333444\",\"555666\"]}", OrganizationId = new Guid("c00af6d2-5c26-44cc-8414-dbb420d0f942"), Phone = "{\"main\": \"1234\",\"work\": [\"1234\",\"5678\"], \"private\": [\"91011\"]}", ProfileImage = "image" }
                    );
                });

            modelBuilder.Entity("UserManager.Dal.UserGroup", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<Guid>("GroupId");

                    b.Property<bool>("IsMain");

                    b.Property<string>("UserId");

                    b.HasKey("Id");

                    b.ToTable("UserGroups");

                    b.HasData(
                        new { Id = new Guid("540ab5fc-4615-4cde-a284-cc9d9698a238"), GroupId = new Guid("3777ec35-2393-4053-95ad-cc587d87a3e3"), IsMain = true, UserId = "12345" },
                        new { Id = new Guid("a2bc5162-d036-436e-9ac2-ab4571ec0694"), GroupId = new Guid("ab2ace08-2daf-4422-9242-293025aab9f6"), IsMain = false, UserId = "12345" }
                    );
                });

            modelBuilder.Entity("UserManager.Dal.UserRole", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<bool>("IsMain");

                    b.Property<Guid>("RoleId");

                    b.Property<string>("UserId");

                    b.HasKey("Id");

                    b.ToTable("UserRoles");

                    b.HasData(
                        new { Id = new Guid("d0bd5e70-f6b3-4671-b587-0a87995daf84"), IsMain = true, RoleId = new Guid("d1eb257f-9a58-4751-8a6d-a1f0ed91b3ba"), UserId = "12345" }
                    );
                });

            modelBuilder.Entity("UserManager.Dal.Group", b =>
                {
                    b.HasOne("UserManager.Dal.Organization", "Organization")
                        .WithMany()
                        .HasForeignKey("OrganizationId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("UserManager.Dal.User", b =>
                {
                    b.HasOne("UserManager.Dal.Organization", "Organization")
                        .WithMany()
                        .HasForeignKey("OrganizationId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
