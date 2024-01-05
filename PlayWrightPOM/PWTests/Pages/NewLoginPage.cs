using Microsoft.Playwright;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlayWrightPOM.PWTests.Pages
{
    internal class NewLoginPage
    {

        private IPage? _page;
        private ILocator? _lnkLogin => _page?.Locator(selector: "text=Login");
        private ILocator? _inUserName => _page?.Locator(selector: "#UserName");
        private ILocator? _inPassword => _page?.Locator(selector: "#Password");
        private ILocator? _btnLogin => _page?.Locator(selector: "input", new PageLocatorOptions
        {
            HasTextString = "Log in"
        });
        private ILocator? LinkWelMess => _page?.Locator(selector: "text='Hello admin!'");

        public NewLoginPage(IPage? page) => _page = page;

        public async Task ClickLoginLink() => await _lnkLogin.ClickAsync();

        public async Task Login(string username, string password)
        {
            await _inUserName.FillAsync(username);
            await _inPassword.FillAsync(password);
            await _btnLogin.ClickAsync();
        }
        public async Task<bool> CheckWelMsg() => await LinkWelMess.IsVisibleAsync();

    }
}
   
  