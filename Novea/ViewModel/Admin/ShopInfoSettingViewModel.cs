﻿using Microsoft.Win32;
using Novea.Model;
using Novea.View.Admin;
using Novea.ViewModel.Login;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Novea.ViewModel.Admin
{
    public class ShopInfoSettingViewModel : BaseViewModel
    {
        private string _Ava;
        public string Ava { get => _Ava; set { _Ava = value; OnPropertyChanged(); } }
        private string _Name;
        public string Name { get => _Name; set { _Name = value; OnPropertyChanged(); } }
        private string _DoB;
        public string DoB { get => _DoB; set { _DoB = value; OnPropertyChanged(); } }
        private string _DiaChi;
        public string DiaChi { get => _DiaChi; set { _DiaChi = value; OnPropertyChanged(); } }
        private string _Mail;
        public string Mail { get => _Mail; set { _Mail = value; OnPropertyChanged(); } }

        private string _SDT;
        public string SDT { get => _SDT; set { _SDT = value; OnPropertyChanged(); } }
        private string _TenTK;
        public string TenTK { get => _TenTK; set { _TenTK = value; OnPropertyChanged(); } }
        private CUAHANG _User;
        public CUAHANG User { get => _User; set { _User = value; OnPropertyChanged(); } }
        public ICommand Loadwd { get; set; }
        public ICommand UpdateInfo { get; set; }
        public ICommand AddImage { get; set; }
        public ICommand ChangePass { get; set; }
        public ShopInfoSettingViewModel()
        {
            Loadwd = new RelayCommand<ShopInfoSetting>((p) => true, (p) => _Loadwd(p));
            AddImage = new RelayCommand<ImageBrush>((p) => true, (p) => _AddImage(p));
            UpdateInfo = new RelayCommand<ShopInfoSetting>((p) => true, (p) => _UdpateInfo(p));
        }
        void _AddImage(ImageBrush p)
        {
            OpenFileDialog open = new OpenFileDialog();
            open.Filter = "Image Files(*.jpg; *.png)|*.jpg; *.png";
            if (open.ShowDialog() == true)
            {
                Ava = open.FileName;
            }
            p.ImageSource = new BitmapImage(new Uri(Ava));
        }
        void _Loadwd(ShopInfoSetting p)
        {
            if (Const.IsLogin)
            {
                string a = Const.TenDangNhap;
                User = DataProvider.Ins.DB.CUAHANGs.Where(x => x.TAIKHOAN == a).FirstOrDefault();
                Ava = User.AVATAR;
                Name = User.TENCH;
                DoB = User.NGDK.ToString();
                DiaChi = User.DIADIEM;
                SDT = User.SDT;
                TenTK = User.TAIKHOAN;
                Mail = User.EMAIL;
            }
        }
        void _UdpateInfo(ShopInfoSetting p)
        {
            foreach (CUAHANG temp2 in DataProvider.Ins.DB.CUAHANGs)
            {
                if (temp2.EMAIL == p.Mail.Text && p.Mail.Text != Const.CH.EMAIL)
                {
                    MessageBox.Show("Email này đã được sử dụng !", "THÔNG BÁO", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
            }
            string match = @"\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*";
            Regex reg = new Regex(match);
            if (!reg.IsMatch(p.Mail.Text))
            {
                MessageBox.Show("Email không hợp lệ !", "THÔNG BÁO", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            var temp = DataProvider.Ins.DB.CUAHANGs.Where(pa => pa.TAIKHOAN == TenTK).FirstOrDefault();
            temp.TENCH = p.NameBox.Text;
            temp.SDT = p.SDTBox.Text;
            temp.DIADIEM = p.AddressBox.Text;
            temp.EMAIL = p.Mail.Text;
            string rd = StringGenerator();
            //if (User.AVATAR != Ava)
            //    temp.AVATAR = "/Resource/Ava/" + rd + (Ava.Contains(".jpg") ? ".jpg" : ".png").ToString();
            DataProvider.Ins.DB.SaveChanges();
            //try
            //{
            //    if (User.AVATAR != Ava)
            //        File.Copy(Ava, Const._localLink + @"Resource/Ava/" + rd + (Ava.Contains(".jpg") ? ".jpg" : ".png").ToString(), true);
            //}
            //catch { }
            MessageBox.Show("Cập nhật thành công!", "Thông báo");
        }
        static string StringGenerator()
        {
            Random rd = new Random();
            int length = rd.Next(5, 20);
            StringBuilder str_build = new StringBuilder();
            Random random = new Random();
            char letter;
            for (int i = 0; i < length; i++)
            {
                double flt = random.NextDouble();
                int shift = Convert.ToInt32(Math.Floor(25 * flt));
                letter = Convert.ToChar(shift + 65);
                str_build.Append(letter);
            }
            return str_build.ToString();
        }
    }
}
