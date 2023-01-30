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

namespace Novea.ViewModel.Admin
{
    public class DetailProductsViewModel : BaseViewModel
    {
        public ICommand Closewd { get; set; }
        public ICommand MoveWindow { get; set; }
        public ICommand UpdateProduct { get; set; }
        public ICommand ChangeAvailProduct { get; set; } 
        public ICommand GetMaSP { get; set; }
        private string MaSP_Now;
        public ICommand Loadwd { get; set; }
        public ICommand DeleteProduct { get; set; }
        private string _linkaddimage;
        public string linkaddimage { get => _linkaddimage; set { _linkaddimage = value; OnPropertyChanged(); } }
        //private SANPHAM _Product;
        //public SANPHAM Product { get => _Product; set { _Product = value; OnPropertyChanged(); } }
        private bool _IsChecked;
        public bool IsChecked { get { return _IsChecked; } set { _IsChecked = value; OnPropertyChanged(); } }
        public DetailProductsViewModel()
        {
            Closewd = new RelayCommand<DetailProducts>((p) => true, (p) => Close(p));
            MoveWindow = new RelayCommand<DetailProducts>((p) => true, (p) => moveWindow(p));
            GetMaSP = new RelayCommand<DetailProducts>((p) => true, (p) => _GetMaSP(p));
            UpdateProduct = new RelayCommand<DetailProducts>((p) => true, (p) => _UpdateProduct(p));
            Loadwd = new RelayCommand<DetailProducts>((p) => true, (p) => _Loadwd(p));
            DeleteProduct = new RelayCommand<DetailProducts>((p) => true, (p) => _DeleteProduct(p));
            ChangeAvailProduct = new RelayCommand<DetailProducts>((p) => true, (p) => _ChangeAvailProduct(p));
        }
        void _Loadwd(DetailProducts paramater)
        {
            GetMaSP = new RelayCommand<DetailProducts>((p) => true, (p) => _GetMaSP(p));
            //Product = DataProvider.Ins.DB.SANPHAMs.Where(p => p.MASP == MaSP_Now).FirstOrDefault();
            //linkaddimage = Product.HINHSP;
            IsChecked = true;
            paramater.TenSP.IsEnabled = true;
            paramater.Mota.IsEnabled = true;
            paramater.GiaSP.IsEnabled = true;
            paramater.LoaiSP.IsEnabled = true;
            paramater.DVT.IsEnabled = true;
            paramater.Size.IsEnabled = true;
        }
        void _ChangeAvailProduct(DetailProducts parameter)
        {
            
            if(parameter.Avail.IsChecked.Value == true)
            {
                foreach (SANPHAM sp in DataProvider.Ins.DB.SANPHAMs.Where(pa => (pa.MASP == MaSP_Now)))
                {
                    sp.AVAILABLE = true;
                }
                DataProvider.Ins.DB.SaveChanges();
            }
            else
            {
                foreach (SANPHAM sp in DataProvider.Ins.DB.SANPHAMs.Where(pa => (pa.MASP == MaSP_Now)))
                {
                    sp.AVAILABLE = false;
                }
                DataProvider.Ins.DB.SaveChanges();
            }
        }
        void _DeleteProduct(DetailProducts parameter)
        {
            MessageBoxResult h = System.Windows.MessageBox.Show("Bạn muốn xóa sản phẩm ?", "THÔNG BÁO", MessageBoxButton.YesNoCancel, MessageBoxImage.Question);
            if (h == MessageBoxResult.Yes)
            {
                var itemToRemove = DataProvider.Ins.DB.SANPHAMs.SingleOrDefault(pa => (pa.MASP == MaSP_Now));
                if (itemToRemove != null)
                {
                    DataProvider.Ins.DB.SANPHAMs.Remove(itemToRemove);
                    DataProvider.Ins.DB.SaveChanges();
                    MessageBox.Show("Xóa sản phẩm thành công !", "THÔNG BÁO");
                }                              
            }
        }
        void moveWindow(DetailProducts p)
        {
            p.DragMove();
        }
        void Close(DetailProducts p)
        {
            p.Close();
        }
        void _GetMaSP(DetailProducts p)
        {
            MaSP_Now = p.MaSP.Text;
        }
        void _UpdateProduct(DetailProducts p)
        {
            MessageBoxResult h = System.Windows.MessageBox.Show("Bạn muốn cập nhật sản phẩm ?", "THÔNG BÁO", MessageBoxButton.YesNoCancel, MessageBoxImage.Question);
            if (h == MessageBoxResult.Yes)
            {
                if (string.IsNullOrEmpty(p.TenSP.Text) || string.IsNullOrEmpty(p.Mota.Text) )
                {
                    MessageBox.Show("Thông tin chưa đầy đủ !", "THÔNG BÁO");
                }
                else
                {
                    foreach (SANPHAM a in DataProvider.Ins.DB.SANPHAMs.Where(pa => (pa.MASP == MaSP_Now && pa.AVAILABLE != false)))
                    {
                        a.TENSP = p.TenSP.Text;
                        a.MOTA = p.Mota.Text;
                    }
                    DataProvider.Ins.DB.SaveChanges();
                    MessageBox.Show("Cập nhật sản phẩm thành công !", "THÔNG BÁO");
                }
            }
        }
    }
}
