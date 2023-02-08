using Novea.Model;
using Novea.View;
using Novea.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Xml.Linq;

namespace Novea.ViewModel
{
    public class ForgotPasswordViewModel
    {
        public ICommand Closewd { get; set; }
        public ICommand Minimizewd { get; set; }
        public ICommand SendPass { get; set; }
        public ICommand movewd { get; set; }
        public ForgotPasswordViewModel()
        {
            Closewd = new RelayCommand<ForgotPassword>((p) => true, (p) => Close(p));
            Minimizewd = new RelayCommand<ForgotPassword>((p) => true, (p) => Minimize(p));
            SendPass = new RelayCommand<ForgotPassword>((p) => true, (p) => _SendPass(p));
        }
        void Close(ForgotPassword p)
        {
            p.Close();
        }
        void Minimize(ForgotPassword p)
        {
            p.WindowState = WindowState.Minimized;
        }
        void _SendPass(ForgotPassword parameter)
        {
            int dem = DataProvider.Ins.DB.CUAHANGs.Where(p => p.EMAIL == parameter.email.Text).Count();
            if (dem == 0)
            {
                MessageBox.Show("Email này chưa được đăng ký !", "THÔNG BÁO", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            Random rand = new Random();
            string newpass = rand.Next(100000, 999999).ToString();
            foreach (CUAHANG temp in DataProvider.Ins.DB.CUAHANGs)
            {
                if (temp.EMAIL == parameter.email.Text)
                {
                    temp.MATKHAU = LoginViewModel.MD5Hash(LoginViewModel.Base64Encode(newpass));
                    break;
                }
            }
            DataProvider.Ins.DB.SaveChanges();
            string body = "Mật khẩu mới của bạn là: " + newpass + " . Vui lòng đổi mật khẩu mới ngay khi đăng nhập!";

            try
            {
                MailMessage message = new MailMessage("noveamanagement@gmail.com", parameter.email.Text, "Lấy lại mật khẩu đã quên", body);
                SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587);
                smtp.EnableSsl = true;
                smtp.UseDefaultCredentials = false; 
                smtp.Credentials = new NetworkCredential("noveamanagement@gmail.com", "meilideshenhua");
                smtp.Send(message);
            }
            catch
            {
                MessageBox.Show("Vui lòng cho phép 'quyền truy cập ứng dụng kém an toàn' của gmail");
                return;
            }
            MessageBox.Show("Đã gửi mật khẩu vào Email đăng ký !", "Thông báo");

        }
    }
}
