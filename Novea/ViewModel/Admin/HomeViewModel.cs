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

namespace Novea.ViewModel.Admin
{
    public class Result
    {
        private int _Hour;
        public int Hour { get => _Hour; set { _Hour = value; } }
        private int _SP;
        public int SP { get => _SP; set { _SP = value; } }
        public Result(int h = 0, int sp = 0)
        {
            Hour = h; SP = sp;
        }
    }
    class HomeViewModel : BaseViewModel
    {

        private string _DoanhThu;
        public string DoanhThu { get => _DoanhThu; set { _DoanhThu = value; OnPropertyChanged(); } }
        public string SanPham { get; set; }
        public int SL { get; set; }
        public DateTime Ngay { get; set; }
        public ICommand LoadDoanhThu { get; set; }
        public ICommand LoadDon { get; set; }
        public ICommand LoadChart { get; set; }
        public ICommand LoadSP { get; set; }

        private ObservableCollection<KHACH> _listKH;
        public ObservableCollection<KHACH> listKH { get => _listKH; set { _listKH = value; OnPropertyChanged(); } }
        private ObservableCollection<KHACH> _listKH1;
        public ObservableCollection<KHACH> listKH1 { get => _listKH1; set { _listKH1 = value; OnPropertyChanged(); } }
        private ObservableCollection<CTHD> _CTHD;
        public ObservableCollection<CTHD> CTHD { get => _CTHD; set { _CTHD = value; OnPropertyChanged(); } }
      
        public List<Result> Data { get; set; }
        public HomeViewModel()
        {
            listKH1 = new ObservableCollection<KHACH>(DataProvider.Ins.DB.KHACHes);
            listKH = new ObservableCollection<KHACH>(listKH1.GroupBy(p => p.HOTEN).Select(grp => grp.FirstOrDefault()));
            LoadDoanhThu = new RelayCommand<HomeView>((p) => true, (p) => LoadDT(p));
            LoadDon = new RelayCommand<HomeView>((p) => true, (p) => SoDon(p));
            CTHD = new ObservableCollection<CTHD>(DataProvider.Ins.DB.CTHDs);
            LoadChart = new RelayCommand<HomeView>((p) => true, (p) => LineChart(p));
            LoadSP = new RelayCommand<HomeView>((p) => true, (p) => _LoadSP(p));
        }
        void _LoadSP(HomeView p)
        {
            int count = 0;
            if (DataProvider.Ins.DB.HOADONs.Count() > 0)
                count = (int)DataProvider.Ins.DB.CTHDs.Sum(x => x.SOLUONG);
            p.totalproducts.Text = count.ToString();
        }
        public void LineChart(HomeView p)
        {
            var query = from a in DataProvider.Ins.DB.CTHDs  
                        join b in DataProvider.Ins.DB.HOADONs on a.SOHD equals b.SOHD
                        where a.SOHD == b.SOHD

                        select new HomeViewModel()
                        {
                            Ngay = (System.DateTime)b.NGMH,
                            SL = (int)a.SOLUONG,
                            SanPham = a.MASP
                        };
            Data = new List<Result>();           
            for (int h = 0; h < 24; h++)
            {
                int value = 0;
                if (query.Where(x => x.Ngay.Hour == h && x.Ngay.Day == DateTime.Now.Day && x.Ngay.Month == DateTime.Now.Month && x.Ngay.Year == DateTime.Now.Year).Select(x => x.SL).Count() > 0)
                {
                    value = query.Where(x => x.Ngay.Hour == h && x.Ngay.Day == DateTime.Now.Day && x.Ngay.Month == DateTime.Now.Month && x.Ngay.Year == DateTime.Now.Year).Select(x => x.SL).Sum();
                }
                Result result = new Result(h, value);
                Data.Add(result);
            }
            p.Chart.ItemsSource = Data;

        }
        public void SoDon(HomeView p)
        {
            int count = DataProvider.Ins.DB.HOADONs.Count();
            p.totalorders.Text = count.ToString();
        }

        public void LoadDT(HomeView p)
        {
            long total = 0;
            if (DataProvider.Ins.DB.HOADONs.Select(x => x.TONGTIEN).Count() != 0)
            {
                total = (long)DataProvider.Ins.DB.HOADONs.Select(x => x.TONGTIEN).Sum();
                DoanhThu = total.ToString("#,###") + " VNĐ";
            }
            else DoanhThu = "0 VNĐ";
            p.totalrevenue.Text = DoanhThu;
        }

    }
}
