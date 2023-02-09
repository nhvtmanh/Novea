using Novea.Model;
using Novea.View;
using Novea.View.Client;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Novea.ViewModel.Client
{
    public class StoreViewModel : BaseViewModel
    {
        private ObservableCollection<CUAHANG> listStore;
        public ObservableCollection<CUAHANG> ListStore
        {
            get => listStore;
            set { listStore = value; OnPropertyChanged(); }
        }
        private ObservableCollection<CUAHANG> listStore1;
        public ObservableCollection<CUAHANG> ListStore1
        {
            get => listStore1;
            set { listStore1 = value; OnPropertyChanged(); }
        }
        public ICommand LoadStoreCommand { get; set; }
        public ICommand StoreDetailCommand { get; set; }
        public ICommand SearchCommand { get; set; }
        public StoreViewModel()
        {
            Const.CH = null;
            ListStore1 = new ObservableCollection<CUAHANG>(DataProvider.Ins.DB.CUAHANGs);
            ListStore = new ObservableCollection<CUAHANG>(ListStore1.GroupBy(p => p.TENCH).Select(grp => grp.FirstOrDefault()));            
            LoadStoreCommand = new RelayCommand<Home>((p) => true, (p) => loadStore(p));
            StoreDetailCommand = new RelayCommand<Home>((p) => { return p.ListViewStore.SelectedItem == null ? false : true; }, (p) => DisplayStoreDetail(p));
            SearchCommand = new RelayCommand<Home>((p) => { return p == null ? false : true; }, (p) => Search(p));
        }

        void Search(Home parameter)
        {
            ObservableCollection<CUAHANG> temp = new ObservableCollection<CUAHANG>();
            if (parameter.txbSearch.Text == "")
            {
                parameter.ListViewStore.ItemsSource = ListStore;
            }
            else
            {
                foreach (CUAHANG c in ListStore)
                {
                    if (c.TENCH.ToLower().Contains(parameter.txbSearch.Text.ToLower()))
                    {
                        temp.Add(c);
                    }
                }
                if (temp != null)
                {
                    parameter.ListViewStore.ItemsSource = temp;
                }
                else
                {
                    MessageBox.Show("Không tìm thấy cửa hàng");
                }
            }
        }

        void loadStore(Home parameter)
        {
            DataProvider.Ins.Refresh();
            //ListStore1 = new ObservableCollection<CUAHANG>(DataProvider.Ins.DB.CUAHANGs);           
            //ListStore = new ObservableCollection<CUAHANG>(ListStore1.GroupBy(p => p.TENCH).Select(grp => grp.FirstOrDefault()));           
            Const.CH = null;
        }
        void DisplayStoreDetail(Home parameter)
        {
            CUAHANG temp = (CUAHANG)parameter.ListViewStore.SelectedItem;
            Const.CH = temp;
            Page detailStore = new StoreDetail();
            Guest.Instance.Main.NavigationService.Navigate(detailStore);
        }
    }
}
