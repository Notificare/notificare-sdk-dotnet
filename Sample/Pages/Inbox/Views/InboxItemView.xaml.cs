using NotificareSdk.Inbox.Core.Models;

namespace Sample.Pages.Inbox.Views;

public partial class InboxItemView : ContentView
{
    public static readonly BindableProperty ItemProperty =
        BindableProperty.Create(nameof(Item), typeof(NotificareInboxItem), typeof(InboxItemView));

    public NotificareInboxItem Item
    {
        get => (NotificareInboxItem)GetValue(ItemProperty);
        set => SetValue(ItemProperty, value);
    }

    public InboxItemView()
    {
        InitializeComponent();
        BindingContext = this;
    }
}