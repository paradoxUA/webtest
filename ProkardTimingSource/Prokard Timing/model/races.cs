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
    
    public partial class races
    {
        public races()
        {
            this.jurnals = new HashSet<jurnal>();
            this.noracekarts = new HashSet<noracekart>();
            this.race_data = new HashSet<race_data>();
        }
    
        public int id { get; set; }
        public Nullable<System.DateTime> racedate { get; set; }
        public string raceid { get; set; }
        public System.DateTime created { get; set; }
        public System.DateTime modified { get; set; }
        public Nullable<int> stat { get; set; }
        public int track_id { get; set; }
        public bool light_mode { get; set; }
        //public Nullable<bool> is_race { get; set; }
    
        public virtual tracks track { get; set; }
        public virtual ICollection<jurnal> jurnals { get; set; }
        public virtual ICollection<noracekart> noracekarts { get; set; }
        public virtual ICollection<race_data> race_data { get; set; }
    }
}
