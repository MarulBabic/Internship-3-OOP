using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManagerApp.Classes
{
    public class Task
    {
        public string taskName {  get; set; }
        public string taskDescription{  get; set; }
        public DateTime deadline {  get; set; }
        public TaskStatus status;
        public int expectedDurationMinutes {  get; set; }
        private string projectName {  get; set; }
        private Guid taskId;
        private Guid projectId {  get; set; }

        public Task(string taskName, string taskDescription,DateTime deadline,TaskStatus status,int expectedDurationMinutes,string projectName,Guid id)
        {
            this.taskName = taskName;
            this.taskDescription = taskDescription;
            this.deadline = deadline;
            this.status = status;
            this.expectedDurationMinutes = expectedDurationMinutes;
            this.projectName = projectName;
            projectId = id;
            taskId = Guid.NewGuid();
        }

        public Task(string taskName, string taskDescription, DateTime deadline, int expectedDurationMinutes, string projectName, Guid id)
        {
            this.taskName = taskName;
            this.taskDescription = taskDescription;
            this.deadline = deadline;
            this.status = TaskStatus.Active;
            this.expectedDurationMinutes = expectedDurationMinutes;
            this.projectName = projectName;
            projectId = id;
            taskId = Guid.NewGuid();
        }

        public string GetProjectName()
        {
            return this.projectName;
        }
    }
}
