using Novea.View;
using Novea.View.Client;
using Novea.ViewModel.Login;
using Novea.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Novea.ViewModel.Client
{
    public class GuestViewModel : BaseViewModel
    {
        private KHACH _User;
        public KHACH User { get => _User; set { _User = value; OnPropertyChanged(); } }
        private string _Ava;
        public string Ava { get => _Ava; set { _Ava = value; OnPropertyChanged(); } }
        public ICommand SwitchTab { get; set; }
        public ICommand GetIdTab { get; set; }
        public ICommand LogOutCommand { get; set; }
        public ICommand CloseGuestwd { get; set; }
        public ICommand MinimizeGuestwd { get; set; }
        public ICommand Loadwd { get; set; }
        public ICommand TenDangNhap_Loaded { get; set; }
        public ICommand MoveWindow { get; set; }
        public string name;
        public GuestViewModel()
        {
            SwitchTab = new RelayCommand<Guest>((p) => true, (p) => switchTab(p));
            GetIdTab = new RelayCommand<RadioButton>((p) => true, (p) => name = p.Uid);
            LogOutCommand = new RelayCommand<Guest>((p) => { return true; }, (p) => logOut(p));
            CloseGuestwd = new RelayCommand<Guest>((p) => true, (p) => Close());
            MinimizeGuestwd = new RelayCommand<Guest>((p) => true, (p) => Minimize(p));
            Loadwd = new RelayCommand<Guest>((p) => true, (p) => _Loadwd(p));
            TenDangNhap_Loaded = new RelayCommand<Guest>((p) => true, (p) => LoadTenAD(p));
            MoveWindow = new RelayCommand<Guest>((p) => true, (p) => moveWindow(p));
        }
        void _Loadwd(Guest p)
        {
            if (Const.IsLogin)
            {
                string a = Const.TenDangNhap;
                User = DataProvider.Ins.DB.KHACHes.Where(x => x.TAIKHOAN == a).FirstOrDefault();
                Const.KH = User;
                Ava = User.AVATAR;
                LoadTenAD(p);
            }
        }
        public void LoadTenAD(Guest p)
        {
            p.TenDangNhap.Text = string.Join(" ", User.HOTEN.Split().Reverse().Take(2).Reverse());
        }
        void switchTab(Guest p)
        {
            int i = int.Parse(name);
            switch (i)
            {
                case 0:
                    p.Main.NavigationService.Navigate(new View.Client.Home());
                    break;
                case 1:
                    p.Main.NavigationService.Navigate(new View.Client.Cart());
                    break;
                case 2:
                    p.Main.NavigationService.Navigate(new View.Client.OrderHistory());
                    break;
                case 3:
                    p.Main.NavigationService.Navigate(new View.Client.Setting());
                    break;
                default:
                    break;
            }
        }
        void logOut(Guest p)
        {
            LoginWindow login = new LoginWindow();
            login.Show();
            p.Close();
        }
        public void Close()
        {
            Application.Current.Shutdown();
        }
        public void Minimize(Guest p)
        {
            p.WindowState = WindowState.Minimized;
        }
        public void moveWindow(Guest p)
        {
            p.DragMove();
        }
    }
}
