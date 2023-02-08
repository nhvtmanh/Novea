using Novea.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows;
using Novea.View.Admin;

namespace Novea.ViewModel.Admin
{
    public class PersonInfoSettingVM : BaseViewModel
    {
        private string _OldPass;
        public string OldPass { get => _OldPass; set { _OldPass = value; OnPropertyChanged(); } }
        private string _NewPass;
        public string NewPass { get => _NewPass; set { _NewPass = value; OnPropertyChanged(); } }
        private string _Password;
        public string Password { get => _Password; set { _Password = value; OnPropertyChanged(); } }
        private CUAHANG _User;
        public CUAHANG User { get => _User; set { _User = value; OnPropertyChanged(); } }
        public ICommand OldPassChangedCommand { get; set; }
        public ICommand PasswordChangedCommand { get; set; }
        public ICommand NewPassChangedCommand { get; set; }
        public ICommand Save { get; set; }
        public PersonInfoSettingVM()
        {
            Save = new RelayCommand<PersonInfoSetting>((p) => true, (p) => SaveNewMATKHAU(p));
            OldPassChangedCommand = new RelayCommand<PasswordBox>((p) => true, (p) => { OldPass = p.Password; });
            PasswordChangedCommand = new RelayCommand<PasswordBox>((p) => true, (p) => { Password = p.Password; });
            NewPassChangedCommand = new RelayCommand<PasswordBox>((p) => true, (p) => { NewPass = p.Password; });
        }
        void SaveNewMATKHAU(PersonInfoSetting p)
        {
            string a = Const.TenDangNhap;
            User = DataProvider.Ins.DB.CUAHANGs.Where(x => x.TAIKHOAN == a).FirstOrDefault();

                if (Password == "" || OldPass == "" || NewPass == "")
                {
                    MessageBox.Show("Vui lòng nhập thông tin!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
                else if (User.MATKHAU != MD5Hash(Base64Encode(OldPass)))
                {
                    MessageBox.Show("Mật khẩu cũ không đúng!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
                else if (Password == OldPass)
                {
                    MessageBox.Show("Mật khẩu mới không được giống mật khẩu cũ!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
                else if (Password != NewPass)
                {
                    MessageBox.Show("Mật khẩu nhập lại không đúng!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
                else
                {
                    User.MATKHAU = MD5Hash(Base64Encode(Password));
                    DataProvider.Ins.DB.SaveChanges();
                    MessageBox.Show("Đổi mật khẩu thành công!", "Thông báo");
                    p.oldpass.Clear();
                    p.newpass.Clear();
                    p.passagain.Clear();
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
    }
}
