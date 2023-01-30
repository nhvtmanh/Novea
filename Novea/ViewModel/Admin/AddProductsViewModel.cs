using Novea.Model;
using Novea.View;
using Novea.ViewModel.Admin;
using Novea.View.Admin;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Imaging;

namespace Novea.ViewModel.Admin
{
    public class AddProductsViewModel : BaseViewModel
    {
        private string _localLink = System.Reflection.Assembly.GetExecutingAssembly().Location.Remove(System.Reflection.Assembly.GetExecutingAssembly().Location.IndexOf(@"bin\Debug"));
        public ICommand AddImage { get; set; }
        private string _linkimage;
        public string linkimage { get => _linkimage; set { _linkimage = value; OnPropertyChanged(); } }
        public ICommand AddProduct { get; set; }
        public ICommand Loadwd { get; set; }
        public ICommand Closewd { get; set; }
        public ICommand Minimizewd { get; set; }
        public AddProductsViewModel()
        {
            linkimage = "/Resources/Images/add.png";
            AddImage = new RelayCommand<Image>((p) => true, (p) => _AddImage(p));
            AddProduct = new RelayCommand<AddProducts>((p) => true, (p) => _AddProduct(p));
            Loadwd = new RelayCommand<AddProducts>((p) => true, (p) => _Loadwd(p));
            Closewd = new RelayCommand<AddProducts>((p) => true, (p) => Close(p));
            Minimizewd = new RelayCommand<AddProducts>((p) => true, (p) => Minimize(p));
        }
        void _Loadwd(AddProducts paramater)
        {
            linkimage = "/Resources/Images/add.png";
        }
        void _AddImage(Image img)
        {
            OpenFileDialog open = new OpenFileDialog();
            open.Filter = "Image Files(*.jpg; *.png)|*.jpg; *.png";
            if (open.ShowDialog() == true)
            {
                linkimage = open.FileName;
            };
            if (linkimage == "/Resources/Images/add.png")
            {
                Uri fileUri = new Uri(linkimage, UriKind.Relative);
                img.Source = new BitmapImage(fileUri);
            }
            else
            {
                Uri fileUri = new Uri(linkimage);
                img.Source = new BitmapImage(fileUri);
            }
        }
        bool check(string m)
        {
            foreach (SANPHAM temp in DataProvider.Ins.DB.SANPHAMs)
            {
                if (temp.MASP == m)
                    return true;
            }
            return false;
        }
        string _rdmaSP()
        {
            string maSP;
            do
            {
                Random rand = new Random();
                maSP = "SP" + rand.Next(0, 10000).ToString();
            } while (check(maSP));
            return maSP;
        }
        void _AddProduct(AddProducts paramater)
        {
            if (string.IsNullOrEmpty(paramater.MaSp.Text) || string.IsNullOrEmpty(paramater.TenSp.Text) || string.IsNullOrEmpty(paramater.LoaiSp.Text) || string.IsNullOrEmpty(paramater.GiaSp.Text) || string.IsNullOrEmpty(paramater.SizeSp.Text) || string.IsNullOrEmpty(paramater.DvSp.Text) || linkimage == "/Resource/Image/add.png")
            {
                MessageBox.Show("Bạn chưa nhập đủ thông tin.", "Thông Báo", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                MessageBoxResult h = System.Windows.MessageBox.Show("Bạn muốn thêm sản phẩm mới ?", "THÔNG BÁO", MessageBoxButton.YesNoCancel, MessageBoxImage.Question);
                if (h == MessageBoxResult.Yes)
                {
                    if (DataProvider.Ins.DB.SANPHAMs.Where(p => p.MASP == paramater.MaSp.Text).Count() > 0)
                    {
                        MessageBox.Show("Mã sản phẩm đã tồn tại.", "Thông Báo");
                    }
                    else
                    {
                        SANPHAM sanpham = new SANPHAM();
                        sanpham.MASP = paramater.MaSp.Text;
                        sanpham.TENSP = paramater.TenSp.Text;
                        try
                        {
                            sanpham.DONGIA = int.Parse(paramater.GiaSp.Text);
                        }
                        catch
                        {
                            MessageBox.Show("Giá sản phẩm không hợp lệ !", "THÔNG BÁO", MessageBoxButton.OK, MessageBoxImage.Error);
                            return;
                        }
                        if (int.Parse(paramater.GiaSp.Text) < 0)
                        {
                            MessageBox.Show("Giá sản phẩm không hợp lệ !", "THÔNG BÁO", MessageBoxButton.OK, MessageBoxImage.Error);
                            return;
                        }
                        sanpham.LOAISP = paramater.LoaiSp.Text;
                        sanpham.DONVI = paramater.DvSp.Text;                       
                        sanpham.SIZE = paramater.SizeSp.Text;
                        sanpham.MOTA = paramater.MotaSp.Text;
                        sanpham.MACH = Const.CH.MACH;
                        sanpham.AVAILABLE = true;
                        sanpham.HINHSP = "/Resources/ImgProduct/" + paramater.MaSp.Text + ((linkimage.Contains(".jpg")) ? ".jpg" : ".png").ToString();
                        DataProvider.Ins.DB.SANPHAMs.Add(sanpham);
                        DataProvider.Ins.DB.SaveChanges();
                        try
                        {
                            File.Copy(linkimage, _localLink + @"Resources\ImgProduct\" + paramater.MaSp.Text + ((linkimage.Contains(".jpg")) ? ".jpg" : ".png").ToString(), true);
                        }
                        catch { }
                        MessageBox.Show("Thêm sản phẩm mới thành công !", "THÔNG BÁO");
                        paramater.MaSp.Text = _rdmaSP();
                        paramater.TenSp.Clear();
                        paramater.LoaiSp.SelectedItem = null;
                        paramater.GiaSp.Clear();
                        paramater.DvSp.Clear();
                        paramater.SizeSp.SelectedItem = null;
                        Uri fileUri = new Uri(Const._localLink + "/Resources/Images/add.png");
                        paramater.HinhAnh.Source = new BitmapImage(fileUri);
                        paramater.MotaSp.Clear();
                    }
                }
            }
        }
        void Close(AddProducts p)
        {
            p.Close();
        }
        void Minimize(AddProducts p)
        {
            p.WindowState = WindowState.Minimized;
        }
    }
}
