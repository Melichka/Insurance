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
using BLL;

namespace Insurance.View
{
    /// <summary>
    /// Логика взаимодействия для InsuranceInfoPage.xaml
    /// </summary>
    public partial class InsuranceInfoPage : Page
    {
        private ViewModel.InsuranceInfoViewModel VM;
        public InsuranceInfoPage(Frame mainFrame, IAutorizationService ass, IDbCrud crud, IPaymentService payServ, int insId)
        {
            InitializeComponent();
            VM = new ViewModel.InsuranceInfoViewModel(mainFrame, ass, crud, payServ, insId);
            DataContext = VM;
        }
    }
}
