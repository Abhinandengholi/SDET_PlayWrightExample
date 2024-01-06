using CaseStudyPW.PWTests.Pages;
using Microsoft.Playwright.NUnit;
using Microsoft.Playwright;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using CaseStudyPW.Utilities;
using CaseStudyPW.Test_Data_Classes;

namespace CaseStudyPW.PWTests.Tests
{
    [TestFixture]
    public class SearchPageTest : PageTest
    {
        Dictionary<string, string>? Properties;
        string? currdir;
        private void ReadConfigSettings()
        {
            currdir = Directory.GetParent(@"../../../")?.FullName;
            Properties = new Dictionary<string, string>();               //declaring the dictionary
            string filename = currdir + "/ConfigSettings/config.properties"; //taking the file from working directory
            string[] lines = File.ReadAllLines(filename);

            foreach (string line in lines)                               //for geting the file data even if there are whitespaces
            {
                if (!string.IsNullOrWhiteSpace(line) && line.Contains("="))
                {
                    string[] parts = line.Split('=');
                    string key = parts[0].Trim();
                    string value = parts[1].Trim();
                    Properties[key] = value;
                }
            }
        }
        [SetUp]
        public async Task Setup()
        {
            ReadConfigSettings();
            Console.WriteLine("Opened browser");
            await Page.GotoAsync(Properties["baseUrl"]);
            Console.WriteLine("Page loaded");
        }

        [Test]
        public async Task SearchingTest()
        {
            HomePage homepage = new(Page);
            SearchResultPage srpage = new(Page);

            string? excelFilePath = currdir + "/Test Data/CSData.xlsx";
            string? sheetName = "SearchTerm";

            List<ATData> DataList = DataRead.ReadData(excelFilePath, sheetName);
            foreach (var DataRead in DataList)
            {
                string? searchtext = DataRead.SearchText;

                await homepage.Searching(searchtext);
                await srpage.Clkproduct();
                await Page.ScreenshotAsync(new()
                {
                    Path = currdir + "/Screenshots/ss_" + DateTime.Now.ToString("yyyy.mm.dd_HH.mm.ss") + ".png"
                });

                Assert.IsTrue(await srpage.CheckMsg());
            }
        }

    }
}