//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace hentry.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class assignee
    {
        public int id { get; set; }
        public string user { get; set; }
        public int task { get; set; }
        public System.DateTime created { get; set; }
    
        public virtual aspnetusers aspnetusers { get; set; }
        public virtual task task1 { get; set; }
    }
}
