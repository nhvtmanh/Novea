//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Novea.Model
{
    using System;
    using System.Collections.Generic;
    
    public partial class HOADON
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public HOADON()
        {
            this.CTHDs = new HashSet<CTHD>();
            this.CTHDs1 = new HashSet<CTHD>();
        }
    
        public int SOHD { get; set; }
        public Nullable<System.DateTime> NGMH { get; set; }
        public Nullable<decimal> TONGTIEN { get; set; }
        public string MAKH { get; set; }
        public string MACH { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CTHD> CTHDs { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CTHD> CTHDs1 { get; set; }
        public virtual CUAHANG CUAHANG { get; set; }
        public virtual CUAHANG CUAHANG1 { get; set; }
        public virtual KHACH KHACH { get; set; }
        public virtual KHACH KHACH1 { get; set; }
    }
}
