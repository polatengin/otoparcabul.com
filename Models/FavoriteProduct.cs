//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace www.otoparcabul.com.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class FavoriteProduct
    {
        public int ID { get; set; }
        public int MemberID { get; set; }
        public int FavoriteProductID { get; set; }
    
        public virtual Member Member { get; set; }
        public virtual Product Product { get; set; }
    }
}
