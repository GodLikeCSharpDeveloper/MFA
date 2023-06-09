﻿using Bogus;
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
                    realm.Add<Topic>(topic);
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
