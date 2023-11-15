using CommunityToolkit.Mvvm.Input;
using System.Windows.Input;

namespace WPF_IPBanUtility;

internal class KeyViewModel : ViewModelBase
{
     public InfoBarViewModel? InfoBarVM { get; set; }
   
     
     private bool _isInfoBarOpen;
     public bool IsInfoBarOpen
     {
          get => _isInfoBarOpen;
          set
          {
               _isInfoBarOpen = value;

               if (_isInfoBarOpen)
                    InfoBarVM = new InfoBarViewModel(_key.Comment);
               else
                    InfoBarVM = null;

               OnPropertyChanged(nameof(InfoBarVM));
               OnPropertyChanged(nameof(IsInfoBarOpen));
          }
     }

     private Logic_IPBanUtility.Key _key;
     public string Name => _key.Name;
     public string Comment { get; }

     private string _value;
     public string Value
     {
          get => _value;
          set
          {
               _value = value;
               OnPropertyChanged(nameof(Value));
          }
     }
     public KeyViewModel(Logic_IPBanUtility.Key key)
     {
          InfoBarVM = new(key.Comment);
          IsInfoBarOpen = false;
          Comment = key.Comment;
          _value = key.Value;

          _key = key;
          ISaveChangedCommand = new RelayCommand(SaveChanged);
          IInfoBarOpenCommand = new RelayCommand(ChangeInfoBarOpen);
     }
     public void ChangeInfoBarOpen()
     {
          IsInfoBarOpen = !IsInfoBarOpen;
     }

     public void SaveChanged()
     {
          _key.InsertValue(_value);
     }

     public ICommand ISaveChangedCommand { get; }
     public ICommand IInfoBarOpenCommand { get; }
}
