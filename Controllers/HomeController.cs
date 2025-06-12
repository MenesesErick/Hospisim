using Microsoft.AspNetCore.Mvc;

namespace Hospisim.Controllers
{
    public class HomeController : Controller
    {
        // N�o precisamos mais do DbContext aqui! O construtor pode ser removido ou deixado vazio.
        public HomeController()
        {
        }

        public IActionResult Index()
        {
            // Ele s� precisa retornar a View est�tica.
            return View();
        }

        // Voc� pode apagar o m�todo Privacy() se j� apagou a View dele.
    }
}
