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
        public ICommand MoveWindow { get; set; }
        public DetailBillViewModel()
        {
            Closewd = new RelayCommand<DetailBill>((p) => true, (p) => Close(p));
            Minimizewd = new RelayCommand<DetailBill>((p) => true, (p) => Minimize(p));
            MoveWindow = new RelayCommand<DetailBill>((p) => true, (p) => moveWindow(p));
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