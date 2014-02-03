using System;
using System.ComponentModel;
using System.Diagnostics;
using System.IO.IsolatedStorage;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
namespace ContactAppStud.ViewModels
{
    //Classe du model d'un contact
    public class ContactViewModel : INotifyPropertyChanged
    {
        private string _id;
        /// <summary>
        /// Propriété id d'un contact ; cette propriété est utilisée pour identifier l'objet.
        /// </summary>
        /// <returns></returns>
        public string ID
        {
            get
            {
                return _id;
            }
            set
            {
                if (value != _id)
                {
                    _id = value;
                    NotifyPropertyChanged("ID");
                }
            }
        }

        private string _Nom;
        /// <summary>
        /// Exemple de propriété ViewModel ; cette propriété est utilisée dans la vue pour afficher sa valeur à l'aide d'une liaison.
        /// </summary>
        /// <returns></returns>
        public string Nom
        {
            get
            {
                return _Nom;
            }
            set
            {
                if (value != _Nom)
                {
                    _Nom = value;
                    NotifyPropertyChanged("Nom");
                }
            }
        }

        private string _Prenom;
   
        public string Prenom
        {
            get
            {
                return _Prenom;
            }
            set
            {
                if (value != _Prenom)
                {
                    _Prenom = value;
                    NotifyPropertyChanged("Prenom");
                }
            }
        }

        private string _Mail;
   
        public string Mail
        {
            get
            {
                return _Mail;
            }
            set
            {
                if (value != _Mail)
                {
                    _Mail = value;
                    NotifyPropertyChanged("Mail");
                }
            }
        }


        private string _Picture;
 
        public string Picture
        {
            get
            {
                return _Picture;
            }
            set
            {
                if (value != _Picture)
                {
                    _Picture = value;
                    NotifyPropertyChanged("Picture");
                }
            }
        }

        private ImageSource _image;

        public ImageSource Image
        {
            get
            { 
                return _image;
            }
            set
            {
                if (value != _image)
                {
                    _image = value;
                    NotifyPropertyChanged("Image");
                }
            }
        }

   


        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged(String propertyName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (null != handler)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    

    }
}