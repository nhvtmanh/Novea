using Novea.Model;
using Novea.View;
using Novea.ViewModel.Admin;
using Novea.View.Admin;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;

namespace Novea.ViewModel.Admin
{
    public class OrdersViewModel : BaseViewModel
    {
        private ObservableCollection<HOADON> _listSP;
        public ObservableCollection<HOADON> listSP { get => _listSP; set { _listSP = value; OnPropertyChanged(); } }
        public ICommand SearchCommand { get; set; }
        public ICommand Detail { get; set; }
        public ICommand LoadCsCommand { get; set; }
        private ObservableCollection<string> _listTK;
        public ObservableCollection<string> listTK { get => _listTK; set { _listTK = value; OnPropertyChanged(); } }
        public OrdersViewModel()
        {
            listTK = new ObservableCollection<string>() { "Họ tên", "Số HD", "Ngày" };
            listSP = new ObservableCollection<HOADON>(DataProvider.Ins.DB.HOADONs);
            SearchCommand = new RelayCommand<OrdersView>((p) => true, (p) => _SearchCommand(p));
            //Detail = new RelayCommand<OrdersView>((p) => p.ListViewProduct.SelectedItem != null ? true : false, (p) => _Detail(p));
            LoadCsCommand = new RelayCommand<OrdersView>((p) => true, (p) => _LoadCsCommand(p));
        }
        void _LoadCsCommand(OrdersView parameter)
        {
            parameter.cbxChon.SelectedIndex = 0;
        }
        bool check(int m)
        {
            foreach (HOADON temp in DataProvider.Ins.DB.HOADONs)
            {
                if (temp.SOHD == m)
                    return true;
            }
            return false;
        }
        int rdma()
        {
            int ma;
            do
            {
                Random rand = new Random();
                ma = rand.Next(0, 10000);
            } while (check(ma));
            return ma;
        }
        
        void _SearchCommand(OrdersView paramater)
        {
            ObservableCollection<HOADON> temp = new ObservableCollection<HOADON>();
            if (paramater.txbSearch.Text != "")
            {
                switch (paramater.cbxChon.SelectedItem.ToString())
                {
                    case "Số HD":
                        {
                            try
                            {
                                foreach (HOADON s in listSP)
                                {
                                    if (s.SOHD == int.Parse(paramater.txbSearch.Text))
                                    {
                                        temp.Add(s);
                                    }
                                }

                            }
                            catch { }
                            break;
                        }
                    case "Họ tên":
                        {
                            foreach (HOADON s in listSP)
                            {
                                if (s.KHACH.HOTEN.ToLower().Contains(paramater.txbSearch.Text.ToLower()))
                                {
                                    temp.Add(s);
                                }
                            }
                            break;
                        }
                    case "Ngày":
                        {
                            foreach (HOADON s in listSP)
                            {
                                if (s.NGMH.ToString().Contains(paramater.txbSearch.Text))
                                {
                                    temp.Add(s);
                                }
                            }
                            break;
                        }
                    default:
                        {
                            foreach (HOADON s in listSP)
                            {
                                if (s.KHACH.HOTEN.ToLower().Contains(paramater.txbSearch.Text.ToLower()))
                                {
                                    temp.Add(s);
                                }
                            }
                            break;
                        }
                }
                paramater.ListViewProduct.ItemsSource = temp;
            }
            else
                paramater.ListViewProduct.ItemsSource = listSP;
        }
        //void _Detail(OrdersView parameter)
        //{
        //    DetailOrder detailOrder = new DetailOrder();
        //    HOADON temp = (HOADON)parameter.ListViewHD.SelectedItem;
        //    detailOrder.MaND.Text = temp.CUAHANG.MACH;
        //    detailOrder.TenND.Text = temp.KHACH.HOTEN;
        //    detailOrder.Ngay.Text = temp.NGMH.ToString("dd/MM/yyyy hh:mm tt");
        //    detailOrder.SoHD.Text = temp.SOHD.ToString();
        //    detailOrder.MaKH.Text = temp.MAKH.ToString();
        //    detailOrder.TenKH.Text = temp.KHACHHANG.HOTEN;
        //    detailOrder.KM.Text = temp.KHUYENMAI.ToString() + "%";
        //    List<HienThi> list = new List<HienThi>();
        //    foreach (CTHD a in temp.CTHDs)
        //    {
        //        list.Add(new HienThi(a.MASP, a.SANPHAM.TENSP, a.SANPHAM.SIZE, a.SL, a.SANPHAM.GIA, a.SL * a.SANPHAM.GIA));
        //    }
        //    detailOrder.ListViewSP.ItemsSource = list;
        //    detailOrder.GG.Text = "- " + String.Format("{0:0,0}", (temp.TRIGIA * 100 / (100 - temp.KHUYENMAI)) * temp.KHUYENMAI / 100) + " VND";
        //    detailOrder.TT.Text = String.Format("{0:0,0}", temp.TRIGIA) + " VND";
        //    detailOrder.TT1.Text = String.Format("{0:0,0}", temp.TRIGIA) + " VND";
        //    detailOrder.ShowDialog();
        //    parameter.ListViewHD.SelectedItem = null;
        //    listHD = new ObservableCollection<HOADON>(DataProvider.Ins.DB.HOADONs);
        //    parameter.ListViewHD.ItemsSource = listHD;
        //    _SearchCommand(parameter);
        //}
    }
}
