using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using ContactAppStud.Resources;
using System.Net;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
namespace ContactAppStud.ViewModels
{

    //Classe comprenant la liste des models de vue contact
    public class ContactsViewModel : INotifyPropertyChanged
    {
        

     
        public ContactsViewModel()
        {
        
            this.Items = new ObservableCollection<ContactViewModel>();
            
        }

  

        /// <summary>
        /// Collection pour les objets ContactViewModel.
        /// </summary>
        public ObservableCollection<ContactViewModel> Items { get; private set; }
    
        // Propriété de valeur pour les samples sur l'IDE
        private string _sampleProperty = "Sample Runtime Property Value";
        /// <summary>
        /// Exemple de propriété ViewModel ; cette propriété est utilisée dans la vue pour afficher sa valeur à l'aide d'une liaison
        /// </summary>
        /// <returns></returns>
        public string SampleProperty
        {
            get
            {
                return _sampleProperty;
            }
            set
            {
                if (value != _sampleProperty)
                {
                    _sampleProperty = value;
                    NotifyPropertyChanged("SampleProperty");
                }
            }
        }

        /// <summary>
        /// Exemple de propriété qui retourne une chaîne localisée
        /// </summary>
        public string LocalizedSampleProperty
        {
            get
            {
                return AppResources.SampleProperty;
            }
        }

     
        /// <summary>
        /// Crée et ajoute quelques objets ItemViewModel dans la collection Items.
        /// </summary>
      

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