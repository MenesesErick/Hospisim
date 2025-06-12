using System.ComponentModel.DataAnnotations; // Adicionado para usar o [Display]

namespace Hospisim.Models
{
    public class AltaHospitalar
    {
        public Guid Id { get; set; }

        [Display(Name = "Data da Alta")]
        public DateTime DataAlta { get; set; }

        [Display(Name = "Condição do Paciente na Alta")]
        public string CondicaoPaciente { get; set; } = null!;

        [Display(Name = "Instruções Pós-Alta")]
        public string? InstrucoesPosAlta { get; set; }

        // Relacionamentos
        [Display(Name = "Internação")]
        public Guid InternacaoId { get; set; }
        public Internacao Internacao { get; set; } = null!;
    }
}