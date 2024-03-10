using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.Imaging;
using System.Xml;

namespace CafeApplication.utils
{
    public class ImageLoader
    {
        OpenFileDialog Ofd { get; set; }

        public FileInfo ImageInfo { get; private set; }
        public BitmapImage BitmapImage { get; private set; }

        private string _uniqueName;
        public string PhotoName { get { return _uniqueName; }  }
        public ImageLoader(string initialPhotoName = null) {

            Ofd = new OpenFileDialog
            {
                Filter = "Files (.jpg)|*.jpg"
            };
            _uniqueName = initialPhotoName;
        }
        public void SaveFile(string _imageDirectory = null)
        {
            File.Copy(ImageInfo.FullName, $"{_imageDirectory ?? Directory.GetCurrentDirectory()}\\images\\{_uniqueName}");
        }
        public string SaveAndReturnFileName(string _imageDirectory = null)
        {
            File.Copy(ImageInfo.FullName, $"{_imageDirectory ?? Directory.GetCurrentDirectory()}\\images\\{_uniqueName}");
            return _uniqueName;
        }
        public BitmapImage ShowDialog(int fileSize)
        {
            if (Ofd.ShowDialog() == true)
            {
                FileInfo image = new FileInfo(Ofd.FileName);

                if (image.Length > 8 * 1024 * 1024 * fileSize)
                {
                    MessageBox.Show("Файл не может весить больше 5 мб.");
                    return null;
                }

                ImageInfo = image;
                BitmapImage = new BitmapImage( new Uri(image.FullName));

                _uniqueName = Guid.NewGuid().ToString() + ImageInfo.Extension;

                return BitmapImage;
            }
            return null;
        }
    }
}
