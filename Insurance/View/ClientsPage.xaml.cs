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
    /// Логика взаимодействия для ClientsPage.xaml
    /// </summary>
    public partial class ClientsPage : Page
    {
        private ViewModel.ClientsViewModel VM;
        public ClientsPage(MainWindowViewModel main, IDbCrud db)
        {
            InitializeComponent();
            VM = new ViewModel.ClientsViewModel(main, db);
            DataContext = VM;
        }
    }
}
