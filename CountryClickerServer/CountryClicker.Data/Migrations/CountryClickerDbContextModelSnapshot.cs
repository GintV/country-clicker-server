﻿// <auto-generated />
using CountryClicker.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.Storage.Internal;
using Microsoft.EntityFrameworkCore.ValueGeneration;
using System;

namespace CountryClicker.Data.Migrations
{
    [DbContext(typeof(CountryClickerDbContext))]
    partial class CountryClickerDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.0.0-rtm-26452")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("CountryClicker.Domain.Group", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Discriminator")
                        .IsRequired();

                    b.Property<long>("Score");

                    b.Property<string>("Title");

                    b.HasKey("Id");

                    b.ToTable("Group");

                    b.HasDiscriminator<string>("Discriminator").HasValue("Group");
                });

            modelBuilder.Entity("CountryClicker.Domain.GroupSprint", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<Guid>("GroupId");

                    b.Property<long>("Score");

                    b.Property<Guid>("SprintId");

                    b.HasKey("Id");

                    b.HasIndex("GroupId");

                    b.HasIndex("SprintId");

                    b.ToTable("GroupSprint");
                });

            modelBuilder.Entity("CountryClicker.Domain.Player", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Nickname");

                    b.Property<long>("Score");

                    b.Property<Guid>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("Players");
                });

            modelBuilder.Entity("CountryClicker.Domain.PlayerSprint", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<Guid>("PlayerId");

                    b.Property<long>("Score");

                    b.Property<Guid>("SprintId");

                    b.HasKey("Id");

                    b.HasIndex("PlayerId");

                    b.HasIndex("SprintId");

                    b.ToTable("PlayerSprints");
                });

            modelBuilder.Entity("CountryClicker.Domain.PlayerSubscription", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<Guid>("GroupId");

                    b.Property<Guid>("PlayerId");

                    b.Property<DateTime>("SubscribeTime");

                    b.HasKey("Id");

                    b.HasIndex("GroupId");

                    b.HasIndex("PlayerId");

                    b.ToTable("PlayerSubscriptions");
                });

            modelBuilder.Entity("CountryClicker.Domain.Sprint", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("FinishTime");

                    b.Property<long>("Score");

                    b.Property<DateTime>("StartTime");

                    b.HasKey("Id");

                    b.ToTable("Sprints");
                });

            modelBuilder.Entity("CountryClicker.Domain.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Password");

                    b.Property<string>("Username");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("CountryClicker.Domain.City", b =>
                {
                    b.HasBaseType("CountryClicker.Domain.Group");

                    b.Property<Guid>("CountryId");

                    b.HasIndex("CountryId");

                    b.ToTable("City");

                    b.HasDiscriminator().HasValue("City");
                });

            modelBuilder.Entity("CountryClicker.Domain.Continent", b =>
                {
                    b.HasBaseType("CountryClicker.Domain.Group");


                    b.ToTable("Continent");

                    b.HasDiscriminator().HasValue("Continent");
                });

            modelBuilder.Entity("CountryClicker.Domain.Country", b =>
                {
                    b.HasBaseType("CountryClicker.Domain.Group");

                    b.Property<Guid>("ContinentId");

                    b.HasIndex("ContinentId");

                    b.ToTable("Country");

                    b.HasDiscriminator().HasValue("Country");
                });

            modelBuilder.Entity("CountryClicker.Domain.CustomGroup", b =>
                {
                    b.HasBaseType("CountryClicker.Domain.Group");

                    b.Property<Guid>("CreatedById");

                    b.HasIndex("CreatedById");

                    b.ToTable("CustomGroup");

                    b.HasDiscriminator().HasValue("CustomGroup");
                });

            modelBuilder.Entity("CountryClicker.Domain.GroupSprint", b =>
                {
                    b.HasOne("CountryClicker.Domain.Group", "Group")
                        .WithMany()
                        .HasForeignKey("GroupId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("CountryClicker.Domain.Sprint", "Sprint")
                        .WithMany("GroupSprints")
                        .HasForeignKey("SprintId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("CountryClicker.Domain.Player", b =>
                {
                    b.HasOne("CountryClicker.Domain.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("CountryClicker.Domain.PlayerSprint", b =>
                {
                    b.HasOne("CountryClicker.Domain.Player", "Player")
                        .WithMany()
                        .HasForeignKey("PlayerId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("CountryClicker.Domain.Sprint", "Sprint")
                        .WithMany("PlayerSprints")
                        .HasForeignKey("SprintId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("CountryClicker.Domain.PlayerSubscription", b =>
                {
                    b.HasOne("CountryClicker.Domain.Group", "Group")
                        .WithMany("GroupSubscribers")
                        .HasForeignKey("GroupId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("CountryClicker.Domain.Player", "Player")
                        .WithMany("SubscribedGroups")
                        .HasForeignKey("PlayerId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("CountryClicker.Domain.City", b =>
                {
                    b.HasOne("CountryClicker.Domain.Country", "Country")
                        .WithMany("CountryCities")
                        .HasForeignKey("CountryId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("CountryClicker.Domain.Country", b =>
                {
                    b.HasOne("CountryClicker.Domain.Continent", "Continent")
                        .WithMany("ContinentCountries")
                        .HasForeignKey("ContinentId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("CountryClicker.Domain.CustomGroup", b =>
                {
                    b.HasOne("CountryClicker.Domain.Player", "CreatedBy")
                        .WithMany("CreatedCustomGroups")
                        .HasForeignKey("CreatedById")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}