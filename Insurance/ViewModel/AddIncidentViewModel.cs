using BLL;
using Insurance.Window;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Insurance.ViewModel
{
    public class AddIncidentViewModel
    {
        #region Command

        private RelayCommand addIncidentCommand;
        public RelayCommand AddIncidentCommand
        {
            get
            {
                return addIncidentCommand ?? (addIncidentCommand = new RelayCommand(obj =>
                {
                    var values = (object[])obj;
                    if ((decimal)values[0] == 0 || values[1] == null)
                    {
                        var cp = new CustomPopUp("Введите все данные");
                        cp.ShowDialog();
                        return;
                    }
                    decimal cost = (decimal)values[0];
                    DateTime date = (DateTime)values[1];
                    if (date > DateTime.Now)
                    {
                        var cp = new CustomPopUp("Введите корректную\nдату");
                        cp.ShowDialog();
                        return;
                    }
                    AddIncident(cost, date);
                }));
            }
        }

        #endregion

        private Frame mainFrame;
        private IDbCrud crud;
        private AutoModel auto;
        public AddIncidentViewModel(Frame mainFrame, IDbCrud crud, AutoModel auto)
        {
            this.mainFrame = mainFrame;
            this.crud = crud;
            this.auto = auto;
        }

        private void AddIncident(decimal cost, DateTime date)
        {
            IncidentModel incidentModel = new IncidentModel
            {
                Id = -1,
                Date = date,
                Price = cost,
                Status = "На рассмотрении",
                InsuranceAgentId = 0,
                InsuranceId = crud.GetAllInsurance().Where(i => i.AutoId == auto.Id).OrderByDescending(i => i.FinishDate).FirstOrDefault().Id
            };

            crud.CreateIncident(incidentModel);
            mainFrame.NavigationService.GoBack();
        }
    }
}
