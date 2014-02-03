using ContactAppStud.ViewModels;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.IsolatedStorage;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace ContactAppStud
{

  public  class JsonController
  {
      string jsonstring = @"";
      List<ContactViewModel> contacts = new List<ContactViewModel>();
      IsolatedStorageFile myIsolatedStorage = IsolatedStorageFile.GetUserStoreForApplication();
      ImageController imageController = new ImageController();
   public void  SaveJson(string content){

        using (IsolatedStorageFileStream isoStream = new IsolatedStorageFileStream("Contact.json", FileMode.Create, myIsolatedStorage))
        {
            StreamWriter writer = new StreamWriter(isoStream);
            writer.Write(content);
            writer.Close();
        }
      
      }
    public void  LoadJsonUrl (string url)
   {
       WebClient webClient = new WebClient();
       webClient.DownloadStringCompleted += webClient_DownloadStringCompleted;
       webClient.DownloadStringAsync(new Uri(url, UriKind.Absolute));
    
    }
// Une fois le Json téléchargé
    void webClient_DownloadStringCompleted(object sender, DownloadStringCompletedEventArgs e)
    {
        //On formate le fichier avant de l'enregistrer
        jsonstring = e.Result;
        jsonstring = jsonstring.Remove(0, 1);
        jsonstring = jsonstring.Remove(jsonstring.Length - 2, 1);
        jsonstring = jsonstring.Remove(jsonstring.IndexOf('"'), 11);
        //On enregistre
        SaveJson(jsonstring);
        //On charge
        LoadJsonFromStorage();
    }

  public  void LoadJsonFromStorage()
    {
      
      string content;
    using (StreamReader reader = new StreamReader(myIsolatedStorage.OpenFile("Contact.json", FileMode.Open)))
    {

        content = reader.ReadToEnd();
    }

        int i = 0;
      
        contacts = JsonConvert.DeserializeObject<List<ContactViewModel>>(content);
        while (i < contacts.Count)
        {
            contacts[i].ID = i.ToString();
            App.ViewModel.Items.Add(contacts[i]);
            if (myIsolatedStorage.FileExists(App.ViewModel.Items[i].Mail + ".jpg")) { }
            
            else  
            { 
            
            }
            App.ViewModel.Items[i].Image = imageController.LoadImageFromIsolatedStorage(App.ViewModel.Items[i].Mail);

  
            imageController.LoadImageUrl(contacts[i].Picture, i.ToString());
            i++;

        }
    }


    }
}
