using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Deployment.Application;
using System.Windows.Input;

namespace Intāfēsu
{
    class ViewModelMainWindow : ViewModelBase
    {
        private ICommand _changePageCommand;
        public ICommand ChangePageCommand { get; set; }

        private bool CanExecuteChangePageCommand(object parameter)
        {
            return true;
        }

        private Uri _MainFrameSource;
        public Uri MainFrameSource { get => _MainFrameSource;
            set
            {
                _MainFrameSource = value;
                OnPropertyChanged(nameof(MainFrameSource));
            }
        }

        public ViewModelMainWindow()
        {
            
            ChangePageCommand = new RelayCommand(ChangePage, CanExecuteChangePageCommand);
            


            readCurrentVersion(); //about page please
        }

        private void ChangePage(object parameter)
        {
            var selectedButton = (System.Windows.Controls.Button) parameter;
            var buttonName = selectedButton.Name ;

            string[] separator = { "Page" };
            var pageName = buttonName.Split(separator, 2, StringSplitOptions.RemoveEmptyEntries);
            MainFrameSource = new Uri("Pages/" + pageName[0] + ".Page.xaml", UriKind.Relative);
        }


     //   public void updatePageSourceOnClick(object sender, RoutedEventArgs e)
     //   {
     //       // Convert RoutedEventArgs to a button
     //       if (e == null) return;
     //       // Retrieve element name
     //       if (!(e.Source is Button buttonSrc)) return;
     //       var buttonName = buttonSrc.Name;
     //
     //       string[] separator = { "Page" };
     //       // Split the buttonName to parse the matching page name.
     //       var pageName = buttonName.Split(separator, 2, StringSplitOptions.RemoveEmptyEntries);
     //       // Update the frame to the relevant page
     //       MainFrame.Source = new Uri("Pages/" + pageName[0] + ".Page.xaml", UriKind.Relative);
     //
     //       // set active buttons to disabled
     //       foreach (var b in MainNavigation.FindChildren<Button>())
     //       {
     //           b.IsEnabled = b != buttonSrc;
     //       }
     //       // trigger page transition
     //       FrameTransition.Reload();
     //   }


        #region ThisGoesInAbout



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
        public void readCurrentVersion()
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
        #endregion

    }
}
