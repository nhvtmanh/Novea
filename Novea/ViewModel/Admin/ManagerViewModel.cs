using Novea.Model;
using Novea.View.Admin;
using System;
using System.Collections.Generic;
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
            LoadCsCommand = new RelayCommand<ManagerView>((p) => true, (p) => _LoadCsCommand(p));
            SortCommand = new RelayCommand<ManagerView>((p) => { return p == null ? false : true; }, (p) => _SortCommand(p));
            DetailPdCommand = new RelayCommand<ManagerView>((p) => { return p.ListViewBill.SelectedItem == null ? false : true; }, (p) => _DetailPd(p));
            listHD = new ObservableCollection<HOADON>(listHD1.GroupBy(p => p.SOHD).Select(grp => grp.FirstOrDefault()).Where(p=>p.MACH==Const.MACH && p.FINISHORDERCLIENT == true && p.DONE == true));
            if (DesignerProperties.GetIsInDesignMode(new DependencyObject()))
                return;
        }
        void _LoadCsCommand(ManagerView parameter)
        {            
            listHD1 = new ObservableCollection<HOADON>(DataProvider.Ins.DB.HOADONs);
            listHD = new ObservableCollection<HOADON>(listHD1.GroupBy(p => p.SOHD).Select(grp => grp.FirstOrDefault()).Where(p => p.MACH == Const.MACH && p.FINISHORDERCLIENT == true && p.DONE == true));
            parameter.cbxChon.SelectedIndex = 0;
            parameter.ListViewBill.Items.SortDescriptions.Add(new SortDescription("TONGTIEN", ListSortDirection.Ascending));
        }

        void _SortCommand(ManagerView parameter)
        {
            var SortDirection = parameter.cbxChon.SelectedIndex.ToString() == "0" ? ListSortDirection.Ascending : ListSortDirection.Descending;
            parameter.ListViewBill.Items.SortDescriptions[0] = new SortDescription("TONGTIEN", SortDirection);
        }

        void _SearchCommand(ManagerView paramater)
        {
            ObservableCollection<HOADON> temp = new ObservableCollection<HOADON>();
            if (paramater.txbSearch.Text == "")
            {
                paramater.ListViewBill.ItemsSource = listHD;
            }
            else
            {
                foreach (HOADON s in listHD)
                {
                    if (s.SOHD.ToLower().Contains(paramater.txbSearch.Text.ToLower()))
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
                    MessageBox.Show("Không tìm thấy số hóa đơn");
                }
            }
        }

        void _DetailPd(ManagerView paramater)
        {
            DetailBill detailBill = new DetailBill();
            HOADON temp = (HOADON)paramater.ListViewBill.SelectedItem;
            detailBill.SoHD.Text = temp.SOHD;
            detailBill.GIA.Text = string.Format("{0:0,0}", temp.TONGTIEN) + " VNĐ";
            detailBill.NGMH.Text = temp.NGMH.ToString();
            detailBill.MAKH.Text = temp.MAKH;
            detailBill.MACCH.Text = temp.MACH;
            
            detailBill.ShowDialog();
            listHD = new ObservableCollection<HOADON>(DataProvider.Ins.DB.HOADONs.Where(p => p.MACH == Const.MACH && p.FINISHORDERCLIENT == true && p.DONE == true));
            paramater.ListViewBill.SelectedItem = null;
            paramater.ListViewBill.ItemsSource = listHD;
            _SearchCommand(paramater);
        }
    }
}
