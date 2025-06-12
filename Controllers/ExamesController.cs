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

        // GET: Exames/Create
        public IActionResult Create(Guid atendimentoId)
        {
            if (atendimentoId == Guid.Empty)
            {
                return NotFound("É necessário um atendimento para solicitar um exame.");
            }

            var exame = new Exame
            {
                AtendimentoId = atendimentoId,
                DataSolicitacao = DateTime.Now, // Preenche a data de solicitação automaticamente
                Status = "Solicitado" // Define o status inicial
            };

            return View(exame);
        }

        // POST: Exames/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Tipo,DataSolicitacao,Status,AtendimentoId")] Exame exame)
        {
            // Ignora a validação do objeto de navegação, pois recebemos apenas o ID
            ModelState.Remove("Atendimento");

            if (ModelState.IsValid)
            {
                exame.Id = Guid.NewGuid();
                _context.Add(exame);
                await _context.SaveChangesAsync();
                // Redireciona de volta para a tela de detalhes do atendimento original
                return RedirectToAction("Details", "Atendimentos", new { id = exame.AtendimentoId });
            }
            return View(exame);
        }
    }
}