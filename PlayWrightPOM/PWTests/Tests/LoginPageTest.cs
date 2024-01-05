using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using PlayWrightPOM.PWTests.Pages;
using PlayWrightPOM.Test_Data_classes;
using PlayWrightPOM.Utilities;

namespace PlayWrightPOM.PWTests.Tests
{
    public class LoginPageTest:PageTest
    {
        Dictionary<string, string>? Properties;
        string? currdir;
        private void ReadConfigSettings()
        {

            Properties = new Dictionary<string, string>();//declaring  the dictionary
            currdir = Directory.GetParent(@"../../../")?.FullName;
            string filename = currdir + "/ConfigSettings/config.properties";//taking the file from wworking directory
            string[] lines = File.ReadAllLines(filename);
            foreach (string line in lines)//for getting file data even if there are whitespace
            {
                if (!string.IsNullOrWhiteSpace(line) && line.Contains('='))
                {
                    string[] parts = line.Split('=');
                    string key = parts[0].Trim();
                    string value = parts[1].Trim();
                    Properties[key] = value;
                }
            }
        }
        [SetUp]
        public async Task SetUp()
        {
            ReadConfigSettings();
            Console.WriteLine("Opened browser");
            await Page.GotoAsync(Properties["baseUrl"]);
            Console.WriteLine("Page loaded");
        }
        /*
         [Test]
        
        public async Task LoginTest()
        {
            LoginPage loginpage=new (Page);
            await loginpage.ClickLoginLink();
            await loginpage.Login("admin", "password");
            Assert.IsTrue(await loginpage.CheckWelMsg());
        }
       
        [TestCase("admin","password")]
        */

        [Test]
      
        public async Task LoginTest1()
        {
            // LoginPage loginpage = new(Page);
            NewLoginPage loginpage = new(Page);

            string? excelFilePath = currdir + "/Test Data/EAData.xlsx";
            string? sheetName = "Login Data";

            List<EAText> excelDataList = DataRead.ReadLoginData(excelFilePath, sheetName);

            foreach (var excelData in excelDataList)
            {
                string? username = excelData.UserName;
                string? password = excelData.Password;
                await loginpage.ClickLoginLink();
                await loginpage.Login(username, password);
                await Page.ScreenshotAsync(new()
                {
                    Path= currdir + "/Screenshots/ss_" + DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".png",
                //FullPage=true

            });

                Assert.IsTrue(await loginpage.CheckWelMsg());
            }
        }
    }
}