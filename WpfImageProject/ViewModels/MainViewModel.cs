using System.Windows.Input;
using WpfImageNet8.Infrastructure;
using System.IO;
using System.Windows;

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
        private ICommand _loadImageCommand;
        private ICommand _deleteImageCommand;

        public MainViewModel()
        {
        }

        public string ImagePath
        {
            get => _imagePath;
            set 
            { 
                _imagePath = value;
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
