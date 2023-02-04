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
        private ObservableCollection<HOADON> _listHD;
        public ObservableCollection<HOADON> listHD { get => _listHD; set { _listHD = value; OnPropertyChanged(); } }
        public ICommand SearchCommand { get; set; }
        public ICommand Detail { get; set; }
        public ICommand LoadCsCommand { get; set; }
        private ObservableCollection<string> _listTK;
        public ObservableCollection<string> listTK { get => _listTK; set { _listTK = value; OnPropertyChanged(); } }
        public OrdersViewModel()
        {
            listTK = new ObservableCollection<string>() { "Họ tên", "Số HD", "Ngày" };
            listHD = new ObservableCollection<HOADON>(DataProvider.Ins.DB.HOADONs.Where(p => p.MACH == Const.CH.MACH && p.FINISHORDERCLIENT == true && p.DONE == false));
            SearchCommand = new RelayCommand<OrdersView>((p) => true, (p) => _SearchCommand(p));
            Detail = new RelayCommand<OrdersView>((p) => p.ListViewHD.SelectedItem != null ? true : false, (p) => _Detail(p));
            LoadCsCommand = new RelayCommand<OrdersView>((p) => true, (p) => _LoadCsCommand(p));
        }
        void _LoadCsCommand(OrdersView parameter)
        {
            listTK = new ObservableCollection<string>() { "Họ tên", "Số HD", "Ngày" };
            listHD = new ObservableCollection<HOADON>(DataProvider.Ins.DB.HOADONs.Where(p => p.MACH == Const.CH.MACH && p.FINISHORDERCLIENT == true && p.DONE == false));
            parameter.cbxChon.SelectedIndex = 0;
            _SearchCommand(parameter);
        }
        bool check(string m)
        {
            foreach (HOADON temp in DataProvider.Ins.DB.HOADONs)
            {
                if (temp.SOHD == m)
                    return true;
            }
            return false;
        }
        string rdmaHD()
        {
            string maHD;
            do
            {
                Random rand = new Random();
                maHD = "HD" + rand.Next(0, 10000);
            } while (check(maHD));
            return maHD;
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
                                foreach (HOADON s in listHD)
                                {
                                    if (s.SOHD.ToLower().Contains(paramater.txbSearch.Text.ToLower())) 
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
                            foreach (HOADON s in listHD)
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
                            foreach (HOADON s in listHD)
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
                            foreach (HOADON s in listHD)
                            {
                                if (s.KHACH.HOTEN.ToLower().Contains(paramater.txbSearch.Text.ToLower()))
                                {
                                    temp.Add(s);
                                }
                            }
                            break;
                        }
                }
                paramater.ListViewHD.ItemsSource = temp;
            }
            else
                paramater.ListViewHD.ItemsSource = listHD;
        }
        void _Detail(OrdersView parameter)
        {
            DetailOrders detailOrder = new DetailOrders();
            HOADON temp = (HOADON)parameter.ListViewHD.SelectedItem;
            Const.HD = temp;
            detailOrder.TenKH.Text = temp.KHACH.HOTEN;
            detailOrder.DCKH.Text = temp.KHACH.DIACHI;
            detailOrder.ShowDialog();
            parameter.ListViewHD.SelectedItem = null;
            listHD = new ObservableCollection<HOADON>(DataProvider.Ins.DB.HOADONs.Where(p => p.MACH == Const.CH.MACH && p.FINISHORDERCLIENT == true && p.DONE == false));
            parameter.ListViewHD.ItemsSource = listHD;
            _SearchCommand(parameter);
        }
    }
}
