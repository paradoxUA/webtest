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
    
    public partial class karts
    {
        public karts()
        {
            this.fuels = new HashSet<fuel>();
            this.race_data = new HashSet<race_data>();
            this.messages = new HashSet<messages>();
        }
    
        public int id { get; set; }
        public string name { get; set; }
        public string number { get; set; }
        public string transponder { get; set; }
        public string color { get; set; }
        public System.DateTime created { get; set; }
        public Nullable<System.DateTime> modified { get; set; }
        public bool repair { get; set; }
        public Nullable<int> message_id { get; set; }
        public bool wait { get; set; }
    
        public virtual ICollection<fuel> fuels { get; set; }
        public virtual ICollection<race_data> race_data { get; set; }
        public virtual ICollection<messages> messages { get; set; }
    }
}
