using Novea.Model;
using Novea.View;
using Novea.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

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
            movewd = new RelayCommand<ForgotPassword>((p) => true, (p) => _movewd(p));
        }
        void Close(ForgotPassword p)
        {
            p.Close();
        }
        void _movewd(ForgotPassword p)
        {
            p.DragMove();
        }
        void Minimize(ForgotPassword p)
        {
            p.WindowState = WindowState.Minimized;
        }
        void _SendPass(ForgotPassword parameter)
        {
            int dem = DataProvider.Ins.DB.CHUs.Where(p => p.EMAIL == parameter.email.Text).Count();
            if (dem == 0)
            {
                MessageBox.Show("Email này chưa được đăng ký !", "THÔNG BÁO", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            Random rand = new Random();
            string newpass = rand.Next(100000, 999999).ToString();
            foreach (CHU temp in DataProvider.Ins.DB.CHUs)
            {
                if (temp.EMAIL == parameter.email.Text)
                {
                    temp.MATKHAU = LoginViewModel.MD5Hash(LoginViewModel.Base64Encode(newpass));
                    break;
                }
            }
            DataProvider.Ins.DB.SaveChanges();
            string nd = "Vui lòng nhập mật khẩu " + newpass + " để đăng nhập. Trân trọng !";
            MailMessage message = new MailMessage("beveragemanagementNovea@gmail.com", parameter.email.Text, "Lấy lại mật khẩu", nd);
            SmtpClient smtpClient = new SmtpClient("smtp.gmail.com", 587);
            smtpClient.EnableSsl = true;
            smtpClient.Credentials = new NetworkCredential("tài khoản mail của bạn", "mật khẩu email của bạn");
            smtpClient.Send(message);
            MessageBox.Show("Đã gửi mật khẩu vào Email đăng ký !", "Thông báo");
        }
    }
}
