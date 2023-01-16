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
        private ObservableCollection<CHUCUAHANG> listStore;
        public ObservableCollection<CHUCUAHANG> ListStore
        {
            get => listStore;
            set { listStore = value; }
        }
        private ObservableCollection<CHUCUAHANG> listStore1;
        public ObservableCollection<CHUCUAHANG> ListStore1
        {
            get => listStore1;
            set { listStore1 = value; }
        }
        public ICommand LoadStoreCommand { get; set; }
        public StoreViewModel()
        {
            ListStore1 = new ObservableCollection<CHUCUAHANG>(DataProvider.Ins.DB.CHUCUAHANGs);
            ListStore = new ObservableCollection<CHUCUAHANG>(ListStore1.GroupBy(p => p.TENCUAHANG).Select(grp => grp.FirstOrDefault()));
            LoadStoreCommand = new RelayCommand<HomeView>((p) => true, (p) => loadStore(p));
        }
        void loadStore(HomeView parameter)
        {
            ListStore1 = new ObservableCollection<CHUCUAHANG>(DataProvider.Ins.DB.CHUCUAHANGs);
            ListStore = new ObservableCollection<CHUCUAHANG>(ListStore1.GroupBy(p => p.TENCUAHANG).Select(grp => grp.FirstOrDefault()));
        }
        public void moveWindow(MainWindow p)
        {
            p.DragMove();
        }
    }
}
