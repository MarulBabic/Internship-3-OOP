using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManagerApp.Classes
{
    public class Project
    {
        public string projectName {  get; set; }
        public string projectDescription { get; set; }
        public DateTime startDate {  get; set; }
        public DateTime finishDate{  get; set; }
        public ProjectStatus status;
        private Guid projectId;


        public Project(string name, string description, DateTime startDate, DateTime finishDate, ProjectStatus status)
        {
            this.projectName = name;
            this.projectDescription = description;
            this.startDate = startDate;
            this.finishDate = finishDate;
            this.status = status;
            projectId = Guid.NewGuid();
        }

        public Project(string name, string description, DateTime startDate, DateTime finishDate)
        {
            this.projectName = name;
            this.projectDescription = description;
            this.startDate = startDate;
            this.finishDate = finishDate;
            this.status = ProjectStatus.Active;
            projectId = Guid.NewGuid();
        }


        public Guid getId()
        {
            return projectId;
        }
    }
}
