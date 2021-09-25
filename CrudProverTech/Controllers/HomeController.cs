using CrudProverTech.Data;
using CrudProverTech.Models;
using CrudProverTech.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace CrudProverTech.Controllers
{

    public class HomeController : Controller
    {
        private readonly UserManager<Usuario> _gerenciadorUsuario;
        private readonly SignInManager<Usuario> _gerenciadorLogin;
        private readonly RoleManager<NivelAcesso> _roleManager;
        private readonly ApplicationDbContext _context;

        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger, UserManager<Usuario> gerenciadorUsuario, SignInManager<Usuario> gerenciadorLogin, RoleManager<NivelAcesso> roleManager, ApplicationDbContext context)
        {
            _gerenciadorUsuario = gerenciadorUsuario;
            _gerenciadorLogin = gerenciadorLogin;
            _roleManager = roleManager;
            _context = context;

            _logger = logger;
        }

        [Authorize]
        public IActionResult Index()
        {
            return View();
        }

        [Authorize]
        public async Task<IActionResult> AssociarUsuario()
        {
            ViewData["UsuarioId"] = new SelectList(await _context.Usuarios.ToListAsync(), "Id", "UserName");
            ViewData["NivelAcessoId"] = new SelectList(await _context.NiveisAcesso.ToListAsync(), "Name", "Name");
            return View();
        }
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> AssociarUsuario(UsuariosRolesViewModel usuarioRoles)
        {
            if (ModelState.IsValid)
            {
                var usuario = await _gerenciadorUsuario.FindByIdAsync(usuarioRoles.UsuarioId);
                await _gerenciadorUsuario.AddToRoleAsync(usuario, usuarioRoles.NivelAcessoId);

                return RedirectToAction("Index", "Home");
            }
            return View(usuarioRoles);
        }

        public async Task<IActionResult> LogOut()
        {
            await _gerenciadorLogin.SignOutAsync();
            return RedirectToAction(nameof(Login));
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(UsuarioViewModel usuarioModel)
        {
            if (ModelState.IsValid)
            {
                var usu = _gerenciadorUsuario.FindByNameAsync(usuarioModel.NomeUsuario);
                Usuario usuario = usu.Result;
                if(usuario != null)
                {
                    bool validacao =  await _gerenciadorUsuario.CheckPasswordAsync(usuario, usuarioModel.Senha);
                    if (validacao)
                    {
                        await _gerenciadorLogin.SignInAsync(usuario, false);
                       return RedirectToAction(nameof(Index));
                    }
                }
            }
            TempData["Messagem"] = "Usuário ou senha incorretos";
            return View(usuarioModel);
        }

        [Authorize]
        [HttpGet]
        public IActionResult NovoNivelAcesso()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> NovoNivelAcesso(NivelAcesso nivelAcesso)
        {
            if (ModelState.IsValid)
            {
                bool roleExiste = await _roleManager.RoleExistsAsync(nivelAcesso.Name);
                if (!roleExiste)
                {
                    await _roleManager.CreateAsync(nivelAcesso);
                    return RedirectToAction("Index", "Home");
                }
            }
            
            return View(nivelAcesso);
        }

        public IActionResult Registro()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Registro(RegistroViewModel registro)
        {
            if (ModelState.IsValid)
            {
                var usuario = new Usuario
                {
                    UserName = registro.NomeUsuario,
                    Nome = registro.Nome,
                    Sobrenome = registro.Sobrenome,
                    Idade = registro.Idade,
                    Email = registro.Email
                };

                var usuarioCriado = await _gerenciadorUsuario.CreateAsync(usuario, registro.Senha);

                if (usuarioCriado.Succeeded)
                {
                    //await _gerenciadorLogin.SignInAsync(usuario, false);
                    TempData["Message"] = "Cadastro efeturado com sucesso!";
                    
                    return RedirectToAction("Login", "Home");
                }
            }

            return View(registro);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
