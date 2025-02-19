using CommunityToolkit.Mvvm.ComponentModel;
using NotificareSdk.InAppMessaging;

namespace Sample.ViewModels;

public partial class InAppMessagingViewModel : ObservableObject
{
    [ObservableProperty] private bool _evaluateContext;
    [ObservableProperty] private bool _suppressed;

    public InAppMessagingViewModel()
    {
        Suppressed = NotificareInAppMessaging.HasMessagesSuppressed;
    }

    partial void OnSuppressedChanged(bool suppressed)
    {
        SetMessagesSuppressed(suppressed);
    }

    private void SetMessagesSuppressed(bool  suppressed)
    {
      NotificareInAppMessaging.SetMessagesSuppressed(suppressed, EvaluateContext);
    }
}