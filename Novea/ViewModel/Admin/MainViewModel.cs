﻿using Novea.Model;
using Novea.View;
using Novea.View.Admin;
using Novea.View.Login;
using Novea.ViewModel.Login;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using System.Xml.Linq;

namespace Novea.ViewModel.Admin
{

    public class MainViewModel : BaseViewModel
    {
        private ObservableCollection<HOADON> _listHD;
        public ObservableCollection<HOADON> listHD { get => _listHD; set { SetProperty(ref _listHD, value); } }
        public ICommand CloseLG { get; set; }        
        public ICommand Closewd { get; set; }
        public ICommand Minimizewd { get; set; }
        public ICommand GetIdTab { get; set; }
        public ICommand SwitchTab { get; set; }
        public ICommand LogOutCommand { get; set; }
        public ICommand Loadwd { get; set; }
        public ICommand MoveWindow { get; set; }
        public ICommand TenDangNhap_Loaded { get; set; }
        public ICommand Refresh { get; set; }


        private CUAHANG _User;
        public CUAHANG User { get => _User; set { _User = value; OnPropertyChanged(); } }

        private BitmapImage _Ava;
        public BitmapImage Ava { get => _Ava; set { _Ava = value; OnPropertyChanged(); } }

        public string Name;

        public MainViewModel()
        {
            Refresh = new RelayCommand<MainWindow>((p) => true, (p) => Refreshwd(p));
            Closewd = new RelayCommand<MainWindow>((p) => true, (p) => Close());
            Minimizewd = new RelayCommand<MainWindow>((p) => true, (p) => Minimize(p));
            GetIdTab = new RelayCommand<RadioButton>((p) => true, (p) => Name = p.Uid);
            SwitchTab = new RelayCommand<MainWindow>((p) => true, (p) => switchtab(p));
            LogOutCommand = new RelayCommand<MainWindow>((p) => { return true; }, (p) => LogOut(p));
            Loadwd = new RelayCommand<MainWindow>((p) => true, (p) => _Loadwd(p));
            MoveWindow = new RelayCommand<MainWindow>((p) => true, (p) => moveWindow(p));
            TenDangNhap_Loaded = new RelayCommand<MainWindow>((p) => true, (p) => LoadTenAD(p));
        }
        void LoadData()
        {
            DataProvider.Ins.Refresh();
            listHD = new ObservableCollection<HOADON>(DataProvider.Ins.DB.HOADONs.Where(hd => hd.MACH == Const.CH.MACH && hd.FINISHORDERCLIENT == true && hd.DONE == false));
            listHD.CollectionChanged += (sender, args) =>
            {
                MessageBox.Show("Bạn vừa nhận được một đơn hàng mới!");
                MessageBox.Show("hello");
            };
        }
        void Refreshwd(MainWindow p)
        {
            DataProvider.Ins.Refresh();
            MessageBox.Show("Làm mới dữ liệu thành công!");
        }
        void LogOut(MainWindow p)
        {
            MainLogin login = new MainLogin();
            login.Show();
            p.Close();
        }

        void switchtab(MainWindow p)
        {
            int index = int.Parse(Name);
            switch (index)
            {
                case 0:
                    {
                        _Loadwd(p);
                        p.Main.NavigationService.Navigate(new View.Admin.HomeView());
                        break;
                    }
                case 1:
                    {
                        _Loadwd(p);
                        p.Main.NavigationService.Navigate(new View.Admin.OrdersView());
                        break;
                    }
                case 2:
                    {
                        _Loadwd(p);
                        p.Main.NavigationService.Navigate(new View.Admin.Product());
                        break;
                    }
                case 3:
                    {
                        _Loadwd(p);
                        p.Main.NavigationService.Navigate(new View.Admin.CustomerView());
                        break;
                    }
                case 4:
                    {
                        _Loadwd(p);
                        p.Main.NavigationService.Navigate(new View.Admin.ManagerView());
                        break;
                    }
                case 5:
                    {
                        _Loadwd(p);
                        p.Main.NavigationService.Navigate(new View.Admin.SettingView());
                        break;
                    }
                default:
                    {
                        break;
                    }
            }
        }
        public void Close()
        {
            System.Windows.Application.Current.Shutdown();
        }
        public void Minimize(MainWindow p)
        {
            p.WindowState = WindowState.Minimized;
        }
        void _Loadwd(MainWindow p)
        {            
            if (Const.IsLogin)
            {
                string a = Const.TenDangNhap;
                User = DataProvider.Ins.DB.CUAHANGs.Where(x => x.TAIKHOAN == a).FirstOrDefault();
                Const.CH = User;
                Const.MACH = User.MACH;
                byte[] imageData = Const.CH.AVATAR;
                BitmapImage bitmapImage = new BitmapImage();
                bitmapImage.BeginInit();
                bitmapImage.StreamSource = new MemoryStream(imageData);
                bitmapImage.EndInit();
                Ava = bitmapImage;
                LoadTenAD(p);
            }
            LoadData();
        }
        public void LoadTenAD(MainWindow p)
        {
            p.TenDangNhap.Text = string.Join(" ", User.TENCH.Split().Reverse().Take(2).Reverse());
        }
        public void moveWindow(MainWindow p)
        {
            p.DragMove();
        }
    }
}
