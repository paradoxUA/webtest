//------------------------------------------------------------------------------
// <auto-generated>
//    Этот код был создан из шаблона.
//
//    Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//    Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Prokard_Timing.model
{
    using System;
    using System.Collections.Generic;
    
    public partial class user_cash
    {
        public int id { get; set; }
        public Nullable<int> user_id { get; set; }
        public Nullable<double> sum { get; set; }
        public bool sign { get; set; }
        public System.DateTime created { get; set; }
        public int doc_id { get; set; }
    
        public virtual users user { get; set; }
        public virtual jurnal jurnal { get; set; }
    }
}
