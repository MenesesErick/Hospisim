using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Hospisim.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Especialidades",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Nome = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Especialidades", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Pacientes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    NomeCompleto = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    CPF = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DataNascimento = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Sexo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TipoSanguineo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Telefone = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EnderecoCompleto = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NumeroCartaoSUS = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EstadoCivil = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PossuiPlanoSaude = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pacientes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ProfissionaisDeSaude",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    NomeCompleto = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CPF = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Telefone = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RegistroConselho = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TipoRegistro = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DataAdmissao = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CargaHorariaSemanal = table.Column<int>(type: "int", nullable: false),
                    Turno = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Ativo = table.Column<bool>(type: "bit", nullable: false),
                    EspecialidadeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProfissionaisDeSaude", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProfissionaisDeSaude_Especialidades_EspecialidadeId",
                        column: x => x.EspecialidadeId,
                        principalTable: "Especialidades",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Prontuarios",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    NumeroProntuario = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DataAbertura = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ObservacoesGerais = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PacienteId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Prontuarios", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Prontuarios_Pacientes_PacienteId",
                        column: x => x.PacienteId,
                        principalTable: "Pacientes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Atendimentos",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DataHora = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Tipo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Local = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PacienteId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProfissionalId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProntuarioId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Atendimentos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Atendimentos_Pacientes_PacienteId",
                        column: x => x.PacienteId,
                        principalTable: "Pacientes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Atendimentos_ProfissionaisDeSaude_ProfissionalId",
                        column: x => x.ProfissionalId,
                        principalTable: "ProfissionaisDeSaude",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Atendimentos_Prontuarios_ProntuarioId",
                        column: x => x.ProntuarioId,
                        principalTable: "Prontuarios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Exames",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Tipo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DataSolicitacao = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DataRealizacao = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Resultado = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AtendimentoId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Exames", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Exames_Atendimentos_AtendimentoId",
                        column: x => x.AtendimentoId,
                        principalTable: "Atendimentos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Internacoes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DataEntrada = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PrevisaoAlta = table.Column<DateTime>(type: "datetime2", nullable: true),
                    MotivoInternacao = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Leito = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Quarto = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Setor = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PlanoSaudeUtilizado = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ObservacoesClinicas = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StatusInternacao = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AtendimentoId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Internacoes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Internacoes_Atendimentos_AtendimentoId",
                        column: x => x.AtendimentoId,
                        principalTable: "Atendimentos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Prescricoes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Medicamento = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Dosagem = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Frequencia = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ViaAdministracao = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DataInicio = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DataFim = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Observacoes = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StatusPrescricao = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ReacoesAdversas = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AtendimentoId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProfissionalId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Prescricoes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Prescricoes_Atendimentos_AtendimentoId",
                        column: x => x.AtendimentoId,
                        principalTable: "Atendimentos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Prescricoes_ProfissionaisDeSaude_ProfissionalId",
                        column: x => x.ProfissionalId,
                        principalTable: "ProfissionaisDeSaude",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AltasHospitalares",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DataAlta = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CondicaoPaciente = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    InstrucoesPosAlta = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    InternacaoId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AltasHospitalares", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AltasHospitalares_Internacoes_InternacaoId",
                        column: x => x.InternacaoId,
                        principalTable: "Internacoes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

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

            migrationBuilder.CreateIndex(
                name: "IX_AltasHospitalares_InternacaoId",
                table: "AltasHospitalares",
                column: "InternacaoId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Atendimentos_PacienteId",
                table: "Atendimentos",
                column: "PacienteId");

            migrationBuilder.CreateIndex(
                name: "IX_Atendimentos_ProfissionalId",
                table: "Atendimentos",
                column: "ProfissionalId");

            migrationBuilder.CreateIndex(
                name: "IX_Atendimentos_ProntuarioId",
                table: "Atendimentos",
                column: "ProntuarioId");

            migrationBuilder.CreateIndex(
                name: "IX_Exames_AtendimentoId",
                table: "Exames",
                column: "AtendimentoId");

            migrationBuilder.CreateIndex(
                name: "IX_Internacoes_AtendimentoId",
                table: "Internacoes",
                column: "AtendimentoId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Prescricoes_AtendimentoId",
                table: "Prescricoes",
                column: "AtendimentoId");

            migrationBuilder.CreateIndex(
                name: "IX_Prescricoes_ProfissionalId",
                table: "Prescricoes",
                column: "ProfissionalId");

            migrationBuilder.CreateIndex(
                name: "IX_ProfissionaisDeSaude_EspecialidadeId",
                table: "ProfissionaisDeSaude",
                column: "EspecialidadeId");

            migrationBuilder.CreateIndex(
                name: "IX_Prontuarios_PacienteId",
                table: "Prontuarios",
                column: "PacienteId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AltasHospitalares");

            migrationBuilder.DropTable(
                name: "Exames");

            migrationBuilder.DropTable(
                name: "Prescricoes");

            migrationBuilder.DropTable(
                name: "Internacoes");

            migrationBuilder.DropTable(
                name: "Atendimentos");

            migrationBuilder.DropTable(
                name: "ProfissionaisDeSaude");

            migrationBuilder.DropTable(
                name: "Prontuarios");

            migrationBuilder.DropTable(
                name: "Especialidades");

            migrationBuilder.DropTable(
                name: "Pacientes");
        }
    }
}
