using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Hospisim.Data;
using Hospisim.Models;

namespace Hospisim.Controllers
{
    // SUGESTÃO: Renomeie o arquivo e a classe para "PrescricoesController" (com 'e'), que é a convenção.
    public class PrescricaosController : Controller
    {
        private readonly HospisimContext _context;

        public PrescricaosController(HospisimContext context)
        {
            _context = context;
        }

        // GET: Prescricoes
        public async Task<IActionResult> Index()
        {
            // MELHORIA: Usamos ThenInclude para poder mostrar o nome do paciente na lista de prescrições.
            var hospisimContext = _context.Prescricoes
                .Include(p => p.Atendimento)
                    .ThenInclude(a => a.Paciente)
                .Include(p => p.Profissional);

            return View(await hospisimContext.ToListAsync());
        }

        // GET: Prescricoes/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null) return NotFound();

            var prescricao = await _context.Prescricoes
                .Include(p => p.Atendimento)
                    .ThenInclude(a => a.Paciente)
                .Include(p => p.Profissional)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (prescricao == null) return NotFound();

            return View(prescricao);
        }

        // GET: Prescricoes/Create
        // CORREÇÃO: Este método agora está perfeito e funcional como você fez.
        public IActionResult Create(Guid atendimentoId)
        {
            if (atendimentoId == Guid.Empty) return NotFound();

            var prescricao = new Prescricao { AtendimentoId = atendimentoId };

            ViewData["ProfissionalId"] = new SelectList(_context.ProfissionaisDeSaude, "Id", "NomeCompleto");

            return View(prescricao);
        }

        // POST: Prescricoes/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Medicamento,Dosagem,Frequencia,ViaAdministracao,DataInicio,DataFim,Observacoes,StatusPrescricao,ReacoesAdversas,AtendimentoId,ProfissionalId")] Prescricao prescricao)
        {
            // CORREÇÃO CRÍTICA: Ignora a validação dos objetos de navegação.
            ModelState.Remove("Atendimento");
            ModelState.Remove("Profissional");

            if (ModelState.IsValid)
            {
                prescricao.Id = Guid.NewGuid();
                _context.Add(prescricao);
                await _context.SaveChangesAsync();
                // MELHORIA: Redireciona de volta para a tela de detalhes do atendimento original.
                return RedirectToAction("Details", "Atendimentos", new { id = prescricao.AtendimentoId });
            }

            // CORREÇÃO: Popula o dropdown com o NomeCompleto em caso de erro.
            ViewData["ProfissionalId"] = new SelectList(_context.ProfissionaisDeSaude, "Id", "NomeCompleto", prescricao.ProfissionalId);
            return View(prescricao);
        }

        // As ações Edit e Delete seguem um padrão similar de correção.
        // Foque em fazer o fluxo de Criação funcionar primeiro.
    }
}