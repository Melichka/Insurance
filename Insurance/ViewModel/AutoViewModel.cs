using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data.Entity.Core.Metadata.Edm;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using BLL;
using Insurance.Window;

namespace Insurance.ViewModel
{
    public struct AutoMiniButton
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Number { get; set; }
    }



    public class AutoViewModel : INotifyPropertyChanged
    {
        #region NotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        private AutoModel _currentAuto;
        public AutoModel currentAuto 
        {

            get { return _currentAuto; }
            set
            {
                _currentAuto = value;
                NotifyPropertyChanged("currentAuto");
            }
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

        #region Command

        private RelayCommand openAutoInfo;
        public RelayCommand OpenAutoInfo
        {
            get
            {
                return openAutoInfo ?? (openAutoInfo = new RelayCommand(obj =>
                {
                    currentAuto = crud.GetAllAuto().Where(i => i.Id == (int)obj).FirstOrDefault();
                }));
            }
        }

        private RelayCommand addCarCommand;
        public RelayCommand AddCarCommand
        {
            get
            {
                return addCarCommand ?? (addCarCommand = new RelayCommand(obj =>
                {
                    mainFrame.Navigate(new View.AddCarPage(mainFrame, crud, autorizationService));
                }));
            }
        }

        private RelayCommand insuranceCommand;
        public RelayCommand InsuranceCommand
        {
            get
            {
                return insuranceCommand ?? (insuranceCommand = new RelayCommand(obj =>
                {
                    if (currentAuto != null)
                    {
                        if (crud.GetAllInsurance().Where(i => i.AutoId == currentAuto.Id).FirstOrDefault() == null)
                            mainFrame.Navigate(new View.AddInsurancePage(mainFrame, crud, currentAuto));
                        else if (crud.GetAllInsurance().Where(i => i.AutoId == currentAuto.Id).OrderByDescending(i => i.FinishDate).FirstOrDefault().FinishDate < DateTime.Now)
                            mainFrame.Navigate(new View.AddInsurancePage(mainFrame, crud, currentAuto));
                        else
                        {
                            var cpp = new CustomPopUp("Страховка уже есть");
                            cpp.ShowDialog();
                        }
                    }
                    else
                    {
                        var cpp = new CustomPopUp("Авто не выбрано");
                        cpp.ShowDialog();
                    }

                }));
            }
        }

        private RelayCommand incidentCommand;
        public RelayCommand IncidentCommand
        {
            get
            {
                return incidentCommand ?? (incidentCommand = new RelayCommand(obj =>
                {
                    if (currentAuto != null)
                    {
                        if (crud.GetAllInsurance().Where(i => i.AutoId == currentAuto.Id).FirstOrDefault() != null && crud.GetAllInsurance().Where(i => i.AutoId == currentAuto.Id).OrderByDescending(i => i.FinishDate).FirstOrDefault().FinishDate > DateTime.Now)
                        {
                                mainFrame.Navigate(new View.AddIncidentPage(mainFrame, crud, currentAuto));
                        }
                        else
                        {
                            var cpp = new CustomPopUp("Страховка отсутствует\nили просрочена");
                            cpp.ShowDialog();
                        }
                    }
                    else
                    {
                        var cpp = new CustomPopUp("Авто не выбрано");
                        cpp.ShowDialog();
                    }
                }));
            }
        }

        #endregion

        private Frame mainFrame;
        private IDbCrud crud;
        private IAutorizationService autorizationService;
        private int clientId;
        public ObservableCollection<AutoMiniButton> Autos { get; set; }
        
        public AutoViewModel(Frame mainFrame, IDbCrud crud, IAutorizationService authServ, int clientId = -1)
        {
            currentAuto = null;
            this.crud = crud;
            this.mainFrame = mainFrame;
            this.autorizationService = authServ;
            this.clientId = clientId;

            IsAgent = !(authServ.GetCurrentUser().type == UserType.InsuranceAgent);

            Autos = new ObservableCollection<AutoMiniButton>();
            if (clientId != -1)
                FillAutos(crud.GetAllAuto(), true);
            else
                FillAutos(crud.GetAllAuto());

        }

        private void FillAutos(List<AutoModel> AutoList, bool isClientsAutos = false)
        {
            if (isClientsAutos)
            {
                foreach (var a in AutoList)
                    if (a.ClientId == clientId)
                        Autos.Add(new AutoMiniButton { Id = a.Id, Name = a.Brand + " " + a.Model, Number = a.NumberAuto });
            }
            else
                foreach (var aa in AutoList)
                    Autos.Add(new AutoMiniButton { Id = aa.Id, Name = aa.Brand + " " + aa.Model, Number = aa.NumberAuto });
        }
    }
}
