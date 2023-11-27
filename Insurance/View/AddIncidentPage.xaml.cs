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
    /// Логика взаимодействия для AddIncidentPage.xaml
    /// </summary>
    public partial class AddIncidentPage : Page
    {
        private ViewModel.AddIncidentViewModel VM;
        public AddIncidentPage(Frame mf,IDbCrud db, AutoModel a)
        {
            InitializeComponent();
            VM = new ViewModel.AddIncidentViewModel(mf,db, a);
            DataContext = VM;
        }
    }
}
