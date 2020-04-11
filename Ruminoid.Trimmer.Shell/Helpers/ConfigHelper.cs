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

        private bool editorShowEndOfLine;

        public bool EditorShowEndOfLine
        {
            get => editorShowEndOfLine;
            set
            {
                editorShowEndOfLine = value;
                OnPropertyChanged();
            }
        }

        private bool editorWordWrap;

        public bool EditorWordWrap
        {
            get => editorWordWrap;
            set
            {
                editorWordWrap = value;
                OnPropertyChanged();
            }
        }

        private bool editorShowSpaces;

        public bool EditorShowSpaces
        {
            get => editorShowSpaces;
            set
            {
                editorShowSpaces = value;
                OnPropertyChanged();
            }
        }

        private bool editorShowLineNumbers;

        public bool EditorShowLineNumbers
        {
            get => editorShowLineNumbers;
            set
            {
                editorShowLineNumbers = value;
                OnPropertyChanged();
            }
        }

        private double editorFontSize = 12;

        public double EditorFontSize
        {
            get => editorFontSize;
            set
            {
                editorFontSize = value;
                OnPropertyChanged();
            }
        }

        #endregion

        #region ExportConfig

        private bool exportAverageWords;

        public bool ExportAverageWords
        {
            get => exportAverageWords;
            set
            {
                exportAverageWords = value;
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
