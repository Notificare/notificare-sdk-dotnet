using Sample.ViewModels;

namespace Sample.Pages.Tags;

public partial class TagsPage : ContentPage
{
    public TagsPage()
    {
        InitializeComponent();

        UpdateNoDataLabelVisibility();
    }
    
    private void UpdateNoDataLabelVisibility()
    {
        var viewModel = (TagsViewModel)BindingContext;

        NoTagsLabel.IsVisible = viewModel.Tags.Count == 0;

        viewModel.PropertyChanged += (sender, e) =>
        {
            if (e.PropertyName == nameof(TagsViewModel.Tags))
            {
                MainThread.InvokeOnMainThreadAsync(() => NoTagsLabel.IsVisible = viewModel.Tags.Count == 0);
            }
        };
    }
}