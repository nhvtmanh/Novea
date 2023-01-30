using Novea.View.Client;
using Novea.Model;
using Novea.View;
using Novea.View.Admin;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Controls;
using System.Xml.Linq;

namespace Novea.ViewModel.Client
{   
    public class ProductDetailViewModel : BaseViewModel
    {
        private CTHD _Cthd;
        public CTHD Cthd { get => _Cthd; set { _Cthd = value; /*OnPropertyChanged();*/ } }
        private string _SL;
        public string SL { get => _SL; set { _SL = value; OnPropertyChanged(); } }
        private string _Trigia;
        public string Trigia { get => _Trigia; set { _Trigia = value; OnPropertyChanged(); } }
        public ICommand CloseProductDetailwdCommand { get; set; }
        public ICommand Loadwd { get; set; }
        public ICommand UpdateSLCommand { get; set; }
        public ProductDetailViewModel()
        {
            Loadwd = new RelayCommand<ProductDetail>((p) => true, (p) => _Loadwd(p));
            UpdateSLCommand = new RelayCommand<ProductDetail>((p) => true, (p) => _UpdateSLCommand(p));

            CloseProductDetailwdCommand = new RelayCommand<ProductDetail>((p) => true, (p) => CloseProductDetailwd(p));            
        }
        void _UpdateSLCommand(ProductDetail parameter)
        {
            SL = parameter.txbSL.Text;
            if(SL == "")
            {
                Trigia = "0";
            }
            else
            {
                Trigia = (Int32.Parse(SL) * Decimal.ToInt32(Const.SP_temp.DONGIA)).ToString();
            }
        }
        void _Loadwd(ProductDetail p)
        {
            //Cthd.SOHD = Const.HD.SOHD;
            //Cthd.MASP = Const.SP_temp.MASP;
            
            //DataProvider.Ins.DB.CUAHANGs.Add(temp);
            //DataProvider.Ins.DB.SaveChanges();
        }
        void CloseProductDetailwd(ProductDetail p)
        {
            p.Close();
        }
    }
}
