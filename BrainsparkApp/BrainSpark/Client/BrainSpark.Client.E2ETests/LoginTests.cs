using System;
using System.Threading.Tasks;
using Microsoft.Playwright;
using NUnit.Framework;

namespace BrainSpark.Client.E2ETests
{
    public class LoginTests : IDisposable
    {
        private readonly IBrowser _browser;
        private readonly IBrowserContext _context;

        public LoginTests()
        {
            var playwright = Playwright.CreateAsync().Result;
            _browser = playwright.Chromium.LaunchAsync().Result;
            _context = _browser.NewContextAsync().Result;
        }

        [Test]
        public async Task SuccessfulLoginTest()
        {
            var page = await _context.NewPageAsync();
            await page.GotoAsync("https://gentle-pond-071fafa03.3.azurestaticapps.net/login");
    
            await page.ClickAsync("text=Login with Azure AD B2C");
            await page.FillAsync("input[type=email]", "nikagok979@chambile.com");
            await page.FillAsync("input[type=password]", "Parola12");
            await page.ClickAsync("#next");
    
            await page.WaitForURLAsync("https://gentle-pond-071fafa03.3.azurestaticapps.net/");
            await page.GotoAsync("https://gentle-pond-071fafa03.3.azurestaticapps.net/login");
    
            var welcomeText = await page.InnerTextAsync("text=Welcome, Temp-Name3!");
            Assert.IsNotNull(welcomeText);
        }

        [Test]
        public async Task IncorrectPasswordTest()
        {
            var page = await _context.NewPageAsync();
            await page.GotoAsync("https://gentle-pond-071fafa03.3.azurestaticapps.net/login");
            
            await page.ClickAsync("text=Login with Azure AD B2C");
            await page.FillAsync("input[type=email]", "nikagok979@chambile.com");
            await page.FillAsync("input[type=password]", "Parola13");
            await page.ClickAsync("#next");
            
            var errorMsg = await page.InnerTextAsync("text=Your password is incorrect");
            Assert.IsNotNull(errorMsg);
        }

        [Test]
        public async Task IncorrectEmailTest()
        {
            var page = await _context.NewPageAsync();
            await page.GotoAsync("https://gentle-pond-071fafa03.3.azurestaticapps.net/login");
            
            await page.ClickAsync("text=Login with Azure AD B2C");
            await page.FillAsync("input[type=email]", "nikagok9@chambile.com");
            await page.FillAsync("input[type=password]", "Parola12");
            await page.ClickAsync("#next");
            
            var errorMsg = await page.InnerTextAsync("text=We can't seem to find your account");
            Assert.IsNotNull(errorMsg);
        }

        public void Dispose()
        {
            _context.CloseAsync().Wait();
            _browser.CloseAsync().Wait();
        }
    }
}