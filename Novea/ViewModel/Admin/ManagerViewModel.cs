using Novea.Model;
using Novea.View;
using Novea.View.Admin;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media.Imaging;

namespace Novea.ViewModel.Admin
{
    public class ManagerViewModel : BaseViewModel
    {
        private ObservableCollection<HOADON> _listHD;
        public ObservableCollection<HOADON> listHD { get => _listHD; set { _listHD = value; OnPropertyChanged(); } }

        private ObservableCollection<HOADON> _listHD1;
        public ObservableCollection<HOADON> listHD1 { get => _listHD1; set { _listHD1 = value; OnPropertyChanged(); } }
        public ICommand LoadCsCommand { get; set; }

        public ManagerViewModel()
        {
            listHD1 = new ObservableCollection<HOADON>(DataProvider.Ins.DB.HOADONs);
            LoadCsCommand = new RelayCommand<ManagerView>((p) => true, (p) => _LoadCsCommand(p));
            listHD = new ObservableCollection<HOADON>(listHD1.GroupBy(p => p.SOHD).Select(grp => grp.FirstOrDefault()));
            if (DesignerProperties.GetIsInDesignMode(new DependencyObject()))
                return;
        }
        void _LoadCsCommand(ManagerView parameter)
        {
            //listTK = new ObservableCollection<string>() { "Tên SP", "Giá SP" };
            listHD = new ObservableCollection<HOADON>(DataProvider.Ins.DB.HOADONs);
            listHD = new ObservableCollection<HOADON>(listHD1.GroupBy(p => p.SOHD).Select(grp => grp.FirstOrDefault()));
            //parameter.cbxChon.SelectedIndex = 0;
            //parameter.cbxChon1.SelectedIndex = 0;
            //_Filter(parameter);
            //_SearchCommand(parameter);
        }
    }
}
