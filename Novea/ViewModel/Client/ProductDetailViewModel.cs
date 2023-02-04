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
using System.Windows.Media.Imaging;
using System.IO;

namespace Novea.ViewModel.Client
{   
    public class ProductDetailViewModel : BaseViewModel
    {
        private HOADON _HoaDon;
        public HOADON HoaDon { get => _HoaDon; set { _HoaDon = value; OnPropertyChanged(); } }
        private CTHD _Cthd;
        public CTHD Cthd { get => _Cthd; set { _Cthd = value; OnPropertyChanged(); } }
        private string _SL;
        public string SL { get => _SL; set { _SL = value; OnPropertyChanged(); } }
        private string _Trigia;
        public string Trigia { get => _Trigia; set { _Trigia = value; OnPropertyChanged(); } }
        private BitmapImage image;
        public BitmapImage Image { get => image; set { image = value; OnPropertyChanged(); } }  
        public ICommand CloseProductDetailwdCommand { get; set; }
        public ICommand Loadwd { get; set; }
        public ICommand UpdateSLCommand { get; set; }
        public ICommand AddToCartCommand { get; set; }
        public ProductDetailViewModel()
        {
            Loadwd = new RelayCommand<ProductDetail>((p) => true, (p) => _Loadwd(p));
            UpdateSLCommand = new RelayCommand<ProductDetail>((p) => true, (p) => _UpdateSLCommand(p));
            AddToCartCommand = new RelayCommand<ProductDetail>((p) => true, (p) => _AddToCartCommand(p));
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
        void _Loadwd(ProductDetail parameter)
        {
            byte[] imageData = Const.SP_temp.HINHSP;
            BitmapImage bitmapImage = new BitmapImage();
            bitmapImage.BeginInit();
            bitmapImage.StreamSource = new MemoryStream(imageData);
            bitmapImage.EndInit();
            Image = bitmapImage;
            HoaDon = DataProvider.Ins.DB.HOADONs.Where(p => p.SOHD == parameter.txbSOHD.Text).FirstOrDefault();
            Const.HD = HoaDon;

            CTHD CT = new CTHD();
            CT.SOHD = parameter.txbSOHD.Text;
            CT.MASP = Const.SP_temp.MASP;
            CT.SOLUONG = 0;
            CT.TRIGIA = 0;
            CT.LuongDa = "0";
            CT.LuongDuong = "0";

            Cthd = CT;

            parameter.txbSL.Text = "1";
            _UpdateSLCommand(parameter); 
        }
        void _AddToCartCommand(ProductDetail parameter)
        {
            CTHD Cthd_temp = new CTHD();
            Cthd_temp = Cthd;
            Cthd_temp.SOLUONG = Int32.Parse(parameter.txbSL.Text);
            Cthd_temp.LuongDa = parameter.cbbLuongDa.Text;
            Cthd_temp.LuongDuong = parameter.cbbLuongDuong.Text;

            DataProvider.Ins.DB.CTHDs.Add(Cthd_temp);
            DataProvider.Ins.DB.SaveChanges();

            parameter.Close();
        }
        void CloseProductDetailwd(ProductDetail p)
        {
            p.Close();
        }
    }
}
