using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;
using Tesseract;

namespace Notes.Pages
{
    public class OCRModel : PageModel
    {
        public void OnGet()
        {
        }

        [Required]
        [BindProperty]
        public IFormFile? File { get; set; }

        public async Task<IActionResult> OnPostUpload() 
        {
            if (File != null)
            {
                using var engine = new TesseractEngine(@"./tessdata", "spa", EngineMode.Default);

                using var memoryStream = new MemoryStream();
                await File.CopyToAsync(memoryStream);
                memoryStream.Position = 0;

                // have to load Pix via a bitmap since Pix doesn't support loading a stream.
                using var image = new System.Drawing.Bitmap(memoryStream);

                using var pix = PixConverter.ToPix(image);
                using var resized = pix.Scale(5, 5);

                using var page = engine.Process(resized);

                var meanConfidence = page.GetMeanConfidence();
                var text = page.GetText();

                if (!String.IsNullOrWhiteSpace(text))
                {
                    var resultString = Regex.Replace(text.Trim(), @"^\s+$[\r\n]*", string.Empty, RegexOptions.Multiline);
                    TempData["fileText"] = resultString;
                    TempData["meanConfidence"] = meanConfidence.ToString();
                }
            }

            return Redirect("/ocr");
        }

        
    }
}
