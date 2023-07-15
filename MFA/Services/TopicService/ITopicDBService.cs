namespace MFA.Services.TopicService
{
    public interface ITopicDBService
    {
        public List<Topic> GetAllTopics();

        public async Task<bool> AddNewTopic(Topic topic)
        {
            throw new NotImplementedException();
        }



    }
}
