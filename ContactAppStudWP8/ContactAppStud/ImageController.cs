using ContactAppStud.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.IsolatedStorage;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace ContactAppStud
{
    class ImageController
    {


        public void SaveImage(BitmapImage bmp, int orientation, int quality, string fileName)
        {

            using (var isolatedStorage = IsolatedStorageFile.GetUserStoreForApplication())
            {
              
                IsolatedStorageFileStream fileStream = isolatedStorage.CreateFile(fileName + ".jpg");
                WriteableBitmap wb = new WriteableBitmap(bmp);
                Extensions.SaveJpeg(wb, fileStream, wb.PixelWidth, wb.PixelHeight, 0, 100);
                fileStream.Close();

            }
        }

        public void LoadImageUrl(string url, string i)
        {

            // Use WebClient to download web server's images.
            WebClient webClientImg = new WebClient();
            webClientImg.OpenReadCompleted += webClientImg_OpenReadCompleted;
            webClientImg.OpenReadAsync(new Uri(url, UriKind.Absolute), i);


        }

        public void webClientImg_OpenReadCompleted(object sender, OpenReadCompletedEventArgs e)
        {
            BitmapImage bmp = new BitmapImage();
            bmp.SetSource(e.Result);
            int i = int.Parse(e.UserState as string);
            SaveImage(bmp, 1, 100, App.ViewModel.Items[i].Mail);
            App.ViewModel.Items[i].Image = LoadImageFromIsolatedStorage(App.ViewModel.Items[i].Mail);

        }
    

        public BitmapImage LoadImageFromIsolatedStorage(string fileName)
        {
            // The image will be read from isolated storage into the following byte array
            byte[] data;

            // Read the entire image in one go into a byte array
            try
            {
                using (IsolatedStorageFile isf = IsolatedStorageFile.GetUserStoreForApplication())
                {
                    // Open the file - error handling omitted for brevity
                    // Note: If the image does not exist in isolated storage the following exception will be generated:
                    // System.IO.IsolatedStorage.IsolatedStorageException was unhandled 
                    // Message=Operation not permitted on IsolatedStorageFileStream 
                    using (IsolatedStorageFileStream isfs = isf.OpenFile(fileName + ".jpg", FileMode.Open, FileAccess.Read))
                    {
                        // Allocate an array large enough for the entire file
                        data = new byte[isfs.Length];
                        // Read the entire file and then close it
                        isfs.Read(data, 0, data.Length);
                        isfs.Close();
                    }
                }

                // Create memory stream and bitmap
                MemoryStream ms = new MemoryStream(data);
                BitmapImage bi = new BitmapImage();

                // Set bitmap source to memory stream
                bi.SetSource(ms);
             

                return bi;

            }
            catch (Exception e)
            {
                return null;
            }
        }
    }
}
