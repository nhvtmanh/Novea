using Novea.Model;
using Novea.View.Admin;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Documents;
using System.Windows.Input;

namespace Novea.ViewModel.Client
{
    public class StoreViewModel : BaseViewModel
    {
        private ObservableCollection<CUAHANG> _listStore;
        public ObservableCollection<CUAHANG> listStore
        {
            get => _listStore;
            set { _listStore = value; }
        }
        private ObservableCollection<CUAHANG> _listStore1;
        public ObservableCollection<CUAHANG> listStore1
        {
            get => _listStore1;
            set { _listStore1 = value; }
        }
        public ICommand LoadStoreCommand { get; set; }
        public StoreViewModel()
        {
            listStore1 = new ObservableCollection<CUAHANG>(DataProvider.Ins.DB.CUAHANGs);
            listStore = new ObservableCollection<CUAHANG>(listStore1.GroupBy(p => p.TENCH).Select(grp => grp.FirstOrDefault()));
            LoadStoreCommand = new RelayCommand<HomeView>((p) => true, (p) => loadStore(p));
        }
        void loadStore(HomeView parameter)
        {
            listStore1 = new ObservableCollection<CUAHANG>(DataProvider.Ins.DB.CUAHANGs);
            listStore = new ObservableCollection<CUAHANG>(listStore1.GroupBy(p => p.TENCH).Select(grp => grp.FirstOrDefault()));
        }
    }
}
