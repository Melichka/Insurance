using BLL;
using Insurance.Window;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Xml.Linq;

namespace Insurance.ViewModel
{
    public class AuthorizationViewModel : INotifyPropertyChanged
    {
        #region NotifyPropertyChanged

        private bool _isreg = false;
        public bool IsRegister
        {
            get { return _isreg; }
            set
            {
                _isreg = value;
                NotifyPropertyChanged("IsRegister");
            }
        }


        private void NotifyPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        public event PropertyChangedEventHandler PropertyChanged;
        #endregion

        #region Command

        private RelayCommand logInCommand;
        public RelayCommand LogInCommand
        {
            get
            {
                return logInCommand ?? (logInCommand = new RelayCommand(obj =>
                {
                    var values = (object[])obj;
                    string login = (string)values[0];
                    PasswordBox password = (PasswordBox)values[1];
                    Authorization(login, password.Password);
                }));
            }
        }

        private RelayCommand openRegisterCommand;
        public RelayCommand OpenRegisterCommand
        {
            get
            {
                return openRegisterCommand ?? (openRegisterCommand = new RelayCommand(obj =>
                {
                    IsRegister = true;
                    NotifyPropertyChanged("IsRegister");
                }));
            }
        }

        private RelayCommand closeRegisterCommand;
        public RelayCommand CloseRegisterCommand
        {
            get
            {
                return closeRegisterCommand ?? (closeRegisterCommand = new RelayCommand(obj =>
                {
                    IsRegister = false;
                    NotifyPropertyChanged("IsRegister");
                }));
            }
        }


        private RelayCommand registerCommand;
        public RelayCommand RegisterCommand
        {
            get
            {
                return registerCommand ?? (registerCommand = new RelayCommand(obj =>
                {
                    var values = (object[])obj;
                    string FIO = (string)values[0];
                    DateTime? DoB = (DateTime?)values[1];
                    string Passport = (string)values[2];
                    string Sertificate = (string)values[3];
                    string Phone = (string)values[4];
                    string Email = (string)values[5];
                    string Login = (string)values[6];
                    PasswordBox Password = (PasswordBox)values[7];
                    PasswordBox PasswordCheck = (PasswordBox)values[8];

                if (FIO == null || DoB == null || Passport == null || Sertificate == null || Phone == null || Email == null || Login == null || Password == null || PasswordCheck == null)
                {
                    var ccp = new CustomPopUp("Не все данные\nвведены");
                    ccp.ShowDialog();
                }
                else if (Password.Password != PasswordCheck.Password)
                    {
                        var ccp = new CustomPopUp("Пароли\nне совпадают");
                        ccp.ShowDialog();
                    }
                else
                    Register(FIO, (DateTime)DoB, Passport, Sertificate, Phone, Email, Login, Password);
                }));
            }
        }
        #endregion



        private Frame authFrame;
        private IAutorizationService authService;
        private MainWindowViewModel mainWindowViewModel;
        public AuthorizationViewModel(Frame authFrame, IAutorizationService service, MainWindowViewModel mainWindowViewModel = null)
        {
            this.authFrame = authFrame;
            this.authService = service;
            this.mainWindowViewModel = mainWindowViewModel;
            PlayFadeInAnimation();
        }

        public void Authorization(string login, string password)
        {
            if (authService.TryAuthorization(login, password))
            {
                PlayFadeAnimation();
                if (mainWindowViewModel != null)
                    mainWindowViewModel.UpdateData(authService.GetCurrentUser());
            }
            else
            {
                var ccp = new CustomPopUp("Неверный логин\nили пароль");
                ccp.ShowDialog();

            }

        }

        private void PlayFadeAnimation()
        {
            DoubleAnimation animation = new DoubleAnimation();
            animation.To = 0.0;
            animation.From = 1.0;
            animation.Duration = new TimeSpan(0, 0, 0, 0, 400);
            animation.EasingFunction = new SineEase();

            Storyboard sb = new Storyboard();
            sb.Children.Add(animation);

            authFrame.Opacity = 1.0;

            Storyboard.SetTarget(sb, authFrame);
            Storyboard.SetTargetProperty(sb, new PropertyPath(Control.OpacityProperty));


            sb.Completed += delegate (object sender, EventArgs e)
            {
                authFrame.Navigate(null);
            };

            sb.Begin();
        }

        private void PlayFadeInAnimation()
        {
            DoubleAnimation animation = new DoubleAnimation();
            animation.To = 1.0;
            animation.From = 0.0;
            animation.Duration = new TimeSpan(0, 0, 0, 0, 400);
            animation.EasingFunction = new SineEase();

            Storyboard sb = new Storyboard();
            sb.Children.Add(animation);

            authFrame.Opacity =01.0;

            Storyboard.SetTarget(sb, authFrame);
            Storyboard.SetTargetProperty(sb, new PropertyPath(Control.OpacityProperty));



            sb.Begin();
        }
        
        private void Register(string FIO, DateTime DoB, string Passport, string Sertificate, string Phone, string Email, string Login, PasswordBox Password)
        {
            authService.CreateClient(FIO, DoB, Passport, Sertificate, Phone, Email, Login, Password.Password);
            authService.TryAuthorization(Login, Password.Password);
            PlayFadeAnimation();
            mainWindowViewModel.UpdateData(authService.GetCurrentUser());
        }
    }
    
}
