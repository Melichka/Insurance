using BLL;
using Insurance.Window;
using KonstLab2;
using MaterialDesignThemes.Wpf.Converters;
using Ninject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;

namespace Insurance.ViewModel
{
    public class PaymentViewModel
    {

        #region Command

        private RelayCommand sendPaymentCommand;
        public RelayCommand SendPaymentCommand
        {
            get
            {
                return sendPaymentCommand ?? (sendPaymentCommand = new RelayCommand(obj =>
                {
                    var values = (object[])obj;
                    string CardNumber = (string)values[0];
                    decimal CVC = (decimal)values[1];
                    decimal ExpDateMonth = (decimal)values[2];
                    decimal ExpDateYear = (decimal)values[3];
                    decimal PaymentPrice = (decimal)values[4];

                    if (CardNumber == "" || CVC == 0 || ExpDateMonth == 0 || ExpDateYear == 0 || PaymentPrice == 0)
                    {
                        var cp = new CustomPopUp("Введите все данные");
                        cp.ShowDialog();
                        return;
                    }

                    PaymentModel model = new PaymentModel
                    {
                        Id = -1,
                        Date = DateTime.Now,
                        InsuranceId = Insurance.Id,
                        Price = PaymentPrice
                    };
                    SendPayment(model);
                  
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
                    var values = (object[])obj;
                    string BankName = (string)values[0];
                    string CardNumber = (string)values[1];

                    if (CardNumber == "" || BankName == "")
                    {
                        var cp = new CustomPopUp("Введите все данные");
                        cp.ShowDialog();
                        return;
                    }
                    ReceivePayment(Incident);
                }));
            }
        }
        #endregion


        private Frame mainFrame;
        private IPaymentService paymentService;
        private IDbCrud crud;
        private IAutorizationService ass;
        private int clientId;
        public InsuranceModel Insurance { get; set; }
        public IncidentModel Incident { get; set; }
        public bool Receive { get; set; }
        public string FIO { get; set; }
        public PaymentViewModel(Frame mf, IDbCrud crud, IPaymentService paymentService, InsuranceModel ins)
        {
            clientId = -1;
            ass = null;
            mainFrame = mf;
            Incident = null;
            Insurance = ins;
            this.crud = crud;
            this.paymentService = paymentService;
            Receive = false;
            FIO = ins.FIO;
        }

        public PaymentViewModel(Frame mf, IDbCrud crud, IAutorizationService ass, IPaymentService paymentService, IncidentModel inc, int clientId)
        {
            this.clientId = clientId;
            this.ass = ass;
            mainFrame = mf;
            Incident = inc;
            Insurance = null;
            this.crud = crud;
            this.paymentService = paymentService;
            Receive = true;
            FIO = crud.GetInsurance((int)inc.InsuranceId).FIO;
        }

        private void SendPayment(PaymentModel model)
        {
            decimal finalPrice = paymentService.SendPayment(model);

            CustomPopUp cpp;
            if (finalPrice != model.Price)
                cpp = new CustomPopUp("Указанная сумма\n больше долга\nбыла произведена оплата\n на сумму\n" + finalPrice.ToString() + " Руб.");
            else
                cpp = new CustomPopUp("Была произведена\n оплата на сумму\n" + finalPrice.ToString() + " Руб.");
            mainFrame.Navigate(new View.InsuranceInfoPage(mainFrame, ass, crud, paymentService, model.InsuranceId));
            cpp.ShowDialog();
        }

        private void ReceivePayment(IncidentModel model)
        {
            paymentService.RecivePayment(model);
            CustomPopUp cpp = new CustomPopUp("Оплата на сумму\n " + model.Price + " Руб.\nПолучена" );
            mainFrame.Navigate(new View.IncidentsPage(mainFrame, crud, ass, paymentService, clientId));
            cpp.ShowDialog();
        }

    }
}
