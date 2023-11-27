using BLL;
using Insurance.Window;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Insurance.ViewModel
{
    public class AddCarViewModel
    {
        #region Command

        private RelayCommand addAutoCommand;
        public RelayCommand AddAutoCommand
        {
            get
            {
                return addAutoCommand ?? (addAutoCommand = new RelayCommand(obj =>
                {
                    var values = (object[])obj;
                    string Brand = (string)values[0];
                    string Model = (string)values[1];
                    string Number = (string)values[2];
                    decimal EnginePower = (decimal)values[3];
                    decimal Run = (decimal)values[4];
                    string NumberPTS = (string)values[5];

                    if (Brand == "" || Model == "" || Number == "" || EnginePower == 0 || NumberPTS == "")
                    {
                        var cp = new CustomPopUp("Введите все данные");
                        cp.ShowDialog();
                        return;
                    }

                    AutoModel model = new AutoModel()
                    {
                        Brand = Brand,
                        ClientId = autorizationService.GetCurrentUser().id,
                        EnginePower = (int)EnginePower,
                        Id = -1,
                        Model = Model,
                        NumberAuto = Number,
                        NumberPTS = NumberPTS,
                        Run = (int)Run
                    };
                    AddCar(model);
                    var cpp = new CustomPopUp("Готово!");
                    mainFrame.Navigate(new View.AutoPage(mainFrame, crud, autorizationService, autorizationService.GetCurrentUser().id));
                    cpp.ShowDialog();
                }));
            }
        }
        #endregion

        private Frame mainFrame;
        private IDbCrud crud;
        private IAutorizationService autorizationService;
        public AddCarViewModel(Frame mf, IDbCrud crud, IAutorizationService autorizationService)
        {
            mainFrame = mf;
            this.crud = crud;
            this.autorizationService = autorizationService;
        }

        private void AddCar(AutoModel model)
        {
            crud.CreateAuto(model);
        }
    }
}
