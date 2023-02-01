using Novea.Model;
using Novea.View.Admin;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Documents;
using System.Windows.Input;

namespace Novea.ViewModel.Admin
{
    public class CustomerViewModel : BaseViewModel
    {
        private ObservableCollection<KHACH> _listKH;
        public ObservableCollection<KHACH> listKH { get => _listKH; set { _listKH = value; OnPropertyChanged(); } }

        private ObservableCollection<KHACH> _listKH1;
        public ObservableCollection<KHACH> listKH1 { get => _listKH1; set { _listKH1 = value; OnPropertyChanged(); } }
        public ICommand SortCommand { get; set; }
        public ICommand SearchCommand { get; set; }
        public ICommand Detail { get; set; }
        public ICommand AddCsCommand { get; set; }
        public ICommand LoadCsCommand { get; set; }

        public CustomerViewModel()
        {
            listKH1 = new ObservableCollection<KHACH>(DataProvider.Ins.DB.KHACHes);
            listKH = new ObservableCollection<KHACH>(listKH1.GroupBy(p => p.HOTEN).Select(grp => grp.FirstOrDefault()).Where(kh => kh.HOADONs.Any(hd => hd.MACH == Const.MACH)));
            SearchCommand = new RelayCommand<CustomerView>((p) => { return p == null ? false : true; }, (p) => _SearchCommand(p));
            LoadCsCommand = new RelayCommand<CustomerView>((p) => true, (p) => _LoadCsCommand(p));
            SortCommand = new RelayCommand<CustomerView>((p) => { return p == null ? false : true; }, (p) => _SortCommand(p));
        }
        void _LoadCsCommand(CustomerView parameter)
        {
            listKH1 = new ObservableCollection<KHACH>(DataProvider.Ins.DB.KHACHes);
            listKH = new ObservableCollection<KHACH>(listKH1.GroupBy(p => p.HOTEN).Select(grp => grp.FirstOrDefault()).Where(kh => kh.HOADONs.Any(hd => hd.MACH == Const.MACH)));
            parameter.cbxChon.SelectedIndex = 0;
            parameter.ListViewKH.Items.SortDescriptions.Add(new SortDescription("HOTEN", ListSortDirection.Ascending));
        }

        void _SortCommand(CustomerView parameter)
        {
            var SortDirection = parameter.cbxChon.SelectedIndex.ToString() == "0" ? ListSortDirection.Ascending : ListSortDirection.Descending;
            parameter.ListViewKH.Items.SortDescriptions[0] = new SortDescription("HOTEN", SortDirection);
        }
        void _SearchCommand(CustomerView paramater)
        {
            ObservableCollection<KHACH> temp = new ObservableCollection<KHACH>();
            if (paramater.txbSearch.Text == "")
            {
                paramater.ListViewKH.ItemsSource = listKH;

            }
            else
            {
                foreach (KHACH s in listKH)
                {
                    if (s.HOTEN.ToLower().Contains(paramater.txbSearch.Text.ToLower()))
                    {
                        temp.Add(s);
                    }
                }
                if (temp != null)
                {
                    paramater.ListViewKH.ItemsSource = temp;
                }
                else
                {
                    MessageBox.Show("Không tìm thấy họ tên");
                }
            }
        }
    }
}
