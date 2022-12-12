//using Novea.Model;
using Novea.View;
using Novea.View.Admin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Novea.ViewModel.Admin
{
    public class MainViewModel : BaseViewModel
    {
        public ICommand SwitchTab { get; set; }
        public ICommand GetIdTab { get; set; }

        public string Name;
        public ICommand Loadwd { get; set; }

        public MainViewModel()
        {
            SwitchTab = new RelayCommand<MainWindow>((p) => true, (p) => switchtab(p));
            GetIdTab = new RelayCommand<RadioButton>((p) => true, (p) => Name = p.Uid);
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
            }


        }
    }
}
