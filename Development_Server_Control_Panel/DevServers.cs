using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Development_Server_Control_Panel
{
    class DevServers
    {
        private int id;
        private string projectName;
        private string projectDirectory;
        private string startCommand;
        private string portNumber;

        public DevServers(int id, string projectName, string projectDirectory, string startCommand, string portNumber)
        {
            this.id = id;
            this.projectName = projectName;
            this.projectDirectory = projectDirectory;
            this.startCommand = startCommand;
            this.portNumber = portNumber;
        }


        public int Id { get => id; set => id = value; }
        public string ProjectName { get => projectName; set => projectName = value; }
        public string ProjectDirectory { get => projectDirectory; set => projectDirectory = value; }
        public string StartCommand { get => startCommand; set => startCommand = value; }
        public string PortNumber { get => portNumber; set => portNumber = value; }


        public override string ToString()
        {
            return this.projectName + " \t" + this.projectDirectory + " \t" + this.startCommand + " \t" + this.portNumber;
        }
    }
}
