using BLL;
using Insurance.Window;
using KonstLab2;
using MaterialDesignThemes.Wpf;
using Ninject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Insurance
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : System.Windows.Window
    {
        private ViewModel.MainWindowViewModel VM;
        public MainWindow()
        {
            InitializeComponent();

            var kernel = new StandardKernel(new NinjectRegistrations(), new ServiceModule("InsuranceContext"));
            IAutorizationService serv = kernel.Get<IAutorizationService>();
            
            VM = new ViewModel.MainWindowViewModel(this, mainFrame, authFrame, serv );
            DataContext = VM;

            AutoButton.IsChecked = true;
       
            authFrame.Navigate(new View.AuthorizationPage(authFrame, serv, VM));
        }

        private void DragRecranhle_MouseDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }

       
    }

    public class BoolToVisibility : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            var realV = (bool)value;

            if (realV)
                return Visibility.Visible;
            else
                return Visibility.Collapsed;
        }
        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            var realV = (Visibility)value;

            if (realV == Visibility.Collapsed)
                return false;
            else
                return true;
        }
    }

    public class NegateBoolToVisibility : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            var realV = (bool)value;

            if (!realV)
                return Visibility.Visible;
            else
                return Visibility.Collapsed;
        }
        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            var realV = (Visibility)value;

            if (realV == Visibility.Visible)
                return false;
            else 
                return true;
        }
    }

    public class StringToVisibilityParameter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            var realV = (string)value;
            var reqV = (string)parameter;
            if (realV == reqV)
                return true;
            else
                return false;
        }
        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return null;
        }
    }

}
