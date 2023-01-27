using FontAwesome.Sharp;
using SmallBusinessAdministrationWPF.Model;
using SmallBusinessAdministrationWPF.Repositories;
using System;
using System.Threading;
using System.Windows.Input;

namespace SmallBusinessAdministrationWPF.ViewModel
{
    public class MainViewModel : ViewModelBase
    {
        private UserAccountModel _currentUserAccount;
        private ViewModelBase _currentChildView;
        private string _caption;
        private IconChar _icon;

        private IUserRepository userRepository;

        //--> Properties
        public UserAccountModel CurrentUserAccount
        {
            get
            { 
                return _currentUserAccount; 
            }
            set
            { 
                _currentUserAccount = value; 
                OnPropertyChanged(nameof(CurrentUserAccount));
            }
        }
        public ViewModelBase CurrentChildView
        {
            get
            {
                return _currentChildView;
            }
            set
            {
                _currentChildView = value;
                OnPropertyChanged(nameof(CurrentChildView));
            }
        }
        public string Caption
        {
            get
            {
                return _caption;
            }
            set
            {
                _caption = value;
                OnPropertyChanged(nameof(Caption));
            }
        }
        public IconChar Icon
        {
            get
            {
                return _icon;
            }
            set
            {
                _icon = value;
                OnPropertyChanged(nameof(Icon));
            }
        }

        //--> Commands
        public ICommand ShowHomeViewCommand { get; }
        public ICommand ShowCustomerViewCommand { get; }

        public MainViewModel()
        {
            userRepository = new UserRepository();
            CurrentUserAccount = new UserAccountModel();

            //--> Initialize commands
            ShowHomeViewCommand = new ViewModelCommand(ExecuteShowHomeCommand);
            ShowCustomerViewCommand = new ViewModelCommand(ExecuteShowCustomerCommand);

            //--> Default view
            ExecuteShowHomeCommand(null);

            LoadCurrentUserData();
        }

        private void ExecuteShowCustomerCommand(object obj)
        {
            CurrentChildView = new CustomerViewModel();
            Caption = "Customers";
            Icon = IconChar.UserGroup;
        }

        private void ExecuteShowHomeCommand(object obj)
        {
            CurrentChildView = new HomeViewModel();
            Caption = "Dashboard";
            Icon = IconChar.Home;
        }

        private void LoadCurrentUserData()
        {
            var user = userRepository.GetByUserName(Thread.CurrentPrincipal.Identity.Name);
            if(user != null)
            {

                CurrentUserAccount.Username = user.UserName;
                CurrentUserAccount.DisplayName = $"{user.Name}/{user.LastName}";
                CurrentUserAccount.ProfilePicture = null;
            }
            else
            {
                CurrentUserAccount.DisplayName = "Invalid user, not logged in";
            }
        }
    }
}
