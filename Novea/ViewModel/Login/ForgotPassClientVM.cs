using Novea.Model;
using Novea.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows;
using Novea.View.Login;

namespace Novea.ViewModel.Login
{
    public class ForgotPassClientVM : BaseViewModel
    {
        public ICommand Closewd { get; set; }
        public ICommand SendPass { get; set; }
        public ForgotPassClientVM()
        {
            Closewd = new RelayCommand<ForgotPassClient>((p) => true, (p) => Close(p));
            SendPass = new RelayCommand<ForgotPassClient>((p) => true, (p) => _SendPass(p));
        }

        void Close(ForgotPassClient p)
        {
            p.Close();
        }
        void _SendPass(ForgotPassClient parameter)
        {
            int dem = DataProvider.Ins.DB.KHACHes.Where(p => p.EMAIL == parameter.email.Text).Count();
            if (dem == 0)
            {
                MessageBox.Show("Email này chưa được đăng ký !", "THÔNG BÁO", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            Random rand = new Random();
            string newpass = rand.Next(100000, 999999).ToString();
            foreach (KHACH temp in DataProvider.Ins.DB.KHACHes)
            {
                if (temp.EMAIL == parameter.email.Text)
                {
                    temp.MATKHAU = LoginViewModel.MD5Hash(LoginViewModel.Base64Encode(newpass));
                    break;
                }
            }

            string body = "Mật khẩu mới của bạn là: " + newpass + " . Vui lòng đổi mật khẩu mới ngay khi đăng nhập!";

            try
            {
                MailMessage message = new MailMessage("21522348@gm.uit.edu.vn", parameter.email.Text, "Lấy lại mật khẩu đã quên", body);
                SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587);
                smtp.EnableSsl = true;
                smtp.UseDefaultCredentials = false;
                smtp.Credentials = new NetworkCredential("21522348@gm.uit.edu.vn", "");
                smtp.Send(message);
            }
            catch
            {
                MessageBox.Show("Vui lòng cho phép 'quyền truy cập ứng dụng kém an toàn' của gmail");
                return;
            }
            DataProvider.Ins.DB.SaveChanges();
            MessageBox.Show("Đã gửi mật khẩu vào Email đăng ký !", "Thông báo");
            parameter.Close();
        }
    }
}
