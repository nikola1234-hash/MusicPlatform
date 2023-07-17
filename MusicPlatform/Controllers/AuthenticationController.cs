using Microsoft.AspNetCore.Mvc;
using MusicPlatform.Models.AuthenticationModels;
using MusicPlatform.Services.Authentication;

namespace MusicPlatform.Controllers
{
    public class AuthenticationController : Controller
    {
        private readonly IAuthenticationService auth;
        private readonly ISessionService _session;
        private readonly ILogger<AuthenticationController> _logger;

        public AuthenticationController(IAuthenticationService auth, ISessionService session, ILogger<AuthenticationController> logger)
        {
            this.auth = auth;
            _session = session;
            _logger = logger;
        }

        public IActionResult Login()
        {
            bool isAuthenticated = _session.IsAuthenticated();
            if(isAuthenticated)
            {
                return RedirectToAction("Index", "Home");
            }

            LoginForm form = new LoginForm();
            return View(form);
        }

        public IActionResult LoginSubmit(LoginForm form)
        {
            try
            {

                if (form is null)
                {
                    return RedirectToAction(nameof(Login));
                }

                if (ModelState.IsValid)
                {
                    var result = auth.Login(form.Username, form.Password);
                    if (result == AuthenticationResult.InvalidUsernameOrPassword)
                    {
                        ModelState.AddModelError("InvalidUsernameOrPassword", "Invalid username or password");
                        return View("Login");
                    }
                    if (result == AuthenticationResult.UserNotFound)
                    {
                        ModelState.AddModelError("UserNotFound", "User not found");
                        return View("Login");
                    }

                    return RedirectToAction("Index", "Home");

                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);  
                return View("Error");
            }

            return View("Login");
        }



        public IActionResult Register(RegisterForm form)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return Json(new { success = false, message = "Invalid data" });
                }
                var usernameExists = auth.UsernameExist(form.Username);

                if (usernameExists)
                {
                    ModelState.AddModelError("UsernameExists", "Username already exists");
                    return Json(new { success = false, message = "Username exists" });
                }

                var emailExists = auth.EmailExist(form.Email);
                if (emailExists)
                {
                    ModelState.AddModelError("EmailExists", "Email already exists");
                    return Json(new { success = false, message = "Email exists" });
                }


                auth.Register(form.Username, form.Password, form.Email);
            }
            catch (Exception ex)
            {

                _logger.LogError(ex.Message);
                return Json(new { success = false, message = "Error" });
            }

            return Ok(new { success = true, message = "User registered" });
        }

        public IActionResult Logout()
        {
           if(_session.IsAuthenticated())
           {
                auth.Logout();
           }
            return RedirectToAction("Index", "Home");
        }
    }
}
