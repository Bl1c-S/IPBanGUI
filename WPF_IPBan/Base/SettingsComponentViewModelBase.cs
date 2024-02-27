using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPF_IPBanUtility.Base
{
     public abstract class SettingsComponentViewModelBase : ViewModelBase, ISettingsVMComponent
     {
          public string? Title { get; protected set; }
          public abstract void Save();
     }
}
