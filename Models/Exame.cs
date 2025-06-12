using System.ComponentModel.DataAnnotations;

namespace Hospisim.Models
{
    public class Exame
    {
        public Guid Id { get; set; }

        [Required(ErrorMessage = "O tipo do exame é obrigatório.")]
        [Display(Name = "Tipo de Exame")]
        public string Tipo { get; set; } = null!;

        [Required]
        [Display(Name = "Data da Solicitação")]
        public DateTime DataSolicitacao { get; set; }

        [Display(Name = "Data de Realização")]
        public DateTime? DataRealizacao { get; set; }

        public string? Resultado { get; set; }

        [Required(ErrorMessage = "O status é obrigatório.")]
        public string Status { get; set; } = null!;

        // Relacionamentos
        public Guid AtendimentoId { get; set; }
        public Atendimento Atendimento { get; set; } = null!;
    }
}