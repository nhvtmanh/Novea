using Novea.Model;
using Novea.View;
using Novea.View.Client;
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
using System.Windows.Media.Imaging;

namespace Novea.ViewModel.Client
{
    public class StoreDetailViewModel : BaseViewModel
    {
        private string _localLink = System.Reflection.Assembly.GetExecutingAssembly().Location.Remove(System.Reflection.Assembly.GetExecutingAssembly().Location.IndexOf(@"bin\Debug"));
        private ObservableCollection<SANPHAM> listProduct;
        public ObservableCollection<SANPHAM> ListProduct { get => listProduct; set { listProduct = value; /*OnPropertyChanged();*/ } }
        private ObservableCollection<SANPHAM> listProductTemp;
        public ObservableCollection<SANPHAM> ListProductTemp { get => listProductTemp; set { listProductTemp = value; /*OnPropertyChanged();*/ } }
        public ICommand DetailPdCommand { get; set; }
        public ICommand LoadDetailStoreCommand { get; set; }
        public StoreDetailViewModel()
        {
            ListProductTemp = new ObservableCollection<SANPHAM>(DataProvider.Ins.DB.SANPHAMs.Where(p => p.MACH == Const.CH.MACH));
            ListProduct = new ObservableCollection<SANPHAM>(ListProductTemp.GroupBy(p => p.TENSP).Select(grp => grp.FirstOrDefault()));
            DetailPdCommand = new RelayCommand<StoreDetail>((p) => { return p.ListViewProduct.SelectedItem == null ? false : true; }, (p) => DisplayDetailProduct(p));
            LoadDetailStoreCommand = new RelayCommand<StoreDetail>((p) => true, (p) => LoadDetailStore(p));
        }
        public void LoadDetailStore(StoreDetail parameter)
        {
            ListProductTemp = new ObservableCollection<SANPHAM>(DataProvider.Ins.DB.SANPHAMs.Where(p => p.MACH == Const.CH.MACH));
            ListProduct = new ObservableCollection<SANPHAM>(ListProductTemp.GroupBy(p => p.TENSP).Select(grp => grp.FirstOrDefault()));
        }
        
        public void DisplayDetailProduct(StoreDetail paramater)
        {
            //DetailProducts detailProduct = new DetailProducts();
            //SANPHAM temp = (SANPHAM)paramater.ListViewProduct.SelectedItem;
            //detailProduct.MaSP.Text = temp.MASP;
            //detailProduct.TenSP.Text = temp.TENSP;
            //detailProduct.GiaSP.Text = string.Format("{0:0,0}", temp.DONGIA) + " VNĐ";
            //detailProduct.LoaiSP.Text = temp.LOAISP;
            //detailProduct.DVT.Text = temp.DONVI;
            //detailProduct.Size.Text = temp.SIZE;
            //detailProduct.Mota.Text = temp.MOTA;
            //try
            //{
            //    Uri fileUri = new Uri(temp.HINHSP);
            //    detailProduct.HinhAnh.ImageSource = new BitmapImage(fileUri);
            //}
            //catch { }
            //detailProduct.ShowDialog();
            //listSP_temp = new ObservableCollection<SANPHAM>(DataProvider.Ins.DB.SANPHAMs.Where(p => p.MACH == Const.CH.MACH));
            //paramater.ListViewProduct.SelectedItem = null;
        }
    }
}
