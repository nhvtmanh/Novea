using Novea.Model;
using Novea.View.Admin;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows;
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
        public ICommand SearchCommand { get; set; }
        public ICommand DetailPdCommand { get; set; }
        public ICommand LoadCsCommand { get; set; }
        public ICommand SortCommand { get; set; }

        public ManagerViewModel()
        {
            listHD1 = new ObservableCollection<HOADON>(DataProvider.Ins.DB.HOADONs);
            SearchCommand = new RelayCommand<ManagerView>((p) => { return p == null ? false : true; }, (p) => _SearchCommand(p));
            SortCommand = new RelayCommand<ManagerView>((p) => { return p == null ? false : true; }, (p) => _SortCommand(p));
            DetailPdCommand = new RelayCommand<ManagerView>((p) => { return p.ListViewBill.SelectedItem == null ? false : true; }, (p) => _DetailPd(p));
            LoadCsCommand = new RelayCommand<ManagerView>((p) => true, (p) => _LoadCsCommand(p));
            listHD = new ObservableCollection<HOADON>(listHD1.GroupBy(p => p.SOHD).Select(grp => grp.FirstOrDefault()));

            if (DesignerProperties.GetIsInDesignMode(new DependencyObject()))
                return;           
        }
        void _LoadCsCommand(ManagerView parameter)
        {
            listHD = new ObservableCollection<HOADON>(DataProvider.Ins.DB.HOADONs);
            listHD = new ObservableCollection<HOADON>(listHD1.GroupBy(p => p.SOHD).Select(grp => grp.FirstOrDefault()));
            parameter.cbxChon.SelectedIndex = 0;
            parameter.ListViewBill.Items.SortDescriptions.Add(new SortDescription("SOHD", ListSortDirection.Ascending));
        }
        void _SearchCommand(ManagerView paramater)
        {
            ObservableCollection<HOADON> temp = new ObservableCollection<HOADON>();
            if (paramater.txbSearch.Text == "")
            {
                MessageBox.Show("Vui lòng điền vào ô tìm kiếm");
            }
            else
            {
                foreach (HOADON s in listHD)
                {
                    if (s.SOHD.ToString().Contains(paramater.txbSearch.Text))
                    {
                        temp.Add(s);
                    }
                }
                if(temp != null)
                {
                    paramater.ListViewBill.ItemsSource = temp;
                }
                else
                {
                    MessageBox.Show("Không tìm thấy mã hóa đơn");
                }
            }
        }

        void _SortCommand(ManagerView parameter)
        {
            var SortDirection = parameter.cbxChon.SelectedIndex.ToString() == "0" ? ListSortDirection.Ascending : ListSortDirection.Descending;
            parameter.ListViewBill.Items.SortDescriptions[0] = new SortDescription("SOHD", SortDirection);
        }

        void _DetailPd(ManagerView paramater)
        {
            DetailBill detailProduct = new DetailBill();
            HOADON temp = (HOADON)paramater.ListViewBill.SelectedItem;
            detailProduct.MaHD.Text = temp.SOHD.ToString();
            detailProduct.GIA.Text = string.Format("{0:0,0}", temp.TONGTIEN) + " VNĐ";
            detailProduct.NGMH.Text = temp.NGMH.ToString();
            detailProduct.MAKH.Text = temp.MAKH;
            detailProduct.MACCH.Text = temp.MAKH;
            detailProduct.ShowDialog();
        }
    }
}
