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
    
    public partial class race_data
    {
        public race_data()
        {
            this.race_times = new HashSet<race_times>();
        }
    
        public int id { get; set; }
        public Nullable<int> race_id { get; set; }
        public Nullable<int> pilot_id { get; set; }
        public Nullable<int> car_id { get; set; }
        public System.DateTime created { get; set; }
        public System.DateTime modified { get; set; }
        public Nullable<bool> reserv { get; set; }
        public Nullable<bool> monthrace { get; set; }
        public Nullable<System.DateTime> race_month_date { get; set; }
        public bool light_mode { get; set; }
        public Nullable<double> paid_amount { get; set; }
        public Nullable<int> id_discount_card { get; set; }
        public int id_race_mode { get; set; }
    
        public virtual races race { get; set; }
        public virtual users user { get; set; }
        public virtual karts kart { get; set; }
        public virtual ICollection<race_times> race_times { get; set; }
        public virtual DiscountCard DiscountCard { get; set; }
        public virtual race_modes race_modes { get; set; }
    }
}
