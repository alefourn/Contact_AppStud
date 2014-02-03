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
using Microsoft.Phone.Tasks;
using System.IO;
using System.Windows.Media.Imaging;
using System.IO.IsolatedStorage;
using Microsoft.Xna.Framework.Media;
namespace ContactAppStud
{
    public partial class DetailsPage : PhoneApplicationPage
    {
        int i;
        ContactViewModel itemViewModel;
        CameraCaptureTask photo;
        ImageController imageController;
        // Constructeur
        public DetailsPage()
        {
            InitializeComponent();
            imageController = new ImageController();
            photo = new CameraCaptureTask();
            photo.Completed += new EventHandler<PhotoResult>(photo_Completed);
            // Exemple de code pour la localisation d'ApplicationBar
            //BuildLocalizedApplicationBar();
        }

        // Lors de l'accès à la page, affectez l'élément sélectionné dans la liste au contexte de données
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            if (DataContext == null)
            {
                string selectedIndex = "";
                if (NavigationContext.QueryString.TryGetValue("selectedItem", out selectedIndex))
                {
                    int i = 0;
                    while (i < App.ViewModel.Items.Count)
                    {
                        if (App.ViewModel.Items[i].ID == selectedIndex) {

                            DataContext = App.ViewModel.Items[i];
                            itemViewModel = App.ViewModel.Items[i];
                        }
                        i++;
                    
                    }
                 
                }
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            App.ViewModel.Items.Remove(itemViewModel);


        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            EmailComposeTask emailComposeTask = new EmailComposeTask();
            emailComposeTask.Subject = "sujet";
            emailComposeTask.Body = "message";
            emailComposeTask.To = itemViewModel.Mail;
            emailComposeTask.Show();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {          
            photo.Show();
        }


        void photo_Completed(object sender, Microsoft.Phone.Tasks.PhotoResult e)
        {
            if (e.TaskResult == TaskResult.OK)
            {
             BitmapImage bmp = new BitmapImage();
             bmp.SetSource(e.ChosenPhoto);
             imageController.SaveImage(bmp, 1, 100, itemViewModel.Mail);
             itemViewModel.Image = imageController.LoadImageFromIsolatedStorage(itemViewModel.Mail);

                }
           
             
              


            }
        
    

          private void img_DoubleTap(object sender, System.Windows.Input.GestureEventArgs e)
          {
              photo.Show();
          }

  
        // Exemple de code pour la conception d'une ApplicationBar localisée
        //private void BuildLocalizedApplicationBar()
        //{
        //    // Définit l'ApplicationBar de la page sur une nouvelle instance d'ApplicationBar.
        //    ApplicationBar = new ApplicationBar();

        //    // Crée un bouton et définit la valeur du texte sur la chaîne localisée issue d'AppResources.
        //    ApplicationBarIconButton appBarButton = new ApplicationBarIconButton(new Uri("/Assets/AppBar/appbar.add.rest.png", UriKind.Relative));
        //    appBarButton.Text = AppResources.AppBarButtonText;
        //    ApplicationBar.Buttons.Add(appBarButton);

        //    // Crée un nouvel élément de menu avec la chaîne localisée d'AppResources.
        //    ApplicationBarMenuItem appBarMenuItem = new ApplicationBarMenuItem(AppResources.AppBarMenuItemText);
        //    ApplicationBar.MenuItems.Add(appBarMenuItem);
        //}
    }
}