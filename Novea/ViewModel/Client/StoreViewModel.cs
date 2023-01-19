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
        private ObservableCollection<CUAHANG> listStore;
        public ObservableCollection<CUAHANG> ListStore
        {
            get => listStore;
            set { listStore = value; }
        }
        private ObservableCollection<CUAHANG> listStore1;
        public ObservableCollection<CUAHANG> ListStore1
        {
            get => listStore1;
            set { listStore1 = value; }
        }
        public ICommand LoadStoreCommand { get; set; }
        public StoreViewModel()
        {
            ListStore1 = new ObservableCollection<CUAHANG>(DataProvider.Ins.DB.CUAHANGs);
            ListStore = new ObservableCollection<CUAHANG>(ListStore1.GroupBy(p => p.TENCH).Select(grp => grp.FirstOrDefault()));
            LoadStoreCommand = new RelayCommand<HomeView>((p) => true, (p) => loadStore(p));
        }
        void loadStore(HomeView parameter)
        {
            ListStore1 = new ObservableCollection<CUAHANG>(DataProvider.Ins.DB.CUAHANGs);
            ListStore = new ObservableCollection<CUAHANG>(ListStore1.GroupBy(p => p.TENCH).Select(grp => grp.FirstOrDefault()));
        }
        public void moveWindow(MainWindow p)
        {
            p.DragMove();
        }
    }
}
