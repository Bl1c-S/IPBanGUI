using WPF_IPBanUtility.Properties;
using System.Collections.ObjectModel;
using static Logic_IPBanUtility.Services.WinServicesController;
using Wpf.Ui.Controls;
using Wpf.Ui.Common;
using CommunityToolkit.Mvvm.ComponentModel;
using System.Windows.Input;
using CommunityToolkit.Mvvm.Input;

namespace WPF_IPBanUtility;

public class ServiceViewModel : ViewModelBase
{
     public string Name { get => _service.Name; }
     public ServiceStatus DisplayStatus { get; private set; } = new();
     private Service _service;

     public string Update { get; } = ButtonNames.Update;
     public string Start { get; } = ButtonNames.Start;
     public string Stop { get; } = ButtonNames.Stop;
     public bool StartButtonEnable { get => _service.Status == ServiceProcessStatus.Stopped; }
     public bool StopButtonEnable { get => _service.Status == ServiceProcessStatus.Running; }
     public ObservableCollection<Button> Buttons { get; set; } = new();

     public ServiceViewModel(Service service)
     {
          _service = service;
          _service.StatusChanged += SetStatus;

          IUpdateCommand = new AsyncRelayCommand(_service.Update);
          IStartCommand = new AsyncRelayCommand(_service.Start);
          IStopCommand = new AsyncRelayCommand(_service.Stop);
          SetStatus();
     }
     public ICommand IUpdateCommand { get; }
     public ICommand IStartCommand { get; }
     public ICommand IStopCommand { get; }


     private void SetStatus()
     {
          DisplayStatus.SetStatus(_service.Status);
          OnPropertyChanged(nameof(StartButtonEnable));
          OnPropertyChanged(nameof(StopButtonEnable));
     }
     public override void Dispose()
     {
          _service.StatusChanged -= SetStatus;
          base.Dispose();
     }

     public class ServiceStatus : ObservableObject
     {
          public string Name { get; private set; }
          public string Collor { get; private set; }
          public SymbolRegular Icon { get; private set; }

          public ServiceStatus()
          {
               Name = Status.Running;
               Collor = Collors.GreanCollor;
               Icon = SymbolRegular.PlayCircle20;
          }
          public void SetStatus(ServiceProcessStatus status)
          {
               switch (status)
               {
                    case ServiceProcessStatus.Running:
                         Name = Status.Running;
                         Collor = Collors.GreanCollor;
                         Icon = SymbolRegular.CheckmarkCircle20;
                         break;
                    case ServiceProcessStatus.Starting:
                         Name = Status.Starting;
                         Collor = Collors.YelowCollor;
                         Icon = SymbolRegular.PlayCircle20;
                         break;
                    case ServiceProcessStatus.Stopped:
                         Name = Status.Stoped;
                         Collor = Collors.ReadCollor;
                         Icon = SymbolRegular.RecordStop20;
                         break;
                    case ServiceProcessStatus.Stoping:
                         Name = Status.Stoping;
                         Collor = Collors.YelowCollor;
                         Icon = SymbolRegular.Record20;
                         break;
                    case ServiceProcessStatus.UpdatingStatus:
                         Name = Status.Updating;
                         Collor = Collors.YelowCollor;
                         Icon = SymbolRegular.ArrowSyncCircle20;
                         break;
               }
               OnPropertyChanged(nameof(Name));
               OnPropertyChanged(nameof(Collor));
               OnPropertyChanged(nameof(Icon));
          }
     }
}