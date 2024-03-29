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

namespace Novea.ViewModel.Login
{
    public class AdminLoginViewModel : BaseViewModel
    {
        private string _Username;
        public string Username { get => _Username; set { _Username = value; OnPropertyChanged(); } }
        private string _Password;
        public string Password { get => _Password; set { _Password = value; OnPropertyChanged(); } }
        public ICommand Login { get; set; }
        public ICommand PasswordChangedCommand { get; set; }
        public ICommand RegisterCommand { get; set; }
        public ICommand ForgetPassCommand { get; set; }
        public ICommand _Loadwd { get; set; }

        public AdminLoginViewModel()
        {
            Const.IsLogin = false;
            Password = "";
            Username = "";
            _Loadwd = new RelayCommand<ClientLoginPage>((p) => true, (p) => loadwd());
            Login = new RelayCommand<AdminLoginPage>((p) => true, (p) => login(p));
            PasswordChangedCommand = new RelayCommand<PasswordBox>((p) => true, (p) => { Password = p.Password; });
            RegisterCommand = new RelayCommand<AdminLoginPage>((p) => true, (p) => _RegisterCommand(p));
            ForgetPassCommand = new RelayCommand<AdminLoginPage>((p) => true, (p) => _ForgetPassCommand(p));
        }
        void loadwd()
        {
            Const.IsLogin = false;
        }

        public void login(AdminLoginPage p)
        {
            try
            {
                if (p == null) return;
                string PassEncode = MD5Hash(Base64Encode(Password));
                var accCountCH = DataProvider.Ins.DB.CUAHANGs.Where(x => x.TAIKHOAN == Username && x.MATKHAU == PassEncode).Count();
                if (accCountCH > 0)
                {
                    if (p.Remember.IsChecked == true)
                    {
                        Const.IsLogin = true;
                        Const.TenDangNhap = Username;

                        Properties.Settings.Default.Save();

                        MainWindow mainWindow = new MainWindow();
                        mainWindow.Show();
                    }
                    else
                    {
                        Const.IsLogin = true;
                        Const.TenDangNhap = Username;
                        MainWindow mainWindow = new MainWindow();
                        mainWindow.Show();
                        Username = "";
                    }                       
                }
                else
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
        void _RegisterCommand(AdminLoginPage parameter)
        {
            AdminSignUp adminSignUp = new AdminSignUp();
            adminSignUp.ShowDialog();
        }
        void _ForgetPassCommand(AdminLoginPage parameter)
        {
            ForgotPassword forgetPassView = new ForgotPassword();
            forgetPassView.ShowDialog();
        }
    }
}
