using Novea.Model;
using Novea.View;
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

namespace Novea.ViewModel
{
    public class SignUpViewModel : BaseViewModel
    {
        public ICommand Close1 { get; set; }
        public ICommand Minimizewd { get; set; }
        public ICommand Movewd { get; set; }
        public ICommand Register { get; set; }
        private string _Password;
        public string Password { get => _Password; set { _Password = value; OnPropertyChanged(); } }
        public ICommand PasswordChangedCommand { get; set; }
        private string _linkaddimage;
        public string linkaddimage { get => _linkaddimage; set { _linkaddimage = value; OnPropertyChanged(); } }
        public ICommand AddImage { get; set; }
        public SignUpViewModel()
        {

            linkaddimage = Const._localLink + "/Resource/Image/addava.png";
            Close1 = new RelayCommand<SignUp>((p) => true, (p) => Close(p));
            Minimizewd = new RelayCommand<SignUp>((p) => true, (p) => _Minimize(p));
            Movewd = new RelayCommand<SignUp>((p) => true, (p) => _Move(p));
            Register = new RelayCommand<SignUp>((p) => true, (p) => _Register(p));
            PasswordChangedCommand = new RelayCommand<PasswordBox>((p) => true, (p) => { Password = p.Password; });
            AddImage = new RelayCommand<ImageBrush>((p) => true, (p) => _AddImage(p));
        }
        void Close(SignUp p)
        {
            linkaddimage = Const._localLink + "/Resource/Image/addava.png";
            p.Close();
        }
        void _Minimize(SignUp p)
        {
            p.WindowState = WindowState.Minimized;
        }
        void _Move(SignUp p)
        {
            p.DragMove();
        }
        void _AddImage(ImageBrush img)
        {
            OpenFileDialog open = new OpenFileDialog();
            open.Filter = "Image Files(*.jpg; *.png)|*.jpg; *.png";

            if (open.ShowDialog() == true)
            {
                if (open.FileName != "")
                    linkaddimage = open.FileName;
            };
            Uri fileUri = new Uri(linkaddimage);
            img.ImageSource = new BitmapImage(fileUri);
        }
        bool check(string m)
        {
            foreach (CHUCUAHANG temp in DataProvider.Ins.DB.CHUCUAHANGs)
            {
                if (temp.MACCH == m)
                    return true;
            }
            return false;
        }
        string rdma()
        {
            string ma;
            do
            {
                Random rand = new Random();
                ma = "NV" + rand.Next(0, 10000).ToString();
            } while (check(ma));
            return ma;
        }
        void _Register(SignUp parameter)
        {
            if (parameter.TenND.Text == "" || parameter.GT.Text == "" || parameter.Role.Text == "" || parameter.NS.SelectedDate == null || parameter.SDT.Text == "" || parameter.User.Text == "" || Password == "" || parameter.Mail.Text == "")
            {
                MessageBox.Show("Bạn chưa nhập đầy đủ thông tin !", "THÔNG BÁO", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            int dem = DataProvider.Ins.DB.CHUCUAHANGs.Where(p => p.MACCH == parameter.User.Text).Count();
            if (dem > 0)
            {
                MessageBox.Show("Tên đăng nhập đã tồn tại !", "THÔNG BÁO", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            foreach (CHUCUAHANG temp in DataProvider.Ins.DB.CHUCUAHANGs)
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
                CHUCUAHANG temp = new CHUCUAHANG();
                temp.MACCH = rdma();
                temp.HOTEN = parameter.TenND.Text;
                temp.GIOITINH = parameter.GT.Text;
                temp.DIACHI = parameter.DC.Text;
                temp.NGSINH = (DateTime)parameter.NS.SelectedDate;
                temp.EMAIL = parameter.Mail.Text;
                temp.SDT = parameter.SDT.Text;
                temp.VAITRO = false;
                //temp.TTND = true;
                temp.MACCH = parameter.User.Text;
                temp.MATKHAU = LoginViewModel.MD5Hash(LoginViewModel.Base64Encode(Password));
                if (linkaddimage == "/Resource/Image/addava.png")
                    temp.AVATAR = "/Resource/Image/addava.png";
                else
                    temp.AVATAR = "/Resource/Ava/" + temp.MACCH + ((linkaddimage.Contains(".jpg")) ? ".jpg" : ".png").ToString();
                DataProvider.Ins.DB.CHUCUAHANGs.Add(temp);
                DataProvider.Ins.DB.SaveChanges();
                try
                {
                    File.Copy(linkaddimage, Const._localLink + @"Resource\Ava\" + temp.MACCH + ((linkaddimage.Contains(".jpg")) ? ".jpg" : ".png").ToString(), true);
                }
                catch { }
                MessageBox.Show("Chúc mừng bạn đã đăng ký thành công !", "THÔNG BÁO", MessageBoxButton.OK);
                parameter.User.Clear();
                parameter.password.Clear();
                parameter.TenND.Clear();
                parameter.GT.SelectedItem = null;
                parameter.Role.SelectedItem = null;
                parameter.NS.SelectedDate = null;
                parameter.SDT.Clear();
                parameter.DC.Clear();
                parameter.Mail.Clear();
                linkaddimage = "/Resource/Image/addava.png";
                parameter.HinhAnh1.ImageSource = new BitmapImage(new Uri(Const._localLink + linkaddimage));
            }
        }
    }
}
