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
    
    public partial class SANPHAM
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public SANPHAM()
        {
            this.CTHDs = new HashSet<CTHD>();
            this.CUAHANGs = new HashSet<CUAHANG>();
        }
    
        public string MASP { get; set; }
        public string TENSP { get; set; }
        public string THELOAI { get; set; }
        public string DONVI { get; set; }
        public Nullable<decimal> DONGIA { get; set; }
        public string KICHTHUOC { get; set; }
        public string MOTA { get; set; }
        public string HINHSP { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CTHD> CTHDs { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CUAHANG> CUAHANGs { get; set; }
    }
}
