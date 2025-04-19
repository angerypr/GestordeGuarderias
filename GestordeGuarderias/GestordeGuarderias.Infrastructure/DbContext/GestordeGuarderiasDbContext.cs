using Microsoft.EntityFrameworkCore;
using GestordeGuarderias.Domain.Entities;

namespace GestordeGuarderias.Infrastructure
{
    public class GestordeGuarderiasDbContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=DESKTOP-H3L6RGC;Database=GestordeGuarderiasDB;Trusted_Connection=True;TrustServerCertificate=True");
            }
        }
        public GestordeGuarderiasDbContext(DbContextOptions<GestordeGuarderiasDbContext> options) : base(options) { }
        public DbSet<Guarderia> Guarderias { get; set; }
        public DbSet<Tutor> Tutores { get; set; }
        public DbSet<Nino> Ninos { get; set; }
        public DbSet<Actividad> Actividades { get; set; }
        public DbSet<Asistencia> Asistencias { get; set; }
        public DbSet<Pago> Pagos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Nino>()
                .HasOne(n => n.Guarderia)
                .WithMany(g => g.Ninos)
                .HasForeignKey(n => n.GuarderiaId);

            modelBuilder.Entity<Asistencia>()
                .HasOne(a => a.Guarderia)
                .WithMany(g => g.Asistencias)
                .HasForeignKey(a => a.GuarderiaId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Actividad>()
                .HasOne(a => a.Guarderia)
                .WithMany(g => g.Actividades)
                .HasForeignKey(a => a.GuarderiaId)
                .OnDelete(DeleteBehavior.Cascade); 

            modelBuilder.Entity<Nino>()
                .HasOne(n => n.Tutor)
                .WithMany(t => t.Ninos)
                .HasForeignKey(n => n.TutorId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Asistencia>()
                .HasOne(a => a.Nino)
                .WithMany(n => n.Asistencias)
                .HasForeignKey(a => a.NinoId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Pago>()
                .HasOne(p => p.Nino)
                .WithMany(n => n.Pagos)
                .HasForeignKey(p => p.NinoId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Pago>()
                .HasOne(p => p.Tutor)
                .WithMany(t => t.Pagos)
                .HasForeignKey(p => p.TutorId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Pago>()
                .HasOne(p => p.Guarderia)
                .WithMany(g => g.Pagos)
                .HasForeignKey(p => p.GuarderiaId)
                .OnDelete(DeleteBehavior.Restrict);

            //Configuraciones para tutor
            modelBuilder.Entity<Tutor>(entity =>
            {
                entity.Property(t => t.Nombre)
                      .IsRequired()
                      .HasMaxLength(50);

                entity.Property(t => t.Apellido)
                      .IsRequired()
                      .HasMaxLength(50);

                entity.Property(t => t.Telefono)
                      .IsRequired()
                      .HasMaxLength(13);

                entity.Property(t => t.Cedula)
                      .IsRequired()             
                      .HasMaxLength(13);         

                entity.HasIndex(t => t.Cedula)
                      .IsUnique();

                entity.Property(t => t.CorreoElectronico)
                      .IsRequired()
                      .HasMaxLength(100);
            });

            //Configuraciones Nino
            modelBuilder.Entity<Nino>(entity =>
            {
                entity.Property(t => t.Nombre)
                      .IsRequired()
                      .HasMaxLength(50);

                entity.Property(t => t.Apellido)
                      .IsRequired()
                      .HasMaxLength(50);

                entity.Property(t => t.FechaNacimiento)
                      .IsRequired();
            });

            //Configuraciones actividad
            modelBuilder.Entity<Actividad>(entity =>
                {
                    entity.Property(t => t.Nombre)
                          .IsRequired()
                          .HasMaxLength(100);

                    entity.Property(t => t.Descripcion)
                          .HasMaxLength(300);

                    entity.Property(a => a.Fecha)
                          .IsRequired();

                    entity.Property(a => a.Hora)
                          .IsRequired();
            });

            //Configuraciones asistencia
            modelBuilder.Entity<Asistencia>(entity =>
            {
                entity.Property(a => a.Fecha)
                      .IsRequired();

                entity.Property(a => a.Presente)
                      .IsRequired();
            });

            //Configuraciones de gaurderia
            modelBuilder.Entity<Guarderia>(entity =>
            {
                entity.Property(g => g.Nombre)
                    .IsRequired()
                    .HasMaxLength(200);

                entity.Property(g => g.Direccion)
                    .IsRequired()
                    .HasMaxLength(300);

                entity.Property(g => g.Telefono)
                    .IsRequired()
                    .HasMaxLength(15);
            });

            //Configuraciones pago
            modelBuilder.Entity<Pago>(entity =>
            {
                entity.Property(p => p.Monto)
                    .IsRequired()
                    .HasPrecision(18, 2);

                entity.Property(p => p.Fecha)
                    .IsRequired();
            });
        }
    }
}
