using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MFA.Services.DBService
{
    public interface ITopicDBService
    {
        public List<Topic> GetAllTopics();
        public bool AddNewTopic(Topic topic);
    }
}
