using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Novea.ViewModel.Admin
{
    class HomeViewModel : BaseViewModel
    {
        public HomeViewModel HomeVM { get; set; }
        private object _currentView;
        public object CurrentView
        {
            get { return _currentView; }
            set 
            { 
                _currentView = value; 
                OnPropertyChanged();
            }
        }

        public HomeViewModel()
        {
            HomeVM = new HomeViewModel();
        }
    }
}
