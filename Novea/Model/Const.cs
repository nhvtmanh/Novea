using Novea.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Novea.Model
{
    public class Const : BaseViewModel
    {
        public static string TenDangNhap { get; set; }
        public static string MACH { get; set; }
        public static CUAHANG CH { get; set; }
        public static KHACH KH { get; set; }
        public static string _localLink = System.Reflection.Assembly.GetExecutingAssembly().Location.Remove(System.Reflection.Assembly.GetExecutingAssembly().Location.IndexOf(@"bin\Debug"));
    }

}