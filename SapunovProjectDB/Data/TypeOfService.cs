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
    
    public partial class TypeOfService
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public TypeOfService()
        {
            this.TypeOfWork = new HashSet<TypeOfWork>();
        }
    
        public int IdTypeOfService { get; set; }
        public string NameTypeOfServise { get; set; }
        public int IdCategory { get; set; }
        public Nullable<decimal> PriceOfService { get; set; }
        public string Description { get; set; }
    
        public virtual CategoryOfService CategoryOfService { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TypeOfWork> TypeOfWork { get; set; }
    }
}
