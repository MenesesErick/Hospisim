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
    public class PacientesController : Controller
    {
        private readonly HospisimContext _context;

        public PacientesController(HospisimContext context)
        {
            _context = context;
        }

        // GET: Pacientes
        public async Task<IActionResult> Index(string searchString)
        {
            // Guarda o termo de busca para exibi-lo de volta na caixa de texto
            ViewData["CurrentFilter"] = searchString;

            var pacientes = from p in _context.Pacientes
                            select p;

            if (!String.IsNullOrEmpty(searchString))
            {
                // Filtra a lista, buscando pelo nome ou pelo CPF
                pacientes = pacientes.Where(s => s.NomeCompleto.Contains(searchString)
                                              || s.CPF.Contains(searchString));
            }

            return View(await pacientes.OrderBy(p => p.NomeCompleto).ToListAsync());
        }

        // GET: Pacientes/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            // A CORREÇÃO ESTÁ AQUI: Usando Include e ThenInclude para carregar TUDO
            var paciente = await _context.Pacientes
                .Include(p => p.Prontuarios)              // Carrega os prontuários
                .Include(p => p.Atendimentos)             // Carrega a lista de atendimentos
                    .ThenInclude(a => a.Profissional)   // Para cada atendimento, carrega o nome do profissional
                .FirstOrDefaultAsync(m => m.Id == id);

            if (paciente == null)
            {
                return NotFound();
            }

            return View(paciente);
        }

        // GET: Pacientes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Pacientes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("NomeCompleto,CPF,DataNascimento,Sexo,TipoSanguineo,Telefone,Email,EnderecoCompleto,NumeroCartaoSUS,EstadoCivil,PossuiPlanoSaude")] Paciente paciente)
        {
            if (ModelState.IsValid)
            {
                // --- INÍCIO DA NOSSA NOVA VALIDAÇÃO ---
                bool cpfJaExiste = await _context.Pacientes.AnyAsync(p => p.CPF == paciente.CPF);

                if (cpfJaExiste)
                {
                    ModelState.AddModelError("CPF", "Este CPF já está cadastrado no sistema.");
                    return View(paciente);
                }
                // --- FIM DA VALIDAÇÃO ---

                // Se o CPF é único, o código continua normalmente...
                paciente.Id = Guid.NewGuid();
                _context.Add(paciente);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(paciente);
        }

        // GET: Pacientes/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var paciente = await _context.Pacientes.FindAsync(id);
            if (paciente == null)
            {
                return NotFound();
            }
            return View(paciente);
        }

        // POST: Pacientes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,NomeCompleto,CPF,DataNascimento,Sexo,TipoSanguineo,Telefone,Email,EnderecoCompleto,NumeroCartaoSUS,EstadoCivil,PossuiPlanoSaude")] Paciente paciente)
        {
            if (id != paciente.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(paciente);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PacienteExists(paciente.Id))
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
            return View(paciente);
        }

        // GET: Pacientes/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var paciente = await _context.Pacientes
                .FirstOrDefaultAsync(m => m.Id == id);
            if (paciente == null)
            {
                return NotFound();
            }

            return View(paciente);
        }

        // POST: Pacientes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var paciente = await _context.Pacientes.FindAsync(id);
            if (paciente != null)
            {
                _context.Pacientes.Remove(paciente);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PacienteExists(Guid id)
        {
            return _context.Pacientes.Any(e => e.Id == id);
        }
    }


}
