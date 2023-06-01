using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Android.Database;
using MauiForumApp.Services;
using MFA.Models;

namespace MFA.VIewModels
{
    public class MainPageViewModel
    {
        ObservableCollection<Topic> Topics;
        private TopicService service;
        public MainPageViewModel(TopicService service)
        {
            this.service = service;
            Topics = service.GenerateInfo(25);
        }
    }
}
