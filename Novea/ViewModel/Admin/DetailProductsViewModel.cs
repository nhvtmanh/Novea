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
        //public ICommand Closewd { get; set; }
        //public ICommand Minimizewd { get; set; }
        //public ICommand MoveWindow { get; set; }
        //public ICommand UpdateProduct { get; set; }
        //public ICommand GetName { get; set; }
        //private string TenSP1;
        //public ICommand Loadwd { get; set; }
        //public ICommand DeleteProduct { get; set; }
        //public DetailProductsViewModel()
        //{
        //    Closewd = new RelayCommand<DetailProducts>((p) => true, (p) => Close(p));
        //    Minimizewd = new RelayCommand<DetailProducts>((p) => true, (p) => Minimize(p));
        //    MoveWindow = new RelayCommand<DetailProducts>((p) => true, (p) => moveWindow(p));
        //    GetName = new RelayCommand<DetailProducts>((p) => true, (p) => _GetName(p));
        //    UpdateProduct = new RelayCommand<DetailProducts>((p) => true, (p) => _UpdateProduct(p));
        //    Loadwd = new RelayCommand<DetailProducts>((p) => true, (p) => _Loadwd(p));
        //    DeleteProduct = new RelayCommand<DetailProducts>((p) => true, (p) => _DeleteProduct(p));
        //}
        //void _Loadwd(DetailProducts parmater)
        //{
        //    if (Const.Admin)
        //    {
        //        parmater.TenSP.IsEnabled = true;
        //        parmater.Mota.IsEnabled = true;
        //        parmater.btncapnhat.Visibility = Visibility.Visible;
        //        parmater.btnxoa.Visibility = Visibility.Visible;
        //    }
        //    else
        //    {
        //        parmater.TenSP.IsEnabled = false;
        //        parmater.Mota.IsEnabled = false;
        //        parmater.Mota.Height = 200;
        //    }
        //}
        //void _DeleteProduct(DetailProducts parameter)
        //{
        //    MessageBoxResult h = System.Windows.MessageBox.Show("Bạn muốn xóa sản phẩm ?", "THÔNG BÁO", MessageBoxButton.YesNoCancel, MessageBoxImage.Question);
        //    if (h == MessageBoxResult.Yes)
        //    {
        //        foreach (SANPHAM a in DataProvider.Ins.DB.SANPHAMs.Where(pa => (pa.TENSP == TenSP1 && pa.SL >= 0)))
        //        {
        //            a.SL = -1;
        //        }
        //        DataProvider.Ins.DB.SaveChanges();
        //        MessageBox.Show("Xóa sản phẩm thành công !", "THÔNG BÁO");
        //    }
        //}
        //void moveWindow(DetailProducts p)
        //{
        //    p.DragMove();
        //}
        //void Close(DetailProducts p)
        //{
        //    p.Close();
        //}
        //void Minimize(DetailProducts p)
        //{
        //    p.WindowState = WindowState.Minimized;
        //}
        //void _GetName(DetailProducts p)
        //{
        //    TenSP1 = p.TenSP.Text;
        //}
        //void _UpdateProduct(DetailProducts p)
        //{
        //    MessageBoxResult h = System.Windows.MessageBox.Show("Bạn muốn cập nhật sản phẩm ?", "THÔNG BÁO", MessageBoxButton.YesNoCancel, MessageBoxImage.Question);
        //    if (h == MessageBoxResult.Yes)
        //    {
        //        if (string.IsNullOrEmpty(p.TenSP.Text) || string.IsNullOrEmpty(p.Mota.Text) || string.IsNullOrEmpty(p.Mota.Text))
        //        {
        //            MessageBox.Show("Thông tin chưa đầy đủ !", "THÔNG BÁO");
        //        }
        //        else
        //        {
        //            foreach (SANPHAM a in DataProvider.Ins.DB.SANPHAMs.Where(pa => (pa.TENSP == TenSP1 && pa.SL >= 0)))
        //            {
        //                a.TENSP = p.TenSP.Text;
        //                a.MOTA = p.Mota.Text;
        //                a.MOTA = p.Mota.Text;
        //            }
        //            DataProvider.Ins.DB.SaveChanges();
        //            MessageBox.Show("Cập nhật sản phẩm thành công !", "THÔNG BÁO");
        //        }
        //    }
        //}
    }
}
