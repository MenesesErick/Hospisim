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

        // GET: Internacaos
        public async Task<IActionResult> Index()
        {
            // CORREÇÃO: Usando Include e ThenInclude para carregar o Paciente corretamente.
            var internacoes = await _context.Internacoes
                .Include(i => i.Atendimento)
                    .ThenInclude(a => a.Paciente)
                .OrderByDescending(i => i.DataEntrada)
                .ToListAsync();
            return View(internacoes);
        }

        // GET: Internacaos/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null) return NotFound();
            var internacao = await _context.Internacoes
                .Include(i => i.Atendimento.Paciente)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (internacao == null) return NotFound();
            return View(internacao);
        }

        // GET: Internacaos/Create
        public async Task<IActionResult> Create(Guid atendimentoId)
        {
            if (atendimentoId == Guid.Empty) return NotFound("Atendimento não especificado.");
            var atendimento = await _context.Atendimentos.Include(a => a.Paciente).FirstOrDefaultAsync(a => a.Id == atendimentoId);
            if (atendimento == null) return NotFound("Atendimento não encontrado.");
            var internacao = new Internacao { AtendimentoId = atendimentoId, Atendimento = atendimento, DataEntrada = DateTime.Now };
            return View(internacao);
        }

        // POST: Internacaos/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("DataEntrada,PrevisaoAlta,MotivoInternacao,Leito,Quarto,Setor,AtendimentoId")] Internacao internacao)
        {
            // A SOLUÇÃO: Ignoramos a validação de todos os campos que não vêm diretamente do formulário
            ModelState.Remove("Atendimento");
            ModelState.Remove("AltaHospitalar");
            ModelState.Remove("StatusInternacao"); // <-- A LINHA QUE FALTAVA

            if (ModelState.IsValid)
            {
                // Agora, com a validação passando, podemos definir o status com segurança
                internacao.StatusInternacao = "Ativa";
                internacao.Id = Guid.NewGuid();
                _context.Add(internacao);
                await _context.SaveChangesAsync();
                return RedirectToAction("Details", "Atendimentos", new { id = internacao.AtendimentoId });
            }

            // Se a validação falhar por outro motivo (ex: DataEntrada em branco),
            // precisamos recarregar os dados do atendimento para a tela não quebrar.
            internacao.Atendimento = await _context.Atendimentos
                                                   .Include(a => a.Paciente)
                                                   .FirstOrDefaultAsync(a => a.Id == internacao.AtendimentoId);

            return View(internacao);
        }

        // GET: Internacaos/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null) return NotFound();
            var internacao = await _context.Internacoes.Include(i => i.Atendimento.Paciente).FirstOrDefaultAsync(i => i.Id == id);
            if (internacao == null) return NotFound();
            return View(internacao);
        }

        // POST: Internacaos/Edit/5 - CORRIGIDO com o padrão "Buscar e Atualizar"
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

            // 2. ATUALIZA apenas as propriedades que vieram do formulário na entidade original.
            internacaoParaAtualizar.PrevisaoAlta = dadosDoFormulario.PrevisaoAlta;
            internacaoParaAtualizar.ObservacoesClinicas = dadosDoFormulario.ObservacoesClinicas;
            internacaoParaAtualizar.StatusInternacao = dadosDoFormulario.StatusInternacao;

            try
            {
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

            // CORREÇÃO DO REDIRECIONAMENTO: Volta para os detalhes da internação.
            return RedirectToAction(nameof(Details), new { id = internacaoParaAtualizar.Id });
        }

        // GET: Internacaos/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null) return NotFound();
            var internacao = await _context.Internacoes.Include(i => i.Atendimento.Paciente).FirstOrDefaultAsync(m => m.Id == id);
            if (internacao == null) return NotFound();
            return View(internacao);
        }

        // POST: Internacaos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var internacao = await _context.Internacoes.FindAsync(id);
            if (internacao != null)
            {
                var atendimentoId = internacao.AtendimentoId; // Guarda o ID
                _context.Internacoes.Remove(internacao);
                await _context.SaveChangesAsync();
                return RedirectToAction("Details", "Atendimentos", new { id = atendimentoId }); // Usa o ID guardado
            }
            return RedirectToAction(nameof(Index));
        }

        private bool InternacaoExists(Guid id)
        {
            return _context.Internacoes.Any(e => e.Id == id);
        }
    }
}