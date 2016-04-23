//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Prokard_Timing.model
{
    using System;
    using System.Collections.Generic;
    
    public partial class users
    {
        public users()
        {
            this.certificates = new HashSet<certificate>();
            this.jurnals = new HashSet<jurnal>();
            this.race_data = new HashSet<race_data>();
            this.user_cash = new HashSet<user_cash>();
            this.messages = new HashSet<messages>();
            this.DiscountCards = new HashSet<DiscountCard>();
        }
    
        public int id { get; set; }
        public string name { get; set; }
        public string surname { get; set; }
        public bool gender { get; set; }
        public System.DateTime birthday { get; set; }
        public Nullable<System.DateTime> created { get; set; }
        public Nullable<System.DateTime> modified { get; set; }
        public string nickname { get; set; }
        public bool banned { get; set; }
        public Nullable<System.DateTime> date_banned { get; set; }
        public string email { get; set; }
        public string tel { get; set; }
        public Nullable<int> message_id { get; set; }
        public string barcode { get; set; }
        public Nullable<int> gr { get; set; }
        public bool deleted { get; set; }
        public Nullable<System.DateTime> date_deleted { get; set; }
    
        public virtual ICollection<certificate> certificates { get; set; }
        public virtual groups group { get; set; }
        public virtual ICollection<jurnal> jurnals { get; set; }
        public virtual ICollection<race_data> race_data { get; set; }
        public virtual ICollection<user_cash> user_cash { get; set; }
        public virtual ICollection<messages> messages { get; set; }
        public virtual ICollection<DiscountCard> DiscountCards { get; set; }
    }
}
