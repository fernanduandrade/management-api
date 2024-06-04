using PuppeteerSharp;
using PuppeteerSharp.Media;
using Razor.Templating.Core;

namespace Manager.Application.Shared;

public static class Helper
{
    public static async Task<Stream> CreateReport<T>(T data, string directory, string file)
    {
        var fullPath = Path.Combine("ViewsReport", directory, file);
        var html = await RazorTemplateEngine.RenderAsync(fullPath, data);

        return await GeneratePdf(html);
    }

    private static async Task<Stream> GeneratePdf(string html)
    {
        await using var browser = await Puppeteer.LaunchAsync(new LaunchOptions
        {
            Headless = true,
            ExecutablePath = "/snap/bin/chromium",
            Args = ["--no-sandbox"]
        });
        await using var page = await browser.NewPageAsync();
        await page.EmulateMediaTypeAsync(MediaType.Print);

        await page.SetContentAsync(html);
        var pdfContent = await page.PdfStreamAsync(new PdfOptions
        {
            Format = PaperFormat.A4,
            PrintBackground = true,
        });

        return pdfContent;   
    }
}
