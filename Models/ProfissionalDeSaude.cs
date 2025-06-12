using System.ComponentModel.DataAnnotations;

namespace Hospisim.Models
{
    public class ProfissionalDeSaude
    {
        public Guid Id { get; set; }

        [Required(ErrorMessage = "O nome completo é obrigatório.")]
        [Display(Name = "Nome Completo")]
        public string NomeCompleto { get; set; } = null!;

        [Required(ErrorMessage = "O CPF é obrigatório.")]
        [Display(Name = "CPF")]
        public string CPF { get; set; } = null!;

        [Required(ErrorMessage = "O e-mail é obrigatório.")]
        [EmailAddress(ErrorMessage = "Formato de e-mail inválido.")]
        [Display(Name = "E-mail")]
        public string Email { get; set; } = null!;

        [Required(ErrorMessage = "O telefone é obrigatório.")]
        public string Telefone { get; set; } = null!;

        [Required(ErrorMessage = "O registro do conselho é obrigatório.")]
        [Display(Name = "Registro do Conselho (CRM, COREN, etc.)")]
        public string RegistroConselho { get; set; } = null!;

        [Required(ErrorMessage = "O tipo de registro é obrigatório.")]
        [Display(Name = "Tipo de Registro")]
        public string TipoRegistro { get; set; } = null!;

        [Required]
        [Display(Name = "Data de Admissão")]
        public DateTime DataAdmissao { get; set; }

        [Required(ErrorMessage = "A carga horária é obrigatória.")]
        [Display(Name = "Carga Horária Semanal")]
        public int CargaHorariaSemanal { get; set; }

        [Required(ErrorMessage = "O turno é obrigatório.")]
        public string Turno { get; set; } = null!;

        public bool Ativo { get; set; }

        // Relacionamentos
        [Required(ErrorMessage = "A especialidade é obrigatória.")]
        [Display(Name = "Especialidade")]
        public Guid EspecialidadeId { get; set; }
        public Especialidade Especialidade { get; set; } = null!;

        public ICollection<Atendimento> Atendimentos { get; set; } = new List<Atendimento>();
        public ICollection<Prescricao> Prescricoes { get; set; } = new List<Prescricao>();
    }
}