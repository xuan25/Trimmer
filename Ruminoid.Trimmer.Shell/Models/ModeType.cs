using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Ruminoid.Trimmer.Shell.Models
{

    public sealed class ModeType : INotifyPropertyChanged
    {

        #region Static

        public static ObservableCollection<ModeType> KType = new ObservableCollection<ModeType>()
        {
            new ModeType()
            {
                Key = "k",
                Name = "\\k - 逐字出现",
                Description = "在高亮前以次要颜色与透明度填充，音节开始时立刻切换为主要颜色与透明度。\n绝大多数情况下都应该使用这个标签。"
            },
            new ModeType()
            {
                Key = "K",
                Name = "\\K - 擦除出现",
                Description = "在高亮前以次要颜色与透明度填充，音节开始时以从左向右的擦除效果切换为主要颜色与透明度。"
            },
            new ModeType()
            {
                Key = "kf",
                Name = "\\kf",
                Description = "在高亮前以次要颜色与透明度填充，音节开始时立刻切换为主要颜色与透明度。"
            },
            new ModeType()
            {
                Key = "ko",
                Name = "\\ko",
                Description = "与 \"k\" 相似，但在高亮前音节的边框/边线不会显示，而在音节开始时会立刻出现。"
            }
        };

        public static ObservableCollection<ModeType> PreType = new ObservableCollection<ModeType>()
        {
            new ModeType()
            {
                Key = "SkipData",
                Name = "普通",
                Description = "切割单字。\n绝大多数情况下都应该使用这个预处理器。"
            },
            new ModeType()
            {
                Key = "English",
                Name = "分词",
                Description = "通过空格分词。英语歌词请使用这个预处理器。"
            }
        };

        #endregion

        #region DataContext

        private string _key = "";

        public string Key
        {
            get => _key;
            set
            {
                _key = value;
                OnPropertyChanged();
            }
        }

        private string _description = "";

        public string Description
        {
            get => _description;
            set
            {
                _description = value;
                OnPropertyChanged();
            }
        }

        private string _name = "";

        public string Name
        {
            get => _name;
            set
            {
                _name = value;
                OnPropertyChanged();
            }
        }

        #endregion

        #region PropertyChanged

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion

    }

}
