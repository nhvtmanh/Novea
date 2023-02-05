using Novea.Model;
using Novea.View.Admin;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows;

namespace Novea.ViewModel.Admin
{
    public class DetailBillViewModel : BaseViewModel
    {
        private ObservableCollection<CTHD> _listCTHD;
        public ObservableCollection<CTHD> listCTHD { get => _listCTHD; set { _listCTHD = value; OnPropertyChanged(); } }
        public ICommand Closewd { get; set; }
        public ICommand Minimizewd { get; set; }
        public ICommand DeleteOrder { get; set; }
        public ICommand MoveWindow { get; set; }
        public ICommand Loadwd { get; set; }
        public ICommand GetSoHD { get; set; }
        private string SoHD_Now;
        public DetailBillViewModel()
        {
            GetSoHD = new RelayCommand<DetailBill>((p) => true, (p) => _GetSoHD(p));
            Loadwd = new RelayCommand<DetailBill>((p) => true, (p) => _Loadwd(p));
            Closewd = new RelayCommand<DetailBill>((p) => true, (p) => Close(p));
            Minimizewd = new RelayCommand<DetailBill>((p) => true, (p) => Minimize(p));
            DeleteOrder = new RelayCommand<DetailBill>((p) => true, (p) => _DeleteOrder(p));
            MoveWindow = new RelayCommand<DetailBill>((p) => true, (p) => moveWindow(p));
        }
        void _GetSoHD(DetailBill p)
        {
            SoHD_Now = p.SoHD.Text;
        }

        void _Loadwd(DetailBill p)
        {
            DataProvider.Ins.Refresh();
            listCTHD = new ObservableCollection<CTHD>(DataProvider.Ins.DB.CTHDs.Where(pa => pa.SOHD == SoHD_Now));
            HOADON hd_temp = DataProvider.Ins.DB.HOADONs.Where(pa => pa.SOHD == SoHD_Now).FirstOrDefault();
        }
        void _DeleteOrder(DetailBill parameter)
        {
            MessageBoxResult h = System.Windows.MessageBox.Show("Bạn chắc chắn muốn xóa hóa đơn này?", "THÔNG BÁO", MessageBoxButton.YesNoCancel, MessageBoxImage.Question);
            if (h == MessageBoxResult.Yes)
            {
                foreach (HOADON temp in DataProvider.Ins.DB.HOADONs)
                {
                    if (temp.SOHD == parameter.SoHD.Text)
                    {
                        DataProvider.Ins.DB.HOADONs.Remove(temp);
                    }
                }
                DataProvider.Ins.DB.SaveChanges();
                parameter.Hide();
            }
        }
        void moveWindow(DetailBill p)
        {
            p.DragMove();
        }
        void Close(DetailBill p)
        {
            p.Close();
        }
        void Minimize(DetailBill p)
        {
            p.WindowState = WindowState.Minimized;
        }
    }
}