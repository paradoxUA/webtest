//------------------------------------------------------------------------------
// <auto-generated>
//    Этот код был создан из шаблона.
//
//    Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//    Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Rentix.model
{
    using System;
    using System.Collections.Generic;
    
    public partial class Petroleum
    {
        public int Id { get; set; }
        public double litres { get; set; }
        public int program_users_id { get; set; }
        public System.DateTime Date { get; set; }
        public Nullable<double> Price { get; set; }
    
        public virtual program_users program_users { get; set; }
    }
}
