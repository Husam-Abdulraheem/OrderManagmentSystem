using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Formats;
using SixLabors.ImageSharp.Formats.Jpeg;
using SixLabors.ImageSharp.Formats.Png;
using SixLabors.ImageSharp.Processing;

namespace OrderManagementSystem.Services
{
    public class ImageService
    {
        public static async Task<IFormFile> ResizeAndCompressImage(IFormFile formFile)
        {
            if (formFile == null || formFile.Length == 0)
            {
                return null;
            }

            var fileExtension = Path.GetExtension(formFile.FileName).ToLowerInvariant();
            var validExtensions = new[] { ".jpg", ".jpeg", ".png", ".svg" };
            if (!validExtensions.Contains(fileExtension))
            {
                throw new ArgumentException("Invalid image file format.");
            }

            using (var inputMemoryStream = new MemoryStream())
            {
                await formFile.CopyToAsync(inputMemoryStream);
                inputMemoryStream.Position = 0;

                try
                {
                    using (var image = await Image.LoadAsync(inputMemoryStream))
                    {
                        image.Mutate(x => x.Resize(new ResizeOptions
                        {
                            Mode = ResizeMode.Max,
                            Size = new Size(200, 200)
                        }));

                        var outputMemoryStream = new MemoryStream();
                        IImageEncoder encoder = fileExtension switch
                        {
                            ".jpg" or ".jpeg" => new JpegEncoder { Quality = 75 },
                            ".png" => new PngEncoder(),
                            _ => throw new NotSupportedException($"File extension {fileExtension} is not supported.")
                        };

                        await image.SaveAsync(outputMemoryStream, encoder);
                        outputMemoryStream.Position = 0;

                        var resizedImageFile = new FormFile(outputMemoryStream, 0, outputMemoryStream.Length, formFile.Name, formFile.FileName)
                        {
                            Headers = new HeaderDictionary(),
                            ContentType = formFile.ContentType
                        };

                        return resizedImageFile;
                    }
                }
                catch (Exception ex)
                {
                    throw new InvalidOperationException("An error occurred while processing the image.", ex);
                }
            }
        }
    }
}
