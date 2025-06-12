using System.ComponentModel.DataAnnotations;

namespace Hospisim.Models
{
    public class Atendimento
    {
        public Guid Id { get; set; }

        [Display(Name = "Data e Hora do Atendimento")]
        public DateTime DataHora { get; set; }

        [Display(Name = "Tipo de Atendimento")]
        public string Tipo { get; set; } = null!;

        public string Status { get; set; } = null!;

        public string Local { get; set; } = null!;

        // Relacionamentos
        [Display(Name = "Paciente")]
        public Guid PacienteId { get; set; }
        public Paciente Paciente { get; set; } = null!;

        [Display(Name = "Profissional de Saúde")]
        public Guid ProfissionalId { get; set; }
        public ProfissionalDeSaude Profissional { get; set; } = null!;

        [Display(Name = "Prontuário")]
        public Guid ProntuarioId { get; set; }
        public Prontuario Prontuario { get; set; } = null!;

        // Navegação
        public ICollection<Prescricao> Prescricoes { get; set; } = new List<Prescricao>();
        public ICollection<Exame> Exames { get; set; } = new List<Exame>();
        public Internacao? Internacao { get; set; }
    }
}