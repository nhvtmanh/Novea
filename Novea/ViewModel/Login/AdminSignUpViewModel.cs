using Novea.Model;
using Novea.View.Login;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Text.RegularExpressions;
using Novea.ViewModel;
using Novea;
using Novea.View.Admin;

namespace Novea.ViewModel.Login
{
    public class AdminSignUpViewModel : BaseViewModel
    {
        public ICommand Close1 { get; set; }
        public ICommand Minimizewd { get; set; }
        public ICommand Movewd { get; set; }
        public ICommand Register { get; set; }
        private string _Password;
        public string Password { get => _Password; set { _Password = value; OnPropertyChanged(); } }
        private byte[] imageData;
        public ICommand PasswordChangedCommand { get; set; }
        private BitmapImage selectedImage;
        public BitmapImage SelectedImage
        {
            get { return selectedImage; }
            set { selectedImage = value; OnPropertyChanged(); }
        }
        public ICommand AddImage { get; set; }
        public AdminSignUpViewModel()
        {
            Close1 = new RelayCommand<AdminSignUp>((p) => true, (p) => Close(p));
            Minimizewd = new RelayCommand<AdminSignUp>((p) => true, (p) => _Minimize(p));
            Movewd = new RelayCommand<AdminSignUp>((p) => true, (p) => _Move(p));
            Register = new RelayCommand<AdminSignUp>((p) => true, (p) => _Register(p));
            PasswordChangedCommand = new RelayCommand<PasswordBox>((p) => true, (p) => { Password = p.Password; });
            AddImage = new RelayCommand<ImageBrush>((p) => true, (p) => _AddImage());
        }
        void Close(AdminSignUp p)
        {
            p.Close();
        }
        void _Minimize(AdminSignUp p)
        {
            p.WindowState = WindowState.Minimized;
        }
        void _Move(AdminSignUp p)
        {
            p.DragMove();
        }
        void _AddImage()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Image Files (*.jpg; *.jpeg; *.png)|*.jpg; *.jpeg; *.png";
            if (openFileDialog.ShowDialog() == true)
            {
                SelectedImage = new BitmapImage(new Uri(openFileDialog.FileName));
                MemoryStream memoryStream = new MemoryStream();
                using (FileStream fileStream = new FileStream(openFileDialog.FileName, FileMode.Open, FileAccess.Read))
                {
                    fileStream.CopyTo(memoryStream);
                }
                imageData = memoryStream.ToArray();
            }
        }
        bool checkMACH(string m)
        {
            foreach (CUAHANG temp in DataProvider.Ins.DB.CUAHANGs)
            {
                if (temp.MACH == m)
                    return true;
            }
            return false;
        }
        string rdMACH()
        {
            string MaCH;
            do
            {
                Random rand = new Random();
                MaCH = "CH" + rand.Next(0, 10000).ToString();
            } while (checkMACH(MaCH));
            return MaCH;
        }
        void _Register(AdminSignUp parameter)
        {
            if (parameter.TenCH.Text == "" || parameter.SDT.Text == "" || parameter.User.Text == "" || Password == "" || parameter.Mail.Text == "")
            {
                MessageBox.Show("Bạn chưa nhập đầy đủ thông tin !", "THÔNG BÁO", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            int dem1 = DataProvider.Ins.DB.KHACHes.Where(p => p.TAIKHOAN == parameter.User.Text).Count();
            if (dem1 > 0)
            {
                MessageBox.Show("Tên đăng nhập đã tồn tại !", "THÔNG BÁO", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            int dem2 = DataProvider.Ins.DB.CUAHANGs.Where(p => p.TAIKHOAN == parameter.User.Text).Count();
            if (dem2 > 0)
            {
                MessageBox.Show("Tên đăng nhập đã tồn tại !", "THÔNG BÁO", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            foreach (KHACH temp in DataProvider.Ins.DB.KHACHes)
            {
                if (temp.EMAIL == parameter.Mail.Text)
                {
                    MessageBox.Show("Email này đã được sử dụng !", "THÔNG BÁO", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
            }
            foreach (CUAHANG temp in DataProvider.Ins.DB.CUAHANGs)
            {
                if (temp.EMAIL == parameter.Mail.Text)
                {
                    MessageBox.Show("Email này đã được sử dụng !", "THÔNG BÁO", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
            }
            string match = @"\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*";
            Regex reg = new Regex(match);
            if (!reg.IsMatch(parameter.Mail.Text))
            {
                MessageBox.Show("Email không hợp lệ !", "THÔNG BÁO", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            string match1 = @"^((09(\d){8})|(086(\d){7})|(088(\d){7})|(089(\d){7})|(01(\d){9}))$";
            Regex reg1 = new Regex(match1);
            if (!reg1.IsMatch(parameter.SDT.Text))
            {
                MessageBox.Show("Số điện thoại không hợp lệ !", "THÔNG BÁO", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            MessageBoxResult h = System.Windows.MessageBox.Show("Bạn muốn đăng ký tài khoản ?", "THÔNG BÁO", MessageBoxButton.YesNoCancel, MessageBoxImage.Question);
            if (h == MessageBoxResult.Yes)
            {
                CUAHANG temp = new CUAHANG();
                temp.MACH = rdMACH();
                temp.TENCH = parameter.TenCH.Text;
                temp.DIADIEM = parameter.DCCH.Text;
                temp.EMAIL = parameter.Mail.Text;
                temp.SDT = parameter.SDT.Text;
                temp.DOANHTHU = 0;
                temp.NGDK = DateTime.Now;
                temp.TAIKHOAN = parameter.User.Text;
                temp.MATKHAU = LoginViewModel.MD5Hash(LoginViewModel.Base64Encode(Password));
                temp.AVATAR = imageData;
                DataProvider.Ins.DB.CUAHANGs.Add(temp);
                DataProvider.Ins.DB.SaveChanges();
                MessageBox.Show("Chúc mừng bạn đã đăng ký thành công !", "THÔNG BÁO", MessageBoxButton.OK);
                parameter.User.Clear();
                parameter.password.Clear();
                parameter.TenCH.Clear();
                parameter.SDT.Clear();
                parameter.DCCH.Clear();
                parameter.Mail.Clear();
            }
        }
    }
}
