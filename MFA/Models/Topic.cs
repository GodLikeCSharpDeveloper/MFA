using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MFA.Models
{
    public class Topic
    {
        public int Id { get; set; }
        public string TopicTitle { get; set; }
        public string TopicContent { get; set; }
        public string TopicReleaseDate { get; set; }
        public string TopicUpdateDate { get; set; }
    }
}
