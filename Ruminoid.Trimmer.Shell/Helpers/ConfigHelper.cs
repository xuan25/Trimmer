using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace Ruminoid.Trimmer.Shell.Helpers
{

    /// <summary>
    /// Config Helper.
    /// </summary>
    public static class ConfigHelper
    {

        /// <summary>
        /// The current config.
        /// </summary>
        public static Config Current { get; } = OpenConfig();

        public static string UserDataFolder = Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
            "Il Harper\\Ruminoid\\Trimmer");

        private static Config OpenConfig()
        {
            if (UserDataFolder is null) UserDataFolder = Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
                "Il Harper\\Ruminoid\\Trimmer");
            Directory.CreateDirectory(UserDataFolder);
            FileStream fs = new FileStream(
                Path.Combine(UserDataFolder, "config.dat"), FileMode.OpenOrCreate, FileAccess.Read,
                FileShare.ReadWrite);
            BinaryFormatter formatter = new BinaryFormatter();
            try
            {
                Config config = formatter.Deserialize(fs) as Config;
                fs.Close();
                return config;
            }
            catch (SerializationException)
            {
                fs.Close();
                return new Config();
            }
        }

        /// <summary>
        /// Save the config.
        /// </summary>
        public static void SaveConfig()
        {
            FileStream fs = new FileStream(
                Path.Combine(UserDataFolder, "config.dat"), FileMode.OpenOrCreate, FileAccess.ReadWrite,
                FileShare.ReadWrite);
            BinaryFormatter formatter = new BinaryFormatter();
            formatter.Serialize(fs, Current);
            fs.Close();
        }

    }

    [Serializable]
    public class Config: INotifyPropertyChanged
    {

        #region EditorConfig

        //private bool _editorShowEndOfLine;

        //public bool EditorShowEndOfLine
        //{
        //    get => _editorShowEndOfLine;
        //    set
        //    {
        //        _editorShowEndOfLine = value;
        //        OnPropertyChanged();
        //    }
        //}

        //private bool _editorWordWrap;

        //public bool EditorWordWrap
        //{
        //    get => _editorWordWrap;
        //    set
        //    {
        //        _editorWordWrap = value;
        //        OnPropertyChanged();
        //    }
        //}

        //private bool _editorShowSpaces;

        //public bool EditorShowSpaces
        //{
        //    get => _editorShowSpaces;
        //    set
        //    {
        //        _editorShowSpaces = value;
        //        OnPropertyChanged();
        //    }
        //}

        //private bool _editorShowLineNumbers;

        //public bool EditorShowLineNumbers
        //{
        //    get => _editorShowLineNumbers;
        //    set
        //    {
        //        _editorShowLineNumbers = value;
        //        OnPropertyChanged();
        //    }
        //}

        //private double _editorFontSize = 12;

        //public double EditorFontSize
        //{
        //    get => _editorFontSize;
        //    set
        //    {
        //        _editorFontSize = value;
        //        OnPropertyChanged();
        //    }
        //}

        #endregion

        #region ExportConfig

        private bool _exportAverageWords;

        public bool ExportAverageWords
        {
            get => _exportAverageWords;
            set
            {
                _exportAverageWords = value;
                OnPropertyChanged();
            }
        }

        #endregion

        #region MainWindow

        private bool _hideWelcome;

        public bool HideWelcome
        {
            get => _hideWelcome;
            set
            {
                _hideWelcome = value;
                OnPropertyChanged();
            }
        }

        #endregion

        #region PropertyChanged

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion

    }

}
