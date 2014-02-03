using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using ContactAppStud.Resources;
using ContactAppStud.ViewModels;
using Newtonsoft.Json;
using System.Windows.Media.Imaging;
using System.IO;
using Microsoft.Xna.Framework.Media;
using System.IO.IsolatedStorage;
using System.Threading.Tasks;
using Windows.Storage;
using System.Windows.Media;
using System.Windows.Resources;


namespace ContactAppStud
{
    public partial class MainPage : PhoneApplicationPage
    {
       
      
        JsonController jsonController = new JsonController();
       
        String Url = "http://www.appstud.me/test_recrutement/contacts.json";
    

        // Constructeur
        public MainPage()
        {
           
            InitializeComponent();
            DataContext = App.ViewModel;

          

          
            if (App.ViewModel.Items.Count == 0)
            {
             
                try
                {
                    IsolatedStorageFile local = IsolatedStorageFile.GetUserStoreForApplication();

                    // On vérifie si le fichier Contact n'existe pas
                    if (!local.FileExists("Contact.json"))
                    { // On charge le fichier de l'url
                        jsonController.LoadJsonUrl(Url);

                    }
                    else {

                        jsonController.LoadJsonFromStorage();
                    
                    }


                }
                catch
                {

                }
            }

    
        }


   

        // Gérer la sélection modifiée sur LongListSelector
        private void MainLongListSelector_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // Si l'élément sélectionné a la valeur Null (pas de sélection), ne rien faire
            if (MainLongListSelector.SelectedItem == null)
                return;

            // Naviguer vers la nouvelle page
            NavigationService.Navigate(new Uri("/DetailsPage.xaml?selectedItem=" + (MainLongListSelector.SelectedItem as ContactViewModel).ID, UriKind.Relative));

            // Réinitialiser l'élément sélectionné sur Null (pas de sélection)
            MainLongListSelector.SelectedItem = null;
         
        }

     
        private void StackPanel_Hold(object sender, System.Windows.Input.GestureEventArgs e)
        {
            ContactViewModel itemViewModel = (sender as StackPanel).DataContext as ContactViewModel;
        
            MessageBoxResult mess = MessageBox.Show("Supprimer "+ itemViewModel.Nom +" ?", "Suppression", MessageBoxButton.OKCancel);
            if (mess == MessageBoxResult.OK) 
            {
                App.ViewModel.Items.Remove(itemViewModel);
              
            }

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
           // webClient.DownloadStringAsync(new Uri(Url, UriKind.Absolute));

        }


       
     
    }
}