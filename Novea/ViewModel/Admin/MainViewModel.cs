using Novea.Model;
using Novea.View;
using Novea.View.Admin;
using Novea.View.Login;
using Novea.ViewModel.Login;
using System;
using System.Collections.Generic;
using System.Data.Entity;
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
        public ICommand CloseLG { get; set; }
        
        public ICommand Closewd { get; set; }
        public ICommand Minimizewd { get; set; }
        public ICommand GetIdTab { get; set; }
        public ICommand SwitchTab { get; set; }
        public ICommand LogOutCommand { get; set; }
        public ICommand Loadwd { get; set; }
        public ICommand MoveWindow { get; set; }
        public ICommand TenDangNhap_Loaded { get; set; }


        private CUAHANG _User;
        public CUAHANG User { get => _User; set { _User = value; OnPropertyChanged(); } }

        private string _Ava;
        public string Ava { get => _Ava; set { _Ava = value; OnPropertyChanged(); } }

        public string Name;




        public MainViewModel()
        {
            Closewd = new RelayCommand<MainWindow>((p) => true, (p) => Close());
            Minimizewd = new RelayCommand<MainWindow>((p) => true, (p) => Minimize(p));
            GetIdTab = new RelayCommand<RadioButton>((p) => true, (p) => Name = p.Uid);
            SwitchTab = new RelayCommand<MainWindow>((p) => true, (p) => switchtab(p));
            LogOutCommand = new RelayCommand<MainWindow>((p) => { return true; }, (p) => LogOut(p));
            Loadwd = new RelayCommand<MainWindow>((p) => true, (p) => _Loadwd(p));
            MoveWindow = new RelayCommand<MainWindow>((p) => true, (p) => moveWindow(p));
            TenDangNhap_Loaded = new RelayCommand<MainWindow>((p) => true, (p) => LoadTenAD(p));
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
                Ava = Const._localLink + User.AVATAR;
                LoadTenAD(p);
            }
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
