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
    public class PrescricaosController : Controller
    {
        private readonly HospisimContext _context;

        public PrescricaosController(HospisimContext context)
        {
            _context = context;
        }

        // GET: Prescricaos
        public async Task<IActionResult> Index()
        {
            var hospisimContext = _context.Prescricoes
                .Include(p => p.Atendimento).ThenInclude(a => a.Paciente)
                .Include(p => p.Profissional);
            return View(await hospisimContext.ToListAsync());
        }

        // GET: Prescricaos/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null) return NotFound();

            var prescricao = await _context.Prescricoes
                .Include(p => p.Atendimento).ThenInclude(a => a.Paciente)
                .Include(p => p.Profissional)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (prescricao == null) return NotFound();

            return View(prescricao);
        }

        // GET: Prescricaos/Create
        public IActionResult Create(Guid atendimentoId)
        {
            if (atendimentoId == Guid.Empty) return NotFound();
            var prescricao = new Prescricao { AtendimentoId = atendimentoId };
            ViewData["ProfissionalId"] = new SelectList(_context.ProfissionaisDeSaude.Where(p => p.Ativo), "Id", "NomeCompleto");
            return View(prescricao);
        }

        // POST: Prescricaos/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Medicamento,Dosagem,Frequencia,ViaAdministracao,DataInicio,DataFim,Observacoes,StatusPrescricao,ReacoesAdversas,AtendimentoId,ProfissionalId")] Prescricao prescricao)
        {
            ModelState.Remove("Atendimento");
            ModelState.Remove("Profissional");

            if (ModelState.IsValid)
            {
                prescricao.Id = Guid.NewGuid();
                _context.Add(prescricao);
                await _context.SaveChangesAsync();
                return RedirectToAction("Details", "Atendimentos", new { id = prescricao.AtendimentoId });
            }
            ViewData["ProfissionalId"] = new SelectList(_context.ProfissionaisDeSaude.Where(p => p.Ativo), "Id", "NomeCompleto", prescricao.ProfissionalId);
            return View(prescricao);
        }

        // GET: Prescricaos/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null) return NotFound();

            var prescricao = await _context.Prescricoes.FindAsync(id);
            if (prescricao == null) return NotFound();

            ViewData["ProfissionalId"] = new SelectList(_context.ProfissionaisDeSaude.Where(p => p.Ativo), "Id", "NomeCompleto", prescricao.ProfissionalId);
            return View(prescricao);
        }

        // POST: Prescricaos/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,StatusPrescricao,Observacoes")] Prescricao dadosDoFormulario)
        {
            if (id != dadosDoFormulario.Id) return NotFound();

            // 1. Busca a prescrição original do banco
            var prescricaoParaAtualizar = await _context.Prescricoes.FindAsync(id);
            if (prescricaoParaAtualizar == null) return NotFound();

            // 2. Atualiza apenas os campos que vieram do formulário de edição
            prescricaoParaAtualizar.StatusPrescricao = dadosDoFormulario.StatusPrescricao;
            prescricaoParaAtualizar.Observacoes = dadosDoFormulario.Observacoes;

            try
            {
                // 3. Salva o objeto atualizado
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PrescricaoExists(prescricaoParaAtualizar.Id)) return NotFound();
                else throw;
            }

            // Redireciona de volta para a tela de detalhes do atendimento
            return RedirectToAction("Details", "Atendimentos", new { id = prescricaoParaAtualizar.AtendimentoId });
        }

        // GET: Prescricaos/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null) return NotFound();

            var prescricao = await _context.Prescricoes
                .Include(p => p.Atendimento).ThenInclude(a => a.Paciente)
                .Include(p => p.Profissional)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (prescricao == null) return NotFound();

            return View(prescricao);
        }

        // POST: Prescricaos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            // 1. Busca a prescrição no banco
            var prescricao = await _context.Prescricoes.FindAsync(id);

            if (prescricao != null)
            {
                // 2. GUARDA o ID do atendimento antes de deletar
                var atendimentoId = prescricao.AtendimentoId;

                // 3. Deleta a prescrição
                _context.Prescricoes.Remove(prescricao);
                await _context.SaveChangesAsync();

                // 4. REDIRECIONA para a página de detalhes do atendimento original
                return RedirectToAction("Details", "Atendimentos", new { id = atendimentoId });
            }

            // Se a prescrição não for encontrada, volta para a lista geral como um fallback.
            return RedirectToAction(nameof(Index));
        }

        private bool PrescricaoExists(Guid id)
        {
            return _context.Prescricoes.Any(e => e.Id == id);
        }
    }
}