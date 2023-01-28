using Novea.View.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Novea.ViewModel.Client
{
    public class ProductDetailViewModel : BaseViewModel
    {
        public ICommand CloseProductDetailwdCommand { get; set; }
        public ProductDetailViewModel()
        {
            CloseProductDetailwdCommand = new RelayCommand<ProductDetail>((p) => true, (p) => CloseProductDetailwd(p));
        }
        void CloseProductDetailwd(ProductDetail p)
        {
            p.Close();
        }
    }
}
