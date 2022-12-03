using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;
using Novea.View;
using System.Linq;
using Novea.ViewModel;

namespace Novea.ViewModel.Admin
{
    internal class MainViewModel : BaseViewModel
    {
        public ICommand SwitchTab { get; set; }
        public ICommand GetIdTab { get; set; }

        public string Name;

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

            }


        }
    }
}
