using Novea.Model;
using Novea.View;
using Novea.View.Admin;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Novea.ViewModel.Admin
{
    public class DetailOrdersViewModel : BaseViewModel
    {
        private ObservableCollection<CTHD> _listCTHD;
        public ObservableCollection<CTHD> listCTHD { get => _listCTHD; set { _listCTHD = value; OnPropertyChanged(); } }
        public ICommand GetSoHD { get; set; }
        private string SoHD_Now;
        public ICommand Closewd { get; set; }
        public ICommand MoveWindow { get; set; }
        public ICommand Loadwd { get; set; }
        public ICommand FinishOrderCommand { get; set; }
        private int _TongTien;
        public int TongTien { get => _TongTien; set { _TongTien = value; OnPropertyChanged(); } }
        public DetailOrdersViewModel()
        {
            GetSoHD = new RelayCommand<DetailOrders>((p) => true, (p) => _GetSoHD(p));
            Closewd = new RelayCommand<DetailOrders>((p) => true, (p) => Close(p));
            MoveWindow = new RelayCommand<DetailOrders>((p) => true, (p) => moveWindow(p));
            Loadwd = new RelayCommand<DetailOrders>((p) => true, (p) => _Loadwd(p));
            FinishOrderCommand = new RelayCommand<DetailOrders>((p) => true, (p) => _FinishOrderCommand(p));
        }
        void _FinishOrderCommand(DetailOrders p)
        {
            MessageBoxResult h = System.Windows.MessageBox.Show("Bạn xác nhận hoàn thành đơn hàng này ?", "THÔNG BÁO", MessageBoxButton.YesNoCancel, MessageBoxImage.Question);
            if (h == MessageBoxResult.Yes)
            {
                var uRow = DataProvider.Ins.DB.HOADONs.Where(w => w.SOHD == SoHD_Now).FirstOrDefault();
                uRow.DONE = true;
                DataProvider.Ins.DB.SaveChanges();

                p.Close();
            }
        }

        void _GetSoHD(DetailOrders p)
        {
            SoHD_Now = Const.HD.SOHD;
        }

        void _Loadwd(DetailOrders p)
        {
            DataProvider.Ins.Refresh();
            listCTHD = new ObservableCollection<CTHD>(DataProvider.Ins.DB.CTHDs.Where(pa => pa.SOHD == SoHD_Now));
            HOADON hd_temp = DataProvider.Ins.DB.HOADONs.Where(pa => pa.SOHD == SoHD_Now).FirstOrDefault();
            TongTien = (int)hd_temp.TONGTIEN;
        }
        void moveWindow(DetailOrders p)
        {
            p.DragMove();
        }
        void Close(DetailOrders p)
        {
            p.Close();
        }
    }
}
