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
    
    public partial class prices
    {
        public int id { get; set; }
        public int week { get; set; }
        public string d1 { get; set; }
        public string d2 { get; set; }
        public string d3 { get; set; }
        public string d4 { get; set; }
        public string d5 { get; set; }
        public string d6 { get; set; }
        public string d7 { get; set; }
        public int idRaceMode { get; set; }
    
        public virtual race_modes race_modes { get; set; }
    }
}
