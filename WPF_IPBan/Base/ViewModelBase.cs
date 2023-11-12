using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Windows.Controls;

namespace WPF_IPBanUtility;

internal class ViewModelBase : ObservableObject, IDisposable
{
     public bool IsEnable = true;
     public virtual void Dispose()
     {
     }
}
