using System.ComponentModel.DataAnnotations;

namespace Hospisim.Models
{
    public class Especialidade
    {
        public Guid Id { get; set; }

        [Required(ErrorMessage = "O nome da especialidade é obrigatório.")]
        [Display(Name = "Nome da Especialidade")]
        public string Nome { get; set; } = null!;

        public ICollection<ProfissionalDeSaude> Profissionais { get; set; } = new List<ProfissionalDeSaude>();
    }
}