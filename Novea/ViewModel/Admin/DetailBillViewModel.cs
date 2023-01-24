using Novea.Model;
using Novea.View.Admin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows;

namespace Novea.ViewModel.Admin
{
    public class DetailBillViewModel : BaseViewModel
    {
        public ICommand Closewd { get; set; }
        public ICommand Minimizewd { get; set; }
        public ICommand DeleteOrder { get; set; }
        public ICommand MoveWindow { get; set; }
        public DetailBillViewModel()
        {
            Closewd = new RelayCommand<DetailBill>((p) => true, (p) => Close(p));
            Minimizewd = new RelayCommand<DetailBill>((p) => true, (p) => Minimize(p));
            DeleteOrder = new RelayCommand<DetailBill>((p) => true, (p) => _DeleteOrder(p));
            MoveWindow = new RelayCommand<DetailBill>((p) => true, (p) => moveWindow(p));
        }
        void _DeleteOrder(DetailBill parameter)
        {
            MessageBoxResult h = System.Windows.MessageBox.Show("Bạn muốn xóa hóa đơn này?", "THÔNG BÁO", MessageBoxButton.YesNoCancel, MessageBoxImage.Question);
            if (h == MessageBoxResult.Yes)
            {
                foreach (HOADON temp in DataProvider.Ins.DB.HOADONs)
                {
                    if (temp.SOHD == parameter.MaHD.Text)
                    {
                        DataProvider.Ins.DB.HOADONs.Remove(temp);
                    }
                }
                DataProvider.Ins.DB.SaveChanges();
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