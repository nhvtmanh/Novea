using Novea.Model;
using System.Security.Cryptography;
using System.Text;
using System.Windows.Input;
using System.Windows;
using Novea.View.Client;

namespace Novea.ViewModel.Client
{
    public class ChangePasswordViewModel : BaseViewModel
    {
        public ICommand SaveCommand { get; set; }
        public ChangePasswordViewModel()
        {
            SaveCommand = new RelayCommand<ChangePassword>((p) => true, (p) => ChangPassword(p));
        }
        void ChangPassword(ChangePassword p)
        {
            if (p.pbOLDPASS.Password == "" || p.pbNEWPASS.Password == "" || p.pbNEWPASSAGAIN.Password == "")
            {
                MessageBox.Show("Vui lòng nhập thông tin!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            else if (Const.KH.MATKHAU != MD5Hash(Base64Encode(p.pbOLDPASS.Password)))
            {
                MessageBox.Show("Mật khẩu cũ không đúng!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            else if (p.pbNEWPASS.Password == p.pbOLDPASS.Password)
            {
                MessageBox.Show("Mật khẩu mới không được giống mật khẩu cũ!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            else if (p.pbNEWPASS.Password != p.pbNEWPASSAGAIN.Password)
            {
                MessageBox.Show("Mật khẩu nhập lại không đúng!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            else
            {
                Const.KH.MATKHAU = MD5Hash(Base64Encode(p.pbNEWPASS.Password));
                DataProvider.Ins.DB.SaveChanges();
                MessageBox.Show("Đổi mật khẩu thành công!", "Thông báo");
                p.pbOLDPASS.Clear();
                p.pbNEWPASS.Clear();
                p.pbNEWPASSAGAIN.Clear();
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
