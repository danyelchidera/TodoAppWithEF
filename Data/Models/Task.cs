using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Models
{
    public class Task
    {
        public Task()
        {

        }
        public Task(string task)
        {
            TodoTask = task;
        }
        public int Id { get; set; }
        public string TodoTask { get; set; }
        public DateTime Date { get; set; }
    }
}
