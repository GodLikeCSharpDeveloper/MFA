using System.Text.Json;
using Realms;
using Realms.Sync;
using MFAUsers = MFA.Models.User;
using User = Realms.Sync.User;

namespace MFA.Services.DBService
{
    public static class RealmService
    {
        public static bool ServiceInitialised;

        public static Realms.Sync.App app;

        public static Realm MainThreadRealm;

        public static User CurrentUser => app.CurrentUser;

        public static async Task Init()
        {
            if (ServiceInitialised)
            {
                return;
            }
            await using Stream fileStream = await FileSystem.Current.OpenAppPackageFileAsync("atlasConfig.json");
            using StreamReader reader = new StreamReader(fileStream);
            var fileContent = await reader.ReadToEndAsync();
            var config = JsonSerializer.Deserialize<RealmAppConfig>(fileContent,
                new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

            var appConfiguration = new Realms.Sync.AppConfiguration(config.AppId)
            {
                BaseUri = new Uri(config.BaseUrl),
                SyncTimeoutOptions = new SyncTimeoutOptions()
            };

            app = Realms.Sync.App.Create(appConfiguration);
            ServiceInitialised = true;
            
        }

        public static Realm GetMainThreadRealm()
        {
            return MainThreadRealm ??= GetRealm();
        }

        public static Realm GetRealm()
        {
            if (app.CurrentUser == null) return Realm.GetInstance();
            var config = new FlexibleSyncConfiguration(app.CurrentUser)
            {
                PopulateInitialSubscriptions = (realm) =>
                {
                    realm.Subscriptions.Add(realm.All<Topic>(), new SubscriptionOptions());
                    realm.Subscriptions.Add(realm.All<ImageData>(), new SubscriptionOptions());
                    realm.Subscriptions.Add(realm.All<MFAUsers>(),  new SubscriptionOptions());
                    realm.Subscriptions.Add(realm.All<UsersComment>(), new SubscriptionOptions());
                    realm.Subscriptions.Add(realm.All<UserLikes>(), new SubscriptionOptions());
                }
            };

            return Realm.GetInstance(config);
        }
        

        public static async Task SetSubscription(Realm realm, SubscriptionType subType)
        {
            if (GetCurrentSubscriptionType(realm) == subType)
            {
                return;
            }

            realm.Subscriptions.Update(() =>
            {
                realm.Subscriptions.RemoveAll(true);

                var (query, queryName) = GetQueryForSubscriptionType(realm, subType);

                realm.Subscriptions.Add(query, new SubscriptionOptions { Name = queryName });
            });

            //There is no need to wait for synchronization if we are disconnected
            if (realm.SyncSession.ConnectionState != ConnectionState.Disconnected)
            {
                await realm.Subscriptions.WaitForSynchronizationAsync();
            }
        }

        public static SubscriptionType GetCurrentSubscriptionType(Realm realm)
        {
            var activeSubscription = realm.Subscriptions.FirstOrDefault();

            return activeSubscription.Name switch
            {
                "all" => SubscriptionType.All,
                "mine" => SubscriptionType.Mine,
                _ => throw new InvalidOperationException("Unknown subscription type")
            };
        }

        private static (IQueryable<MFAUsers> Query, string Name) GetQueryForSubscriptionType(Realm realm, SubscriptionType subType)
        {
            IQueryable<MFAUsers> query = null;
            string queryName = null;

            if (subType == SubscriptionType.Mine)
            {
                query = realm.All<MFAUsers>();
                queryName = "mine";
            }
            else if (subType == SubscriptionType.All)
            {
                query = realm.All<MFAUsers>();
                queryName = "all";
            }
            else
            {
                throw new ArgumentException("Unknown subscription type");
            }

            return (query, queryName);
        }
    }




}
