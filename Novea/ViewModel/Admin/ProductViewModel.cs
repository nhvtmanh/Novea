﻿using Novea.Model;
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
using System.Windows.Media.Imaging;

namespace Novea.ViewModel.Admin
{
    public class ProductViewModel : BaseViewModel
    {
        private string _localLink = System.Reflection.Assembly.GetExecutingAssembly().Location.Remove(System.Reflection.Assembly.GetExecutingAssembly().Location.IndexOf(@"bin\Debug"));
        private ObservableCollection<SANPHAM> _listSP;
        public ObservableCollection<SANPHAM> listSP { get => _listSP; set { _listSP = value;} }
        private ObservableCollection<SANPHAM> _listSP_temp;
        public ObservableCollection<SANPHAM> listSP_temp { get => _listSP_temp; set { _listSP_temp = value;} }
        public ICommand SearchCommand { get; set; }
        public ICommand DetailPdCommand { get; set; }
        public ICommand AddPdPdCommand { get; set; }
        public ICommand LoadCsCommand { get; set; }
        public ICommand Filter { get; set; }
        public ICommand Orderby { get; set; }
        private ObservableCollection<string> _listTK;
        public ObservableCollection<string> listTK { get => _listTK; set { _listTK = value; OnPropertyChanged(); } }
        
        public ProductViewModel()
        {
            listTK = new ObservableCollection<string>() { "Không sắp xếp", "Giá tăng dần", "Giá giảm dần" };
            listSP_temp = new ObservableCollection<SANPHAM>(DataProvider.Ins.DB.SANPHAMs.Where(p => p.MACH == Const.CH.MACH));
            listSP = new ObservableCollection<SANPHAM>(listSP_temp.GroupBy(p => p.TENSP).Select(grp => grp.FirstOrDefault()));
            AddPdPdCommand = new RelayCommand<Product>((p) => { return p == null ? false : true; }, (p) => _AddPdCommand(p));
            SearchCommand = new RelayCommand<Product>((p) => { return p == null ? false : true; }, (p) => _SearchCommand(p));
            DetailPdCommand = new RelayCommand<Product>((p) => { return p.ListViewProduct.SelectedItem == null ? false : true; }, (p) => _DetailPd(p));
            LoadCsCommand = new RelayCommand<Product>((p) => true, (p) => _LoadCsCommand(p));
            Filter = new RelayCommand<Product>((p) => true, (p) => _Filter(p));
            Orderby = new RelayCommand<Product>((p) => true, (p) => _Orderby(p));
        }
        public void _LoadCsCommand(Product parameter)
        {
            listTK = new ObservableCollection<string>() {"Không sắp xếp", "Giá tăng dần", "Giá giảm dần" };
            listSP_temp = new ObservableCollection<SANPHAM>(DataProvider.Ins.DB.SANPHAMs.Where(p => p.MACH == Const.CH.MACH));
            listSP = new ObservableCollection<SANPHAM>(listSP_temp.GroupBy(p => p.TENSP).Select(grp => grp.FirstOrDefault()));
            parameter.cbxChon2.SelectedIndex = 0;
            parameter.cbxChon1.SelectedIndex = 0;
            _Filter(parameter);
            _SearchCommand(parameter);
            _Orderby(parameter);
        }
        public void _Filter(Product parameter)
        {
            switch (parameter.cbxChon1.SelectedIndex.ToString())
            {
                case "0":
                    {
                        listSP = new ObservableCollection<SANPHAM>(listSP_temp.GroupBy(p => p.TENSP).Select(grp => grp.FirstOrDefault()));
                        parameter.ListViewProduct.ItemsSource = listSP;
                        break;
                    }
                case "1":
                    {
                        listSP = new ObservableCollection<SANPHAM>(listSP_temp.GroupBy(p => p.TENSP).Select(grp => grp.FirstOrDefault()).Where(p => p.LOAISP == "Cà phê"));
                        parameter.ListViewProduct.ItemsSource = listSP;
                        break;
                    }
                case "2":
                    {
                        listSP = new ObservableCollection<SANPHAM>(listSP_temp.GroupBy(p => p.TENSP).Select(grp => grp.FirstOrDefault()).Where(p => p.LOAISP == "Trà"));
                        parameter.ListViewProduct.ItemsSource = listSP;
                        break;
                    }
                case "3":
                    {
                        listSP = new ObservableCollection<SANPHAM>(listSP_temp.GroupBy(p => p.TENSP).Select(grp => grp.FirstOrDefault()).Where(p => p.LOAISP == "Trà sữa"));
                        parameter.ListViewProduct.ItemsSource = listSP;
                        break;
                    }
                case "4":
                    {
                        listSP = new ObservableCollection<SANPHAM>(listSP_temp.GroupBy(p => p.TENSP).Select(grp => grp.FirstOrDefault()).Where(p => p.LOAISP == "Đá xay"));
                        parameter.ListViewProduct.ItemsSource = listSP;
                        break;
                    }
                case "5":
                    {
                        listSP = new ObservableCollection<SANPHAM>(listSP_temp.GroupBy(p => p.TENSP).Select(grp => grp.FirstOrDefault()).Where(p => p.LOAISP == "Sinh tố"));
                        parameter.ListViewProduct.ItemsSource = listSP;
                        break;
                    }
                case "6":
                    {
                        listSP = new ObservableCollection<SANPHAM>(listSP_temp.GroupBy(p => p.TENSP).Select(grp => grp.FirstOrDefault()).Where(p => p.LOAISP == "Sữa chua"));
                        parameter.ListViewProduct.ItemsSource = listSP;
                        break;
                    }
                case "7":
                    {
                        listSP = new ObservableCollection<SANPHAM>(listSP_temp.GroupBy(p => p.TENSP).Select(grp => grp.FirstOrDefault()).Where(p => p.LOAISP == "Nước ép"));
                        parameter.ListViewProduct.ItemsSource = listSP;
                        break;
                    }
                case "8":
                    {
                        listSP = new ObservableCollection<SANPHAM>(listSP_temp.GroupBy(p => p.TENSP).Select(grp => grp.FirstOrDefault()).Where(p => p.LOAISP == "Nước ngọt"));
                        parameter.ListViewProduct.ItemsSource = listSP;
                        break;
                    }
            }

        }
        public void _Orderby(Product parameter)
        {
            switch (parameter.cbxChon2.SelectedIndex.ToString())
            {
                case "0":
                    {
                        listSP = new ObservableCollection<SANPHAM>(listSP_temp.GroupBy(p => p.TENSP).Select(grp => grp.FirstOrDefault()));
                        parameter.ListViewProduct.ItemsSource = listSP;
                        break;
                    }
                case "1":
                    {
                        listSP = new ObservableCollection<SANPHAM>(listSP_temp.GroupBy(p => p.TENSP).Select(grp => grp.FirstOrDefault()).OrderBy(p => p.DONGIA));
                        parameter.ListViewProduct.ItemsSource = listSP;
                        break;
                    }
                case "2":
                    {
                        listSP = new ObservableCollection<SANPHAM>(listSP_temp.GroupBy(p => p.TENSP).Select(grp => grp.FirstOrDefault()).OrderByDescending(p => p.DONGIA));
                        parameter.ListViewProduct.ItemsSource = listSP;
                        break;
                    }
            }
        }
        public void _SearchCommand(Product paramater)
        {
            ObservableCollection<SANPHAM> temp = new ObservableCollection<SANPHAM>();
            foreach (SANPHAM s in listSP)
            {
                if (paramater.txbSearch.Text != "")
                {
                    if (s.TENSP.ToLower().Contains(paramater.txbSearch.Text.ToLower()))
                    {
                        temp.Add(s);
                    }
                    paramater.ListViewProduct.ItemsSource = temp;
                }
                else
                    paramater.ListViewProduct.ItemsSource = listSP;
            }
        }
        public void _DetailPd(Product paramater)
        {
            DetailProducts detailProduct = new DetailProducts();
            SANPHAM temp = (SANPHAM)paramater.ListViewProduct.SelectedItem;
            detailProduct.MaSP.Text = temp.MASP;
            detailProduct.TenSP.Text = temp.TENSP;
            detailProduct.GiaSP.Text = string.Format("{0:0,0}", temp.DONGIA) + " VNĐ";
            detailProduct.LoaiSP.Text = temp.LOAISP;
            detailProduct.DVT.Text = temp.DONVI;
            detailProduct.Size.Text = temp.SIZE;
            detailProduct.Mota.Text = temp.MOTA;
            detailProduct.ShowDialog();
            listSP_temp = new ObservableCollection<SANPHAM>(DataProvider.Ins.DB.SANPHAMs.Where(p => p.MACH == Const.CH.MACH));
            paramater.ListViewProduct.SelectedItem = null;
            _Filter(paramater);
            _SearchCommand(paramater);
            _Orderby(paramater);
        }
        public bool check(string m)
        {
            foreach (SANPHAM temp in DataProvider.Ins.DB.SANPHAMs)
            {
                if (temp.MASP == m)
                    return true;
            }
            return false;
        }
        public string rdmaSP()
        {
            string maSP;
            do
            {
                Random rand = new Random();
                maSP = "SP" + rand.Next(0, 10000).ToString();
            } while (check(maSP));
            return maSP;
        }
        public void _AddPdCommand(Product paramater)
        {
            AddProducts addProductView = new AddProducts();
            addProductView.MaSp.Text = rdmaSP();
            addProductView.ShowDialog();
            listSP_temp = new ObservableCollection<SANPHAM>(DataProvider.Ins.DB.SANPHAMs.Where(p => p.MACH == Const.CH.MACH));
            _Filter(paramater);
            _SearchCommand(paramater);
        }
    }
}

