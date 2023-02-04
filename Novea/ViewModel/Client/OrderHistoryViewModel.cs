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
        public ICommand LoadOrderHistoryCommand { get; set; }
        public OrderHistoryViewModel()
        {
            LoadOrderHistoryCommand = new RelayCommand<OrderHistory>((p) => true, (p) => LoadOrderHistory(p));
        }

        void LoadOrderHistory(OrderHistory p)
        {
            TongTien = (int)DataProvider.Ins.DB.HOADONs.Where(h => h.MAKH == Const.KH.MAKH && h.DONE == true).Select(h => h.TONGTIEN).Sum();
            
        }
    }
}
