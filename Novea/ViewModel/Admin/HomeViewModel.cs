﻿using Novea.Model;
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
using Novea.View.Admin;

namespace Novea.ViewModel.Admin
{
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
        private ObservableCollection<CTHD> _CT;
        public ObservableCollection<CTHD> CT { get => _CT; set { _CT = value; OnPropertyChanged(); } }
        private SeriesCollection _SeriesCollection { get; set; }
        public SeriesCollection SeriesCollection { get => _SeriesCollection; set { _SeriesCollection = value; OnPropertyChanged(); } }

        /*        private Func<ChartPoint, string> _PointLabel;
                public Func<ChartPoint, string> PointLabel { get => _PointLabel; set { _PointLabel = value; OnPropertyChanged(); } }
                private int _AT;
                public int AT { get => _AT; set { _AT = value; OnPropertyChanged(); } }
                private int _AK;
                public int AK { get => _AK; set { _AK = value; OnPropertyChanged(); } }
                private int _SM;
                public int SM { get => _SM; set { _SM = value; OnPropertyChanged(); } }*/
        /*        private int _SP;
                public int SP { get => _SP; set { _SP = value; OnPropertyChanged(); } }*/
        //public List<Result> Data { get; set; }
        public HomeViewModel()
        {
            LoadDoanhThu = new RelayCommand<HomeView>((p) => true, (p) => LoadDT(p));
            LoadDon = new RelayCommand<HomeView>((p) => true, (p) => SoDon(p));
            //CT = new ObservableCollection<CTHD>(DataProvider.Ins.DB.CTHDs);
            //LoadChart = new RelayCommand<HomeView>((p) => true, (p) => LineChart(p));
            LoadSP = new RelayCommand<HomeView>((p) => true, (p) => _LoadSP(p));
            //LineChart();
        }
        void _LoadSP(HomeView p)
        {
            int count = 0;
            if (DataProvider.Ins.DB.HOADONs.Count() > 0)
                count = (int)DataProvider.Ins.DB.CTHDs.Sum(x => x.SOLUONG);
            p.totalproducts.Text = count.ToString();
        }
        //public void LineChart(HomeView p)
        //{
        //    // PointLabel = ChartPoint => string.Format("{0}({1:P})", ChartPoint.Y, ChartPoint.Participation);
        //    //List<int> allValues = new List<int>();
        //    var query = from a in DataProvider.Ins.DB.CTHDs
        //                join b in DataProvider.Ins.DB.HOADONs on a.SOHD equals b.SOHD
        //                where a.SOHD == b.SOHD
                        
        //                select new HomeViewModel()
        //                {
        //                    SL = a.SL,
        //                    Ngay = b.NGHD,
        //                    SanPham = a.MASP
        //                };
        //    Data = new List<Result>();
        //    /*            foreach (var obj in query)
        //                {*/
        //    for (int h = 0; h < 24; h++)
        //    {
        //        int value = 0;
        //        if (query.Where(x => x.Ngay.Hour == h && x.Ngay.Day == DateTime.Now.Day && x.Ngay.Month == DateTime.Now.Month && x.Ngay.Year == DateTime.Now.Year).Select(x => x.SL).Count() > 0)
        //        {
        //            value = query.Where(x => x.Ngay.Hour == h && x.Ngay.Day == DateTime.Now.Day && x.Ngay.Month == DateTime.Now.Month && x.Ngay.Year == DateTime.Now.Year).Select(x => x.SL).Sum();
        //        }
        //        Result result = new Result(h, value);
        //        Data.Add(result);
        //    }
        //    p.Chart.ItemsSource = Data;

        //}
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
