using Bogus;
using MFA.Services.DBService;
using Realms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MFA.Services.TopicService
{
    public class TopicDbService : ITopicDBService
    {
        List<Topic> _topics;
        public TopicDbService()
        {


        }

        public List<Topic> GetAllTopics()
        {
            try
            {
                var realm = RealmService.GetRealm();
                var topicList = realm.All<Topic>().ToList();
                return topicList;
            }
            catch (Exception ex)
            {
                Shell.Current.DisplayAlert("Error", ex.Message, "OK");
                return new List<Topic>();
            }
        }


        public async Task<bool> AddNewTopic(Topic topic)
        {
            try
            {
                var realm = RealmService.GetRealm();
                await realm.WriteAsync(() =>
                {
                    realm.Add(topic);
                });
                return true;
            }
            catch (Exception ex)
            {
                await Shell.Current.DisplayAlert("Error", ex.Message, "OK");
                return false;
            }
        }
    }
}
