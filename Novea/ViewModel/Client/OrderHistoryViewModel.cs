﻿using Novea.Model;
using Novea.View.Client;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity.Core.Metadata.Edm;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Novea.ViewModel.Client
{
    public class OrderHistoryViewModel : BaseViewModel
    {
        private int tongtien;
        public int TongTien { get => tongtien; set { tongtien = value; OnPropertyChanged(); } }
        private ObservableCollection<HOADON> listHD;
        public ObservableCollection<HOADON> ListHD { get => listHD; set { listHD = value; OnPropertyChanged(); } }
        private ObservableCollection<HOADON> listHD1;
        public ObservableCollection<HOADON> ListHD1 { get => listHD1; set { listHD1 = value; OnPropertyChanged(); } }
        public ICommand LoadOrderHistoryCommand { get; set; }
        public OrderHistoryViewModel()
        {
            LoadOrderHistoryCommand = new RelayCommand<OrderHistory>((p) => true, (p) => LoadOrderHistory(p));
        }

        void LoadOrderHistory(OrderHistory p)
        {
            DataProvider.Ins.Refresh();
            listHD1 = new ObservableCollection<HOADON>(DataProvider.Ins.DB.HOADONs.Where(hd => hd.MAKH == Const.KH.MAKH));
            ListHD = new ObservableCollection<HOADON>(listHD1.Where(hd => hd.FINISHORDERCLIENT == true && hd.DONE == true));
            TongTien = (int)DataProvider.Ins.DB.HOADONs.Where(h => h.MAKH == Const.KH.MAKH && h.DONE == true && h.FINISHORDERCLIENT == true).Select(h => h.TONGTIEN).Sum();         
        }
    }
}
