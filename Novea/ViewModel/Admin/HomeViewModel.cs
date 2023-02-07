using Novea.Model;
using Novea.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using LiveCharts;
using LiveCharts.Wpf;
using System.Data;
using System.Data.SqlClient;
using System.Collections.ObjectModel;
using LiveCharts.Defaults;
using Syncfusion.UI.Xaml.Charts;
using Novea.View.Admin;
using System.Windows.Documents;
using System.ComponentModel;
using System.IO;
using System.Windows.Media.Imaging;

namespace Novea.ViewModel.Admin
{
    public class KetQuaHienThiList
    {
        private int _Hour;
        public int Hour { get => _Hour; set { _Hour = value; } }
        private int _SP;
        public int SP { get => _SP; set { _SP = value; } }
        public KetQuaHienThiList(int h = 0, int sp = 0)
        {
            Hour = h; SP = sp;
        }
    }
    class HomeViewModel : BaseViewModel
    {
        public ICommand Loadwd { get; set; }

        private string _DoanhThu;
        public string DoanhThu { get => _DoanhThu; set { _DoanhThu = value; OnPropertyChanged(); } }
        public string SanPham { get; set; }
        public int SL { get; set; }
      

        private ObservableCollection<KHACH> _listKH;
        public ObservableCollection<KHACH> listKH { get => _listKH; set { _listKH = value; OnPropertyChanged(); } }

        private ObservableCollection<SANPHAM> _listSP;
        public ObservableCollection<SANPHAM> listSP { get => _listSP; set { _listSP = value; OnPropertyChanged(); } }

        private ObservableCollection<HOADON> _listHD;
        public ObservableCollection<HOADON> listHD { get => _listHD; set { _listHD = value; OnPropertyChanged(); } }

        private ObservableCollection<CTHD> _CTHD;
        public ObservableCollection<CTHD> CTHD { get => _CTHD; set { _CTHD = value; OnPropertyChanged(); } }
      
        public List<KetQuaHienThiList> Data { get; set; }
        public DateTime Ngay { get; set; }
        public ICommand LoadDoanhThu { get; set; }
        public ICommand LoadDon { get; set; }
        public ICommand LoadChart { get; set; }
        public ICommand LoadSP { get; set; }
        public HomeViewModel()
        {
            Loadwd = new RelayCommand<HomeView>((p) => true, (p) => _Loadwd(p));

            

            LoadDoanhThu = new RelayCommand<HomeView>((p) => true, (p) => LoadDT(p));
            LoadDon = new RelayCommand<HomeView>((p) => true, (p) => SoDon(p));
            CTHD = new ObservableCollection<CTHD>(DataProvider.Ins.DB.CTHDs);
            LoadChart = new RelayCommand<HomeView>((p) => true, (p) => LineChart(p));
            LoadSP = new RelayCommand<HomeView>((p) => true, (p) => _LoadSP(p));
        }

        void _Loadwd(HomeView p)
        {
            listKH = new ObservableCollection<KHACH>(DataProvider.Ins.DB.KHACHes.Where(kh => kh.HOADONs.Any(hd => hd.MACH == Const.MACH && hd.DONE == true && hd.FINISHORDERCLIENT == true)));
            listSP = new ObservableCollection<SANPHAM>(DataProvider.Ins.DB.SANPHAMs.Where(sp => sp.MACH == Const.CH.MACH));
            listHD = new ObservableCollection<HOADON>(DataProvider.Ins.DB.HOADONs.Where(hd => hd.MACH == Const.CH.MACH && hd.FINISHORDERCLIENT == true && hd.DONE == false));
        }
        public void LineChart(HomeView p)
        {
            var query = from a in DataProvider.Ins.DB.CTHDs
                        join b in DataProvider.Ins.DB.HOADONs
                        on a.SOHD equals b.SOHD
                        where b.MACH == Const.MACH
            select new HomeViewModel()                       
            {                        
                Ngay = (System.DateTime)b.NGMH,                        
                SL = (int)a.SOLUONG,                        
                SanPham = a.MASP                       
            };
            Data = new List<KetQuaHienThiList>();           
            for (int h = 0; h < 24; h++)
            {
                int value = 0;
                if (query.Where(x => x.Ngay.Hour == h && x.Ngay.Day == DateTime.Now.Day && x.Ngay.Month == DateTime.Now.Month && x.Ngay.Year == DateTime.Now.Year).Select(x => x.SL).Count() > 0)
                {
                    value = query.Where(x => x.Ngay.Hour == h && x.Ngay.Day == DateTime.Now.Day && x.Ngay.Month == DateTime.Now.Month && x.Ngay.Year == DateTime.Now.Year).Select(x => x.SL).Sum();
                }
                KetQuaHienThiList KetQuaHienThiList = new KetQuaHienThiList(h, value);
                Data.Add(KetQuaHienThiList);
            }
            p.Chart.ItemsSource = Data;
        }

        void _LoadSP(HomeView p)
        {
            int count = (int)listSP.Count();
            p.totalproducts.Text = count.ToString();
        }

        public void SoDon(HomeView p)
        {
            int count = (int)listHD.Count();
            p.totalorders.Text = count.ToString();
        }

        public void LoadDT(HomeView p)
        {
            long total = 0;
            if (listHD.Select(x => x.TONGTIEN).Count() != 0)
            {
                total = (long)listHD.Select(x => x.TONGTIEN).Sum();
                DoanhThu = total.ToString("#,###") + " VNĐ";
            }
            else DoanhThu = "0 VNĐ";
            p.totalrevenue.Text = DoanhThu;
        }

    }
}
