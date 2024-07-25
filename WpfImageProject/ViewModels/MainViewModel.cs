using System.Windows.Input;
using WpfImageNet8.Infrastructure;
using System.IO;
using System.Windows;
using System.Windows.Media.Imaging;

namespace WpfImageNet8.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        private const string SourceFolderImagePath = "Files";
        private const string ImageName = "sample";
        private const string ImageExt = "jpeg";
        private const string DestinationFolderPath = "Photos";
		
        private string _sourceImageFile = Path.Combine(SourceFolderImagePath, $"{ImageName}.{ImageExt}");
        private string _imagePath;
        private string _imageFileName;
        private string _imageSecondFileName;
        private BitmapImage _imageSecond;

        private ICommand _loadImageCommand;
        private ICommand _deleteImageCommand;

        public string ImagePath
        {
            get => _imagePath;
            set 
            { 
                _imagePath = value;
                OnPropertyChanged();
            }
        }
        
        public string ImageFileName
        {
            get => _imageFileName;
            set 
            { 
                _imageFileName = value;
                OnPropertyChanged();
            }
        }
        
        public string ImageSecondFileName
        {
            get => _imageSecondFileName;
            set 
            { 
                _imageSecondFileName = value;
                OnPropertyChanged();
            }
        }

        public BitmapImage ImageSecond
        {
            get => _imageSecond;
            set 
            {
                _imageSecond = value;
                OnPropertyChanged();
            }
        }

        public ICommand LoadImageCommand
        {
            get
            {
                return _loadImageCommand ??= new Command(arg =>
                {
                    var destPath = Path.Combine(DestinationFolderPath, $"{ImageName}-{arg}.{ImageExt}");
                    if (!Directory.Exists(DestinationFolderPath))
                    {
                        Directory.CreateDirectory(DestinationFolderPath);
                    }
                    try
                    {
                        File.Copy(_sourceImageFile, destPath, true);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                    if (File.Exists(destPath))
                    {
                        switch (arg)
                        {
                            case "1":
                                ImagePath = Path.GetFullPath(destPath);
                                ImageFileName = Path.GetFileName(destPath);
                                break;
                            case "2":
                                BitmapImage bitmapImage = new();
                                bitmapImage.BeginInit();
                                bitmapImage.CacheOption = BitmapCacheOption.OnLoad;
                                bitmapImage.UriSource = new Uri(Path.GetFullPath(destPath));
                                bitmapImage.EndInit();
                                bitmapImage.Freeze();
                                ImageSecond = bitmapImage;
                                ImageSecondFileName = Path.GetFileName(destPath);
                                break;
                        }
                    }
                });
            }
        }

        public ICommand DeleteImageCommand
        {
            get
            {
                return _deleteImageCommand ??= new Command(arg =>
                {
                    var destPath = Path.Combine(DestinationFolderPath, $"{ImageName}-{arg}.{ImageExt}");
                    if (File.Exists(destPath))
                    {
                        try
                        {
                            File.Delete(destPath);
                            if (!File.Exists(destPath))
                            {
                                switch (arg)
                                {
                                    case "1":
                                        ImagePath = string.Empty;
                                        ImageFileName = string.Empty;
                                        MessageBox.Show("Image is deleted successfully");
                                        break;
                                    case "2":
                                        ImageSecond = null;
                                        ImageSecondFileName = string.Empty;
                                        MessageBox.Show("Image is deleted successfully");
                                        break;
                                }
                                return;
                            }
                            MessageBox.Show("Deleting file is impossible");
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message);
                        }
                    }
                });
            }
        }
    }
}
