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
        }
    
        public string MASP { get; set; }
        public string TENSP { get; set; }
        public string LOAISP { get; set; }
        public string DONVI { get; set; }
        public decimal DONGIA { get; set; }
        public string SIZE { get; set; }
        public string MOTA { get; set; }
        public Nullable<bool> AVAILABLE { get; set; }
        public byte[] HINHSP { get; set; }
        public string MACH { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CTHD> CTHDs { get; set; }
        public virtual CUAHANG CUAHANG { get; set; }
    }
}
