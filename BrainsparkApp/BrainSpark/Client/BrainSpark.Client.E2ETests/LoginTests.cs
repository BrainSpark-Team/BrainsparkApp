// LoginTests.cs

using System.Threading.Tasks;
using Microsoft.Playwright;
using Xunit;

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

        [Fact]
    public async Task SuccessfulLoginTest()
{
        var page = await _context.NewPageAsync();
        await page.GotoAsync("https://gentle-pond-071fafa03.3.azurestaticapps.net/login");
    
        await page.ClickAsync("text=Login with Azure AD B2C");
        await page.FillAsync("input[type=email]", "nikagok979@chambile.com");
        await page.FillAsync("input[type=password]", "Parola12");
        await page.ClickAsync("#next");
    
        // Wait for navigation to complete and ensure we're on the home page
        await page.WaitForURLAsync("https://gentle-pond-071fafa03.3.azurestaticapps.net/");
    
        // Navigate to the /login page to check for the welcome text
        await page.GotoAsync("https://gentle-pond-071fafa03.3.azurestaticapps.net/login");
    
        // Check for the presence of the welcome text
        var welcomeText = await page.InnerTextAsync("text=Welcome, Temp-Name3!");
        Assert.NotNull(welcomeText);
}

        [Fact]
        public async Task IncorrectPasswordTest()
        {
            var page = await _context.NewPageAsync();
            await page.GotoAsync("https://gentle-pond-071fafa03.3.azurestaticapps.net/login");
            
            await page.ClickAsync("text=Login with Azure AD B2C");
            await page.FillAsync("input[type=email]", "nikagok979@chambile.com");
            await page.FillAsync("input[type=password]", "Parola13");
            await page.ClickAsync("#next");            
            var errorMsg = await page.InnerTextAsync("text=Your password is incorrect");
            Assert.NotNull(errorMsg);
        }

        [Fact]
        public async Task IncorrectEmailTest()
        {
            var page = await _context.NewPageAsync();
            await page.GotoAsync("https://gentle-pond-071fafa03.3.azurestaticapps.net/login");
            
            await page.ClickAsync("text=Login with Azure AD B2C");
            await page.FillAsync("input[type=email]", "nikagok9@chambile.com");
            await page.FillAsync("input[type=password]", "Parola12");
            await page.ClickAsync("#next");            
            var errorMsg = await page.InnerTextAsync("text=We can't seem to find your account");
            Assert.NotNull(errorMsg);
        }

        public void Dispose()
        {
            _context.CloseAsync().Wait();
            _browser.CloseAsync().Wait();
        }
    }
}
    