using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using jobboard.Data;

namespace jobboard.backend.Migrations
{
    [DbContext(typeof(JobBoardContext))]
    [Migration("20170116075203_'init'")]
    partial class init
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.0.1")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("jobboard.Model.Entities.Content", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("JobId");

                    b.Property<string>("Text")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasAnnotation("MaxLength", 100000);

                    b.HasKey("Id");

                    b.HasIndex("JobId")
                        .IsUnique();

                    b.ToTable("Content");
                });

            modelBuilder.Entity("jobboard.Model.Entities.Job", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<bool>("Analyzed")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValue(false);

                    b.Property<string>("City");

                    b.Property<string>("Employer")
                        .IsRequired();

                    b.Property<DateTime>("PostAt");

                    b.Property<string>("Province");

                    b.Property<DateTime>("ReadAt");

                    b.Property<string>("Title")
                        .IsRequired();

                    b.Property<string>("Url")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("PostAt");

                    b.ToTable("Job");
                });

            modelBuilder.Entity("jobboard.Model.Entities.JobSkill", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("JobId");

                    b.Property<int>("Level");

                    b.Property<int>("SkillId");

                    b.HasKey("Id");

                    b.HasIndex("JobId");

                    b.HasIndex("SkillId");

                    b.HasIndex("JobId", "SkillId");

                    b.ToTable("JobSkill");
                });

            modelBuilder.Entity("jobboard.Model.Entities.Skill", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<bool>("IsReg");

                    b.Property<string>("KeyWords")
                        .IsRequired();

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.HasIndex("Name");

                    b.ToTable("Skill");
                });

            modelBuilder.Entity("jobboard.Model.Entities.Content", b =>
                {
                    b.HasOne("jobboard.Model.Entities.Job", "Job")
                        .WithOne("Content")
                        .HasForeignKey("jobboard.Model.Entities.Content", "JobId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("jobboard.Model.Entities.JobSkill", b =>
                {
                    b.HasOne("jobboard.Model.Entities.Job", "Job")
                        .WithMany("JobSkills")
                        .HasForeignKey("JobId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("jobboard.Model.Entities.Skill", "Skill")
                        .WithMany("JobSkills")
                        .HasForeignKey("SkillId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
        }
    }
}
