// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using PassportsAPI.EfCore;

#nullable disable

namespace PassportsAPI.Migrations
{
    [DbContext(typeof(DataContext))]
    [Migration("20220901151535_AddTriggers")]
    partial class AddTriggers
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("PassportsAPI.EfCore.InactivePassports", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("id"));

                    b.Property<DateTime>("ChangeTime")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("changetime");

                    b.Property<bool>("IsActive")
                        .HasColumnType("boolean")
                        .HasColumnName("isactive");

                    b.Property<int>("Number")
                        .HasColumnType("integer")
                        .HasColumnName("number");

                    b.Property<int>("Series")
                        .HasColumnType("integer")
                        .HasColumnName("series");

                    b.HasKey("id");

                    b.HasIndex("Series", "Number")
                        .IsUnique();

                    b.ToTable("passports");
                });

            modelBuilder.Entity("PassportsAPI.EfCore.PassportsHistory", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("id"));

                    b.Property<DateTime>("ChangeTime")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("changetime");

                    b.Property<bool>("IsActive")
                        .HasColumnType("boolean")
                        .HasColumnName("isactive");

                    b.Property<int>("PassportId")
                        .HasColumnType("integer")
                        .HasColumnName("passportid");

                    b.HasKey("id");

                    b.HasIndex("PassportId");

                    b.ToTable("history");
                });

            modelBuilder.Entity("PassportsAPI.EfCore.TempTable", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("id"));

                    b.Property<int>("Number")
                        .HasColumnType("integer")
                        .HasColumnName("number");

                    b.Property<int>("Series")
                        .HasColumnType("integer")
                        .HasColumnName("series");

                    b.HasKey("id");

                    b.HasIndex("Series", "Number")
                        .IsUnique();

                    b.ToTable("temptable");
                });

            modelBuilder.Entity("PassportsAPI.EfCore.PassportsHistory", b =>
                {
                    b.HasOne("PassportsAPI.EfCore.InactivePassports", "Passport")
                        .WithMany()
                        .HasForeignKey("PassportId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Passport");
                });
#pragma warning restore 612, 618
        }
    }
}
