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
    
    public partial class projectmanager
    {
        public int id { get; set; }
        public int user { get; set; }
        public int project { get; set; }
        public System.DateTime created { get; set; }
    
        public virtual project project1 { get; set; }
        public virtual user user1 { get; set; }
    }
}