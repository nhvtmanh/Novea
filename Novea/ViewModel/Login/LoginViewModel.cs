﻿using Novea.Model;
using Novea.View;
using Novea.View.Client;
using Microsoft.Win32;
using Novea.ViewModel;
using Novea;
using Novea.View.Admin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Novea.View.Login;

namespace Novea.ViewModel
{ 
    public class LoginViewModel : BaseViewModel
    {
        public static bool IsLogin { get; set; }
        private string _Username;
        public string Username { get => _Username; set { _Username = value; OnPropertyChanged(); } }
        private string _Password;
        public string Password { get => _Password; set { _Password = value; OnPropertyChanged(); } }

        public ICommand GetIdTab { get; set; }
        public ICommand SwitchTab { get; set; }

        public string Name;

        public ICommand CloseLogin { get; set; }
        public ICommand MinimizeLogin { get; set; }
        public ICommand MoveLogin { get; set; }
        public ICommand Login { get; set; }
        public ICommand PasswordChangedCommand { get; set; }
        public ICommand RegisterCommand { get; set; }
        public ICommand Loadwd { get; set; }
        public ICommand ForgetPassCommand { get; set; }

        public LoginViewModel()
        {
            IsLogin = false;
            Password = "";
            Username = "";
            GetIdTab = new RelayCommand<Button>((p) => true, (p) => Name = p.Uid);
            SwitchTab = new RelayCommand<MainLogin>((p) => true, (p) => switchtab(p));
            CloseLogin = new RelayCommand<LoginWindow>((p) => true, (p) => Close());
            MinimizeLogin = new RelayCommand<LoginWindow>((p) => true, (p) => Minimize(p));
            MoveLogin = new RelayCommand<LoginWindow>((p) => true, (p) => Move(p));
            Login = new RelayCommand<LoginWindow>((p) => true, (p) => login(p));
            PasswordChangedCommand = new RelayCommand<PasswordBox>((p) => true, (p) => { Password = p.Password; });
            RegisterCommand = new RelayCommand<LoginWindow>((p) => true, (p) => _RegisterCommand(p));
            ForgetPassCommand = new RelayCommand<LoginWindow>((p) => true, (p) => _ForgetPassCommand(p));
        }

        void switchtab(MainLogin p)
        {
            int index = int.Parse(Name);
            switch (index)
            {
                case 0:
                    {
                        p.Login.NavigationService.Navigate(new View.Login.AdminLoginPage());
                        break;
                    }
                case 1:
                    {
                        p.Login.NavigationService.Navigate(new View.Login.ClientLoginPage());
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
        public void Minimize(LoginWindow p)
        {
            p.WindowState = WindowState.Minimized;
        }
        public void Move(LoginWindow p)
        {
            p.DragMove();
        }
        public void login(LoginWindow p)
        {
            try
            {
                if (p == null) return;
                string PassEncode = MD5Hash(Base64Encode(Password));
                var accCountCHU = DataProvider.Ins.DB.CUAHANGs.Where(x => x.TAIKHOAN == Username && x.MATKHAU == PassEncode).Count();
                if (accCountCHU > 0)
                {
                    IsLogin = true;
                    Const.TenDangNhap = Username;
                    MainWindow mainWindow = new MainWindow();
                    mainWindow.Show();
                    Username = "";
                    p.Hide();
                }
                var accCountKHACH = DataProvider.Ins.DB.KHACHes.Where(x => x.TAIKHOAN == Username && x.MATKHAU == PassEncode).Count();
                if (accCountKHACH > 0)
                {
                    IsLogin = true;
                    Const.TenDangNhap = Username;
                    Guest guest = new Guest();
                    guest.Show();
                    Username = "";
                    p.Hide();
                }
                if(accCountCHU <= 0 && accCountKHACH <= 0)
                {
                    MessageBox.Show("Tên đăng nhập hoặc mật khẩu không đúng!", "Thông báo", MessageBoxButton.OK);
                }
            }
            catch
            {
                MessageBox.Show("Mất kết nối đến cơ sở dữ liệu!", "Thông báo", MessageBoxButton.OK);
            }
        }
        public static string Base64Encode(string plainText)
        {
            var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(plainText);
            return System.Convert.ToBase64String(plainTextBytes);
        }
        public static string MD5Hash(string input)
        {
            StringBuilder hash = new StringBuilder();
            MD5CryptoServiceProvider md5provider = new MD5CryptoServiceProvider();
            byte[] bytes = md5provider.ComputeHash(new UTF8Encoding().GetBytes(input));

            for (int i = 0; i < bytes.Length; i++)
            {
                hash.Append(bytes[i].ToString("x2"));
            }
            return hash.ToString();
        }
        void _RegisterCommand(LoginWindow parameter)
        {
            SignUp signUp = new SignUp();
            signUp.ShowDialog();
        }
        void _ForgetPassCommand(LoginWindow parameter)
        {
            ForgotPassword forgetPassView = new ForgotPassword();
            forgetPassView.ShowDialog();
        }

        
    }
}
