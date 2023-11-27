
using System.Collections.Generic;


namespace BLL
{
    public interface IDbCrud

    {
        List<AutoModel> GetAllAuto();
        List<InsuranceTypeModel> GetAllType();
        List<IncidentStatusModel> GetAllStatus();
        List<ClientModel> GetAllClient();
        List<InsuranceModel> GetAllInsurance();
        List<PaymentModel> GetAllPayment();
        List<IncidentModel> GetAllIncident();
        InsuranceModel GetInsurance(int InsId);
        ClientModel GetClient(int Id);
        void CreateInsurance(InsuranceModel i);
        void CreateClient(ClientModel i);
        void UpdateInsurance(InsuranceModel p);
        void UpdateClient(ClientModel p);
        void UpdateIncident(IncidentModel i);
        void DeleteInsurance(int id);
        void DeleteClient(int id);
        void CreateAuto(AutoModel a);
        void CreateIncident(IncidentModel i);
    }
}
