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
    
    public partial class DiscountCardGroup
    {
        public DiscountCardGroup()
        {
            this.DiscountCards = new HashSet<DiscountCard>();
        }
    
        public int Id { get; set; }
        public short PercentOfDiscount { get; set; }
        public bool IsDeleted { get; set; }
    
        public virtual ICollection<DiscountCard> DiscountCards { get; set; }
    }
}
