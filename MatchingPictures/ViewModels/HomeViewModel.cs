using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Threading;
using System.Windows;
using System.Runtime;
using MatchingPictures.Models;
using System.Text.RegularExpressions;
using System.Windows.Input;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using System.Windows.Media;
using System.Collections.ObjectModel;
using MatchingPictures.Helps;
using MatchingPictures.ViewModels;
using MatchingPictures.Views;
using MatchingPictures;

namespace MatchingPictures.ViewModels
{
    public class HomeViewModel : NotifyViewModel
    {

        // H E ＿ L ＿
        private User user;

        private Users users;
        private Images images = new Images();
        private DispatcherTimer timer = new DispatcherTimer();
        private bool firstCreation = true;
        private bool resumeGame = false;
        private ObservableCollection<Button> buttons = new ObservableCollection<Button>();
        private SerializationActions serializationActions = new SerializationActions();

        public HomeViewModel()
        {

        }
        public HomeViewModel(User user, bool resumeGame = false)
        {
            users = serializationActions.DeserializeUsers(Constants.UsersFile);

            foreach (var userInList in users.List)
            {
                if (userInList.Name == user.Name)
                {
                    users.List.Remove(userInList);
                    users.List.Add(user);
                    break;
                }
            }
            serializationActions.SerializeUsers(Constants.UsersFile, users);
            users = serializationActions.DeserializeUsers(Constants.UsersFile);
            this.user = user;

        }


        public string Name
        {
            get
            {
                return user.Name;
            }
        }





        public BitmapImage UserImageSource
        {
            get
            {
                return images.Emojis[user.ImageIndex];
            }
        }

















        private ICommand saveCommand;
        public ICommand SaveCommand
        {
            get
            {
                if (saveCommand == null)
                {
                    saveCommand = new RelayCommand(SaveGame);
                }
                return saveCommand;
            }
        }

        public void SaveGame(object param)
        {
            
            
            users = serializationActions.DeserializeUsers(Constants.UsersFile);
            foreach (var userInList in users.List)
            {
                if (user.Name == userInList.Name)
                {
                    users.List.Remove(userInList);
                    break;
                }
            }
            user.GameProperty.SavedGame = true;
            users.List.Add(user);
            serializationActions.SerializeUsers(Constants.UsersFile, users);
            SignInWindow signInWindow = new SignInWindow();
            SignInViewModel signInVM = new SignInViewModel();
            signInWindow.DataContext = signInVM;
            App.Current.MainWindow.Close();
            App.Current.MainWindow = signInWindow;
            signInWindow.Show();
        }

        private ICommand aboutCommand;
        public ICommand AboutCommand
        {
            get
            {
                if (aboutCommand == null)
                {
                    aboutCommand = new RelayCommand(About);
                }
                return aboutCommand;
            }
        }

        public void About(object param)
        {
            
           
            AboutWindow aboutWindow = new AboutWindow();
            aboutWindow.ShowDialog();
            
        }







        private ICommand newCommand;
        public ICommand NewCommand
        {
            get
            {
                if (newCommand == null)
                {
                    newCommand = new RelayCommand(NewPressed);
                }
                return newCommand;
            }
        }

        public void NewPressed(object param)
        {
            
            MessageBoxResult messageBoxResult = MessageBox.Show("If you start a new game this game will count as lost.\nAre you sure you want to start a new game?", "New game", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (messageBoxResult == MessageBoxResult.Yes)
            {
                
                switch (user.GameProperty.CategoryProperty)
                {
                    case Category.All:
                        
                        break;

                    default:
                        break;
                }
                foreach (var userInList in users.List)
                {
                    if (userInList.Name == user.Name)
                    {
                        
                        if (resumeGame)
                        {
                            userInList.GameProperty = new Game();
                        }
                    }
                }
                serializationActions.SerializeUsers(Constants.UsersFile, users);
                CategoryWindow categoryWindow = new CategoryWindow();
                CategoryViewModel categoryVM = new CategoryViewModel(user);
                categoryWindow.DataContext = categoryVM;
                App.Current.MainWindow.Close();
                App.Current.MainWindow = categoryWindow;
                categoryWindow.Show();
            }
           
        }

        private ICommand exitCommand;
        public ICommand ExitCommand
        {
            get
            {
                if (exitCommand == null)
                {
                    exitCommand = new RelayCommand(ExitPressed);
                }
                return exitCommand;
            }
        }

        public void ExitPressed(object param)
        {


            

            foreach (var userInList in users.List)
            {
                if (userInList.Name == user.Name)
                {
                    
                    if (resumeGame)
                    {
                        userInList.GameProperty = new Game();
                    }
                }
            }
            serializationActions.SerializeUsers(Constants.UsersFile, users);
            SignInWindow signInWindow = new SignInWindow();
            SignInViewModel signInVM = new SignInViewModel(users);
            signInWindow.DataContext = signInVM;
            App.Current.MainWindow.Close();
            App.Current.MainWindow = signInWindow;
            signInWindow.Show();
            //}
            //else
            //{
            //StartTimer(seconds);
            //}
        }



        private ObservableCollection<MenuItem> menuItems = new ObservableCollection<MenuItem>();




    }
}
