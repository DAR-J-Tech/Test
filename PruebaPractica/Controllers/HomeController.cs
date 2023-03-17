using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using PruebaPractica.Models;
using System.Diagnostics;
using System.Security.Claims;
using PruebaPractica.Services;

namespace PruebaPractica.Controllers
{
    public class HomeController : Controller
    {
        private readonly IApiService _service;

        public HomeController(IApiService service)
        {
            _service = service;
        }

        public IActionResult Index()
        {
            return View("Login");
        }

        public async Task<IActionResult> BuscadorAsync()
        {
            string user = HttpContext.Session.GetString("user");
            if (!String.IsNullOrEmpty(user))
            {
                ViewBag.user = HttpContext.Session.GetString("user");
                var datos = await _service.GetBuscador();
                return View("Index", datos);
            }
            return View();
        }

        public IActionResult Ciudadano()
        {
            return View();
        }

        #region Acceso al Sistema
        [Route("Login")]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> Login(string user, string pass)
        {
            string usuario = user.ToUpper(); 
            if (!String.IsNullOrEmpty(user))
            {
                if (usuario.Equals("BUSCADOR"))
                {
                    var claims = new List<Claim>
                        {
                            new Claim("user",usuario)
                        };
                    var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));
                    HttpContext.Session.SetString("user", usuario);
                    return RedirectToAction("Buscador");
                }
                if (usuario.Equals("CIUDADANO"))
                {
                    var claims = new List<Claim>
                        {
                            new Claim("user",usuario)
                        };
                    var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));
                    HttpContext.Session.SetString("user", usuario);
                    return RedirectToAction("Ciudadano");
                }
                TempData["AlertLogin"] = "Usuario o Contraseña incorrectas.";
                return View("Login");
            }
            else
            {
                TempData["AlertLogin"] = "Usuario o Contraseña incorrectas.";
                return View("Login");
            }
        }

        [Route("Logout")]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            HttpContext.Session.Remove("user");
            return View("Login");
        }
        #endregion

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}