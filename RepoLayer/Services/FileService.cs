using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

public class FileService
{
    private readonly IWebHostEnvironment _environment;

    public FileService(IWebHostEnvironment environment)
    {
        _environment = environment;
    }

    public async Task<Tuple<int, string>> SaveImage(IFormFile imageFile)
    {
        try
        {
            var contentPath = _environment.ContentRootPath;
            var path = Path.Combine(contentPath, "Uploads");

            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);

            // Check the allowed extensions
            var ext = Path.GetExtension(imageFile.FileName);
            var allowedExtensions = new string[] { ".jpg", ".png", ".jpeg" };

            if (!allowedExtensions.Contains(ext))
            {
                string msg = string.Format("Only {0} extensions are allowed", string.Join(", ", allowedExtensions));
                return new Tuple<int, string>(0, msg);
            }
            string uniqueString = Guid.NewGuid().ToString();
            string fileName = uniqueString + ext;
            string fullPath = Path.Combine(path, fileName);
            using (var fileStream = new FileStream(fullPath, FileMode.Create))
            {
                await imageFile.CopyToAsync(fileStream);
            }

            return new Tuple<int, string>(1, "Image saved successfully");
        }
        catch (Exception ex)
        {
            return new Tuple<int, string>(0, "An error occurred: " + ex.Message);
        }
    }
}
