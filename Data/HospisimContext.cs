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






            // 1. IDs para Especialidades
            var cardiologiaId = Guid.NewGuid();
            var pediatriaId = Guid.NewGuid();
            var ortopediaId = Guid.NewGuid();
            var dermatologiaId = Guid.NewGuid();
            var neurologiaId = Guid.NewGuid();

            // 2. IDs para Pacientes
            var pacienteIds = new Guid[10];
            for (int i = 0; i < 10; i++) pacienteIds[i] = Guid.NewGuid();

            // 3. Seed de Especialidades
            modelBuilder.Entity<Especialidade>().HasData(
                new Especialidade { Id = cardiologiaId, Nome = "Cardiologia" },
                new Especialidade { Id = pediatriaId, Nome = "Pediatria" },
                new Especialidade { Id = ortopediaId, Nome = "Ortopedia" },
                new Especialidade { Id = dermatologiaId, Nome = "Dermatologia" },
                new Especialidade { Id = neurologiaId, Nome = "Neurologia" }
            );

            // 4. Seed de Pacientes (10 registros)
            modelBuilder.Entity<Paciente>().HasData(
                new Paciente { Id = pacienteIds[0], NomeCompleto = "Carlos Eduardo Lima", CPF = "111.222.333-01", DataNascimento = new DateTime(1985, 3, 15), Sexo = "Masculino", TipoSanguineo = "A+", Telefone = "11987654321", Email = "carlos.lima@example.com", EnderecoCompleto = "Rua das Flores, 101, São Paulo, SP", NumeroCartaoSUS = "123456789012345", EstadoCivil = "Casado", PossuiPlanoSaude = true },
                new Paciente { Id = pacienteIds[1], NomeCompleto = "Ana Beatriz Costa", CPF = "222.333.444-02", DataNascimento = new DateTime(1992, 7, 22), Sexo = "Feminino", TipoSanguineo = "O-", Telefone = "21912345678", Email = "ana.costa@example.com", EnderecoCompleto = "Av. Principal, 202, Rio de Janeiro, RJ", NumeroCartaoSUS = "234567890123456", EstadoCivil = "Solteira", PossuiPlanoSaude = false },
                new Paciente { Id = pacienteIds[2], NomeCompleto = "João Pedro Martins", CPF = "333.444.555-03", DataNascimento = new DateTime(2018, 1, 30), Sexo = "Masculino", TipoSanguineo = "B+", Telefone = "31923456789", Email = "joao.martins@example.com", EnderecoCompleto = "Rua da Paz, 303, Belo Horizonte, MG", NumeroCartaoSUS = "345678901234567", EstadoCivil = "Solteiro", PossuiPlanoSaude = true },
                new Paciente { Id = pacienteIds[3], NomeCompleto = "Mariana Fernandes Alves", CPF = "444.555.666-04", DataNascimento = new DateTime(1976, 11, 5), Sexo = "Feminino", TipoSanguineo = "AB+", Telefone = "41934567890", Email = "mariana.alves@example.com", EnderecoCompleto = "Travessa dos Sonhos, 404, Curitiba, PR", NumeroCartaoSUS = "456789012345678", EstadoCivil = "Divorciada", PossuiPlanoSaude = true },
                new Paciente { Id = pacienteIds[4], NomeCompleto = "Lucas Gabriel Souza", CPF = "555.666.777-05", DataNascimento = new DateTime(2001, 9, 12), Sexo = "Masculino", TipoSanguineo = "A-", Telefone = "51945678901", Email = "lucas.souza@example.com", EnderecoCompleto = "Alameda dos Anjos, 505, Porto Alegre, RS", NumeroCartaoSUS = "567890123456789", EstadoCivil = "Solteiro", PossuiPlanoSaude = false },
                new Paciente { Id = pacienteIds[5], NomeCompleto = "Julia Ribeiro Santos", CPF = "666.777.888-06", DataNascimento = new DateTime(1964, 2, 28), Sexo = "Feminino", TipoSanguineo = "O+", Telefone = "61956789012", Email = "julia.santos@example.com", EnderecoCompleto = "Praça Central, 606, Brasília, DF", NumeroCartaoSUS = "678901234567890", EstadoCivil = "Viúva", PossuiPlanoSaude = false },
                new Paciente { Id = pacienteIds[6], NomeCompleto = "Rafael Oliveira Lima", CPF = "777.888.999-07", DataNascimento = new DateTime(1988, 8, 18), Sexo = "Masculino", TipoSanguineo = "B-", Telefone = "71967890123", Email = "rafael.lima@example.com", EnderecoCompleto = "Av. Beira Mar, 707, Salvador, BA", NumeroCartaoSUS = "789012345678901", EstadoCivil = "Casado", PossuiPlanoSaude = true },
                new Paciente { Id = pacienteIds[7], NomeCompleto = "Isabela Pereira Gomes", CPF = "888.999.000-08", DataNascimento = new DateTime(1999, 4, 25), Sexo = "Feminino", TipoSanguineo = "AB-", Telefone = "81978901234", Email = "isabela.gomes@example.com", EnderecoCompleto = "Rua do Sol, 808, Recife, PE", NumeroCartaoSUS = "890123456789012", EstadoCivil = "Solteira", PossuiPlanoSaude = false },
                new Paciente { Id = pacienteIds[8], NomeCompleto = "Mateus Correia Rocha", CPF = "999.000.111-09", DataNascimento = new DateTime(1955, 6, 10), Sexo = "Masculino", TipoSanguineo = "A+", Telefone = "91989012345", Email = "mateus.rocha@example.com", EnderecoCompleto = "Largo das Neves, 909, Belém, PA", NumeroCartaoSUS = "901234567890123", EstadoCivil = "Casado", PossuiPlanoSaude = true },
                new Paciente { Id = pacienteIds[9], NomeCompleto = "Laura Azevedo Cunha", CPF = "000.111.222-10", DataNascimento = new DateTime(2005, 12, 1), Sexo = "Feminino", TipoSanguineo = "O+", Telefone = "85990123456", Email = "laura.cunha@example.com", EnderecoCompleto = "Viela da Lua, 1010, Fortaleza, CE", NumeroCartaoSUS = "012345678901234", EstadoCivil = "Solteira", PossuiPlanoSaude = true }
            );

            // 5. IDs para Profissionais e Prontuários
            var profissionalIds = new Guid[10];
            for (int i = 0; i < 10; i++) profissionalIds[i] = Guid.NewGuid();

            var prontuarioIds = new Guid[10];
            for (int i = 0; i < 10; i++) prontuarioIds[i] = Guid.NewGuid();

            // 6. Seed de Profissionais de Saúde (10 registros)
            modelBuilder.Entity<ProfissionalDeSaude>().HasData(
                new ProfissionalDeSaude { Id = profissionalIds[0], NomeCompleto = "Dra. Helena Corrêa", CPF = "123.456.789-01", Email = "helena.correa@hospisim.com", Telefone = "11991112222", RegistroConselho = "CRM-SP 111111", TipoRegistro = "CRM", EspecialidadeId = cardiologiaId, DataAdmissao = new DateTime(2019, 5, 20), CargaHorariaSemanal = 40, Turno = "Manhã", Ativo = true },
                new ProfissionalDeSaude { Id = profissionalIds[1], NomeCompleto = "Dr. Bruno Faria", CPF = "123.456.789-02", Email = "bruno.faria@hospisim.com", Telefone = "21992223333", RegistroConselho = "CRM-RJ 222222", TipoRegistro = "CRM", EspecialidadeId = pediatriaId, DataAdmissao = new DateTime(2020, 2, 15), CargaHorariaSemanal = 36, Turno = "Tarde", Ativo = true },
                new ProfissionalDeSaude { Id = profissionalIds[2], NomeCompleto = "Enf. Lúcia Mendes", CPF = "123.456.789-03", Email = "lucia.mendes@hospisim.com", Telefone = "31993334444", RegistroConselho = "COREN-MG 333333", TipoRegistro = "COREN", EspecialidadeId = pediatriaId, DataAdmissao = new DateTime(2018, 10, 1), CargaHorariaSemanal = 40, Turno = "Noite", Ativo = true },
                new ProfissionalDeSaude { Id = profissionalIds[3], NomeCompleto = "Dr. Ricardo Nunes", CPF = "123.456.789-04", Email = "ricardo.nunes@hospisim.com", Telefone = "41994445555", RegistroConselho = "CRM-PR 444444", TipoRegistro = "CRM", EspecialidadeId = ortopediaId, DataAdmissao = new DateTime(2022, 1, 30), CargaHorariaSemanal = 44, Turno = "Integral", Ativo = true },
                new ProfissionalDeSaude { Id = profissionalIds[4], NomeCompleto = "Dra. Patrícia Moreira", CPF = "123.456.789-05", Email = "patricia.moreira@hospisim.com", Telefone = "51995556666", RegistroConselho = "CRM-RS 555555", TipoRegistro = "CRM", EspecialidadeId = dermatologiaId, DataAdmissao = new DateTime(2021, 8, 5), CargaHorariaSemanal = 20, Turno = "Tarde", Ativo = true },
                new ProfissionalDeSaude { Id = profissionalIds[5], NomeCompleto = "Dr. Fábio Gusmão", CPF = "123.456.789-06", Email = "fabio.gusmao@hospisim.com", Telefone = "61996667777", RegistroConselho = "CRM-DF 666666", TipoRegistro = "CRM", EspecialidadeId = neurologiaId, DataAdmissao = new DateTime(2017, 11, 11), CargaHorariaSemanal = 40, Turno = "Manhã", Ativo = false },
                new ProfissionalDeSaude { Id = profissionalIds[6], NomeCompleto = "Enf. Roberto Dias", CPF = "123.456.789-07", Email = "roberto.dias@hospisim.com", Telefone = "71997778888", RegistroConselho = "COREN-BA 777777", TipoRegistro = "COREN", EspecialidadeId = cardiologiaId, DataAdmissao = new DateTime(2023, 4, 14), CargaHorariaSemanal = 40, Turno = "Noite", Ativo = true },
                new ProfissionalDeSaude { Id = profissionalIds[7], NomeCompleto = "Dra. Simone Braga", CPF = "123.456.789-08", Email = "simone.braga@hospisim.com", Telefone = "81998889999", RegistroConselho = "CRM-PE 888888", TipoRegistro = "CRM", EspecialidadeId = dermatologiaId, DataAdmissao = new DateTime(2022, 6, 22), CargaHorariaSemanal = 30, Turno = "Tarde", Ativo = true },
                new ProfissionalDeSaude { Id = profissionalIds[8], NomeCompleto = "Dr. André Villas", CPF = "123.456.789-09", Email = "andre.villas@hospisim.com", Telefone = "91999990000", RegistroConselho = "CRM-PA 999999", TipoRegistro = "CRM", EspecialidadeId = ortopediaId, DataAdmissao = new DateTime(2023, 2, 2), CargaHorariaSemanal = 40, Turno = "Integral", Ativo = true },
                new ProfissionalDeSaude { Id = profissionalIds[9], NomeCompleto = "Dr. Marcos Telles", CPF = "123.456.789-10", Email = "marcos.telles@hospisim.com", Telefone = "85990001111", RegistroConselho = "CRM-CE 101010", TipoRegistro = "CRM", EspecialidadeId = neurologiaId, DataAdmissao = new DateTime(2020, 7, 7), CargaHorariaSemanal = 25, Turno = "Manhã", Ativo = true }
            );

            // 7. Seed de Prontuários (um para cada paciente)
            modelBuilder.Entity<Prontuario>().HasData(
                new Prontuario { Id = prontuarioIds[0], PacienteId = pacienteIds[0], NumeroProntuario = "PRONT-00001", DataAbertura = new DateTime(2023, 1, 10), ObservacoesGerais = "Paciente com histórico de hipertensão." },
                new Prontuario { Id = prontuarioIds[1], PacienteId = pacienteIds[1], NumeroProntuario = "PRONT-00002", DataAbertura = new DateTime(2023, 1, 15), ObservacoesGerais = "Alergia a penicilina." },
                new Prontuario { Id = prontuarioIds[2], PacienteId = pacienteIds[2], NumeroProntuario = "PRONT-00003", DataAbertura = new DateTime(2023, 2, 20), ObservacoesGerais = "Acompanhamento pediátrico regular." },
                new Prontuario { Id = prontuarioIds[3], PacienteId = pacienteIds[3], NumeroProntuario = "PRONT-00004", DataAbertura = new DateTime(2023, 3, 5), ObservacoesGerais = "" },
                new Prontuario { Id = prontuarioIds[4], PacienteId = pacienteIds[4], NumeroProntuario = "PRONT-00005", DataAbertura = new DateTime(2023, 3, 12), ObservacoesGerais = "Asma crônica." },
                new Prontuario { Id = prontuarioIds[5], PacienteId = pacienteIds[5], NumeroProntuario = "PRONT-00006", DataAbertura = new DateTime(2023, 4, 1), ObservacoesGerais = "Diabetes tipo 2." },
                new Prontuario { Id = prontuarioIds[6], PacienteId = pacienteIds[6], NumeroProntuario = "PRONT-00007", DataAbertura = new DateTime(2023, 4, 18), ObservacoesGerais = "" },
                new Prontuario { Id = prontuarioIds[7], PacienteId = pacienteIds[7], NumeroProntuario = "PRONT-00008", DataAbertura = new DateTime(2023, 5, 2), ObservacoesGerais = "Tratamento dermatológico em andamento." },
                new Prontuario { Id = prontuarioIds[8], PacienteId = pacienteIds[8], NumeroProntuario = "PRONT-00009", DataAbertura = new DateTime(2023, 5, 25), ObservacoesGerais = "Histórico de enxaqueca." },
                new Prontuario { Id = prontuarioIds[9], PacienteId = pacienteIds[9], NumeroProntuario = "PRONT-00010", DataAbertura = new DateTime(2023, 6, 8), ObservacoesGerais = "Acompanhamento ortopédico." }

            );

            // 8. IDs para os Atendimentos
            var atendimentoIds = new Guid[15];
            for (int i = 0; i < 15; i++) atendimentoIds[i] = Guid.NewGuid();

            // 9. Seed de Atendimentos (15 registros para criar um histórico rico)
            modelBuilder.Entity<Atendimento>().HasData(
                // Paciente 1 (Carlos) com o Cardiologista (Dra. Helena)
                new Atendimento { Id = atendimentoIds[0], PacienteId = pacienteIds[0], ProfissionalId = profissionalIds[0], ProntuarioId = prontuarioIds[0], DataHora = new DateTime(2024, 10, 5, 10, 0, 0), Tipo = "Consulta", Status = "Realizado", Local = "Consultório 101" },
                new Atendimento { Id = atendimentoIds[1], PacienteId = pacienteIds[0], ProfissionalId = profissionalIds[0], ProntuarioId = prontuarioIds[0], DataHora = new DateTime(2025, 3, 1, 10, 0, 0), Tipo = "Retorno", Status = "Realizado", Local = "Consultório 101" },

                // Paciente 2 (Ana) com a Dermatologista (Dra. Patrícia)
                new Atendimento { Id = atendimentoIds[2], PacienteId = pacienteIds[1], ProfissionalId = profissionalIds[4], ProntuarioId = prontuarioIds[1], DataHora = new DateTime(2025, 4, 12, 15, 0, 0), Tipo = "Primeira Consulta", Status = "Realizado", Local = "Dermatologia - Sala 3" },
                new Atendimento { Id = atendimentoIds[3], PacienteId = pacienteIds[1], ProfissionalId = profissionalIds[4], ProntuarioId = prontuarioIds[1], DataHora = new DateTime(2025, 6, 20, 15, 30, 0), Tipo = "Retorno", Status = "Agendado", Local = "Dermatologia - Sala 3" },

                // Paciente 3 (João, criança) com o Pediatra (Dr. Bruno)
                new Atendimento { Id = atendimentoIds[4], PacienteId = pacienteIds[2], ProfissionalId = profissionalIds[1], ProntuarioId = prontuarioIds[2], DataHora = new DateTime(2025, 5, 5, 9, 0, 0), Tipo = "Consulta Pediátrica", Status = "Realizado", Local = "Pediatria - Sala 1" },

                // Paciente 4 (Mariana) com o Ortopedista (Dr. Ricardo)
                new Atendimento { Id = atendimentoIds[5], PacienteId = pacienteIds[3], ProfissionalId = profissionalIds[3], ProntuarioId = prontuarioIds[3], DataHora = new DateTime(2025, 5, 10, 11, 0, 0), Tipo = "Emergência", Status = "Realizado", Local = "Emergência - Leito 5" },

                // Paciente 5 (Lucas) com o Neurologista (Dr. Fábio, que está inativo)
                new Atendimento { Id = atendimentoIds[6], PacienteId = pacienteIds[4], ProfissionalId = profissionalIds[5], ProntuarioId = prontuarioIds[4], DataHora = new DateTime(2024, 1, 20, 16, 0, 0), Tipo = "Consulta", Status = "Cancelado", Local = "Consultório 202" },

                // Mais atendimentos para dar volume e testar os fluxos
                new Atendimento { Id = atendimentoIds[7], PacienteId = pacienteIds[5], ProfissionalId = profissionalIds[8], ProntuarioId = prontuarioIds[5], DataHora = new DateTime(2025, 6, 1, 8, 0, 0), Tipo = "Consulta Ortopédica", Status = "Agendado", Local = "Ortopedia - Sala 2" },
                new Atendimento { Id = atendimentoIds[8], PacienteId = pacienteIds[6], ProfissionalId = profissionalIds[0], ProntuarioId = prontuarioIds[6], DataHora = new DateTime(2025, 4, 30, 14, 0, 0), Tipo = "Check-up Cardiológico", Status = "Realizado", Local = "Cardiologia - Sala de Exames" },
                new Atendimento { Id = atendimentoIds[9], PacienteId = pacienteIds[7], ProfissionalId = profissionalIds[7], ProntuarioId = prontuarioIds[7], DataHora = new DateTime(2025, 6, 15, 13, 20, 0), Tipo = "Consulta", Status = "Agendado", Local = "Dermatologia - Sala 1" },
                new Atendimento { Id = atendimentoIds[10], PacienteId = pacienteIds[8], ProfissionalId = profissionalIds[5], ProntuarioId = prontuarioIds[8], DataHora = new DateTime(2025, 2, 10, 10, 40, 0), Tipo = "Consulta", Status = "Cancelado", Local = "Consultório 202" },
                new Atendimento { Id = atendimentoIds[11], PacienteId = pacienteIds[9], ProfissionalId = profissionalIds[3], ProntuarioId = prontuarioIds[9], DataHora = new DateTime(2025, 5, 22, 18, 0, 0), Tipo = "Retorno", Status = "Realizado", Local = "Ortopedia - Sala 5" },
                new Atendimento { Id = atendimentoIds[12], PacienteId = pacienteIds[1], ProfissionalId = profissionalIds[2], ProntuarioId = prontuarioIds[1], DataHora = new DateTime(2025, 3, 3, 19, 0, 0), Tipo = "Atendimento de Enfermagem", Status = "Realizado", Local = "Triagem" },
                new Atendimento { Id = atendimentoIds[13], PacienteId = pacienteIds[2], ProfissionalId = profissionalIds[1], ProntuarioId = prontuarioIds[2], DataHora = new DateTime(2025, 7, 1, 9, 0, 0), Tipo = "Consulta de Rotina", Status = "Agendado", Local = "Pediatria - Sala 1" },
                new Atendimento { Id = atendimentoIds[14], PacienteId = pacienteIds[0], ProfissionalId = profissionalIds[6], ProntuarioId = prontuarioIds[0], DataHora = new DateTime(2025, 6, 5, 22, 0, 0), Tipo = "Atendimento de Enfermagem", Status = "Realizado", Local = "UTI - Leito 3" }
            );

            // 10. IDs para as Internações
            var internacaoId1 = Guid.NewGuid();
            var internacaoId2 = Guid.NewGuid();

            // 11. Seed de Prescrições
            modelBuilder.Entity<Prescricao>().HasData(
                // Prescrição para o atendimento de cardiologia do Paciente 1
                new Prescricao { Id = Guid.NewGuid(), AtendimentoId = atendimentoIds[0], ProfissionalId = profissionalIds[0], Medicamento = "Sinvastatina 20mg", Dosagem = "1 comprimido", Frequencia = "1 vez ao dia, à noite", ViaAdministracao = "Oral", DataInicio = new DateTime(2024, 10, 5), StatusPrescricao = "Ativa" },
                // Prescrição para o atendimento pediátrico do Paciente 3
                new Prescricao { Id = Guid.NewGuid(), AtendimentoId = atendimentoIds[4], ProfissionalId = profissionalIds[1], Medicamento = "Amoxicilina 250mg/5ml", Dosagem = "5ml", Frequencia = "De 8 em 8 horas por 7 dias", ViaAdministracao = "Oral", DataInicio = new DateTime(2025, 5, 5), StatusPrescricao = "Encerrada" },
                // Prescrição para a emergência ortopédica do Paciente 4
                new Prescricao { Id = Guid.NewGuid(), AtendimentoId = atendimentoIds[5], ProfissionalId = profissionalIds[3], Medicamento = "Torsilax", Dosagem = "1 comprimido", Frequencia = "De 12 em 12 horas por 3 dias", ViaAdministracao = "Oral", DataInicio = new DateTime(2025, 5, 10), StatusPrescricao = "Ativa" },
                // Prescrição para o check-up do Paciente 7
                new Prescricao { Id = Guid.NewGuid(), AtendimentoId = atendimentoIds[8], ProfissionalId = profissionalIds[0], Medicamento = "AAS Infantil 100mg", Dosagem = "1 comprimido", Frequencia = "1 vez ao dia", ViaAdministracao = "Oral", DataInicio = new DateTime(2025, 4, 30), StatusPrescricao = "Ativa" }
            );

            // 12. Seed de Exames
            modelBuilder.Entity<Exame>().HasData(
                // Exame para o check-up do Paciente 7
                new Exame { Id = Guid.NewGuid(), AtendimentoId = atendimentoIds[8], Tipo = "Hemograma Completo", DataSolicitacao = new DateTime(2025, 4, 30), Status = "Resultado Disponível", DataRealizacao = new DateTime(2025, 5, 2), Resultado = "Todos os índices dentro do padrão de normalidade." },
                // Exame para a emergência ortopédica do Paciente 4
                new Exame { Id = Guid.NewGuid(), AtendimentoId = atendimentoIds[5], Tipo = "Raio-X do Tornozelo Esquerdo", DataSolicitacao = new DateTime(2025, 5, 10), Status = "Realizado", DataRealizacao = new DateTime(2025, 5, 10) },
                // Exame para a consulta de dermatologia da Paciente 2
                new Exame { Id = Guid.NewGuid(), AtendimentoId = atendimentoIds[2], Tipo = "Biópsia de pele", DataSolicitacao = new DateTime(2025, 4, 12), Status = "Solicitado" }
            );

            // 13. Seed de Internações
            modelBuilder.Entity<Internacao>().HasData(
                // Internação vinda da emergência ortopédica do Paciente 4
                new Internacao { Id = internacaoId1, AtendimentoId = atendimentoIds[5], DataEntrada = new DateTime(2025, 5, 10, 11, 30, 0), PrevisaoAlta = new DateTime(2025, 5, 12), MotivoInternacao = "Fratura no tornozelo esquerdo para observação e cirurgia.", Leito = "204-A", Quarto = "204", Setor = "Ortopedia", StatusInternacao = "Alta concedida", ObservacoesClinicas = "Paciente evoluindo bem no pós-operatório." },
                // Internação vinda do atendimento de enfermagem na UTI do Paciente 1
                new Internacao { Id = internacaoId2, AtendimentoId = atendimentoIds[14], DataEntrada = new DateTime(2025, 6, 5, 22, 5, 0), MotivoInternacao = "Monitoramento cardíaco pós-procedimento.", Leito = "UTI-02", Quarto = "UTI", Setor = "UTI", StatusInternacao = "Ativa", ObservacoesClinicas = "Sinais vitais estáveis." }
            );

            // 14. Seed de Alta Hospitalar
            modelBuilder.Entity<AltaHospitalar>().HasData(
                // Alta para a internação do Paciente 4
                new AltaHospitalar { Id = Guid.NewGuid(), InternacaoId = internacaoId1, DataAlta = new DateTime(2025, 5, 12, 14, 0, 0), CondicaoPaciente = "Estável, com recomendação de repouso e fisioterapia.", InstrucoesPosAlta = "Manter o pé elevado, evitar esforço por 15 dias e iniciar fisioterapia na próxima semana." }
            );

        }
    }
}