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
        public ICommand Closewd { get; set; }
        public ICommand MoveWindow { get; set; }
        public DetailOrdersViewModel()
        {
            Closewd = new RelayCommand<DetailOrders>((p) => true, (p) => Close(p));
            MoveWindow = new RelayCommand<DetailOrders>((p) => true, (p) => moveWindow(p));
            listCTHD = new ObservableCollection<CTHD>(DataProvider.Ins.DB.CTHDs.Where(p => p.SOHD == Const.HD.SOHD));
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
