using Microsoft.Playwright.NUnit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment
{
    internal class NaaptolPWTest : PageTest
    {
        [SetUp]
        public async Task SetUp()
        {
            Console.WriteLine("Opened browser");
            await Page.GotoAsync("https://www.naaptol.com/");
            Console.WriteLine("Page loaded");

        }
        [Test]
        public async Task ProudctsearchTest()
        {
            
            await Page.FillAsync(selector: "#header_search_text", "eyewear");
            await Console.Out.WriteLineAsync("Typed");
            await Page.Locator("(//input[@value='search'])[2]").ClickAsync();
            await Console.Out.WriteLineAsync("searched");

        }
    }
}
