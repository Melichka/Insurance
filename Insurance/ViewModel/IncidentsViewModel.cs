using BLL;
using MaterialDesignThemes.Wpf.Converters;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Insurance.ViewModel
{
    public struct IncidentItem
    {
        public int Id { get; set; }
        public string Date { get; set; }
        public string FullAutoName { get; set; }
        public string FIO { get; set; }
        public string Price { get; set; }
        public string InsuranceType { get; set; }
        public string Status { get; set; }
        public bool IsActive { get; set; }
        public bool IsEnded { get; set; }
        public bool IsAgent { get; set; }
    }
    public class IncidentsViewModel : INotifyPropertyChanged
    {
        #region NotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        private AutoMiniButton _currentAuto;
        public AutoMiniButton currentAuto
        {

            get { return _currentAuto; }
            set
            {
                _currentAuto = value;
                NotifyPropertyChanged("currentAuto");
            }
        }

        #endregion

        #region Command

        private RelayCommand changeStatusCommand;
        public RelayCommand ChangeStatusCommand
        {
            get
            {
                return changeStatusCommand ?? (changeStatusCommand = new RelayCommand(obj =>
                {
                    var values = (object[])obj;
                    int incidentId = Int32.Parse((string)values[0]);
                    int statusId = -1;
                    string statusName = (string)values[1];
                    switch(statusName)
                    {
                        case "На рассмотрении":
                            statusId = 0;
                            break;
                        case "Одобрить":
                            statusId = 1;
                            break;
                        case "Отклонить":
                            statusId = 3;
                            break;
                        default:
                            statusId = -1;
                            break;
                    }
                    ChangeIncidentStatus(incidentId, statusId);
                }));
            }
        }

        private RelayCommand receivePaymentCommand;
        public RelayCommand ReceivePaymentCommand
        {
            get
            {
                return receivePaymentCommand ?? (receivePaymentCommand = new RelayCommand(obj =>
                {
                    IncidentModel inc = crud.GetAllIncident().Where(i => i.Id == (int)obj).FirstOrDefault();
                    mainFrame.Navigate(new View.PaymentPage(mainFrame, crud,autorizationService, payServ, inc,clientId));   
                }));
            }
        }

        #endregion 

        public ObservableCollection<IncidentItem> Incidents { get; set; }
        private Frame mainFrame;
        private IDbCrud crud;
        private IAutorizationService autorizationService;
        private IPaymentService payServ;
        private int clientId;
        public IncidentsViewModel(Frame mainFrame, IDbCrud crud, IAutorizationService autorizationService, IPaymentService payServ, int clientId = -1)
        {
            this.payServ = payServ;
            this.mainFrame = mainFrame;
            this.crud = crud;
            this.clientId = clientId;
            this.autorizationService = autorizationService;

            Incidents = new ObservableCollection<IncidentItem>();
            if (clientId != -1)
                FillIncidentList(crud.GetAllIncident(), true);
            else
                FillIncidentList(crud.GetAllIncident());
        }

        private void FillIncidentList(List<IncidentModel> incidents, bool isClientsAutos = false)
        {
            if (isClientsAutos)
            {
                foreach (var i in incidents)
                    if(crud.GetAllAuto().Where(a => a.Id == crud.GetInsurance((int)i.InsuranceId).AutoId).FirstOrDefault().ClientId == clientId)
                        Incidents.Add(new IncidentItem
                        {
                            Id = i.Id,
                            Date = i.Date.ToString(),
                            IsActive = (i.StatusId == 1),
                            IsAgent = autorizationService.GetCurrentUser().type == UserType.InsuranceAgent,
                            Status = i.Status,
                            IsEnded = i.IsEnded,
                            FIO = crud.GetClient(crud.GetAllAuto().Where(a => a.Id == crud.GetInsurance((int)i.InsuranceId).AutoId).FirstOrDefault().ClientId).FIO,
                            FullAutoName = crud.GetAllInsurance().Where(a => a.Id == i.InsuranceId).FirstOrDefault().AutoFullName,
                            Price = String.Format("{0:0.00}", i.Price) + "Руб.",
                            InsuranceType = crud.GetAllInsurance().Where(a => a.Id == i.InsuranceId).FirstOrDefault().Type
                        });
            }
            else
                foreach (var i in incidents)
                Incidents.Add(new IncidentItem
                {
                    Id = i.Id,
                    Date = i.Date.ToString(),
                    IsActive = (i.StatusId == 1),
                    IsAgent = autorizationService.GetCurrentUser().type == UserType.InsuranceAgent,
                    Status = i.Status,
                    IsEnded = i.IsEnded,
                    FIO = crud.GetClient(crud.GetAllAuto().Where(a => a.Id == crud.GetInsurance((int)i.InsuranceId).AutoId).FirstOrDefault().ClientId).FIO,
                    FullAutoName = crud.GetAllInsurance().Where(a => a.Id == i.InsuranceId).FirstOrDefault().AutoFullName,
                    Price = String.Format("{0:0.00}",i.Price) + "Руб.",
                    InsuranceType = crud.GetAllInsurance().Where(a => a.Id == i.InsuranceId).FirstOrDefault().Type
                });
        }

        private void ChangeIncidentStatus(int incidentId, int statusId)
        {
            var inc = crud.GetAllIncident().Where(a => a.Id == incidentId).FirstOrDefault();
            inc.StatusId = statusId;
            crud.UpdateIncident(inc);
        }
    }
}
