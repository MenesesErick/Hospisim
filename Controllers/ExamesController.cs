using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Hospisim.Data;
using Hospisim.Models;

namespace Hospisim.Controllers
{
    public class ExamesController : Controller
    {
        private readonly HospisimContext _context;

        public ExamesController(HospisimContext context)
        {
            _context = context;
        }

        // GET: Exames - COM BUSCA
        public async Task<IActionResult> Index(string searchString)
        {
            ViewData["CurrentFilter"] = searchString;

            var exames = _context.Exames
                .Include(e => e.Atendimento.Paciente)
                .AsQueryable();

            if (!String.IsNullOrEmpty(searchString))
            {
                exames = exames.Where(e => e.Tipo.Contains(searchString)
                                        || e.Atendimento.Paciente.NomeCompleto.Contains(searchString));
            }

            return View(await exames.OrderByDescending(e => e.DataSolicitacao).ToListAsync());
        }

        // GET: Exames/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null) return NotFound();
            var exame = await _context.Exames
                .Include(e => e.Atendimento.Paciente)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (exame == null) return NotFound();
            return View(exame);
        }

        // GET: Exames/Create
        public IActionResult Create(Guid atendimentoId)
        {
            if (atendimentoId == Guid.Empty) return NotFound("Atendimento não especificado.");
            var exame = new Exame { AtendimentoId = atendimentoId, DataSolicitacao = DateTime.Now, Status = "Solicitado" };
            return View(exame);
        }

        // POST: Exames/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Tipo,AtendimentoId")] Exame exame)
        {
            // A SOLUÇÃO: Ignoramos a validação destes campos que preenchemos manualmente no código.
            ModelState.Remove("Atendimento");
            ModelState.Remove("Status");
            ModelState.Remove("DataSolicitacao");

            if (ModelState.IsValid)
            {
                // Agora, com a validação passando, podemos definir os valores com segurança.
                exame.Id = Guid.NewGuid();
                exame.DataSolicitacao = DateTime.Now;
                exame.Status = "Solicitado";

                _context.Add(exame);
                await _context.SaveChangesAsync();

                return RedirectToAction("Details", "Atendimentos", new { id = exame.AtendimentoId });
            }

            // Se falhar (ex: campo Tipo em branco), retorna para a View
            return View(exame);
        }

        // GET: Exames/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null) return NotFound();
            var exame = await _context.Exames.Include(e => e.Atendimento.Paciente).FirstOrDefaultAsync(e => e.Id == id);
            if (exame == null) return NotFound();
            return View(exame);
        }

        // POST: Exames/Edit/5 - LÓGICA CORRIGIDA
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,DataRealizacao,Resultado,Status,AtendimentoId")] Exame exameDoFormulario)
        {
            if (id != exameDoFormulario.Id) return NotFound();

            var exameParaAtualizar = await _context.Exames.FindAsync(id);
            if (exameParaAtualizar == null) return NotFound();

            // Atualiza o objeto do banco com os dados que vieram do formulário
            exameParaAtualizar.DataRealizacao = exameDoFormulario.DataRealizacao;
            exameParaAtualizar.Resultado = exameDoFormulario.Resultado;
            exameParaAtualizar.Status = exameDoFormulario.Status;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ExameExists(exameParaAtualizar.Id)) return NotFound();
                else throw;
            }
            // REDIRECIONAMENTO CORRIGIDO
            return RedirectToAction("Details", "Atendimentos", new { id = exameParaAtualizar.AtendimentoId });
        }

        // GET: Exames/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null) return NotFound();
            var exame = await _context.Exames.Include(e => e.Atendimento.Paciente).FirstOrDefaultAsync(m => m.Id == id);
            if (exame == null) return NotFound();
            return View(exame);
        }

        // POST: Exames/Delete/5 - REDIRECIONAMENTO CORRIGIDO
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var exame = await _context.Exames.FindAsync(id);
            if (exame != null)
            {
                var atendimentoId = exame.AtendimentoId; // Guarda o ID antes de deletar
                _context.Exames.Remove(exame);
                await _context.SaveChangesAsync();
                return RedirectToAction("Details", "Atendimentos", new { id = atendimentoId }); // Usa o ID guardado
            }
            return RedirectToAction(nameof(Index));
        }

        private bool ExameExists(Guid id)
        {
            return _context.Exames.Any(e => e.Id == id);
        }
    }
}