using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Hospisim.Data;
using Hospisim.Models;
using Microsoft.EntityFrameworkCore;

namespace Hospisim.Controllers
{
    public class AltasHospitalaresController : Controller
    {
        private readonly HospisimContext _context;

        public AltasHospitalaresController(HospisimContext context)
        {
            _context = context;
        }

        // GET: AltasHospitalares/Create
        public IActionResult Create(Guid internacaoId)
        {
            if (internacaoId == Guid.Empty) return NotFound();

            var alta = new AltaHospitalar
            {
                InternacaoId = internacaoId,
                DataAlta = DateTime.Now
            };
            return View(alta);
        }

        // POST: AltasHospitalares/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CondicaoPaciente,InstrucoesPosAlta,InternacaoId")] AltaHospitalar altaHospitalar)
        {
            ModelState.Remove("Internacao");

            if (ModelState.IsValid)
            {
                // --- INÍCIO DA LÓGICA EXTRA ---

                // 1. Busca a internação original para atualizar seu status
                var internacaoOriginal = await _context.Internacoes.FindAsync(altaHospitalar.InternacaoId);
                if (internacaoOriginal != null)
                {
                    internacaoOriginal.StatusInternacao = "Alta concedida"; // Atualiza o status
                    _context.Update(internacaoOriginal);
                }

                // --- FIM DA LÓGICA EXTRA ---

                altaHospitalar.Id = Guid.NewGuid();
                altaHospitalar.DataAlta = DateTime.Now;
                _context.Add(altaHospitalar);

                await _context.SaveChangesAsync(); // Salva as duas alterações (na Alta e na Internação)

                // Redireciona para a lista de internações
                return RedirectToAction("Index", "Internacaos");
            }
            return View(altaHospitalar);
        }
    }
}