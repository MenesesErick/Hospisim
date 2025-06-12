using System.ComponentModel.DataAnnotations;

namespace Hospisim.Models
{
    public class Internacao
    {
        public Guid Id { get; set; }

        [Display(Name = "Data de Entrada")]
        public DateTime DataEntrada { get; set; }

        [Display(Name = "Previsão de Alta")]
        public DateTime? PrevisaoAlta { get; set; }

        [Required(ErrorMessage = "O motivo da internação é obrigatório.")]
        [Display(Name = "Motivo da Internação")]
        public string MotivoInternacao { get; set; } = null!;

        [Required(ErrorMessage = "O leito é obrigatório.")]
        [Display(Name = "Leito")]
        public string Leito { get; set; } = null!;

        [Required(ErrorMessage = "O quarto é obrigatório.")]
        [Display(Name = "Quarto")]
        public string Quarto { get; set; } = null!;

        [Required(ErrorMessage = "O setor é obrigatório.")]
        [Display(Name = "Setor")]
        public string Setor { get; set; } = null!;

        [Display(Name = "Plano de Saúde Utilizado")]
        public string? PlanoSaudeUtilizado { get; set; }

        [Display(Name = "Observações Clínicas")]
        public string? ObservacoesClinicas { get; set; }

        [Required(ErrorMessage = "O status da internação é obrigatório.")]
        [Display(Name = "Status da Internação")]
        public string StatusInternacao { get; set; } = null!;

        // --- Relacionamentos ---
        [Required]
        [Display(Name = "Atendimento de Origem")]
        public Guid AtendimentoId { get; set; }
        public Atendimento Atendimento { get; set; } = null!;

        public AltaHospitalar? AltaHospitalar { get; set; }
    }
}