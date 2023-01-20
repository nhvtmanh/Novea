using Novea.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Novea.ViewModel.Client
{
    public class StoreDetailViewModel : BaseViewModel
    {
        private ObservableCollection<SANPHAM> listProduct;
        public ObservableCollection<SANPHAM> ListProduct
        {
            get => listProduct;
            set { listProduct = value; }
        }
    }
}
