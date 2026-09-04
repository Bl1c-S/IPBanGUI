using System.Windows.Forms;

namespace WPF_IPBanUtility;

internal class PreparatoryViewModel : ViewModelBase
{
     public string? SelectFolder()
     {
          using (var dialog = new FolderBrowserDialog())
          {
               DialogResult result = dialog.ShowDialog();
               if (result == DialogResult.OK)
                    return dialog.SelectedPath;
          }
          return null;
     }
}