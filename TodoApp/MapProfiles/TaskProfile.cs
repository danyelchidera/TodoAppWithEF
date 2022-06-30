using AutoMapper;
using Data.ViewModel;

namespace TodoApp.MapProfiles
{
    public class TaskProfile: Profile
    {
        public TaskProfile()
        {
            CreateMap<Data.Models.Task, TaskViewModel>().ReverseMap();
        }
    }
}
