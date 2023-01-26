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
        private ObservableCollection<SANPHAM> _listProduct;
        public ObservableCollection<SANPHAM> listProduct { get => _listProduct; set { _listProduct = value; /*OnPropertyChanged();*/ } }
        private ObservableCollection<SANPHAM> _listProduct_temp;
        public ObservableCollection<SANPHAM> listProduct_temp { get => _listProduct_temp; set { _listProduct_temp = value; /*OnPropertyChanged();*/ } }
        public ICommand DetailPdCommand { get; set; }
        public ICommand LoadCsCommand { get; set; }
        public ICommand BackToHomeCommand { get; set; }
        public StoreDetailViewModel()
        {
            listProduct_temp = new ObservableCollection<SANPHAM>(DataProvider.Ins.DB.SANPHAMs.Where(p => p.MACH == Const.CH.MACH));
            listProduct = new ObservableCollection<SANPHAM>(listProduct_temp.GroupBy(p => p.TENSP).Select(grp => grp.FirstOrDefault()));
            DetailPdCommand = new RelayCommand<StoreDetail>((p) => { return p.ListViewProduct.SelectedItem == null ? false : true; }, (p) => _DetailPd(p));
            LoadCsCommand = new RelayCommand<StoreDetail>((p) => true, (p) => _LoadCsCommand(p));
            BackToHomeCommand = new RelayCommand<StoreDetail>((p) => true, (p) => backhome(p));
        }
        void backhome(StoreDetail p)
        {
            Home home = new Home();
            Guest.Instance.Main.NavigationService.Navigate(home);
        }
        public void _LoadCsCommand(StoreDetail parameter)
        {
            listProduct_temp = new ObservableCollection<SANPHAM>(DataProvider.Ins.DB.SANPHAMs.Where(p => p.MACH == Const.CH.MACH));
            listProduct = new ObservableCollection<SANPHAM>(listProduct_temp.GroupBy(p => p.TENSP).Select(grp => grp.FirstOrDefault()));
            parameter.tbTENCH.Text = Const.CH.TENCH;
            parameter.tbDIADIEM.Text = Const.CH.DIADIEM;
        }
        
        public void _DetailPd(StoreDetail paramater)
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
