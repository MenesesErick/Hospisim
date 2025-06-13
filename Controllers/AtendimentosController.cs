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
    public class AtendimentosController : Controller
    {
        private readonly HospisimContext _context;

        public AtendimentosController(HospisimContext context)
        {
            _context = context;
        }

        // GET: Atendimentos
        public async Task<IActionResult> Index(string searchString)
        {
            ViewData["CurrentFilter"] = searchString;
            var atendimentos = _context.Atendimentos.Include(a => a.Paciente).Include(a => a.Profissional).AsQueryable();
            if (!String.IsNullOrEmpty(searchString))
            {
                atendimentos = atendimentos.Where(a => a.Paciente.NomeCompleto.Contains(searchString) || a.Paciente.CPF.Contains(searchString));
            }
            return View(await atendimentos.OrderByDescending(a => a.DataHora).ToListAsync());
        }

        // GET: Atendimentos/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null) return NotFound();
            var atendimento = await _context.Atendimentos
                .Include(a => a.Paciente).Include(a => a.Profissional)
                .Include(a => a.Prescricoes).Include(a => a.Exames).Include(a => a.Internacao)
                .AsSplitQuery().FirstOrDefaultAsync(m => m.Id == id);
            if (atendimento == null) return NotFound();
            return View(atendimento);
        }

        // GET: Atendimentos/Create
        public IActionResult Create()
        {
            ViewData["PacienteId"] = new SelectList(_context.Pacientes, "Id", "NomeCompleto");
            ViewData["ProfissionalId"] = new SelectList(_context.ProfissionaisDeSaude.Where(p => p.Ativo), "Id", "NomeCompleto");
            var atendimento = new Atendimento { DataHora = DateTime.Now };
            return View(atendimento);
        }

        // POST: Atendimentos/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("DataHora,Tipo,Status,Local,PacienteId,ProfissionalId")] Atendimento atendimento)
        {
            ModelState.Remove("Paciente");
            ModelState.Remove("Profissional");
            ModelState.Remove("Prontuario");
            if (ModelState.IsValid)
            {
                if (atendimento.DataHora < DateTime.Now.AddMinutes(-1))
                {
                    ModelState.AddModelError("DataHora", "A data do atendimento não pode ser no passado.");
                }
                var profissionalEstaAtivo = await _context.ProfissionaisDeSaude.AnyAsync(p => p.Id == atendimento.ProfissionalId && p.Ativo);
                if (!profissionalEstaAtivo)
                {
                    ModelState.AddModelError("ProfissionalId", "O profissional selecionado não está mais ativo no sistema.");
                }

                if (ModelState.ErrorCount == 0)
                {
                    var prontuario = await _context.Prontuarios.FirstOrDefaultAsync(p => p.PacienteId == atendimento.PacienteId);
                    if (prontuario == null)
                    {
                        prontuario = new Prontuario { Id = Guid.NewGuid(), PacienteId = atendimento.PacienteId, DataAbertura = DateTime.Now, NumeroProntuario = $"PRONT-{new Random().Next(10000, 99999)}", ObservacoesGerais = "Prontuário criado no primeiro atendimento." };
                        _context.Add(prontuario);
                    }
                    atendimento.ProntuarioId = prontuario.Id;
                    atendimento.Id = Guid.NewGuid();
                    _context.Add(atendimento);
                    await _context.SaveChangesAsync();
                    TempData["SuccessMessage"] = "Atendimento registrado com sucesso!";
                    return RedirectToAction(nameof(Index));
                }
            }
            ViewData["PacienteId"] = new SelectList(_context.Pacientes, "Id", "NomeCompleto", atendimento.PacienteId);
            ViewData["ProfissionalId"] = new SelectList(_context.ProfissionaisDeSaude.Where(p => p.Ativo), "Id", "NomeCompleto", atendimento.ProfissionalId);
            return View(atendimento);
        }

        // GET: Atendimentos/Edit/5 - COM REGRA DE NEGÓCIO
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null) return NotFound();
            var atendimento = await _context.Atendimentos.Include(a => a.Paciente).FirstOrDefaultAsync(a => a.Id == id);
            if (atendimento == null) return NotFound();

            // --- NOSSA NOVA REGRA DE NEGÓCIO ---
            if (atendimento.Status == "Realizado" || atendimento.Status == "Cancelado")
            {
                TempData["ErrorMessage"] = $"Este atendimento não pode ser editado, pois seu status é '{atendimento.Status}'.";
                return RedirectToAction(nameof(Details), new { id = id });
            }

            ViewData["ProfissionalId"] = new SelectList(_context.ProfissionaisDeSaude.Where(p => p.Ativo), "Id", "NomeCompleto", atendimento.ProfissionalId);
            return View(atendimento);
        }

        // POST: Atendimentos/Edit/5 - LÓGICA DE EDIÇÃO CORRIGIDA E SEGURA
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,DataHora,Tipo,Status,Local,PacienteId,ProfissionalId,ProntuarioId")] Atendimento atendimentoEditado)
        {
            if (id != atendimentoEditado.Id) return NotFound();

            var atendimentoParaAtualizar = await _context.Atendimentos.AsNoTracking().FirstOrDefaultAsync(a => a.Id == id);
            if (atendimentoParaAtualizar.Status == "Realizado" || atendimentoParaAtualizar.Status == "Cancelado")
            {
                ModelState.AddModelError("", "Não é possível salvar um atendimento que já foi Realizado ou Cancelado.");
            }

            ModelState.Remove("Paciente");
            ModelState.Remove("Profissional");
            ModelState.Remove("Prontuario");

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(atendimentoEditado);
                    await _context.SaveChangesAsync();
                    TempData["SuccessMessage"] = "Atendimento atualizado com sucesso!";
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AtendimentoExists(atendimentoEditado.Id)) return NotFound();
                    else throw;
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["ProfissionalId"] = new SelectList(_context.ProfissionaisDeSaude.Where(p => p.Ativo), "Id", "NomeCompleto", atendimentoEditado.ProfissionalId);
            return View(atendimentoEditado);
        }

        // ... (Métodos Delete e Exists completos) ...
        #region Delete e Exists
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null) return NotFound();
            var atendimento = await _context.Atendimentos.Include(a => a.Paciente).Include(a => a.Profissional).FirstOrDefaultAsync(m => m.Id == id);
            if (atendimento == null) return NotFound();
            return View(atendimento);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var atendimento = await _context.Atendimentos.FindAsync(id);
            if (atendimento != null)
            {
                _context.Atendimentos.Remove(atendimento);
                await _context.SaveChangesAsync();
                TempData["SuccessMessage"] = "Atendimento deletado com sucesso.";
            }
            return RedirectToAction(nameof(Index));
        }

        private bool AtendimentoExists(Guid id)
        {
            return _context.Atendimentos.Any(e => e.Id == id);
        }
        #endregion
    }
}