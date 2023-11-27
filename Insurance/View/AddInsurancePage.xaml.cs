using BLL;
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
    /// Логика взаимодействия для AddInsurancePage.xaml
    /// </summary>
    public partial class AddInsurancePage : Page
    {
        private ViewModel.AddInsuranceViewModel VM;
        public AddInsurancePage(Frame mf, IDbCrud db, AutoModel auto)
        {
            InitializeComponent();
            VM = new ViewModel.AddInsuranceViewModel(mf, db, auto);
            DataContext = VM;
        }

        private void Type_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
