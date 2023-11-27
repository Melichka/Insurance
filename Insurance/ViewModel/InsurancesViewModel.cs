using BLL;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Insurance.ViewModel
{
    public struct InsuranceItem
    {
        public int Id { get; set; }
        public string Date { get; set; }
        public string FullAutoName { get; set; }
        public string FIO { get; set; }
        public string InsuranceType { get; set; }
    }
    public class InsurancesViewModel : INotifyPropertyChanged
    {
        #region NotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        private AutoMiniButton _currentAuto;
        public /*AutoModel*/ AutoMiniButton currentAuto
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

        private RelayCommand openInsuranceInfo;
        public RelayCommand OpenInsuranceInfo
        {
            get
            {
                return openInsuranceInfo ?? (openInsuranceInfo = new RelayCommand(obj =>
                {
                   InsuranceModel ins = crud.GetInsurance((int)obj);
                   mainFrame.Navigate(new View.InsuranceInfoPage(mainFrame, authServ, crud, payServ, ins.Id));
                }));
            }
        }

        #endregion

        public ObservableCollection<InsuranceItem> Insurances { get; set; }
        private Frame mainFrame;
        private IDbCrud crud;
        private IAutorizationService authServ;
        private IPaymentService payServ;
        private int clientId;
        public InsurancesViewModel(Frame mainFrame, IAutorizationService authServ, IDbCrud crud, IPaymentService payServ, int clientId = -1)
        {
            this.authServ = authServ;
            this.mainFrame = mainFrame;
            this.crud = crud;
            this.payServ = payServ;
            this.clientId = clientId;
            Insurances = new ObservableCollection<InsuranceItem>();
            if (clientId != -1)
                FillInsuranceList(crud.GetAllInsurance(), true);
            else
                FillInsuranceList(crud.GetAllInsurance());
        }

        private void FillInsuranceList(List<InsuranceModel> insurances, bool isClientsAutos = false)
        {
            if (isClientsAutos)
            {
                foreach (var i in insurances)
                    if (crud.GetAllAuto().Where(a => a.Id == i.AutoId).FirstOrDefault().ClientId == clientId)
                        Insurances.Add(new InsuranceItem
                        {
                            Id = i.Id,
                            Date = i.FinishDate.ToString(),
                            FullAutoName = i.AutoFullName,
                            InsuranceType = crud.GetAllType().Where(a => a.Id == i.TypeId).FirstOrDefault().Name,
                            FIO = i.FIO
                        });
            }
            else
                foreach (var i in insurances)
                Insurances.Add(new InsuranceItem
                {
                    Id = i.Id,
                    Date = i.FinishDate.ToString(),
                    FullAutoName = i.AutoFullName,
                    InsuranceType = crud.GetAllType().Where(a => a.Id == i.TypeId).FirstOrDefault().Name,
                    FIO = i.FIO
                });
        }
        
    }
}

