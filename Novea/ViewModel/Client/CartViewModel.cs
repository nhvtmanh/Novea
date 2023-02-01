using Novea.Model;
using Novea.View;
using Novea.View.Client;
using Novea.View.Admin;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using System.Globalization;
using System.Windows.Data;

namespace Novea.ViewModel.Client
{
    public class CartViewModel : BaseViewModel
    {
        private ObservableCollection<CTHD> _listCTHD;
        public ObservableCollection<CTHD> listCTHD { get => _listCTHD; set { _listCTHD = value; /*OnPropertyChanged();*/ } }
        public ICommand LoadCartCommand { get; set; }
        public CartViewModel()
        {
            LoadCartCommand = new RelayCommand<Cart>((p) => true, (p) => _LoadCartCommand(p));

        }
        void _LoadCartCommand(Cart parameter)
        {
            if (Const.HD != null)
            {
                listCTHD = new ObservableCollection<CTHD>(DataProvider.Ins.DB.CTHDs.Where(p => p.SOHD == Const.HD.SOHD));
            }
        }
    }
}

