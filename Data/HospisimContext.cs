using Hospisim.Models;
using Microsoft.EntityFrameworkCore;

namespace Hospisim.Data
{
    public class HospisimContext : DbContext
    {
        public HospisimContext(DbContextOptions<HospisimContext> options) : base(options)
        {
        }

        // --- DbSets para todas as suas entidades ---
        public DbSet<Paciente> Pacientes { get; set; } = null!;
        public DbSet<ProfissionalDeSaude> ProfissionaisDeSaude { get; set; } = null!;
        public DbSet<Especialidade> Especialidades { get; set; } = null!;
        public DbSet<Prontuario> Prontuarios { get; set; } = null!;
        public DbSet<Atendimento> Atendimentos { get; set; } = null!;
        public DbSet<Prescricao> Prescricoes { get; set; } = null!;
        public DbSet<Exame> Exames { get; set; } = null!;
        public DbSet<Internacao> Internacoes { get; set; } = null!;
        public DbSet<AltaHospitalar> AltasHospitalares { get; set; } = null!;


        // --- Configuração dos Relacionamentos e Deleção em Cascata ---
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // -----------------------------------------------------------------
            // PARTE 1: CONFIGURAÇÃO DOS RELACIONAMENTOS (O que já fizemos)
            // -----------------------------------------------------------------

            modelBuilder.Entity<Atendimento>()
                .HasOne(a => a.Prontuario)
                .WithMany(p => p.Atendimentos)
                .HasForeignKey(a => a.ProntuarioId)
                .OnDelete(DeleteBehavior.Restrict);


            modelBuilder.Entity<Prescricao>()
                .HasOne(p => p.Profissional)
                .WithMany(pro => pro.Prescricoes)
                .HasForeignKey(p => p.ProfissionalId)
                .OnDelete(DeleteBehavior.Restrict);


            // -----------------------------------------------------------------
            // PARTE 2: POVOAMENTO INICIAL (DATA SEEDING)
            // -----------------------------------------------------------------

            // 1. Gerar IDs para as entidades de base
            var cardiologiaId = Guid.NewGuid();
            var pediatriaId = Guid.NewGuid();
            var ortopediaId = Guid.NewGuid();

            var paciente1Id = Guid.NewGuid();
            var paciente2Id = Guid.NewGuid();

            // 2. Seed de Especialidades
            modelBuilder.Entity<Especialidade>().HasData(
                new Especialidade { Id = cardiologiaId, Nome = "Cardiologia" },
                new Especialidade { Id = pediatriaId, Nome = "Pediatria" },
                new Especialidade { Id = ortopediaId, Nome = "Ortopedia" }
            );

            // 3. Seed de Pacientes
            modelBuilder.Entity<Paciente>().HasData(
                new Paciente
                {
                    Id = paciente1Id,
                    NomeCompleto = "João da Silva",
                    CPF = "111.222.333-44",
                    DataNascimento = new DateTime(1980, 5, 15),
                    Sexo = "Masculino",
                    TipoSanguineo = "O+",
                    Telefone = "(11) 98765-4321",
                    Email = "joao.silva@example.com",
                    EnderecoCompleto = "Rua das Flores, 123, São Paulo, SP",
                    NumeroCartaoSUS = "987654321098765",
                    EstadoCivil = "Casado",
                    PossuiPlanoSaude = true
                },
                new Paciente
                {
                    Id = paciente2Id,
                    NomeCompleto = "Maria Oliveira",
                    CPF = "222.333.444-55",
                    DataNascimento = new DateTime(1992, 10, 20),
                    Sexo = "Feminino",
                    TipoSanguineo = "A-",
                    Telefone = "(21) 91234-5678",
                    Email = "maria.oliveira@example.com",
                    EnderecoCompleto = "Avenida Principal, 456, Rio de Janeiro, RJ",
                    NumeroCartaoSUS = "123456789012345",
                    EstadoCivil = "Solteira",
                    PossuiPlanoSaude = false
                }
            );

            // 4. Seed de Profissionais de Saúde
            modelBuilder.Entity<ProfissionalDeSaude>().HasData(
                new ProfissionalDeSaude
                {
                    Id = Guid.NewGuid(),
                    NomeCompleto = "Dr. Carlos Andrade",
                    CPF = "333.444.555-66",
                    Email = "carlos.andrade@hospisim.com",
                    Telefone = "(11) 98888-7777",
                    RegistroConselho = "CRM-SP 123456",
                    TipoRegistro = "CRM",
                    EspecialidadeId = cardiologiaId, // <-- Relacionando com a especialidade criada
                    DataAdmissao = new DateTime(2020, 1, 10),
                    CargaHorariaSemanal = 40,
                    Turno = "Manhã",
                    Ativo = true
                },
                new ProfissionalDeSaude
                {
                    Id = Guid.NewGuid(),
                    NomeCompleto = "Dra. Ana Paula Souza",
                    CPF = "444.555.666-77",
                    Email = "ana.souza@hospisim.com",
                    Telefone = "(21) 97777-6666",
                    RegistroConselho = "CRM-RJ 654321",
                    TipoRegistro = "CRM",
                    EspecialidadeId = pediatriaId, // <-- Relacionando com a especialidade criada
                    DataAdmissao = new DateTime(2021, 3, 15),
                    CargaHorariaSemanal = 30,
                    Turno = "Tarde",
                    Ativo = true
                }
            );
        }
    }
}