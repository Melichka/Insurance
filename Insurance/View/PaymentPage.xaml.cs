using BLL;
using Insurance.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Insurance.View
{
    /// <summary>
    /// Логика взаимодействия для PaymentPage.xaml
    /// </summary>
    public partial class PaymentPage : Page
    {
        private PaymentViewModel VM;
        public PaymentPage(Frame mf, IDbCrud crud, IPaymentService payServ, InsuranceModel ins)
        {
            InitializeComponent();
            VM = new PaymentViewModel(mf, crud, payServ, ins);
            DataContext = VM;
        }

        public PaymentPage(Frame mf, IDbCrud crud, IAutorizationService ass, IPaymentService payServ, IncidentModel inc, int clientId)
        {
            InitializeComponent();
            VM = new PaymentViewModel(mf, crud,ass, payServ, inc,clientId);
            DataContext = VM;
        }
    }
}
