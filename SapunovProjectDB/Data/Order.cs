//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace SapunovProjectDB.Data
{
    using System;
    using System.Collections.Generic;
    
    public partial class Order
    {
        public int IdOrder { get; set; }
        public System.DateTime DateOfCreate { get; set; }
        public Nullable<int> IdClient { get; set; }
        public Nullable<int> IdTypeOfWork { get; set; }
        public int IdStatusOrder { get; set; }
    
        public virtual Client Client { get; set; }
        public virtual StatusOrder StatusOrder { get; set; }
        public virtual TypeOfWork TypeOfWork { get; set; }
    }
}
