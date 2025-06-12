using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Hospisim.Data;
using Hospisim.Models;

namespace Hospisim.Controllers
{
    public class ProfissionalDeSaudesController : Controller
    {
        private readonly HospisimContext _context;

        public ProfissionalDeSaudesController(HospisimContext context)
        {
            _context = context;
        }

        // GET: ProfissionalDeSaudes
        public async Task<IActionResult> Index(string searchString)
        {
            ViewData["CurrentFilter"] = searchString;
            var profissionais = _context.ProfissionaisDeSaude.Include(p => p.Especialidade).AsQueryable();
            if (!String.IsNullOrEmpty(searchString))
            {
                profissionais = profissionais.Where(p => p.NomeCompleto.Contains(searchString) || p.RegistroConselho.Contains(searchString));
            }
            return View(await profissionais.OrderBy(p => p.NomeCompleto).ToListAsync());
        }

        // GET: ProfissionalDeSaudes/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null) return NotFound();
            var profissionalDeSaude = await _context.ProfissionaisDeSaude
                .Include(p => p.Especialidade)
                .Include(p => p.Atendimentos).ThenInclude(a => a.Paciente)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (profissionalDeSaude == null) return NotFound();
            return View(profissionalDeSaude);
        }

        // GET: ProfissionalDeSaudes/Create
        public IActionResult Create()
        {
            ViewData["EspecialidadeId"] = new SelectList(_context.Especialidades, "Id", "Nome");
            return View();
        }

        // POST: ProfissionalDeSaudes/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("NomeCompleto,CPF,Email,Telefone,RegistroConselho,TipoRegistro,DataAdmissao,CargaHorariaSemanal,Turno,Ativo,EspecialidadeId")] ProfissionalDeSaude profissionalDeSaude)
        {
            // A CORREÇÃO ESTÁ AQUI
            ModelState.Remove("Especialidade"); // Ignora a validação do objeto de navegação

            if (ModelState.IsValid)
            {
                bool registroJaExiste = await _context.ProfissionaisDeSaude.AnyAsync(p => p.RegistroConselho == profissionalDeSaude.RegistroConselho);
                if (registroJaExiste)
                {
                    ModelState.AddModelError("RegistroConselho", "Este número de registro já está cadastrado.");
                }

                bool cpfJaExiste = await _context.ProfissionaisDeSaude.AnyAsync(p => p.CPF == profissionalDeSaude.CPF);
                if (cpfJaExiste)
                {
                    ModelState.AddModelError("CPF", "Este CPF já está cadastrado.");
                }

                if (ModelState.ErrorCount == 0)
                {
                    profissionalDeSaude.Id = Guid.NewGuid();
                    _context.Add(profissionalDeSaude);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
            }
            ViewData["EspecialidadeId"] = new SelectList(_context.Especialidades, "Id", "Nome", profissionalDeSaude.EspecialidadeId);
            return View(profissionalDeSaude);
        }

        // GET: ProfissionalDeSaudes/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null) return NotFound();
            var profissionalDeSaude = await _context.ProfissionaisDeSaude.FindAsync(id);
            if (profissionalDeSaude == null) return NotFound();
            ViewData["EspecialidadeId"] = new SelectList(_context.Especialidades, "Id", "Nome", profissionalDeSaude.EspecialidadeId);
            return View(profissionalDeSaude);
        }

        // POST: ProfissionalDeSaudes/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,NomeCompleto,CPF,Email,Telefone,RegistroConselho,TipoRegistro,DataAdmissao,CargaHorariaSemanal,Turno,Ativo,EspecialidadeId")] ProfissionalDeSaude profissionalDeSaude)
        {
            if (id != profissionalDeSaude.Id) return NotFound();

            // A CORREÇÃO TAMBÉM ESTÁ AQUI
            ModelState.Remove("Especialidade");

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(profissionalDeSaude);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProfissionalDeSaudeExists(profissionalDeSaude.Id)) return NotFound();
                    else throw;
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["EspecialidadeId"] = new SelectList(_context.Especialidades, "Id", "Nome", profissionalDeSaude.EspecialidadeId);
            return View(profissionalDeSaude);
        }

        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var profissionalDeSaude = await _context.ProfissionaisDeSaude
                .Include(p => p.Especialidade) // Inclui a especialidade para mostrar na tela
                .FirstOrDefaultAsync(m => m.Id == id);

            if (profissionalDeSaude == null)
            {
                return NotFound();
            }

            return View(profissionalDeSaude);
        }

        // POST: ProfissionalDeSaudes/Delete/5
        // Executa a exclusão depois que o usuário clica no botão "Sim, Deletar"
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var profissionalDeSaude = await _context.ProfissionaisDeSaude.FindAsync(id);
            if (profissionalDeSaude != null)
            {
                _context.ProfissionaisDeSaude.Remove(profissionalDeSaude);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }

        // Método auxiliar que estava faltando
        private bool ProfissionalDeSaudeExists(Guid id)
        {
            return _context.ProfissionaisDeSaude.Any(e => e.Id == id);
        }
    }
}