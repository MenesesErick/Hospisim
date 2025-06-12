using System.ComponentModel.DataAnnotations;

namespace Hospisim.Models
{
    public class Prontuario
    {
        public Guid Id { get; set; }

        [Display(Name = "Número do Prontuário")]
        public string NumeroProntuario { get; set; } = null!;

        [Display(Name = "Data de Abertura")]
        public DateTime DataAbertura { get; set; }

        [Display(Name = "Observações Gerais")]
        public string? ObservacoesGerais { get; set; }

        // Relacionamentos
        [Display(Name = "Paciente")]
        public Guid PacienteId { get; set; }
        public Paciente Paciente { get; set; } = null!;

        public ICollection<Atendimento> Atendimentos { get; set; } = new List<Atendimento>();
    }
}