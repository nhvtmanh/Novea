//using Novea.Model;
using Novea.View.Admin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Xml.Linq;

namespace Novea.ViewModel.Admin
{
    public class MainViewModel : BaseViewModel
    {
        public ICommand CloseLogin { get; set; }
        public ICommand MaximizeLogin { get; set; }
        public ICommand MinimizeLogin { get; set; }
        public ICommand GetIdTab { get; set; }
        public ICommand SwitchTab { get; set; }
        public string Name;

        public MainViewModel()
        {
            MaximizeLogin = new RelayCommand<MainWindow>((p) => true, (p) => Maximize(p));
            CloseLogin = new RelayCommand<MainWindow>((p) => true, (p) => Close());
            MinimizeLogin = new RelayCommand<MainWindow>((p) => true, (p) => Minimize(p));
            GetIdTab = new RelayCommand<RadioButton>((p) => true, (p) => Name = p.Uid);
            SwitchTab = new RelayCommand<MainWindow>((p) => true, (p) => switchtab(p));
        }

        void switchtab(MainWindow p)
        {
            int index = int.Parse(Name);
            switch (index)
            {
                case 0:
                    {
                        p.Main.NavigationService.Navigate(new View.Admin.HomeView());
                        break;
                    }
                case 1:
                    {
                        p.Main.NavigationService.Navigate(new View.Admin.OrdersView());
                        break;
                    }
                case 2:
                    {
                        p.Main.NavigationService.Navigate(new View.Admin.Product());
                        break;
                    }
                case 3:
                    {
                        p.Main.NavigationService.Navigate(new View.Admin.CustomerView());
                        break;
                    }
                case 4:
                    {
                        p.Main.NavigationService.Navigate(new View.Admin.ManagerView());
                        break;
                    }
                case 5:
                    {
                        p.Main.NavigationService.Navigate(new View.Admin.SettingView());
                        break;
                    }
                default:
                    {
                        break;
                    }
            }
        }
        public void Close()
        {
            System.Windows.Application.Current.Shutdown();
        }
        public void Minimize(MainWindow p)
        {
            p.WindowState = WindowState.Minimized;
        }
        public void Maximize(MainWindow p)
        {
            p.WindowState = WindowState.Maximized;
        }
    }
}
