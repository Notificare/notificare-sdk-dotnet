using CommunityToolkit.Mvvm.ComponentModel;
using NotificareSdk.Assets;
using NotificareSdk.Assets.Core.Models;

namespace Sample.ViewModels;

public partial class AssetsGroupViewModel : ObservableObject
{

    [ObservableProperty] private IList<NotificareAsset> _assets;
    
    public async void FetchAssets(string group)
    {
        try
        {
            Assets = await NotificareAssets.FetchAsync(group);
            Console.WriteLine("Successfully fetched assets.");
        }
        catch (Exception e)
        {
            Assets = Array.Empty<NotificareAsset>();
            Console.WriteLine($"Error fetching assets: {e.Message}.");
        }
    }
}