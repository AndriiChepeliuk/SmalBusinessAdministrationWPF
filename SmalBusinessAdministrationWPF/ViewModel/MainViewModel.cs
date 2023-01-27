using SmallBusinessAdministrationWPF.Model;
using SmallBusinessAdministrationWPF.Repositories;
using System;
using System.Threading;
using System.Windows;

namespace SmallBusinessAdministrationWPF.ViewModel
{
    public class MainViewModel : ViewModelBase
    {
        private UserAccountModel _currentUserAccount;
        private IUserRepository userRepository;

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

        public MainViewModel()
        {
            userRepository = new UserRepository();
            CurrentUserAccount = new UserAccountModel();
            LoadCurrentUserData();
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
