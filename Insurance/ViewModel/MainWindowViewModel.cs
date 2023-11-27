using BLL;
using KonstLab2;
using Ninject;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows;
using System.Runtime.CompilerServices;

namespace Insurance.ViewModel
{
    public class MainWindowViewModel : INotifyPropertyChanged
    {
        #region Command

        private RelayCommand navigate;
        public RelayCommand NavigateCommand
        {
            get
            {
                return navigate ?? (navigate = new RelayCommand(obj =>
                {
                    var real = (string)obj;
                    switch (real)
                    {
                        case "Auto":
                            NavigateToAutoPage();
                            break;
                        case "Insurance":
                            NavigateToInsurancePage();
                            break;
                        case "Incident":
                            NavigateToIncidentPage();
                            break;
                        case "Clients":
                            NavigateToClientsPage();
                            break;
                    }
                }));
            }
        }

        private RelayCommand logOutCommand;
        public RelayCommand LogOutCommand
        {
            get
            {
                return logOutCommand ?? (logOutCommand = new RelayCommand(obj =>
                {
                    authServ.LogOut();
                    authFrame.Navigate(new View.AuthorizationPage(authFrame, authServ, this));
                }));
            }
        }

        private RelayCommand close;
        public RelayCommand CloseCommand
        {
            get
            {
                return close ?? (close = new RelayCommand(obj =>
                {
                    CloseWindow();
                }));
            }
        }

        private RelayCommand maxmin;
        public RelayCommand MaxMinCommand
        {
            get
            {
                return maxmin ?? (maxmin = new RelayCommand(obj =>
                {
                    MaxMinWindow();
                }));
            }
        }

        #endregion

        #region NotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        private bool _isAgent;
        public bool IsAgent
        {

            get { return _isAgent; }
            set
            {
                _isAgent = value;
                NotifyPropertyChanged("IsAgent");
            }
        }
        #endregion

        private MainWindow mainWindow;
        private Frame mainFrame;
        private Frame authFrame;

        private IDbCrud crudServ;
        private IAutorizationService authServ;
        private IPaymentService payServ;
        public MainWindowViewModel(MainWindow mainWindow, Frame mainFrame, Frame authFrame, IAutorizationService authServ)
        {
            this.mainWindow = mainWindow;
            this.mainFrame = mainFrame;
            this.authFrame = authFrame;
            var kernel = new StandardKernel(new NinjectRegistrations(), new ServiceModule("InsuranceContext"));
            crudServ = kernel.Get<IDbCrud>();
            payServ = kernel.Get<IPaymentService>();
            this.authServ = authServ;

            //mainFrame.Navigate(new View.AutoPage(mainFrame, crudServ, authServ));

            IsAgent = (authServ.GetCurrentUser().type == UserType.InsuranceAgent);
        }

        void CloseWindow()
        {
            mainWindow.Close();
        }
        void MaxMinWindow()
        {
            if (mainWindow.WindowState == WindowState.Maximized)
                mainWindow.WindowState = WindowState.Normal;
            else
                mainWindow.WindowState = WindowState.Maximized;
        }

        public void NavigateToAutoPage(int id = -1)
        {
            if (authServ.GetCurrentUser().type == UserType.Client)
                mainFrame.Navigate(new View.AutoPage(mainFrame, crudServ, authServ, authServ.GetCurrentUser().id));
            else
                mainFrame.Navigate(new View.AutoPage(mainFrame, crudServ, authServ, id));
        }
        void NavigateToInsurancePage()
        {
            if (authServ.GetCurrentUser().type == UserType.Client)
                mainFrame.Navigate(new View.InsurancesPage(mainFrame,authServ, crudServ, payServ, authServ.GetCurrentUser().id));
            else
                mainFrame.Navigate(new View.InsurancesPage(mainFrame, authServ, crudServ, payServ));
        }
        void NavigateToIncidentPage()
        {
            if (authServ.GetCurrentUser().type == UserType.Client)
                mainFrame.Navigate(new View.IncidentsPage(mainFrame, crudServ, authServ, payServ, authServ.GetCurrentUser().id));
            else
                mainFrame.Navigate(new View.IncidentsPage(mainFrame, crudServ, authServ,payServ));
        }
        void NavigateToClientsPage()
        {
            mainFrame.Navigate(new View.ClientsPage(this, crudServ));
        }
        public void UpdateData(UserInfo info)
        {
            IsAgent = (info.type == UserType.InsuranceAgent);
            if (IsAgent)
                NavigateToAutoPage();
            else
                NavigateToAutoPage(info.id);

        }

    }
}
