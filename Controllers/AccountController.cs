
using Lombard.Domain.ViewModels;
using Lombard.Helpers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Lombard.Controllers
{
    public class AccountController : Controller
    {
        public class Token
        {
            public int Code { get; set; }
            public bool hasError { get; set; }
            public string Message { get; set; }
            public Dictionary<string, string> Data { get; set; }
        }

        private readonly IBaseURL _baseURl;

        public AccountController(IBaseURL baseURL)
        {
            _baseURl = baseURL;
        }

        #region Otomall Login
        public IActionResult Login(string returnUrl)
        {

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginVm model, string returnUrl)
        {
            try
            {
                ConnectToApis<Token> c = new ConnectToApis<Token>(_baseURl.IdentityUrl);
                string[] headers = new string[1];
                headers[0] = "790ED398-3C37-4AB8-ADB7-78A4657DF703";
                var result = await c.PostUserForLogin("/api/login", model, headers[0]);
                if (result.Code == 0)
                {
                   HttpContext.Session.SetString("Token", result.Data["token"].ToString());

                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ViewBag.State = result.Message;
                }
            }
            catch (Exception ex)
            {

                ViewBag.State = ex.Message; //"Girişdə xata."; Error In Login.
            }

            return RedirectToAction("Login", "Account");

        }
        #endregion
    }
}
