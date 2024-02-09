using CommunityToolkit.Mvvm.ComponentModel;
using System;

namespace WPF_IPBanUtility;

internal class ViewModelBase : ObservableObject, IDisposable
{
     public bool IsEnable = true;
     public virtual void Dispose()
     {
     }
}
