using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace MatchingPictures.Models
{
    public enum Category
    {
        None,
        Basic
        
    }

    [Serializable]
    public class Game : INotifyPropertyChanged
    {

        #region INotifyPropertyChanged Members

        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        #endregion

       
        private Category category;
        [XmlAttribute]
        
        private bool savedGame;

        public Game()
        {
            
            CategoryProperty = Category.None;
            
            savedGame = false;
        }

        public Category CategoryProperty
        {
            get
            {
                return category;
            }
            set
            {
                category = value;
                NotifyPropertyChanged("CategoryProperty");
            }
        }
        public bool SavedGame
        {
            get
            {
                return savedGame;
            }
            set
            {
                savedGame = value;
                NotifyPropertyChanged("SavedGame");
            }
        }
    }
}
