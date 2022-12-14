using Novea.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;

namespace Novea.ViewModel.Client
{
    public class GuestViewModel : BaseViewModel
    {
        public ICommand SwitchTab { get; set; }
        public ICommand GetIdTab { get; set; }
        public ICommand LogOutCommand { get; set; }
        public string name;
        public GuestViewModel()
        {
            SwitchTab = new RelayCommand<Guest>((p) => true, (p) => switchTab(p));
            GetIdTab = new RelayCommand<RadioButton>((p) => true, (p) => name = p.Uid);
            LogOutCommand = new RelayCommand<Guest>((p) => { return true; }, (p) => logOut(p));
        }
        void switchTab(Guest p)
        {
            int i = int.Parse(name);
            switch (i)
            {
                case 0:
                    p.Main.NavigationService.Navigate(new View.Client.Home());
                    break;
                case 1:
                    p.Main.NavigationService.Navigate(new View.Client.Cart());
                    break;
                case 2:
                    p.Main.NavigationService.Navigate(new View.Client.OrderHistory());
                    break;
                case 3:
                    p.Main.NavigationService.Navigate(new View.Client.Setting());
                    break;
                default:
                    break;
            }
        }
        void logOut(Guest p)
        {
            LoginWindow login = new LoginWindow();
            login.Show();
            p.Close();
        }
    }
}
