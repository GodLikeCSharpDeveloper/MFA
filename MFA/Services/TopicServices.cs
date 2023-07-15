using System.Text;
using Bogus;
using MFA.Models;

namespace MFA.Services
{
    public class TopicServices
    {
        public List<Topic> GenerateInfo(int number)
        {
            var faker = new Faker<Topic>().RuleFor(p => p.OwnerId, f => f.IndexFaker.ToString())
                                          .RuleFor(p => p.TopicTitle, f => f.Company.CompanyName())
                                          .RuleFor(p => p.TopicContent, f => f.Lorem.Paragraphs(5, 10, "/n"))
                                          .RuleFor(p => p.TopicReleaseDate, f => f.Date.FutureDateOnly().ToShortDateString())
                                          .RuleFor(p => p.TopicUpdateDate, f => f.Date.FutureDateOnly().ToShortDateString());
            return faker.Generate(number).ToList();
            //TODO DATABASE CONTENT POG POG PogChamp
        }
    }
}
