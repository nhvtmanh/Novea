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
    
    public partial class CUAHANG
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public CUAHANG()
        {
            this.HOADONs = new HashSet<HOADON>();
            this.SANPHAMs = new HashSet<SANPHAM>();
        }
    
        public string MACH { get; set; }
        public string TAIKHOAN { get; set; }
        public string MATKHAU { get; set; }
        public string TENCH { get; set; }
        public string DIADIEM { get; set; }
        public string SDT { get; set; }
        public string EMAIL { get; set; }
        public DateTime NGDK { get; set; }
        public decimal DOANHTHU { get; set; }
        public byte[] AVATAR { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<HOADON> HOADONs { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SANPHAM> SANPHAMs { get; set; }
    }
}
