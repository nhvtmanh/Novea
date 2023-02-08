using Novea.Model;
using Novea.View.Client;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;

namespace Novea.ViewModel.Client
{
    public class OrderHistoryViewModel : BaseViewModel
    {
        private int tongtien;
        public int TongTien { get => tongtien; set { tongtien = value; OnPropertyChanged(); } }
        private ObservableCollection<HOADON> listHD;
        public ObservableCollection<HOADON> ListHD { get => listHD; set { listHD = value; OnPropertyChanged(); } }
        private ObservableCollection<HOADON> listHD1;
        public ObservableCollection<HOADON> ListHD1 { get => listHD1; set { listHD1 = value; OnPropertyChanged(); } }
        public ICommand LoadOrderHistoryCommand { get; set; }
        public ICommand FilterCommand { get; set; }
        public ICommand OrderCommand { get; set; }
        public ICommand SearchCommand { get; set; }
        public static ConvertBooleanToStatus status { get; } = new ConvertBooleanToStatus();
        public OrderHistoryViewModel()
        {
            LoadOrderHistoryCommand = new RelayCommand<OrderHistory>((p) => true, (p) => LoadOrderHistory(p));
            FilterCommand = new RelayCommand<OrderHistory>((p) => true, (p) => Filter(p));
            OrderCommand = new RelayCommand<OrderHistory>((p) => true, (p) => Order(p));
            SearchCommand = new RelayCommand<OrderHistory>((p) => true, (p) => Search(p));
        }
        void Search(OrderHistory p)
        {
            ObservableCollection<HOADON> temp = new ObservableCollection<HOADON>();
            if (p.txbSearch.Text == "")
            {
                p.ListViewHD.ItemsSource = ListHD;
            }
            else
            {
                foreach (HOADON h in ListHD)
                {
                    if (h.SOHD.ToLower().Contains(p.txbSearch.Text.ToLower()))
                    {
                        temp.Add(h);
                    }
                }
                if (temp != null)
                {
                    p.ListViewHD.ItemsSource = temp;
                }
            }
        }
        void Filter(OrderHistory p)
        {
            switch (p.cbbFilter.SelectedIndex.ToString())
            {
                case "0":
                {
                    ListHD = new ObservableCollection<HOADON>(ListHD1.GroupBy(h => h.SOHD).Select(grp => grp.FirstOrDefault()));
                    p.ListViewHD.ItemsSource = ListHD;
                    break;
                }
                case "1":
                {
                    ListHD = new ObservableCollection<HOADON>(ListHD1.GroupBy(h => h.SOHD).Select(grp => grp.FirstOrDefault()).Where(h => h.DONE == false));
                    p.ListViewHD.ItemsSource = ListHD;
                    break;
                }
                case "2":
                {
                    ListHD = new ObservableCollection<HOADON>(ListHD1.GroupBy(h => h.SOHD).Select(grp => grp.FirstOrDefault()).Where(h => h.DONE == true));
                    p.ListViewHD.ItemsSource = ListHD;
                    break;
                }
            }
        }
        void Order(OrderHistory p)
        {
            switch (p.cbbOrder.SelectedIndex.ToString())
            {
                case "0":
                {
                    ListHD = new ObservableCollection<HOADON>(ListHD1.GroupBy(h => h.SOHD).Select(grp => grp.FirstOrDefault()));
                    p.ListViewHD.ItemsSource = ListHD;
                    break;
                }
                case "1":
                {
                    ListHD = new ObservableCollection<HOADON>(ListHD1.GroupBy(h => h.SOHD).Select(grp => grp.FirstOrDefault()).OrderBy(h => h.TONGTIEN));
                    p.ListViewHD.ItemsSource = ListHD;
                    break;
                }
                case "2":
                {
                    ListHD = new ObservableCollection<HOADON>(ListHD1.GroupBy(h => h.SOHD).Select(grp => grp.FirstOrDefault()).OrderByDescending(h => h.TONGTIEN));
                    p.ListViewHD.ItemsSource = ListHD;
                    break;
                }
                case "3":
                {
                    ListHD = new ObservableCollection<HOADON>(ListHD1.GroupBy(h => h.SOHD).Select(grp => grp.FirstOrDefault()).OrderBy(h => h.NGMH));
                    p.ListViewHD.ItemsSource = ListHD;
                    break;
                }
                case "4":
                {
                    ListHD = new ObservableCollection<HOADON>(ListHD1.GroupBy(h => h.SOHD).Select(grp => grp.FirstOrDefault()).OrderByDescending(h => h.NGMH));
                    p.ListViewHD.ItemsSource = ListHD;
                    break;
                }
            }
        }

        void LoadOrderHistory(OrderHistory p)
        {
            try
            {
                TongTien = (int)DataProvider.Ins.DB.HOADONs.Where(h => h.MAKH == Const.KH.MAKH && h.DONE == true).Select(h => h.TONGTIEN).Sum();
            }
            catch 
            {
                TongTien = 0;
            }
            DataProvider.Ins.Refresh();
            ListHD1 = new ObservableCollection<HOADON>(DataProvider.Ins.DB.HOADONs.Where(h => h.MAKH == Const.KH.MAKH && h.FINISHORDERCLIENT == true));
            ListHD = new ObservableCollection<HOADON>(ListHD1.GroupBy(h => h.SOHD).Select(grp => grp.FirstOrDefault()));
            p.cbbFilter.SelectedIndex = 0;
            p.cbbOrder.SelectedIndex = 0;
            Order(p);
            Filter(p);
        }
    }
}
