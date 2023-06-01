using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bogus;
using MFA.Models;

namespace MauiForumApp.Services
{
    public class TopicService
    {
       List<Topic> Topics = new();
        StringBuilder StringBuild = new StringBuilder();
        public List<Topic> GenerateInfo(int number)
        {
            var faker = new Faker<Topic>().RuleFor(p=>p.Id, f=>f.IndexFaker)
                                          .RuleFor(p => p.TopicTitle, f => f.Company.CompanyName())
                                          .RuleFor(p => p.TopicContent, f => f.Lorem.Paragraphs(5,10,"/n"))
                                          .RuleFor(p =>p.TopicReleaseDate,f=>f.Date.FutureDateOnly().ToShortDateString())
                                          .RuleFor(p => p.TopicUpdateDate, f => f.Date.FutureDateOnly().ToShortDateString());
            return faker.Generate(number).ToList();
            //TODO DATABASE CONTENT POG POG PogChamp

        }
    }
}
