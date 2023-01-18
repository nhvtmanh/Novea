using Novea.View.Login;
using Novea.Model;
using Novea.View;
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

namespace Novea.ViewModel.Login
{
    public class MainLoginViewModel : BaseViewModel
    {
        public ICommand GetIdTab { get; set; }
        public ICommand SwitchTab { get; set; }

        public string Name;

        public MainLoginViewModel()
        {
            GetIdTab = new RelayCommand<Button>((p) => true, (p) => Name = p.Uid);
            SwitchTab = new RelayCommand<MainLogin>((p) => true, (p) => switchtab(p));
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
    }
}
