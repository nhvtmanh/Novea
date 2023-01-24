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
    public class HienThi
    {
        public string MaSP { get; set; }
        public string TenSP { get; set; }
        public Nullable<int> SL { get; set; }
        public Nullable<decimal> TriGia { get; set; }
        public string LuongDuong { get; set; }
        public int Tong { get; set; }
        public string Size { get; set; }
        public string LuongDa { get; set; }
        public HienThi(string MaSp = "", string TenSP = "", string Size = "", Nullable<decimal> TriGia = 0, Nullable<int> SL = 0, int Tong = 0, string LuongDuong = "", string LuongDa = "")
        {
            this.MaSP = MaSp;
            this.TenSP = TenSP;
            this.SL = SL;
            this.Size = Size;
            this.TriGia = TriGia;
            this.LuongDuong = LuongDuong;
            this.LuongDa = LuongDa;
        }
    }
    public class ManagerViewModel : BaseViewModel
    {
        private ObservableCollection<HOADON> _listHD;
        public ObservableCollection<HOADON> listHD { get => _listHD; set { _listHD = value; OnPropertyChanged(); } }

        private ObservableCollection<HOADON> _listHD1;
        public ObservableCollection<HOADON> listHD1 { get => _listHD1; set { _listHD1 = value; OnPropertyChanged(); } }
        public ICommand SearchCommand { get; set; }
        public ICommand DetailPdCommand { get; set; }
        public ICommand AddPdPdCommand { get; set; }
        public ICommand LoadCsCommand { get; set; }
        public ICommand SortCommand { get; set; }

        public ManagerViewModel()
        {
            listHD1 = new ObservableCollection<HOADON>(DataProvider.Ins.DB.HOADONs);
            SearchCommand = new RelayCommand<ManagerView>((p) => { return p == null ? false : true; }, (p) => _SearchCommand(p));
            LoadCsCommand = new RelayCommand<ManagerView>((p) => true, (p) => _LoadCsCommand(p));
            SortCommand = new RelayCommand<ManagerView>((p) => { return p == null ? false : true; }, (p) => _SortCommand(p));
            DetailPdCommand = new RelayCommand<ManagerView>((p) => { return p.ListViewBill.SelectedItem == null ? false : true; }, (p) => _DetailPd(p));
            listHD = new ObservableCollection<HOADON>(listHD1.GroupBy(p => p.SOHD).Select(grp => grp.FirstOrDefault()).Where(p=>p.MACH==Const.MACH));
            if (DesignerProperties.GetIsInDesignMode(new DependencyObject()))
                return;
        }
        void _LoadCsCommand(ManagerView parameter)
        {            
            listHD1 = new ObservableCollection<HOADON>(DataProvider.Ins.DB.HOADONs);
            listHD = new ObservableCollection<HOADON>(listHD1.GroupBy(p => p.SOHD).Select(grp => grp.FirstOrDefault()).Where(p => p.MACH == Const.MACH));
            parameter.cbxChon.SelectedIndex = 0;
            parameter.ListViewBill.Items.SortDescriptions.Add(new SortDescription("SOHD", ListSortDirection.Ascending));
        }
        void _SortCommand(ManagerView parameter)
        {
            var SortDirection = parameter.cbxChon.SelectedIndex.ToString() == "0" ? ListSortDirection.Ascending : ListSortDirection.Descending;
            parameter.ListViewBill.Items.SortDescriptions[0] = new SortDescription("SOHD", SortDirection);
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



        void _DetailPd(ManagerView paramater)
        {
            DetailBill detailBill = new DetailBill();
            HOADON temp = (HOADON)paramater.ListViewBill.SelectedItem;
            detailBill.MaHD.Text = temp.SOHD.ToString();
            detailBill.GIA.Text = string.Format("{0:0,0}", temp.TONGTIEN) + " VNĐ";
            detailBill.NGMH.Text = temp.NGMH.ToString();
            detailBill.MAKH.Text = temp.MAKH;
            detailBill.MACCH.Text = temp.MACH;

            List<HienThi> list = new List<HienThi>();
            foreach (CTHD a in temp.CTHDs)
            {
                list.Add(new HienThi(a.MASP, a.SANPHAM.TENSP, a.SANPHAM.SIZE, a.SANPHAM.DONGIA, a.SOLUONG, (int)a.TRIGIA, a.LuongDuong, a.LuongDa));
            }

            detailBill.ListViewSP.ItemsSource = list;

            detailBill.ShowDialog();
            listHD = new ObservableCollection<HOADON>(DataProvider.Ins.DB.HOADONs);
            paramater.ListViewBill.SelectedItem = null;
        }


        void _Filter(ManagerView parameter)
        {
            var SortProperty = parameter.cbxChon.Text;
            

            switch (parameter.cbxChon.SelectedIndex.ToString())
            {
                case "0":
                    {
                        listHD = new ObservableCollection<HOADON>(listHD1.GroupBy(p => p.SOHD).Select(grp => grp.FirstOrDefault()));
                        parameter.ListViewBill.ItemsSource = listHD;
                        break;
                    }
                case "1":
                    {
                        listHD = new ObservableCollection<HOADON>(listHD1.GroupBy(p => p.SOHD).Select(grp => grp.FirstOrDefault())/*.Where(p => p.SOHD)*/);
                        parameter.ListViewBill.ItemsSource = listHD;
                        break;
                    }
                case "2":
                    {
                        listHD = new ObservableCollection<HOADON>(listHD1.GroupBy(p => p.SOHD).Select(grp => grp.FirstOrDefault())/*.Where(p => p.SOHD == "1")*/);
                        parameter.ListViewBill.ItemsSource = listHD;
                        break;
                    }                
            }
        }
    }
}
