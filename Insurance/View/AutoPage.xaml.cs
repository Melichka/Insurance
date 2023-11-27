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
    /// Логика взаимодействия для AutoPage.xaml
    /// </summary>
    public partial class AutoPage : Page
    {
        ViewModel.AutoViewModel VM;
        public AutoPage(Frame mainFrame, IDbCrud db, IAutorizationService ass, int cId = -1)
        {
            InitializeComponent();
            VM = new ViewModel.AutoViewModel(mainFrame, db, ass, cId);
            DataContext = VM;
        }

    }
}
