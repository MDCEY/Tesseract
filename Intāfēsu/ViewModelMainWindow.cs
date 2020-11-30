using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Deployment.Application;

namespace Intāfēsu
{
    class ViewModelMainWindow : ViewModelBase
    {
        private string _currentVersion;

        public string CurrentVersion
        {
            get => _currentVersion;
            set
            {
                _currentVersion = value;
                OnPropertyChanged(nameof(CurrentVersion));
            }
        }

        public ViewModelMainWindow()
        {
            try
            {
                //// get deployment version
                CurrentVersion = ApplicationDeployment.CurrentDeployment.CurrentVersion.ToString();
            }
            catch (InvalidDeploymentException)
            {
                //// you cannot read publish version when app isn't installed 
                //// (e.g. during debug)
                CurrentVersion = "not installed";
            }
        }
    }
}
