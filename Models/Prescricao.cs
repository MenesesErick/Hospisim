using System.ComponentModel.DataAnnotations;

namespace Hospisim.Models
{
    public class Prescricao
    {
        public Guid Id { get; set; }

        [Required(ErrorMessage = "O nome do medicamento é obrigatório.")]
        public string Medicamento { get; set; } = null!;

        [Required(ErrorMessage = "A dosagem é obrigatória.")]
        public string Dosagem { get; set; } = null!;

        [Required(ErrorMessage = "A frequência é obrigatória.")]
        [Display(Name = "Frequência")]
        public string Frequencia { get; set; } = null!;

        [Required(ErrorMessage = "A via de administração é obrigatória.")]
        [Display(Name = "Via de Administração")]
        public string ViaAdministracao { get; set; } = null!;

        [Required]
        [Display(Name = "Data de Início")]
        public DateTime DataInicio { get; set; }

        [Display(Name = "Data de Término")]
        public DateTime? DataFim { get; set; }

        [Display(Name = "Observações")]
        public string? Observacoes { get; set; }

        [Required(ErrorMessage = "O status da prescrição é obrigatório.")]
        [Display(Name = "Status da Prescrição")]
        public string StatusPrescricao { get; set; } = null!;

        [Display(Name = "Reações Adversas")]
        public string? ReacoesAdversas { get; set; }

        // Relacionamentos
        public Guid AtendimentoId { get; set; }
        public Atendimento Atendimento { get; set; } = null!;

        [Display(Name = "Profissional Responsável")]
        public Guid ProfissionalId { get; set; }
        public ProfissionalDeSaude Profissional { get; set; } = null!;
    }
}