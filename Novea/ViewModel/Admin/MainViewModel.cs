using Novea.Model;
using Novea.View;
using Novea.View.Admin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Xml.Linq;

namespace Novea.ViewModel.Admin
{
    public class MainViewModel : BaseViewModel
    {
        public ICommand CloseLogin { get; set; }
        public ICommand MinimizeLogin { get; set; }
        public ICommand GetIdTab { get; set; }
        public ICommand SwitchTab { get; set; }
        public ICommand LogOutCommand { get; set; }
        

        public ICommand Loadwd { get; set; }
        public ICommand MoveWindow { get; set; }
        public ICommand TenDangNhap_Loaded { get; set; }
        public ICommand Quyen_Loaded { get; set; }


        private CHU _User;
        public CHU User { get => _User; set { _User = value; OnPropertyChanged(); } }
        private Visibility _SetQuanLy;
        public Visibility SetQuanLy { get => _SetQuanLy; set { _SetQuanLy = value; OnPropertyChanged(); } }

        private string _Ava;
        public string Ava { get => _Ava; set { _Ava = value; OnPropertyChanged(); } }

        public string Name;

        public MainViewModel()
        {
            CloseLogin = new RelayCommand<MainWindow>((p) => true, (p) => Close());
            MinimizeLogin = new RelayCommand<MainWindow>((p) => true, (p) => Minimize(p));
            GetIdTab = new RelayCommand<RadioButton>((p) => true, (p) => Name = p.Uid);
            SwitchTab = new RelayCommand<MainWindow>((p) => true, (p) => switchtab(p));
            LogOutCommand = new RelayCommand<MainWindow>((p) => { return true; }, (p) => LogOut(p));


            Loadwd = new RelayCommand<MainWindow>((p) => true, (p) => _Loadwd(p));
            MoveWindow = new RelayCommand<MainWindow>((p) => true, (p) => moveWindow(p));
            TenDangNhap_Loaded = new RelayCommand<MainWindow>((p) => true, (p) => LoadTenAD(p));
            Quyen_Loaded = new RelayCommand<MainWindow>((p) => true, (p) => LoadQuyen(p));
        }
        void LogOut(MainWindow p)
        {
            LoginWindow login = new LoginWindow();
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
        //Start Ham
        void _Loadwd(MainWindow p)
        {
            if (LoginViewModel.IsLogin)
            {
                string a = Const.TenDangNhap;
                User = DataProvider.Ins.DB.CHUs.Where(x => x.MACHU == a).FirstOrDefault();
                Const.ND = User;
                SetQuanLy = (bool)User.VAITRO ? Visibility.Visible : Visibility.Collapsed;
                Const.Admin = (bool)User.VAITRO;
                Ava = User.AVATAR;
                LoadTenAD(p);
            }
        }
        public void LoadTenAD(MainWindow p)
        {
            //try 
            //{
            //    p.TenDangNhap.Text = string.Join(" ", User.HOTEN.Split().Reverse().Take(2).Reverse());
            //}
            //catch (Exception ex) 
            //{
            //    MessageBox.Show(ex.ToString());
            //}
            
        }
        public void LoadQuyen(MainWindow p)
        {
            //p.Quyen.Text = (bool)User.VAITRO ? "Chủ" : "khách";
        }
        public void moveWindow(MainWindow p)
        {
            p.DragMove();
        }
        //End Ham

    }
}
