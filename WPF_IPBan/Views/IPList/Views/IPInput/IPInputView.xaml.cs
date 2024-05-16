using System.Windows.Controls;
using System.Windows.Input;

namespace WPF_IPBanUtility;

public partial class IPInputView : UserControl
{
     public IPInputView()
     {
          InitializeComponent();
     }

     private void TextBox_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
     {
          if (e.Key == Key.Enter)
               if (DataContext is IPInputViewModel viewModel)
                    if (viewModel.IPValidator.IsValidIP)
                         viewModel.AddNewIP();
     }
}
