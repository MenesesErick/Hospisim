using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Hospisim.Data;
using Hospisim.Models;

namespace Hospisim.Controllers
{
    public class InternacaosController : Controller
    {
        private readonly HospisimContext _context;

        public InternacaosController(HospisimContext context)
        {
            _context = context;
        }

        // GET: Internacaos - AGORA COM BUSCA!
        public async Task<IActionResult> Index(string searchString)
        {
            ViewData["CurrentFilter"] = searchString;

            var internacoes = _context.Internacoes
                .Include(i => i.Atendimento)
                    .ThenInclude(a => a.Paciente)
                .AsQueryable();

            if (!String.IsNullOrEmpty(searchString))
            {
                internacoes = internacoes.Where(i => i.Atendimento.Paciente.NomeCompleto.Contains(searchString)
                                                  || i.Atendimento.Paciente.CPF.Contains(searchString));
            }

            return View(await internacoes.OrderByDescending(i => i.DataEntrada).ToListAsync());
        }

        // GET: Internacaos/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null) return NotFound();

            var internacao = await _context.Internacoes
                .Include(i => i.Atendimento)
                    .ThenInclude(a => a.Paciente)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (internacao == null) return NotFound();

            return View(internacao);
        }

        // GET: Internacaos/Create
        public async Task<IActionResult> Create(Guid atendimentoId)
        {
            if (atendimentoId == Guid.Empty) return NotFound();

            var atendimento = await _context.Atendimentos.Include(a => a.Paciente).FirstOrDefaultAsync(a => a.Id == atendimentoId);
            if (atendimento == null) return NotFound("Atendimento não encontrado.");

            var internacao = new Internacao { AtendimentoId = atendimentoId, Atendimento = atendimento };

            return View(internacao);
        }

        // POST: Internacaos/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("DataEntrada,PrevisaoAlta,MotivoInternacao,Leito,Quarto,Setor,PlanoSaudeUtilizado,ObservacoesClinicas,AtendimentoId")] Internacao internacao)
        {
            ModelState.Remove("Atendimento");
            ModelState.Remove("AltaHospitalar");

            if (ModelState.IsValid)
            {
                internacao.StatusInternacao = "Ativa";
                internacao.Id = Guid.NewGuid();
                _context.Add(internacao);
                await _context.SaveChangesAsync();
                return RedirectToAction("Details", "Atendimentos", new { id = internacao.AtendimentoId });
            }
            return View(internacao);
        }

        // GET: Internacaos/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null) return NotFound();

            var internacao = await _context.Internacoes
                .Include(i => i.Atendimento).ThenInclude(a => a.Paciente)
                .FirstOrDefaultAsync(i => i.Id == id);

            if (internacao == null) return NotFound();

            return View(internacao);
        }

        // POST: Internacaos/Edit/5 - LÓGICA DE ATUALIZAÇÃO MAIS CLARA
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,PrevisaoAlta,ObservacoesClinicas,StatusInternacao")] Internacao dadosDoFormulario)
        {
            if (id != dadosDoFormulario.Id)
            {
                return NotFound();
            }

            // 1. BUSCA a entidade original e completa do banco de dados.
            var internacaoParaAtualizar = await _context.Internacoes.FindAsync(id);
            if (internacaoParaAtualizar == null)
            {
                return NotFound();
            }

            // Não precisamos do "if (ModelState.IsValid)" aqui, porque estamos atualizando
            // um objeto que já sabemos que é válido (veio do banco) com dados de um
            // formulário simples que não tem validações complexas.

            try
            {
                // 2. ATUALIZA apenas as propriedades que vieram do formulário na entidade original.
                internacaoParaAtualizar.PrevisaoAlta = dadosDoFormulario.PrevisaoAlta;
                internacaoParaAtualizar.ObservacoesClinicas = dadosDoFormulario.ObservacoesClinicas;
                internacaoParaAtualizar.StatusInternacao = dadosDoFormulario.StatusInternacao;

                // 3. SALVA a entidade que foi atualizada.
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!InternacaoExists(internacaoParaAtualizar.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            // 4. Redireciona para a lista após o sucesso.
            return RedirectToAction(nameof(Index));
        }

        // ... Métodos Delete e Exists ...
        public async Task<IActionResult> Delete(Guid? id) { /* ... */ return View(); }
        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(Guid id) { /* ... */ return View(); }
        private bool InternacaoExists(Guid id) { return _context.Internacoes.Any(e => e.Id == id); }
    }
}