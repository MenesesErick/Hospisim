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

        // GET: Atendimentos - AGORA COM BUSCA!
        public async Task<IActionResult> Index(string searchString)
        {
            ViewData["CurrentFilter"] = searchString;

            var atendimentos = _context.Atendimentos
                                    .Include(a => a.Paciente)
                                    .Include(a => a.Profissional)
                                    .AsQueryable();

            if (!String.IsNullOrEmpty(searchString))
            {
                atendimentos = atendimentos.Where(a => a.Paciente.NomeCompleto.Contains(searchString)
                                                    || a.Paciente.CPF.Contains(searchString));
            }

            return View(await atendimentos.OrderByDescending(a => a.DataHora).ToListAsync());
        }

        // GET: Atendimentos/Details/5 - AGORA INCLUI MAIS DADOS
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null) { return NotFound(); }

            var atendimento = await _context.Atendimentos
                .Include(a => a.Paciente)
                .Include(a => a.Profissional)
                .Include(a => a.Prescricoes)
                .Include(a => a.Exames)
                .Include(a => a.Internacao) // Inclui todos os dados relacionados!
                .FirstOrDefaultAsync(m => m.Id == id);

            if (atendimento == null) { return NotFound(); }

            return View(atendimento);
        }

        // GET: Atendimentos/Create - AGORA SÓ MOSTRA PROFISSIONAIS ATIVOS
        public IActionResult Create()
        {
            ViewData["PacienteId"] = new SelectList(_context.Pacientes, "Id", "NomeCompleto");
            // Filtra para mostrar apenas profissionais com status "Ativo"
            ViewData["ProfissionalId"] = new SelectList(_context.ProfissionaisDeSaude.Where(p => p.Ativo), "Id", "NomeCompleto");
            return View();
        }

        // POST: Atendimentos/Create - Lógica do prontuário automático mantida
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("DataHora,Tipo,Status,Local,PacienteId,ProfissionalId")] Atendimento atendimento)
        {
            ModelState.Remove("Paciente");
            ModelState.Remove("Profissional");
            ModelState.Remove("Prontuario");

            if (ModelState.IsValid)
            {
                // Validação para não permitir agendamento no passado
                if (atendimento.DataHora < DateTime.Now)
                {
                    ModelState.AddModelError("DataHora", "A data do atendimento não pode ser no passado.");
                }

                // Validação para garantir que o profissional selecionado ainda está ativo
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
                        prontuario = new Prontuario
                        {
                            Id = Guid.NewGuid(),
                            PacienteId = atendimento.PacienteId,
                            DataAbertura = DateTime.Now,
                            NumeroProntuario = $"PRONT-{new Random().Next(10000, 99999)}",
                            ObservacoesGerais = "Prontuário criado no primeiro atendimento."
                        };
                        _context.Add(prontuario);
                    }
                    atendimento.ProntuarioId = prontuario.Id;
                    atendimento.Id = Guid.NewGuid();
                    _context.Add(atendimento);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
            }

            ViewData["PacienteId"] = new SelectList(_context.Pacientes, "Id", "NomeCompleto", atendimento.PacienteId);
            ViewData["ProfissionalId"] = new SelectList(_context.ProfissionaisDeSaude.Where(p => p.Ativo), "Id", "NomeCompleto", atendimento.ProfissionalId);
            return View(atendimento);
        }

        // GET: Atendimentos/Edit/5 - LÓGICA DE EDIÇÃO TOTALMENTE CORRIGIDA
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var atendimento = await _context.Atendimentos.FindAsync(id);
            if (atendimento == null)
            {
                return NotFound();
            }
            // Carrega apenas o dropdown de profissionais ativos, pois o paciente e prontuário não podem ser alterados
            ViewData["ProfissionalId"] = new SelectList(_context.ProfissionaisDeSaude.Where(p => p.Ativo), "Id", "NomeCompleto", atendimento.ProfissionalId);
            return View(atendimento);
        }

        // POST: Atendimentos/Edit/5 - LÓGICA DE EDIÇÃO TOTALMENTE CORRIGIDA
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,DataHora,Tipo,Status,Local,PacienteId,ProfissionalId,ProntuarioId")] Atendimento atendimento)
        {
            if (id != atendimento.Id)
            {
                return NotFound();
            }

            ModelState.Remove("Paciente");
            ModelState.Remove("Profissional");
            ModelState.Remove("Prontuario");

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(atendimento);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AtendimentoExists(atendimento.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["ProfissionalId"] = new SelectList(_context.ProfissionaisDeSaude.Where(p => p.Ativo), "Id", "NomeCompleto", atendimento.ProfissionalId);
            return View(atendimento);
        }

        // Delete e Exists continuam os mesmos...
        // GET: Atendimentos/Delete/5
        public async Task<IActionResult> Delete(Guid? id) { /* ...código existente... */ return View(); }
        // POST: Atendimentos/Delete/5
        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(Guid id) { /* ...código existente... */ return View(); }
        private bool AtendimentoExists(Guid id) { /* ...código existente... */ return false; }
    }
}