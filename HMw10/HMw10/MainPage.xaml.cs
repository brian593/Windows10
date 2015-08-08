using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Devices.Enumeration;
using Windows.Devices.Sensors;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Foundation.Metadata;
using Windows.Graphics.Display;
using Windows.Media;
using Windows.Media.Capture;
using Windows.Storage;
using Windows.System.Display;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace HMw10
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page


    {

 
        public  MainPage()
        {
            this.InitializeComponent();
            
        }

        async private void CameraCapture()
        {
            // Remember to set permissions in the manifest!

            // using Windows.Media.Capture;
            // using Windows.Storage;
            // using Windows.UI.Xaml.Media.Imaging;

            CameraCaptureUI cameraUI = new CameraCaptureUI();

            cameraUI.PhotoSettings.AllowCropping = false;
            cameraUI.PhotoSettings.MaxResolution = CameraCaptureUIMaxPhotoResolution.MediumXga;

            Windows.Storage.StorageFile capturedMedia =
                await cameraUI.CaptureFileAsync(CameraCaptureUIMode.Photo);

            if (capturedMedia != null)
            {
                using (var streamCamera = await capturedMedia.OpenAsync(FileAccessMode.Read))
                {

                    BitmapImage bitmapCamera = new BitmapImage();
                    bitmapCamera.SetSource(streamCamera);
                    // To display the image in a XAML image object, do this:
                    // myImage.Source = bitmapCamera;

                    // Convert the camera bitap to a WriteableBitmap object, 
                    // which is often a more useful format.

                    int width = bitmapCamera.PixelWidth;
                    int height = bitmapCamera.PixelHeight;

                    WriteableBitmap wBitmap = new WriteableBitmap(width, height);

                    using (var stream = await capturedMedia.OpenAsync(FileAccessMode.Read))
                    {
                        wBitmap.SetSource(stream);
                    }
                }
            }
        }


        private void HamburgerButton_Click(object sender, RoutedEventArgs e)
        {
            MySplitView.IsPaneOpen = !MySplitView.IsPaneOpen;

        }

        private void cameraMenuButton_Click(object sender, RoutedEventArgs e)
        {
            CameraCapture();
        }

        private void HomeMenuButton_Click(object sender, RoutedEventArgs e)
        {
            double remainingWidth = Windows.UI.Xaml.Window.Current.Bounds.Width-50;
            double remainingHeiht = Windows.UI.Xaml.Window.Current.Bounds.Height-20;
            MiWeb.Width = remainingWidth;
            MiWeb.Height = remainingHeiht;
            Uri uri = new Uri ("https://brian593.wordpress.com");

            MiWeb.Navigate(uri);

        }
    }
}
