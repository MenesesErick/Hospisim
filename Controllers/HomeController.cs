using Microsoft.AspNetCore.Mvc;

namespace Hospisim.Controllers
{
    public class HomeController : Controller
    {
        // Não precisamos mais do DbContext aqui! O construtor pode ser removido ou deixado vazio.
        public HomeController()
        {
        }

        public IActionResult Index()
        {
            // Ele só precisa retornar a View estática.
            return View();
        }

        // Você pode apagar o método Privacy() se já apagou a View dele.
    }
}
