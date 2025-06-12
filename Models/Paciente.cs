using System.ComponentModel.DataAnnotations;

namespace Hospisim.Models
{
    public class Paciente
    {
        public Guid Id { get; set; }

        [Required(ErrorMessage = "O nome completo é obrigatório.")]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "O nome deve ter entre 3 e 100 caracteres.")]
        [Display(Name = "Nome Completo")]
        public string NomeCompleto { get; set; } = null!;

        [Required(ErrorMessage = "O CPF é obrigatório.")]
        public string CPF { get; set; } = null!;

        [Required(ErrorMessage = "A data de nascimento é obrigatória.")]
        [Display(Name = "Data de Nascimento")]
        public DateTime DataNascimento { get; set; }

        [Required(ErrorMessage = "O campo Sexo é obrigatório.")]
        public string Sexo { get; set; } = null!;

        [Required(ErrorMessage = "O tipo sanguíneo é obrigatório.")]
        [Display(Name = "Tipo Sanguíneo")]
        public string TipoSanguineo { get; set; } = null!;

        [Required(ErrorMessage = "O telefone é obrigatório.")]
        public string Telefone { get; set; } = null!;

        [Required(ErrorMessage = "O e-mail é obrigatório.")]
        [EmailAddress(ErrorMessage = "O formato do e-mail é inválido.")]
        [Display(Name = "E-mail")]
        public string Email { get; set; } = null!;

        [Required(ErrorMessage = "O endereço é obrigatório.")]
        [Display(Name = "Endereço Completo")]
        public string EnderecoCompleto { get; set; } = null!;

        [Required(ErrorMessage = "O número do Cartão SUS é obrigatório.")]
        [Display(Name = "Nº Cartão SUS")]
        public string NumeroCartaoSUS { get; set; } = null!;

        [Required(ErrorMessage = "O estado civil é obrigatório.")]
        [Display(Name = "Estado Civil")]
        public string EstadoCivil { get; set; } = null!;

        [Display(Name = "Possui Plano de Saúde")]
        public bool PossuiPlanoSaude { get; set; }

        // Propriedades de Navegação
        public ICollection<Prontuario> Prontuarios { get; set; } = new List<Prontuario>();
        public ICollection<Atendimento> Atendimentos { get; set; } = new List<Atendimento>();
    }
}