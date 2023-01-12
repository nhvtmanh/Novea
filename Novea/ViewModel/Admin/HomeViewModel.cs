using Novea.Model;
using Novea.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using LiveCharts;
using LiveCharts.Wpf;
using System.Data;
using System.Data.SqlClient;
using System.Collections.ObjectModel;
using LiveCharts.Defaults;

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
