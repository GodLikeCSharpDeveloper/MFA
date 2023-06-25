using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using MFA.Services.DBService;
using MFA.Services.NotificationService;
using Image = Microsoft.Maui.Controls.Image;

namespace MFA.Utility.ImageManager
{
    public class ImageManager : IImageManager
    {
        INotificationService notificationService;
        public ImageManager(INotificationService notificationService)
        {
            this.notificationService = notificationService;
        }
        public async Task<ImageData> ChangeAvatar()
        {
            var upload = new FilePickerFileType(new Dictionary<DevicePlatform, IEnumerable<string>>
            {
                { DevicePlatform.iOS, new[] { "public.image" } },
                { DevicePlatform.Android, new[] { "image/*"} }
            });
            var option = new PickOptions
            {
                PickerTitle = "Select a new avatar",
                FileTypes = upload,
            };
            try
            {
                var result = await FilePicker.PickAsync(option);
                if (result == null) return null;
                if ((result.FileName.EndsWith("jpg", StringComparison.OrdinalIgnoreCase) ||
                     result.FileName.EndsWith("svg", StringComparison.OrdinalIgnoreCase)))
                {
                    var stream = await result.OpenReadAsync();
                    byte[] imageData;
                    using (var memoryStream = new MemoryStream())
                    {
                        await stream.CopyToAsync(memoryStream);
                        imageData = memoryStream.ToArray();
                    }

                    var image = new ImageData
                    {
                        Name = result.FileName,
                        ContentType = result.ContentType,
                        Data = imageData
                    };


                    return image;

                }
                await notificationService.Notify("Wrong type");
                return null;
            }

            catch (Exception ex)
            {
                await notificationService.Notify(ex.ToString());
                return null;
            }
        }
        public static async Task<byte[]> ReadTextFile()
        {
            var assembly = Assembly.GetExecutingAssembly();
            var defaultNamespace = assembly.GetName().Name;
            // Get the resource stream for the image file
            var resourceNames = assembly.GetManifestResourceNames();
            var resourcePath = resourceNames.FirstOrDefault(name => name.EndsWith("dotnet_bot.svg"));
            var resourceStream = assembly.GetManifestResourceStream(resourcePath);

            if (resourceStream != null)
            {
                // Read the stream into a byte array
                byte[] imageBytes;
                using (var memoryStream = new MemoryStream())
                {
                    await resourceStream.CopyToAsync(memoryStream);
                    imageBytes = memoryStream.ToArray();
                }

                // Use the byte array as needed
                // ...
                return imageBytes;
            }
            return null;
        }
    }
}
