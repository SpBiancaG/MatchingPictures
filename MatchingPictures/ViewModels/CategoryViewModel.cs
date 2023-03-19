using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using MatchingPictures.Models;
using System.Windows.Input;
using MatchingPictures.Helps;
using MatchingPictures.ViewModels;
using MatchingPictures.Views;
using MatchingPictures;

namespace MatchingPictures.ViewModels
{
    public class CategoryViewModel : NotifyViewModel
    {
        private bool fromSignUp = false;
        private User user;
        public ObservableCollection<Category> Categories { get; set; }

        public CategoryViewModel()
        {
            Categories = new ObservableCollection<Category>();
            Categories.Add(Category.All);
            
        }

        public CategoryViewModel(User user, bool fromSignUp = false)
        {
            this.user = user;
            Categories = new ObservableCollection<Category>();
            Categories.Add(Category.All);
            
            this.fromSignUp = fromSignUp;
        }

        private Category selectedCategory = Category.None;
        public Category SelectedCategory
        {
            get
            {
                return selectedCategory;
            }
            set
            {
                selectedCategory = value;
                CategorySelected(selectedCategory);
                NotifyPropertyChanged("SelectedCategory");
            }
        }

        public void CategorySelected(Category category)
        {
            user.GameProperty.CategoryProperty = category;
            
            HomeWindow window = new HomeWindow();
            HomeViewModel homeVM = new HomeViewModel(user);
            window.DataContext = homeVM;
            App.Current.MainWindow.Close();
            App.Current.MainWindow = window;
            window.Show();
        }

        private ICommand backCommand;
        public ICommand BackCommand
        {
            get
            {
                if (backCommand == null)
                {
                    backCommand = new RelayCommand(Back);
                }
                return backCommand;
            }
        }

        public void Back(object param)
        {
            if (fromSignUp == false)
            {
                ChooseWindow chooseWindow = new ChooseWindow();
                ChooseViewModel chooseVM = new ChooseViewModel(user);
                chooseWindow.DataContext = chooseVM;
                App.Current.MainWindow.Close();
                App.Current.MainWindow = chooseWindow;
                chooseWindow.Show();
            }
            else
            {
                SignInWindow signInWindow = new SignInWindow();
                SignInViewModel signInVM = new SignInViewModel();
                signInWindow.DataContext = signInVM;
                App.Current.MainWindow.Close();
                App.Current.MainWindow = signInWindow;
                signInWindow.Show();
            }
        }
    }
}
