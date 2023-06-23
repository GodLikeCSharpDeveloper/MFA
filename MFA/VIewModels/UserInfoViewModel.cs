using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using MFA.Services.DBService;
using MongoDB.Driver.Linq;
using Realms;
using static MFA.ViewModels.MainPageViewModel;


namespace MFA.ViewModels
{
    public partial class UserInfoViewModel : BaseViewModel
    {
        private static User _user;

        public static MFA.Models.User user
        {
            get
            {
                var realm = RealmService.GetRealm();
                return realm.All<User>().FirstOrDefault(x=>x.Email==RealmService.CurrentUser.Profile.Email);
            }
            set
            {
                _user = value;
            }
        }



        

        [ObservableProperty]
        public ImageSource avatarData = ImageSource.FromStream(() =>
        {
            if(user?.Image!=null)
            return new MemoryStream(user.Image.Data);
            return null;
        });

        [RelayCommand]
        public async void KillAvatar()
        {
            AvatarData = null;
        }

        [RelayCommand]
        public async void ChangeAvatar()
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

                    ImageSource imageSource = ImageSource.FromStream(() => new MemoryStream(imageData));
                    AvatarData = imageSource;
                    using var realm = RealmService.GetRealm();
                    await realm.WriteAsync(() =>
                    {
                        user.Image = image;
                    });
                    
                }
            }

            catch (Exception ex)
            {

            }
        }

    }
}
