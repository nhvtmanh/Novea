using Novea.Model;
using Novea.View;
using Novea.View.Client;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Navigation;

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
        public StoreViewModel()
        {
            Const.CH = null;
            ListStore1 = new ObservableCollection<CUAHANG>(DataProvider.Ins.DB.CUAHANGs);
            ListStore = new ObservableCollection<CUAHANG>(ListStore1.GroupBy(p => p.TENCH).Select(grp => grp.FirstOrDefault()));
            LoadStoreCommand = new RelayCommand<Home>((p) => true, (p) => loadStore(p));
            StoreDetailCommand = new RelayCommand<Home>((p) => { return p.ListViewStore.SelectedItem != null; }, (p) => DisplayStoreDetail(p));
        }
        void loadStore(Home parameter)
        {
            ListStore1 = new ObservableCollection<CUAHANG>(DataProvider.Ins.DB.CUAHANGs);
            ListStore = new ObservableCollection<CUAHANG>(ListStore1.GroupBy(p => p.TENCH).Select(grp => grp.FirstOrDefault()));
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
