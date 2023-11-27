using BLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Management.Instrumentation;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Insurance.ViewModel
{
    public class InsuranceInfoViewModel
    {
        private RelayCommand backToInsurances;
        public RelayCommand BackToInsurances
        {
            get
            {
                return backToInsurances ?? (backToInsurances = new RelayCommand(obj =>
                {
                    mainFrame.NavigationService.GoBack();
                }));
            }
        }

        private RelayCommand toPayment;
        public RelayCommand ToPaymentCommand
        {
            get
            {
                return toPayment ?? (toPayment = new RelayCommand(obj =>
                {
                    mainFrame.Navigate(new View.PaymentPage(mainFrame, crud, payServ, Insurance));
                }));
            }
        }

        private Frame mainFrame;
        private IPaymentService payServ;
        private IDbCrud crud;
        public InsuranceModel Insurance { get; set; }
        public bool isActive { get; set; }
        public bool IsAgent { get; set; }
        public string activeText { get; set; }
        public InsuranceInfoViewModel(Frame mainFrame, IAutorizationService authServ, IDbCrud crud, IPaymentService payServ, int insId)
        {
            if (authServ == null)
                IsAgent = false;
            else if (authServ.GetCurrentUser().type == UserType.InsuranceAgent)
                IsAgent = true;
            else
                IsAgent = false;

            this.mainFrame = mainFrame;
            this.payServ = payServ;
            this.crud = crud;
            Insurance = crud.GetInsurance(insId);
            isActive = (crud.GetInsurance(insId).LeftPrice > 0);
            if (isActive)
                activeText = "Оплатить";
            else
                activeText = "Оплачено";
        }

    }
}
