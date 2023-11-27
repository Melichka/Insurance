using BLL;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Insurance.ViewModel
{
    public class ClientsViewModel
    {
        #region Command

        private RelayCommand openAutoPageCommand;
        public RelayCommand OpenAutoPageCommand
        {
            get
            {
                return openAutoPageCommand ?? (openAutoPageCommand = new RelayCommand(obj =>
                {
                    int clientId = (int)obj;
                    main.NavigateToAutoPage(clientId);
                }));
            }
        }
        #endregion
        public ObservableCollection<ClientModel> Clients { get; set; }
        private IDbCrud crud;
        private MainWindowViewModel main;
        public ClientsViewModel(MainWindowViewModel main, IDbCrud db)
        {
            Clients = new ObservableCollection<ClientModel>();
            crud = db;
            this.main = main;
            FillClientsList(crud.GetAllClient());
        }

        private void FillClientsList(List<ClientModel> client)
        {
            foreach(var c in client)
                Clients.Add(c);
        }
    }
}
