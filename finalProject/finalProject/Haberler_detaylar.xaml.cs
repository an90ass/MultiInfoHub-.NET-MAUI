namespace finalProject;

public partial class Haberler_detaylar : ContentPage
{
    Item haber;

    public Haberler_detaylar(Item item)
    {
        InitializeComponent();
        haber = item;
        LoadWebView();
    }

    private void LoadWebView()
    {
        if (!string.IsNullOrEmpty(haber?.link))
        {
            webView.Source = new Uri(haber.link);
        }
        else
        {
            webView.Source = "about:blank";
        }
    }

    private async void Paylas(object sender, EventArgs e)

    {
        shareButtonFrame.BackgroundColor = Colors.Green;
       

        await shareUri(haber.link, Share.Default);
    }

    private async Task shareUri(string link, IShare share)
    {
        await Share.RequestAsync(new ShareTextRequest
        {
            Uri = link
        });
        Title = haber.title;
    }

    private async void Geri(object sender, EventArgs e)
    {
        geriButtonFrame.BackgroundColor = Colors.Red;
        await Navigation.PopModalAsync();
    }

   
}

