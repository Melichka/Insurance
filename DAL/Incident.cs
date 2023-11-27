//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DAL
{
    using System;
    using System.Collections.Generic;
    
    public partial class Incident
    {
        public int Id { get; set; }
        public System.DateTime Date { get; set; }
        public decimal Price { get; set; }
        public int StatusId { get; set; }
        public int InsuranceId { get; set; }
        public int InsuranceAgentId { get; set; }
    
        public virtual IncidentStatus IncidentStatus { get; set; }
        public virtual Insurance Insurance { get; set; }
        public virtual InsuranceAgent InsuranceAgent { get; set; }
    }
}
