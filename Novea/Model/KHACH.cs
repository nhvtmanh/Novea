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
    
    public partial class KHACH
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public KHACH()
        {
            this.HOADONs = new HashSet<HOADON>();
        }
    
        public string MAKH { get; set; }
        public string TAIKHOAN { get; set; }
        public string MATKHAU { get; set; }
        public string HOTEN { get; set; }
        public Nullable<System.DateTime> NGSINH { get; set; }
        public string GIOITINH { get; set; }
        public string SDT { get; set; }
        public string EMAIL { get; set; }
        public string DIACHI { get; set; }
        public DateTime NGDK { get; set; }
        public decimal DOANHSO { get; set; }
        public byte[] AVATAR { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<HOADON> HOADONs { get; set; }
    }
}
