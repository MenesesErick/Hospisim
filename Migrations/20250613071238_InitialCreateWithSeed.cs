using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Hospisim.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreateWithSeed : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Especialidades",
                keyColumn: "Id",
                keyValue: new Guid("21e051f6-14b8-4ad8-9c89-a028fa3b4453"));

            migrationBuilder.DeleteData(
                table: "Pacientes",
                keyColumn: "Id",
                keyValue: new Guid("3f5fd9f7-5ab6-4b0e-890c-768878cf304c"));

            migrationBuilder.DeleteData(
                table: "Pacientes",
                keyColumn: "Id",
                keyValue: new Guid("d1f9889a-ec87-4bfa-9498-1dfad1a85e87"));

            migrationBuilder.DeleteData(
                table: "ProfissionaisDeSaude",
                keyColumn: "Id",
                keyValue: new Guid("ece2dd0c-5852-4c54-b886-c2e2d6502696"));

            migrationBuilder.DeleteData(
                table: "ProfissionaisDeSaude",
                keyColumn: "Id",
                keyValue: new Guid("eeea9f91-41f3-465d-9324-0f0c8f42a004"));

            migrationBuilder.DeleteData(
                table: "Especialidades",
                keyColumn: "Id",
                keyValue: new Guid("00b0fc80-e429-4be6-a542-225faff625ff"));

            migrationBuilder.DeleteData(
                table: "Especialidades",
                keyColumn: "Id",
                keyValue: new Guid("5be4a08e-16fd-4794-bb89-d010afae7dbf"));

            migrationBuilder.InsertData(
                table: "Especialidades",
                columns: new[] { "Id", "Nome" },
                values: new object[,]
                {
                    { new Guid("03e408e2-49c7-49e0-aed1-049cf1c08f93"), "Cardiologia" },
                    { new Guid("3cfe9e0f-92a4-4960-b027-64babc353d60"), "Ortopedia" },
                    { new Guid("530848b2-54b5-4e8b-a725-2a2b566ea3c1"), "Dermatologia" },
                    { new Guid("c1805cf9-20fd-4478-8683-95008147f00d"), "Neurologia" },
                    { new Guid("f925f653-027f-43d3-9b13-198e853a9b2c"), "Pediatria" }
                });

            migrationBuilder.InsertData(
                table: "Pacientes",
                columns: new[] { "Id", "CPF", "DataNascimento", "Email", "EnderecoCompleto", "EstadoCivil", "NomeCompleto", "NumeroCartaoSUS", "PossuiPlanoSaude", "Sexo", "Telefone", "TipoSanguineo" },
                values: new object[,]
                {
                    { new Guid("54f4ef2f-d788-44f5-b4ea-a885ffc59597"), "888.999.000-08", new DateTime(1999, 4, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), "isabela.gomes@example.com", "Rua do Sol, 808, Recife, PE", "Solteira", "Isabela Pereira Gomes", "890123456789012", false, "Feminino", "81978901234", "AB-" },
                    { new Guid("5f4e24fc-fe07-4e87-a975-4240c490b40d"), "666.777.888-06", new DateTime(1964, 2, 28, 0, 0, 0, 0, DateTimeKind.Unspecified), "julia.santos@example.com", "Praça Central, 606, Brasília, DF", "Viúva", "Julia Ribeiro Santos", "678901234567890", false, "Feminino", "61956789012", "O+" },
                    { new Guid("6d508ef8-d548-4049-9a36-f29402b364ae"), "333.444.555-03", new DateTime(2018, 1, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), "joao.martins@example.com", "Rua da Paz, 303, Belo Horizonte, MG", "Solteiro", "João Pedro Martins", "345678901234567", true, "Masculino", "31923456789", "B+" },
                    { new Guid("7b2b6527-66f4-4304-b8c4-4ef12dd7a4c3"), "222.333.444-02", new DateTime(1992, 7, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), "ana.costa@example.com", "Av. Principal, 202, Rio de Janeiro, RJ", "Solteira", "Ana Beatriz Costa", "234567890123456", false, "Feminino", "21912345678", "O-" },
                    { new Guid("8f9fb19c-8987-4816-bbbd-bd3dea802f68"), "000.111.222-10", new DateTime(2005, 12, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "laura.cunha@example.com", "Viela da Lua, 1010, Fortaleza, CE", "Solteira", "Laura Azevedo Cunha", "012345678901234", true, "Feminino", "85990123456", "O+" },
                    { new Guid("97269dd1-6c31-41d1-b4c7-de9e9ff12fa4"), "555.666.777-05", new DateTime(2001, 9, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), "lucas.souza@example.com", "Alameda dos Anjos, 505, Porto Alegre, RS", "Solteiro", "Lucas Gabriel Souza", "567890123456789", false, "Masculino", "51945678901", "A-" },
                    { new Guid("a607bd61-5581-47bf-9d83-527c8935e4d0"), "444.555.666-04", new DateTime(1976, 11, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), "mariana.alves@example.com", "Travessa dos Sonhos, 404, Curitiba, PR", "Divorciada", "Mariana Fernandes Alves", "456789012345678", true, "Feminino", "41934567890", "AB+" },
                    { new Guid("c1ad208e-856f-4e4f-a076-935601580d92"), "999.000.111-09", new DateTime(1955, 6, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "mateus.rocha@example.com", "Largo das Neves, 909, Belém, PA", "Casado", "Mateus Correia Rocha", "901234567890123", true, "Masculino", "91989012345", "A+" },
                    { new Guid("ce888b9e-6b86-4a69-bc4e-17fcb11a5185"), "777.888.999-07", new DateTime(1988, 8, 18, 0, 0, 0, 0, DateTimeKind.Unspecified), "rafael.lima@example.com", "Av. Beira Mar, 707, Salvador, BA", "Casado", "Rafael Oliveira Lima", "789012345678901", true, "Masculino", "71967890123", "B-" },
                    { new Guid("dae56b6e-9a48-4349-b4f5-326186d3118f"), "111.222.333-01", new DateTime(1985, 3, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "carlos.lima@example.com", "Rua das Flores, 101, São Paulo, SP", "Casado", "Carlos Eduardo Lima", "123456789012345", true, "Masculino", "11987654321", "A+" }
                });

            migrationBuilder.InsertData(
                table: "ProfissionaisDeSaude",
                columns: new[] { "Id", "Ativo", "CPF", "CargaHorariaSemanal", "DataAdmissao", "Email", "EspecialidadeId", "NomeCompleto", "RegistroConselho", "Telefone", "TipoRegistro", "Turno" },
                values: new object[,]
                {
                    { new Guid("0cc07cee-6b24-413c-8418-0cf6823226e3"), true, "123.456.789-01", 40, new DateTime(2019, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), "helena.correa@hospisim.com", new Guid("03e408e2-49c7-49e0-aed1-049cf1c08f93"), "Dra. Helena Corrêa", "CRM-SP 111111", "11991112222", "CRM", "Manhã" },
                    { new Guid("17822355-7037-40e5-b324-4e3ec65b5e06"), true, "123.456.789-02", 36, new DateTime(2020, 2, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "bruno.faria@hospisim.com", new Guid("f925f653-027f-43d3-9b13-198e853a9b2c"), "Dr. Bruno Faria", "CRM-RJ 222222", "21992223333", "CRM", "Tarde" },
                    { new Guid("1b556ce0-1cfd-4af2-a630-ba9564c6e484"), true, "123.456.789-09", 40, new DateTime(2023, 2, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), "andre.villas@hospisim.com", new Guid("3cfe9e0f-92a4-4960-b027-64babc353d60"), "Dr. André Villas", "CRM-PA 999999", "91999990000", "CRM", "Integral" },
                    { new Guid("6d7fd56b-b109-43a4-8fa9-6bb0b0791969"), false, "123.456.789-06", 40, new DateTime(2017, 11, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), "fabio.gusmao@hospisim.com", new Guid("c1805cf9-20fd-4478-8683-95008147f00d"), "Dr. Fábio Gusmão", "CRM-DF 666666", "61996667777", "CRM", "Manhã" },
                    { new Guid("818cdb2b-59c5-456a-88fa-ad8de3030bbd"), true, "123.456.789-05", 20, new DateTime(2021, 8, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), "patricia.moreira@hospisim.com", new Guid("530848b2-54b5-4e8b-a725-2a2b566ea3c1"), "Dra. Patrícia Moreira", "CRM-RS 555555", "51995556666", "CRM", "Tarde" },
                    { new Guid("8457597c-08cf-4291-b591-00c145173389"), true, "123.456.789-07", 40, new DateTime(2023, 4, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), "roberto.dias@hospisim.com", new Guid("03e408e2-49c7-49e0-aed1-049cf1c08f93"), "Enf. Roberto Dias", "COREN-BA 777777", "71997778888", "COREN", "Noite" },
                    { new Guid("a4a9fb63-c9cb-4392-9d6b-a421a6300556"), true, "123.456.789-10", 25, new DateTime(2020, 7, 7, 0, 0, 0, 0, DateTimeKind.Unspecified), "marcos.telles@hospisim.com", new Guid("c1805cf9-20fd-4478-8683-95008147f00d"), "Dr. Marcos Telles", "CRM-CE 101010", "85990001111", "CRM", "Manhã" },
                    { new Guid("b9f1f1d0-46b8-4c17-a3be-88b7f161feb7"), true, "123.456.789-03", 40, new DateTime(2018, 10, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "lucia.mendes@hospisim.com", new Guid("f925f653-027f-43d3-9b13-198e853a9b2c"), "Enf. Lúcia Mendes", "COREN-MG 333333", "31993334444", "COREN", "Noite" },
                    { new Guid("c5cc31ba-c713-496f-b908-c64519ca6069"), true, "123.456.789-04", 44, new DateTime(2022, 1, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), "ricardo.nunes@hospisim.com", new Guid("3cfe9e0f-92a4-4960-b027-64babc353d60"), "Dr. Ricardo Nunes", "CRM-PR 444444", "41994445555", "CRM", "Integral" },
                    { new Guid("ff8ec356-17a5-4739-8135-ebe89f9b242f"), true, "123.456.789-08", 30, new DateTime(2022, 6, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), "simone.braga@hospisim.com", new Guid("530848b2-54b5-4e8b-a725-2a2b566ea3c1"), "Dra. Simone Braga", "CRM-PE 888888", "81998889999", "CRM", "Tarde" }
                });

            migrationBuilder.InsertData(
                table: "Prontuarios",
                columns: new[] { "Id", "DataAbertura", "NumeroProntuario", "ObservacoesGerais", "PacienteId" },
                values: new object[,]
                {
                    { new Guid("0ccfef26-a34e-4403-ab2c-1a94ffcd0f25"), new DateTime(2023, 1, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "PRONT-00001", "Paciente com histórico de hipertensão.", new Guid("dae56b6e-9a48-4349-b4f5-326186d3118f") },
                    { new Guid("1050beea-d5c6-44d6-a3fe-204ae095ec4f"), new DateTime(2023, 6, 8, 0, 0, 0, 0, DateTimeKind.Unspecified), "PRONT-00010", "Acompanhamento ortopédico.", new Guid("8f9fb19c-8987-4816-bbbd-bd3dea802f68") },
                    { new Guid("1b020965-722d-4a76-a77d-0312e637ad3a"), new DateTime(2023, 3, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), "PRONT-00005", "Asma crônica.", new Guid("97269dd1-6c31-41d1-b4c7-de9e9ff12fa4") },
                    { new Guid("2d461a0d-c2fe-4598-a6c5-45b53d34dae9"), new DateTime(2023, 5, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), "PRONT-00008", "Tratamento dermatológico em andamento.", new Guid("54f4ef2f-d788-44f5-b4ea-a885ffc59597") },
                    { new Guid("77e05cb5-444d-4486-8f66-1d88e2274ffc"), new DateTime(2023, 2, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), "PRONT-00003", "Acompanhamento pediátrico regular.", new Guid("6d508ef8-d548-4049-9a36-f29402b364ae") },
                    { new Guid("c63bba87-eafc-40b5-b7e0-2854d7c19fe4"), new DateTime(2023, 5, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), "PRONT-00009", "Histórico de enxaqueca.", new Guid("c1ad208e-856f-4e4f-a076-935601580d92") },
                    { new Guid("cfd7b580-ba91-40be-b6b0-eb2ec335dd4a"), new DateTime(2023, 3, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), "PRONT-00004", "", new Guid("a607bd61-5581-47bf-9d83-527c8935e4d0") },
                    { new Guid("d3d01ef6-c399-454f-936b-73134234bb54"), new DateTime(2023, 1, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "PRONT-00002", "Alergia a penicilina.", new Guid("7b2b6527-66f4-4304-b8c4-4ef12dd7a4c3") },
                    { new Guid("ec41ff48-4bdc-4582-bf57-bcc440af6976"), new DateTime(2023, 4, 18, 0, 0, 0, 0, DateTimeKind.Unspecified), "PRONT-00007", "", new Guid("ce888b9e-6b86-4a69-bc4e-17fcb11a5185") },
                    { new Guid("f413cfce-0b29-4dd7-98c6-20a7ef673a43"), new DateTime(2023, 4, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "PRONT-00006", "Diabetes tipo 2.", new Guid("5f4e24fc-fe07-4e87-a975-4240c490b40d") }
                });

            migrationBuilder.InsertData(
                table: "Atendimentos",
                columns: new[] { "Id", "DataHora", "Local", "PacienteId", "ProfissionalId", "ProntuarioId", "Status", "Tipo" },
                values: new object[,]
                {
                    { new Guid("045f3192-90f2-4eb7-87a5-47c495f407c0"), new DateTime(2025, 5, 10, 11, 0, 0, 0, DateTimeKind.Unspecified), "Emergência - Leito 5", new Guid("a607bd61-5581-47bf-9d83-527c8935e4d0"), new Guid("c5cc31ba-c713-496f-b908-c64519ca6069"), new Guid("cfd7b580-ba91-40be-b6b0-eb2ec335dd4a"), "Realizado", "Emergência" },
                    { new Guid("066103d7-cc6a-4cea-98b7-d6defbebe777"), new DateTime(2025, 6, 15, 13, 20, 0, 0, DateTimeKind.Unspecified), "Dermatologia - Sala 1", new Guid("54f4ef2f-d788-44f5-b4ea-a885ffc59597"), new Guid("ff8ec356-17a5-4739-8135-ebe89f9b242f"), new Guid("2d461a0d-c2fe-4598-a6c5-45b53d34dae9"), "Agendado", "Consulta" },
                    { new Guid("1277c3ba-3794-4964-8512-91bdddc6e986"), new DateTime(2025, 3, 3, 19, 0, 0, 0, DateTimeKind.Unspecified), "Triagem", new Guid("7b2b6527-66f4-4304-b8c4-4ef12dd7a4c3"), new Guid("b9f1f1d0-46b8-4c17-a3be-88b7f161feb7"), new Guid("d3d01ef6-c399-454f-936b-73134234bb54"), "Realizado", "Atendimento de Enfermagem" },
                    { new Guid("20541787-e695-4258-bb81-f5702cefe378"), new DateTime(2025, 6, 1, 8, 0, 0, 0, DateTimeKind.Unspecified), "Ortopedia - Sala 2", new Guid("5f4e24fc-fe07-4e87-a975-4240c490b40d"), new Guid("1b556ce0-1cfd-4af2-a630-ba9564c6e484"), new Guid("f413cfce-0b29-4dd7-98c6-20a7ef673a43"), "Agendado", "Consulta Ortopédica" },
                    { new Guid("3a2d2590-e382-4ec2-920c-a0259d3eecb6"), new DateTime(2025, 2, 10, 10, 40, 0, 0, DateTimeKind.Unspecified), "Consultório 202", new Guid("c1ad208e-856f-4e4f-a076-935601580d92"), new Guid("6d7fd56b-b109-43a4-8fa9-6bb0b0791969"), new Guid("c63bba87-eafc-40b5-b7e0-2854d7c19fe4"), "Cancelado", "Consulta" },
                    { new Guid("59c8e80b-e057-4be8-8545-6282a18da3a1"), new DateTime(2025, 6, 5, 22, 0, 0, 0, DateTimeKind.Unspecified), "UTI - Leito 3", new Guid("dae56b6e-9a48-4349-b4f5-326186d3118f"), new Guid("8457597c-08cf-4291-b591-00c145173389"), new Guid("0ccfef26-a34e-4403-ab2c-1a94ffcd0f25"), "Realizado", "Atendimento de Enfermagem" },
                    { new Guid("6eaf4ab6-0f45-415c-9a60-1a79c9c3aa20"), new DateTime(2025, 3, 1, 10, 0, 0, 0, DateTimeKind.Unspecified), "Consultório 101", new Guid("dae56b6e-9a48-4349-b4f5-326186d3118f"), new Guid("0cc07cee-6b24-413c-8418-0cf6823226e3"), new Guid("0ccfef26-a34e-4403-ab2c-1a94ffcd0f25"), "Realizado", "Retorno" },
                    { new Guid("7928ac45-23e9-4c77-8dc3-d17e46bb04c9"), new DateTime(2024, 1, 20, 16, 0, 0, 0, DateTimeKind.Unspecified), "Consultório 202", new Guid("97269dd1-6c31-41d1-b4c7-de9e9ff12fa4"), new Guid("6d7fd56b-b109-43a4-8fa9-6bb0b0791969"), new Guid("1b020965-722d-4a76-a77d-0312e637ad3a"), "Cancelado", "Consulta" },
                    { new Guid("7930304d-ec2c-49c3-a345-6a56bdd2043c"), new DateTime(2025, 6, 20, 15, 30, 0, 0, DateTimeKind.Unspecified), "Dermatologia - Sala 3", new Guid("7b2b6527-66f4-4304-b8c4-4ef12dd7a4c3"), new Guid("818cdb2b-59c5-456a-88fa-ad8de3030bbd"), new Guid("d3d01ef6-c399-454f-936b-73134234bb54"), "Agendado", "Retorno" },
                    { new Guid("7ce97972-19ae-454d-908c-53f0d8e03ac9"), new DateTime(2025, 4, 30, 14, 0, 0, 0, DateTimeKind.Unspecified), "Cardiologia - Sala de Exames", new Guid("ce888b9e-6b86-4a69-bc4e-17fcb11a5185"), new Guid("0cc07cee-6b24-413c-8418-0cf6823226e3"), new Guid("ec41ff48-4bdc-4582-bf57-bcc440af6976"), "Realizado", "Check-up Cardiológico" },
                    { new Guid("86d4e2c6-1309-4620-8ca4-7ac42d96e8d4"), new DateTime(2025, 7, 1, 9, 0, 0, 0, DateTimeKind.Unspecified), "Pediatria - Sala 1", new Guid("6d508ef8-d548-4049-9a36-f29402b364ae"), new Guid("17822355-7037-40e5-b324-4e3ec65b5e06"), new Guid("77e05cb5-444d-4486-8f66-1d88e2274ffc"), "Agendado", "Consulta de Rotina" },
                    { new Guid("95260cf7-a385-429e-88c7-567541f1d97e"), new DateTime(2025, 5, 22, 18, 0, 0, 0, DateTimeKind.Unspecified), "Ortopedia - Sala 5", new Guid("8f9fb19c-8987-4816-bbbd-bd3dea802f68"), new Guid("c5cc31ba-c713-496f-b908-c64519ca6069"), new Guid("1050beea-d5c6-44d6-a3fe-204ae095ec4f"), "Realizado", "Retorno" },
                    { new Guid("b343d049-7078-447f-b6f8-d44efa0263dc"), new DateTime(2025, 4, 12, 15, 0, 0, 0, DateTimeKind.Unspecified), "Dermatologia - Sala 3", new Guid("7b2b6527-66f4-4304-b8c4-4ef12dd7a4c3"), new Guid("818cdb2b-59c5-456a-88fa-ad8de3030bbd"), new Guid("d3d01ef6-c399-454f-936b-73134234bb54"), "Realizado", "Primeira Consulta" },
                    { new Guid("c03c6136-2282-4585-b537-b378c27f3c72"), new DateTime(2025, 5, 5, 9, 0, 0, 0, DateTimeKind.Unspecified), "Pediatria - Sala 1", new Guid("6d508ef8-d548-4049-9a36-f29402b364ae"), new Guid("17822355-7037-40e5-b324-4e3ec65b5e06"), new Guid("77e05cb5-444d-4486-8f66-1d88e2274ffc"), "Realizado", "Consulta Pediátrica" },
                    { new Guid("c3e99f4b-176f-41e7-b527-470a945e727c"), new DateTime(2024, 10, 5, 10, 0, 0, 0, DateTimeKind.Unspecified), "Consultório 101", new Guid("dae56b6e-9a48-4349-b4f5-326186d3118f"), new Guid("0cc07cee-6b24-413c-8418-0cf6823226e3"), new Guid("0ccfef26-a34e-4403-ab2c-1a94ffcd0f25"), "Realizado", "Consulta" }
                });

            migrationBuilder.InsertData(
                table: "Exames",
                columns: new[] { "Id", "AtendimentoId", "DataRealizacao", "DataSolicitacao", "Resultado", "Status", "Tipo" },
                values: new object[,]
                {
                    { new Guid("022559cb-357f-442a-ae5c-63435e335c15"), new Guid("7ce97972-19ae-454d-908c-53f0d8e03ac9"), new DateTime(2025, 5, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 4, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), "Todos os índices dentro do padrão de normalidade.", "Resultado Disponível", "Hemograma Completo" },
                    { new Guid("0d153812-3b07-4a10-9c8a-934bb5fe90ff"), new Guid("b343d049-7078-447f-b6f8-d44efa0263dc"), null, new DateTime(2025, 4, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Solicitado", "Biópsia de pele" },
                    { new Guid("cd847f98-3ac8-49f4-8842-75e5f571f5db"), new Guid("045f3192-90f2-4eb7-87a5-47c495f407c0"), new DateTime(2025, 5, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 5, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Realizado", "Raio-X do Tornozelo Esquerdo" }
                });

            migrationBuilder.InsertData(
                table: "Internacoes",
                columns: new[] { "Id", "AtendimentoId", "DataEntrada", "Leito", "MotivoInternacao", "ObservacoesClinicas", "PlanoSaudeUtilizado", "PrevisaoAlta", "Quarto", "Setor", "StatusInternacao" },
                values: new object[,]
                {
                    { new Guid("15b2dadc-cdec-4a43-ad2c-af122c38d093"), new Guid("045f3192-90f2-4eb7-87a5-47c495f407c0"), new DateTime(2025, 5, 10, 11, 30, 0, 0, DateTimeKind.Unspecified), "204-A", "Fratura no tornozelo esquerdo para observação e cirurgia.", "Paciente evoluindo bem no pós-operatório.", null, new DateTime(2025, 5, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), "204", "Ortopedia", "Alta concedida" },
                    { new Guid("6c18ea96-3b4b-4bed-a33b-9ab8db1a2f71"), new Guid("59c8e80b-e057-4be8-8545-6282a18da3a1"), new DateTime(2025, 6, 5, 22, 5, 0, 0, DateTimeKind.Unspecified), "UTI-02", "Monitoramento cardíaco pós-procedimento.", "Sinais vitais estáveis.", null, null, "UTI", "UTI", "Ativa" }
                });

            migrationBuilder.InsertData(
                table: "Prescricoes",
                columns: new[] { "Id", "AtendimentoId", "DataFim", "DataInicio", "Dosagem", "Frequencia", "Medicamento", "Observacoes", "ProfissionalId", "ReacoesAdversas", "StatusPrescricao", "ViaAdministracao" },
                values: new object[,]
                {
                    { new Guid("37cd556c-4d6a-4690-824a-160abb03ea63"), new Guid("045f3192-90f2-4eb7-87a5-47c495f407c0"), null, new DateTime(2025, 5, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "1 comprimido", "De 12 em 12 horas por 3 dias", "Torsilax", null, new Guid("c5cc31ba-c713-496f-b908-c64519ca6069"), null, "Ativa", "Oral" },
                    { new Guid("4002d9e9-c586-4125-ad35-4116fe6f1327"), new Guid("7ce97972-19ae-454d-908c-53f0d8e03ac9"), null, new DateTime(2025, 4, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), "1 comprimido", "1 vez ao dia", "AAS Infantil 100mg", null, new Guid("0cc07cee-6b24-413c-8418-0cf6823226e3"), null, "Ativa", "Oral" },
                    { new Guid("4640eced-b0a6-4f84-bb1e-874bf0c07b7c"), new Guid("c3e99f4b-176f-41e7-b527-470a945e727c"), null, new DateTime(2024, 10, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), "1 comprimido", "1 vez ao dia, à noite", "Sinvastatina 20mg", null, new Guid("0cc07cee-6b24-413c-8418-0cf6823226e3"), null, "Ativa", "Oral" },
                    { new Guid("7cad31d7-dfd8-4ff7-9ef6-3691793667a8"), new Guid("c03c6136-2282-4585-b537-b378c27f3c72"), null, new DateTime(2025, 5, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), "5ml", "De 8 em 8 horas por 7 dias", "Amoxicilina 250mg/5ml", null, new Guid("17822355-7037-40e5-b324-4e3ec65b5e06"), null, "Encerrada", "Oral" }
                });

            migrationBuilder.InsertData(
                table: "AltasHospitalares",
                columns: new[] { "Id", "CondicaoPaciente", "DataAlta", "InstrucoesPosAlta", "InternacaoId" },
                values: new object[] { new Guid("341baf34-e8d8-401f-a33a-76aed737e98f"), "Estável, com recomendação de repouso e fisioterapia.", new DateTime(2025, 5, 12, 14, 0, 0, 0, DateTimeKind.Unspecified), "Manter o pé elevado, evitar esforço por 15 dias e iniciar fisioterapia na próxima semana.", new Guid("15b2dadc-cdec-4a43-ad2c-af122c38d093") });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AltasHospitalares",
                keyColumn: "Id",
                keyValue: new Guid("341baf34-e8d8-401f-a33a-76aed737e98f"));

            migrationBuilder.DeleteData(
                table: "Atendimentos",
                keyColumn: "Id",
                keyValue: new Guid("066103d7-cc6a-4cea-98b7-d6defbebe777"));

            migrationBuilder.DeleteData(
                table: "Atendimentos",
                keyColumn: "Id",
                keyValue: new Guid("1277c3ba-3794-4964-8512-91bdddc6e986"));

            migrationBuilder.DeleteData(
                table: "Atendimentos",
                keyColumn: "Id",
                keyValue: new Guid("20541787-e695-4258-bb81-f5702cefe378"));

            migrationBuilder.DeleteData(
                table: "Atendimentos",
                keyColumn: "Id",
                keyValue: new Guid("3a2d2590-e382-4ec2-920c-a0259d3eecb6"));

            migrationBuilder.DeleteData(
                table: "Atendimentos",
                keyColumn: "Id",
                keyValue: new Guid("6eaf4ab6-0f45-415c-9a60-1a79c9c3aa20"));

            migrationBuilder.DeleteData(
                table: "Atendimentos",
                keyColumn: "Id",
                keyValue: new Guid("7928ac45-23e9-4c77-8dc3-d17e46bb04c9"));

            migrationBuilder.DeleteData(
                table: "Atendimentos",
                keyColumn: "Id",
                keyValue: new Guid("7930304d-ec2c-49c3-a345-6a56bdd2043c"));

            migrationBuilder.DeleteData(
                table: "Atendimentos",
                keyColumn: "Id",
                keyValue: new Guid("86d4e2c6-1309-4620-8ca4-7ac42d96e8d4"));

            migrationBuilder.DeleteData(
                table: "Atendimentos",
                keyColumn: "Id",
                keyValue: new Guid("95260cf7-a385-429e-88c7-567541f1d97e"));

            migrationBuilder.DeleteData(
                table: "Exames",
                keyColumn: "Id",
                keyValue: new Guid("022559cb-357f-442a-ae5c-63435e335c15"));

            migrationBuilder.DeleteData(
                table: "Exames",
                keyColumn: "Id",
                keyValue: new Guid("0d153812-3b07-4a10-9c8a-934bb5fe90ff"));

            migrationBuilder.DeleteData(
                table: "Exames",
                keyColumn: "Id",
                keyValue: new Guid("cd847f98-3ac8-49f4-8842-75e5f571f5db"));

            migrationBuilder.DeleteData(
                table: "Internacoes",
                keyColumn: "Id",
                keyValue: new Guid("6c18ea96-3b4b-4bed-a33b-9ab8db1a2f71"));

            migrationBuilder.DeleteData(
                table: "Prescricoes",
                keyColumn: "Id",
                keyValue: new Guid("37cd556c-4d6a-4690-824a-160abb03ea63"));

            migrationBuilder.DeleteData(
                table: "Prescricoes",
                keyColumn: "Id",
                keyValue: new Guid("4002d9e9-c586-4125-ad35-4116fe6f1327"));

            migrationBuilder.DeleteData(
                table: "Prescricoes",
                keyColumn: "Id",
                keyValue: new Guid("4640eced-b0a6-4f84-bb1e-874bf0c07b7c"));

            migrationBuilder.DeleteData(
                table: "Prescricoes",
                keyColumn: "Id",
                keyValue: new Guid("7cad31d7-dfd8-4ff7-9ef6-3691793667a8"));

            migrationBuilder.DeleteData(
                table: "ProfissionaisDeSaude",
                keyColumn: "Id",
                keyValue: new Guid("a4a9fb63-c9cb-4392-9d6b-a421a6300556"));

            migrationBuilder.DeleteData(
                table: "Atendimentos",
                keyColumn: "Id",
                keyValue: new Guid("59c8e80b-e057-4be8-8545-6282a18da3a1"));

            migrationBuilder.DeleteData(
                table: "Atendimentos",
                keyColumn: "Id",
                keyValue: new Guid("7ce97972-19ae-454d-908c-53f0d8e03ac9"));

            migrationBuilder.DeleteData(
                table: "Atendimentos",
                keyColumn: "Id",
                keyValue: new Guid("b343d049-7078-447f-b6f8-d44efa0263dc"));

            migrationBuilder.DeleteData(
                table: "Atendimentos",
                keyColumn: "Id",
                keyValue: new Guid("c03c6136-2282-4585-b537-b378c27f3c72"));

            migrationBuilder.DeleteData(
                table: "Atendimentos",
                keyColumn: "Id",
                keyValue: new Guid("c3e99f4b-176f-41e7-b527-470a945e727c"));

            migrationBuilder.DeleteData(
                table: "Internacoes",
                keyColumn: "Id",
                keyValue: new Guid("15b2dadc-cdec-4a43-ad2c-af122c38d093"));

            migrationBuilder.DeleteData(
                table: "ProfissionaisDeSaude",
                keyColumn: "Id",
                keyValue: new Guid("1b556ce0-1cfd-4af2-a630-ba9564c6e484"));

            migrationBuilder.DeleteData(
                table: "ProfissionaisDeSaude",
                keyColumn: "Id",
                keyValue: new Guid("6d7fd56b-b109-43a4-8fa9-6bb0b0791969"));

            migrationBuilder.DeleteData(
                table: "ProfissionaisDeSaude",
                keyColumn: "Id",
                keyValue: new Guid("b9f1f1d0-46b8-4c17-a3be-88b7f161feb7"));

            migrationBuilder.DeleteData(
                table: "ProfissionaisDeSaude",
                keyColumn: "Id",
                keyValue: new Guid("ff8ec356-17a5-4739-8135-ebe89f9b242f"));

            migrationBuilder.DeleteData(
                table: "Prontuarios",
                keyColumn: "Id",
                keyValue: new Guid("1050beea-d5c6-44d6-a3fe-204ae095ec4f"));

            migrationBuilder.DeleteData(
                table: "Prontuarios",
                keyColumn: "Id",
                keyValue: new Guid("1b020965-722d-4a76-a77d-0312e637ad3a"));

            migrationBuilder.DeleteData(
                table: "Prontuarios",
                keyColumn: "Id",
                keyValue: new Guid("2d461a0d-c2fe-4598-a6c5-45b53d34dae9"));

            migrationBuilder.DeleteData(
                table: "Prontuarios",
                keyColumn: "Id",
                keyValue: new Guid("c63bba87-eafc-40b5-b7e0-2854d7c19fe4"));

            migrationBuilder.DeleteData(
                table: "Prontuarios",
                keyColumn: "Id",
                keyValue: new Guid("f413cfce-0b29-4dd7-98c6-20a7ef673a43"));

            migrationBuilder.DeleteData(
                table: "Atendimentos",
                keyColumn: "Id",
                keyValue: new Guid("045f3192-90f2-4eb7-87a5-47c495f407c0"));

            migrationBuilder.DeleteData(
                table: "Especialidades",
                keyColumn: "Id",
                keyValue: new Guid("c1805cf9-20fd-4478-8683-95008147f00d"));

            migrationBuilder.DeleteData(
                table: "Pacientes",
                keyColumn: "Id",
                keyValue: new Guid("54f4ef2f-d788-44f5-b4ea-a885ffc59597"));

            migrationBuilder.DeleteData(
                table: "Pacientes",
                keyColumn: "Id",
                keyValue: new Guid("5f4e24fc-fe07-4e87-a975-4240c490b40d"));

            migrationBuilder.DeleteData(
                table: "Pacientes",
                keyColumn: "Id",
                keyValue: new Guid("8f9fb19c-8987-4816-bbbd-bd3dea802f68"));

            migrationBuilder.DeleteData(
                table: "Pacientes",
                keyColumn: "Id",
                keyValue: new Guid("97269dd1-6c31-41d1-b4c7-de9e9ff12fa4"));

            migrationBuilder.DeleteData(
                table: "Pacientes",
                keyColumn: "Id",
                keyValue: new Guid("c1ad208e-856f-4e4f-a076-935601580d92"));

            migrationBuilder.DeleteData(
                table: "ProfissionaisDeSaude",
                keyColumn: "Id",
                keyValue: new Guid("0cc07cee-6b24-413c-8418-0cf6823226e3"));

            migrationBuilder.DeleteData(
                table: "ProfissionaisDeSaude",
                keyColumn: "Id",
                keyValue: new Guid("17822355-7037-40e5-b324-4e3ec65b5e06"));

            migrationBuilder.DeleteData(
                table: "ProfissionaisDeSaude",
                keyColumn: "Id",
                keyValue: new Guid("818cdb2b-59c5-456a-88fa-ad8de3030bbd"));

            migrationBuilder.DeleteData(
                table: "ProfissionaisDeSaude",
                keyColumn: "Id",
                keyValue: new Guid("8457597c-08cf-4291-b591-00c145173389"));

            migrationBuilder.DeleteData(
                table: "Prontuarios",
                keyColumn: "Id",
                keyValue: new Guid("0ccfef26-a34e-4403-ab2c-1a94ffcd0f25"));

            migrationBuilder.DeleteData(
                table: "Prontuarios",
                keyColumn: "Id",
                keyValue: new Guid("77e05cb5-444d-4486-8f66-1d88e2274ffc"));

            migrationBuilder.DeleteData(
                table: "Prontuarios",
                keyColumn: "Id",
                keyValue: new Guid("d3d01ef6-c399-454f-936b-73134234bb54"));

            migrationBuilder.DeleteData(
                table: "Prontuarios",
                keyColumn: "Id",
                keyValue: new Guid("ec41ff48-4bdc-4582-bf57-bcc440af6976"));

            migrationBuilder.DeleteData(
                table: "Especialidades",
                keyColumn: "Id",
                keyValue: new Guid("03e408e2-49c7-49e0-aed1-049cf1c08f93"));

            migrationBuilder.DeleteData(
                table: "Especialidades",
                keyColumn: "Id",
                keyValue: new Guid("530848b2-54b5-4e8b-a725-2a2b566ea3c1"));

            migrationBuilder.DeleteData(
                table: "Especialidades",
                keyColumn: "Id",
                keyValue: new Guid("f925f653-027f-43d3-9b13-198e853a9b2c"));

            migrationBuilder.DeleteData(
                table: "Pacientes",
                keyColumn: "Id",
                keyValue: new Guid("6d508ef8-d548-4049-9a36-f29402b364ae"));

            migrationBuilder.DeleteData(
                table: "Pacientes",
                keyColumn: "Id",
                keyValue: new Guid("7b2b6527-66f4-4304-b8c4-4ef12dd7a4c3"));

            migrationBuilder.DeleteData(
                table: "Pacientes",
                keyColumn: "Id",
                keyValue: new Guid("ce888b9e-6b86-4a69-bc4e-17fcb11a5185"));

            migrationBuilder.DeleteData(
                table: "Pacientes",
                keyColumn: "Id",
                keyValue: new Guid("dae56b6e-9a48-4349-b4f5-326186d3118f"));

            migrationBuilder.DeleteData(
                table: "ProfissionaisDeSaude",
                keyColumn: "Id",
                keyValue: new Guid("c5cc31ba-c713-496f-b908-c64519ca6069"));

            migrationBuilder.DeleteData(
                table: "Prontuarios",
                keyColumn: "Id",
                keyValue: new Guid("cfd7b580-ba91-40be-b6b0-eb2ec335dd4a"));

            migrationBuilder.DeleteData(
                table: "Especialidades",
                keyColumn: "Id",
                keyValue: new Guid("3cfe9e0f-92a4-4960-b027-64babc353d60"));

            migrationBuilder.DeleteData(
                table: "Pacientes",
                keyColumn: "Id",
                keyValue: new Guid("a607bd61-5581-47bf-9d83-527c8935e4d0"));

            migrationBuilder.InsertData(
                table: "Especialidades",
                columns: new[] { "Id", "Nome" },
                values: new object[,]
                {
                    { new Guid("00b0fc80-e429-4be6-a542-225faff625ff"), "Pediatria" },
                    { new Guid("21e051f6-14b8-4ad8-9c89-a028fa3b4453"), "Ortopedia" },
                    { new Guid("5be4a08e-16fd-4794-bb89-d010afae7dbf"), "Cardiologia" }
                });

            migrationBuilder.InsertData(
                table: "Pacientes",
                columns: new[] { "Id", "CPF", "DataNascimento", "Email", "EnderecoCompleto", "EstadoCivil", "NomeCompleto", "NumeroCartaoSUS", "PossuiPlanoSaude", "Sexo", "Telefone", "TipoSanguineo" },
                values: new object[,]
                {
                    { new Guid("3f5fd9f7-5ab6-4b0e-890c-768878cf304c"), "111.222.333-44", new DateTime(1980, 5, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "joao.silva@example.com", "Rua das Flores, 123, São Paulo, SP", "Casado", "João da Silva", "987654321098765", true, "Masculino", "(11) 98765-4321", "O+" },
                    { new Guid("d1f9889a-ec87-4bfa-9498-1dfad1a85e87"), "222.333.444-55", new DateTime(1992, 10, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), "maria.oliveira@example.com", "Avenida Principal, 456, Rio de Janeiro, RJ", "Solteira", "Maria Oliveira", "123456789012345", false, "Feminino", "(21) 91234-5678", "A-" }
                });

            migrationBuilder.InsertData(
                table: "ProfissionaisDeSaude",
                columns: new[] { "Id", "Ativo", "CPF", "CargaHorariaSemanal", "DataAdmissao", "Email", "EspecialidadeId", "NomeCompleto", "RegistroConselho", "Telefone", "TipoRegistro", "Turno" },
                values: new object[,]
                {
                    { new Guid("ece2dd0c-5852-4c54-b886-c2e2d6502696"), true, "333.444.555-66", 40, new DateTime(2020, 1, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "carlos.andrade@hospisim.com", new Guid("5be4a08e-16fd-4794-bb89-d010afae7dbf"), "Dr. Carlos Andrade", "CRM-SP 123456", "(11) 98888-7777", "CRM", "Manhã" },
                    { new Guid("eeea9f91-41f3-465d-9324-0f0c8f42a004"), true, "444.555.666-77", 30, new DateTime(2021, 3, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "ana.souza@hospisim.com", new Guid("00b0fc80-e429-4be6-a542-225faff625ff"), "Dra. Ana Paula Souza", "CRM-RJ 654321", "(21) 97777-6666", "CRM", "Tarde" }
                });
        }
    }
}
