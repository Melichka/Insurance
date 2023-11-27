﻿using BLL;
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
    /// Логика взаимодействия для InsurancesPage.xaml
    /// </summary>
    public partial class InsurancesPage : Page
    {
        private ViewModel.InsurancesViewModel VM;
        public InsurancesPage(Frame mainFrame, IAutorizationService ass, IDbCrud db, IPaymentService payServ, int clientId = -1)
        {
            InitializeComponent();
            VM = new ViewModel.InsurancesViewModel(mainFrame, ass, db, payServ, clientId);
            DataContext = VM;
        }
    }
}
