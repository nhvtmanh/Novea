using Microsoft.Win32;
using Novea.Model;
using Novea.View;
using Novea.View.Admin;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Novea.ViewModel.Admin
{
    public class DetailProductsViewModel : BaseViewModel
    {
        public ICommand Closewd { get; set; }
        public ICommand MoveWindow { get; set; }
        public ICommand UpdateProduct { get; set; }
        public ICommand SetAvailProduct { get; set; }
        public ICommand SetUnAvailProduct { get; set; }
        public ICommand GetMaSP { get; set; }
        private string MaSP_Now;
        public ICommand Loadwd { get; set; }
        public ICommand DeleteProduct { get; set; }
        public ICommand UpdateImageCommand { get; set; }
        private byte[] imageData;
        private BitmapImage selectedImage;
        public BitmapImage SelectedImage
        {
            get { return selectedImage; }
            set { selectedImage = value; OnPropertyChanged(); }
        }
        private string _Available;
        public string Available { get { return _Available; } set { _Available = value; OnPropertyChanged(); } }
        public DetailProductsViewModel()
        {
            Closewd = new RelayCommand<DetailProducts>((p) => true, (p) => Close(p));
            MoveWindow = new RelayCommand<DetailProducts>((p) => true, (p) => moveWindow(p));
            GetMaSP = new RelayCommand<DetailProducts>((p) => true, (p) => _GetMaSP(p));
            UpdateProduct = new RelayCommand<DetailProducts>((p) => true, (p) => _UpdateProduct(p));
            Loadwd = new RelayCommand<DetailProducts>((p) => true, (p) => _Loadwd(p));
            DeleteProduct = new RelayCommand<DetailProducts>((p) => true, (p) => _DeleteProduct(p));
            SetAvailProduct = new RelayCommand<DetailProducts>((p) => true, (p) => _SetAvailProduct(p));
            SetUnAvailProduct = new RelayCommand<DetailProducts>((p) => true, (p) => _SetUnAvailProduct(p));
            UpdateImageCommand = new RelayCommand<DetailProducts>((p) => true, (p) => UpdateImage(p));
        }

        void UpdateImage(DetailProducts p)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Image Files (*.jpg; *.jpeg; *.png)|*.jpg; *.jpeg; *.png";
            if (openFileDialog.ShowDialog() == true)
            {
                //SelectedImage = new BitmapImage(new Uri(openFileDialog.FileName));
                MemoryStream memoryStream = new MemoryStream();
                using (FileStream fileStream = new FileStream(openFileDialog.FileName, FileMode.Open, FileAccess.Read))
                {
                    fileStream.CopyTo(memoryStream);
                }
                imageData = memoryStream.ToArray();
                BitmapImage bitmapImage = new BitmapImage();
                bitmapImage.BeginInit();
                bitmapImage.StreamSource = new MemoryStream(imageData);
                bitmapImage.EndInit();
                p.HinhAnh.ImageSource = bitmapImage;
            }
        }

        void _Loadwd(DetailProducts paramater)
        {
            SANPHAM temp = DataProvider.Ins.DB.SANPHAMs.Where(p => p.MASP == MaSP_Now).FirstOrDefault();
            imageData = temp.HINHSP;
            BitmapImage bitmapImage = new BitmapImage();
            bitmapImage.BeginInit();
            bitmapImage.StreamSource = new MemoryStream(imageData);
            bitmapImage.EndInit();
            paramater.HinhAnh.ImageSource = bitmapImage;
            paramater.TenSP.IsEnabled = true;
            paramater.Mota.IsEnabled = true;
            paramater.GiaSP.IsEnabled = false;
            paramater.LoaiSP.IsEnabled = false;
            paramater.DVT.IsEnabled = false;
            paramater.Size.IsEnabled = false;
            if(temp.AVAILABLE == false)
            {
                paramater.txbAvail.Text = "UNAVAILABLE";
            }
            else
            {
                paramater.txbAvail.Text = "AVAILABLE";
            }
        }
        void _SetAvailProduct(DetailProducts parameter)
        {
            var uRow = DataProvider.Ins.DB.SANPHAMs.Where(p => p.MASP == MaSP_Now).FirstOrDefault();
            uRow.AVAILABLE = true;
            DataProvider.Ins.DB.SaveChanges();

            parameter.txbAvail.Text = "AVAILABLE";
        }
        void _SetUnAvailProduct(DetailProducts parameter)
        {
            var uRow = DataProvider.Ins.DB.SANPHAMs.Where(p => p.MASP == MaSP_Now).FirstOrDefault();
            uRow.AVAILABLE = false;
            DataProvider.Ins.DB.SaveChanges();

            parameter.txbAvail.Text = "UNAVAILABLE";
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
            parameter.Close();
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
                if (string.IsNullOrEmpty(p.TenSP.Text))
                {
                    MessageBox.Show("Thông tin chưa đầy đủ !", "THÔNG BÁO");
                }
                else
                {
                    foreach (SANPHAM a in DataProvider.Ins.DB.SANPHAMs.Where(pa => (pa.MASP == MaSP_Now)))
                    {
                        a.TENSP = p.TenSP.Text;
                        a.MOTA = p.Mota.Text;
                        a.HINHSP = imageData;
                    }
                    DataProvider.Ins.DB.SaveChanges();
                    MessageBox.Show("Cập nhật sản phẩm thành công !", "THÔNG BÁO");
                }
            }
            p.Close();
        }
    }
}
