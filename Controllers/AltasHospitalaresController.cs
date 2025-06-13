using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Hospisim.Data;
using Hospisim.Models;

namespace Hospisim.Controllers
{
    public class AltasHospitalaresController : Controller
    {
        private readonly HospisimContext _context;

        public AltasHospitalaresController(HospisimContext context)
        {
            _context = context;
        }

        // GET: AltasHospitalares
        public async Task<IActionResult> Index()
        {
            var altas = await _context.AltasHospitalares
                .Include(a => a.Internacao.Atendimento.Paciente) // Busca o paciente através da internação e do atendimento
                .OrderByDescending(a => a.DataAlta)
                .ToListAsync();
            return View(altas);
        }

        // GET: AltasHospitalares/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null) return NotFound();

            var altaHospitalar = await _context.AltasHospitalares
                .Include(a => a.Internacao.Atendimento.Paciente)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (altaHospitalar == null) return NotFound();

            return View(altaHospitalar);
        }

        // GET: AltasHospitalares/Create
        public async Task<IActionResult> Create(Guid internacaoId)
        {
            if (internacaoId == Guid.Empty) return NotFound();

            var internacao = await _context.Internacoes
                .Include(i => i.Atendimento.Paciente)
                .FirstOrDefaultAsync(i => i.Id == internacaoId);

            if (internacao == null) return NotFound("Internação não encontrada.");

            var alta = new AltaHospitalar
            {
                InternacaoId = internacaoId,
                Internacao = internacao, // Anexa o objeto para usar na View
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
                var internacaoOriginal = await _context.Internacoes.FindAsync(altaHospitalar.InternacaoId);
                if (internacaoOriginal != null)
                {
                    internacaoOriginal.StatusInternacao = "Alta concedida";
                    _context.Update(internacaoOriginal);
                }

                altaHospitalar.Id = Guid.NewGuid();
                altaHospitalar.DataAlta = DateTime.Now;
                _context.Add(altaHospitalar);

                await _context.SaveChangesAsync();

                return RedirectToAction("Details", "Internacaos", new { id = altaHospitalar.InternacaoId });
            }
            return View(altaHospitalar);
        }

        // GET: AltasHospitalares/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null) return NotFound();
            var altaHospitalar = await _context.AltasHospitalares.FindAsync(id);
            if (altaHospitalar == null) return NotFound();
            return View(altaHospitalar);
        }

        // POST: AltasHospitalares/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,DataAlta,CondicaoPaciente,InstrucoesPosAlta,InternacaoId")] AltaHospitalar altaHospitalar)
        {
            if (id != altaHospitalar.Id) return NotFound();

            ModelState.Remove("Internacao");

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(altaHospitalar);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AltaHospitalarExists(altaHospitalar.Id)) return NotFound();
                    else throw;
                }
                return RedirectToAction(nameof(Index));
            }
            return View(altaHospitalar);
        }

        // ... (Delete e Exists)
        private bool AltaHospitalarExists(Guid id) => _context.AltasHospitalares.Any(e => e.Id == id);
    }
}