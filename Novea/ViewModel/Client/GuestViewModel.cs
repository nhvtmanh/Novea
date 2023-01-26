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
        private KHACH user;
        public KHACH User { get => user; set { user = value; OnPropertyChanged(); } }
        private string ava;
        public string Ava { get => ava; set { ava = value; OnPropertyChanged(); } }
        public ICommand SwitchTabCommand { get; set; }
        public ICommand GetIdTab { get; set; }
        public ICommand LogOutCommand { get; set; }
        public ICommand CloseGuestwdCommand { get; set; }
        public ICommand MinimizeGuestwdCommand { get; set; }
        public ICommand LoadGuestwdCommand { get; set; }
        public ICommand LoadTenDangNhapCommand { get; set; }
        public ICommand MoveWindowCommand { get; set; }
        public string name;
        public GuestViewModel()
        {
            SwitchTabCommand = new RelayCommand<Guest>((p) => true, (p) => SwitchTab(p));
            GetIdTab = new RelayCommand<RadioButton>((p) => true, (p) => name = p.Uid);
            LogOutCommand = new RelayCommand<Guest>((p) => { return true; }, (p) => LogOut(p));
            CloseGuestwdCommand = new RelayCommand<Guest>((p) => true, (p) => Close());
            MinimizeGuestwdCommand = new RelayCommand<Guest>((p) => true, (p) => Minimize(p));
            LoadGuestwdCommand = new RelayCommand<Guest>((p) => true, (p) => LoadGuestwd(p));
            LoadTenDangNhapCommand = new RelayCommand<Guest>((p) => true, (p) => LoadTenKhach(p));
            MoveWindowCommand = new RelayCommand<Guest>((p) => true, (p) => MoveWindow(p));
        }
        void LoadGuestwd(Guest p)
        {
            if (Const.IsLogin)
            {
                string a = Const.TenDangNhap;
                User = DataProvider.Ins.DB.KHACHes.Where(x => x.TAIKHOAN == a).FirstOrDefault();
                Const.KH = User;
                Ava = User.AVATAR;
                LoadTenKhach(p);
            }
        }
        public void LoadTenKhach(Guest p)
        {
            p.TenDangNhap.Text = string.Join(" ", User.HOTEN.Split().Reverse().Take(2).Reverse());
        }
        void SwitchTab(Guest p)
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
        void LogOut(Guest p)
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
        public void MoveWindow(Guest p)
        {
            p.DragMove();
        }
    }
}
