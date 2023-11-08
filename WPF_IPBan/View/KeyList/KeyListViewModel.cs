using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPF_IPBanUtility
{
    internal class KeyListViewModel : ViewModelBase
    {
          private readonly NavigationService _navigationService;

          public KeyListViewModel(NavigationService navigationService)
          {
               _navigationService = navigationService;
               _navigationService.OnCurrentChanged += IamSiska;
          }
          private void IamSiska()
          {
          }

          public override void Dispose()
          {
               _navigationService.OnCurrentChanged -= IamSiska;
               base.Dispose();
          }
     }
}
