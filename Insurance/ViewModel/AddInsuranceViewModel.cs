using BLL;
using Insurance.Window;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Insurance.ViewModel
{
    public class AddInsuranceViewModel : INotifyPropertyChanged
    {
        #region NotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        private DateTime _startDate;
        public DateTime startDate
        {

            get { return _startDate; }
            set
            {
                _startDate = value;
                NotifyPropertyChanged("startDate");
            }
        }

        private DateTime _finishDate;
        public  DateTime finishDate
        {

            get { return _finishDate; }
            set
            {
                _finishDate = value;
                NotifyPropertyChanged("finishDate");
            }
        }

        private decimal _price;
        public decimal price
        {

            get { return _price; }
            set
            {
                _price = value;
                NotifyPropertyChanged("price");
            }
        }

        private decimal _driverExperience;
        public decimal driverExperience
        {

            get { return _driverExperience; }
            set
            {
                _driverExperience = value;
                NotifyPropertyChanged("driverExperience");
                price = InsurancePrice.GetInsurancePrice((int)value, 30, auto.EnginePower);
            }
        }


        #endregion
        #region Command

        private RelayCommand addInsuranceCommand;
        public RelayCommand AddInsuranceCommand
        {
            get
            {
                return addInsuranceCommand ?? (addInsuranceCommand = new RelayCommand(obj =>
                {
                    var values = (object[])obj;

                    if (values[0] == null)
                        return;
                    int typeId = (int)values[0];
                    string passport = (string)values[1];
                    string policy = (string)values[2];
                    string sertification = (string)values[3];
                    decimal experience = (decimal)values[4];
                    if (policy == "" || passport == "" || sertification == "" || experience == 0)
                    {
                        var cp = new CustomPopUp("Введите все данные");
                        cp.ShowDialog();
                        return;
                    }
                    AddInsurance(typeId, policy, passport, sertification, (int)experience);
                }));
            }
        }

        #endregion

        private Frame mainFrame;
        private IDbCrud crud;
        private AutoModel auto;
        public List<InsuranceTypeModel> InsuranceTypes { get; set; }
        public AddInsuranceViewModel(Frame mainFrame, IDbCrud crud, AutoModel auto)
        {
            this.mainFrame = mainFrame;
            this.crud = crud;
            this.auto = auto;

            startDate = DateTime.Now;
            finishDate = DateTime.Now.AddYears(1); 

            InsuranceTypes = crud.GetAllType();
        }

        private void AddInsurance(int typeId, string policy, string passport, string sertification, int experience)
        {
            InsuranceModel ins = new InsuranceModel
            {
                Id = -1,
                AutoId = auto.Id,
                DrivingExperience = experience,
                FinishDate = finishDate,
                StartDate = startDate,
                OwnerPassport = passport,
                OwnerSertificate = sertification,
                Policy = policy,
                TypeId = typeId,
                Price = price
            };
            crud.CreateInsurance(ins);
            mainFrame.NavigationService.GoBack();
        }
    }
}
