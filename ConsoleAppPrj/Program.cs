﻿using Microsoft.Playwright;

//playwright startup
using var playwright=await Playwright.CreateAsync();

//launch browser
await using var browser=await playwright.Chromium.LaunchAsync();

//page instance
var context = await browser.NewContextAsync();
var page = await context.NewPageAsync();
Console.WriteLine("Opened browser");
await page.GotoAsync("https://www.google.com");
Console.WriteLine("Page loaded");



string title=await page.TitleAsync();
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



