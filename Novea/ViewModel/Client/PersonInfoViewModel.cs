using Novea.View.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Media.Imaging;

namespace Novea.ViewModel.Client
{
    public class PersonInfoViewModel : BaseViewModel
    {
        private BitmapImage avatar;
        public BitmapImage Avatar
        {
            get { return avatar; }
            set { avatar = value; OnPropertyChanged(); }
        }
        public ICommand UpdateAvatarCommand { get; set; }
        public ICommand LoadPersonInfowdCommand { get; set; }
        public PersonInfoViewModel()
        {
            //UpdateAvatarCommand = new RelayCommand<>
            LoadPersonInfowdCommand = new RelayCommand<PersonInfo>((p) => true, (p) => LoadPersonInfowd());
        }

        void LoadPersonInfowd()
        {
            
        }
    }
}
