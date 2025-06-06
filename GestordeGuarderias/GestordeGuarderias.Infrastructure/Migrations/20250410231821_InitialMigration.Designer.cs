﻿// <auto-generated />
using System;
using GestordeGuarderias.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace GestordeGuarderias.Infrastructure.Migrations
{
    [DbContext(typeof(GestordeGuarderiasDbContext))]
    [Migration("20250410231821_InitialMigration")]
    partial class InitialMigration
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("GestordeGuarderias.Domain.Entities.Actividad", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Descripcion")
                        .HasMaxLength(300)
                        .HasColumnType("nvarchar(300)");

                    b.Property<DateTime>("Fecha")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("GuarderiaId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<TimeSpan>("Hora")
                        .HasColumnType("time");

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("Id");

                    b.HasIndex("GuarderiaId");

                    b.ToTable("Actividades");
                });

            modelBuilder.Entity("GestordeGuarderias.Domain.Entities.ActividadNino", b =>
                {
                    b.Property<Guid>("NinoId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("ActividadId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("NinoId", "ActividadId");

                    b.HasIndex("ActividadId");

                    b.ToTable("ActividadesNinos");
                });

            modelBuilder.Entity("GestordeGuarderias.Domain.Entities.Asistencia", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("Fecha")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("GuarderiaId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("NinoId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("Presente")
                        .HasColumnType("bit");

                    b.HasKey("Id");

                    b.HasIndex("GuarderiaId");

                    b.HasIndex("NinoId");

                    b.ToTable("Asistencias");
                });

            modelBuilder.Entity("GestordeGuarderias.Domain.Entities.Guarderia", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Direccion")
                        .IsRequired()
                        .HasMaxLength(300)
                        .HasColumnType("nvarchar(300)");

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<string>("Telefono")
                        .IsRequired()
                        .HasMaxLength(15)
                        .HasColumnType("nvarchar(15)");

                    b.HasKey("Id");

                    b.ToTable("Guarderias");
                });

            modelBuilder.Entity("GestordeGuarderias.Domain.Entities.Mensaje", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Asunto")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<string>("Contenido")
                        .IsRequired()
                        .HasMaxLength(1000)
                        .HasColumnType("nvarchar(1000)");

                    b.Property<DateTime>("Fecha")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("GuarderiaId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<TimeSpan>("Hora")
                        .HasColumnType("time");

                    b.Property<Guid>("NinoId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("TutorId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("GuarderiaId");

                    b.HasIndex("NinoId");

                    b.HasIndex("TutorId");

                    b.ToTable("Mensajes");
                });

            modelBuilder.Entity("GestordeGuarderias.Domain.Entities.Nino", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Apellido")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<DateTime>("FechaNacimiento")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("GuarderiaId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<Guid>("TutorId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("GuarderiaId");

                    b.HasIndex("TutorId");

                    b.ToTable("Ninos");
                });

            modelBuilder.Entity("GestordeGuarderias.Domain.Entities.Pago", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("Fecha")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("GuarderiaId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<decimal>("Monto")
                        .HasPrecision(18, 2)
                        .HasColumnType("decimal(18,2)");

                    b.Property<Guid>("NinoId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("TutorId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("GuarderiaId");

                    b.HasIndex("NinoId");

                    b.HasIndex("TutorId");

                    b.ToTable("Pagos");
                });

            modelBuilder.Entity("GestordeGuarderias.Domain.Entities.Tutor", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Apellido")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Cedula")
                        .IsRequired()
                        .HasMaxLength(13)
                        .HasColumnType("nvarchar(13)");

                    b.Property<string>("CorreoElectronico")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Telefono")
                        .IsRequired()
                        .HasMaxLength(13)
                        .HasColumnType("nvarchar(13)");

                    b.HasKey("Id");

                    b.HasIndex("Cedula")
                        .IsUnique();

                    b.ToTable("Tutores");
                });

            modelBuilder.Entity("GestordeGuarderias.Domain.Entities.Actividad", b =>
                {
                    b.HasOne("GestordeGuarderias.Domain.Entities.Guarderia", "Guarderia")
                        .WithMany("Actividades")
                        .HasForeignKey("GuarderiaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Guarderia");
                });

            modelBuilder.Entity("GestordeGuarderias.Domain.Entities.ActividadNino", b =>
                {
                    b.HasOne("GestordeGuarderias.Domain.Entities.Actividad", "Actividad")
                        .WithMany("ActividadesNinos")
                        .HasForeignKey("ActividadId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("GestordeGuarderias.Domain.Entities.Nino", "Nino")
                        .WithMany("ActividadesNinos")
                        .HasForeignKey("NinoId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Actividad");

                    b.Navigation("Nino");
                });

            modelBuilder.Entity("GestordeGuarderias.Domain.Entities.Asistencia", b =>
                {
                    b.HasOne("GestordeGuarderias.Domain.Entities.Guarderia", "Guarderia")
                        .WithMany("Asistencias")
                        .HasForeignKey("GuarderiaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("GestordeGuarderias.Domain.Entities.Nino", "Nino")
                        .WithMany("Asistencias")
                        .HasForeignKey("NinoId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Guarderia");

                    b.Navigation("Nino");
                });

            modelBuilder.Entity("GestordeGuarderias.Domain.Entities.Mensaje", b =>
                {
                    b.HasOne("GestordeGuarderias.Domain.Entities.Guarderia", "Guarderia")
                        .WithMany("Mensajes")
                        .HasForeignKey("GuarderiaId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("GestordeGuarderias.Domain.Entities.Nino", "Nino")
                        .WithMany("Mensajes")
                        .HasForeignKey("NinoId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("GestordeGuarderias.Domain.Entities.Tutor", "Tutor")
                        .WithMany("Mensajes")
                        .HasForeignKey("TutorId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Guarderia");

                    b.Navigation("Nino");

                    b.Navigation("Tutor");
                });

            modelBuilder.Entity("GestordeGuarderias.Domain.Entities.Nino", b =>
                {
                    b.HasOne("GestordeGuarderias.Domain.Entities.Guarderia", "Guarderia")
                        .WithMany("Ninos")
                        .HasForeignKey("GuarderiaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("GestordeGuarderias.Domain.Entities.Tutor", "Tutor")
                        .WithMany("Ninos")
                        .HasForeignKey("TutorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Guarderia");

                    b.Navigation("Tutor");
                });

            modelBuilder.Entity("GestordeGuarderias.Domain.Entities.Pago", b =>
                {
                    b.HasOne("GestordeGuarderias.Domain.Entities.Guarderia", "Guarderia")
                        .WithMany("Pagos")
                        .HasForeignKey("GuarderiaId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("GestordeGuarderias.Domain.Entities.Nino", "Nino")
                        .WithMany("Pagos")
                        .HasForeignKey("NinoId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("GestordeGuarderias.Domain.Entities.Tutor", "Tutor")
                        .WithMany("Pagos")
                        .HasForeignKey("TutorId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Guarderia");

                    b.Navigation("Nino");

                    b.Navigation("Tutor");
                });

            modelBuilder.Entity("GestordeGuarderias.Domain.Entities.Actividad", b =>
                {
                    b.Navigation("ActividadesNinos");
                });

            modelBuilder.Entity("GestordeGuarderias.Domain.Entities.Guarderia", b =>
                {
                    b.Navigation("Actividades");

                    b.Navigation("Asistencias");

                    b.Navigation("Mensajes");

                    b.Navigation("Ninos");

                    b.Navigation("Pagos");
                });

            modelBuilder.Entity("GestordeGuarderias.Domain.Entities.Nino", b =>
                {
                    b.Navigation("ActividadesNinos");

                    b.Navigation("Asistencias");

                    b.Navigation("Mensajes");

                    b.Navigation("Pagos");
                });

            modelBuilder.Entity("GestordeGuarderias.Domain.Entities.Tutor", b =>
                {
                    b.Navigation("Mensajes");

                    b.Navigation("Ninos");

                    b.Navigation("Pagos");
                });
#pragma warning restore 612, 618
        }
    }
}
