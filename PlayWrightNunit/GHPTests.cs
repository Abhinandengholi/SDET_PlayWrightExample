using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using Newtonsoft.Json;

namespace PlayWrightNunit
{
    public class GHPTests:PageTest
    {
        [SetUp]
        public void Setup()
        {
        }
        //Manual Instance
        /*   [Test]
           public async Task Test1()
           {

               //playwright startup
               using var playwright = await Playwright.CreateAsync();

               //launch browser
               await using var browser = await playwright.Chromium.LaunchAsync(
               new BrowserTypeLaunchOptions
               {
                   Headless = false
               });

               //page instance
               var context = await browser.NewContextAsync();
               var page = await context.NewPageAsync();
               Console.WriteLine("Opened browser");
               await page.GotoAsync("https://www.google.com");
               Console.WriteLine("Page loaded");



               string title = await page.TitleAsync();
               Console.WriteLine(title);

               //await page.GetByTitle("Search").FillAsync("hp laptop");
               await page.Locator("#APjFqb").FillAsync("selenium");
               Console.WriteLine("Typed");
               //await page.GetByRole("button").ClickAsync();
               //Console.WriteLine("Clicked");
               await page.Locator("(//input[@value='Google Search'])[2]").ClickAsync();
               Console.WriteLine("Clicked");


               title = await page.TitleAsync();
               Console.WriteLine(title);


           }
        */

        [Test]//Playwright managed instance
        public async Task Test2()
        {

            Console.WriteLine("Opened browser");
            await Page.GotoAsync("https://www.google.com");
            Console.WriteLine("Page loaded");



            string title = await Page.TitleAsync();
            Console.WriteLine(title);

            //await page.GetByTitle("Search").FillAsync("hp laptop");
            await Page.Locator("#APjFqb").FillAsync("selenium");
            Console.WriteLine("Typed");
           
            await Page.Locator("(//input[@value='Google Search'])[2]").ClickAsync();
            Console.WriteLine("Clicked");


            //title = await Page.TitleAsync();
            //Console.WriteLine(title);
            //Assert.That(title, Does.Contain("selenium"));//Nunit asertion
          
            await Expect(Page).ToHaveTitleAsync("selenium - Google Search");


        }
        
    }
}