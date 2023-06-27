using Bogus;
using Realms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MFA.Services.DBService
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
                using var realm = RealmService.GetRealm();
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
                    //for (var i=0; i<100000; i++)
                    //{
                    //    var faker = new Faker<Topic>().RuleFor(p => p.OwnerId, f => f.IndexFaker.ToString())
                    //        .RuleFor(p => p.TopicTitle, f => f.Company.CompanyName())
                    //        .RuleFor(p => p.TopicContent, f => f.Lorem.Paragraphs(5, 10, "/n"))
                    //        .RuleFor(p => p.TopicReleaseDate, f => f.Date.FutureDateOnly().ToShortDateString())
                    //        .RuleFor(p => p.TopicUpdateDate, f => f.Date.FutureDateOnly().ToShortDateString());
                    //    _topics = faker.Generate(1).ToList();
                    //     realm.Add(_topics);
                    //     Debug.WriteLine($"current is {i}");
                    //}

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
