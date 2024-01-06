using Firebase.Database;
using Firebase.Database.Query;
using System.Reflection;
using static finalProject.Yapilacaklar;


namespace finalProject;

public partial class Planlama : ContentPage
{
    public Planlama()
    {
        InitializeComponent();
    }
    public bool result { get; set; } = false;
    public Missions Yaplacaklar;
    public Action<Missions> UpdateMethod { get; internal set; }


    public Action<Missions> AddMethod { get; internal set; }

    private void Save(object sender, EventArgs e)
    {
        if (Yaplacaklar == null)
        {
            Yaplacaklar = new Missions()
            {
                Head = Bas.Text,
                Detail = Detay.Text,
                Date = Tarih.Date.ToShortDateString(),
                Hour = Saat.Time.ToString(),
            };
        }
        else
        {
            Yaplacaklar.Head = Bas.Text;
            Yaplacaklar.Detail = Detay.Text;
            Yaplacaklar.Date = Tarih.Date.ToShortDateString();
            Yaplacaklar.Hour = Saat.Time.ToString();
        }

        if (islem)
        {
            // Add operation
            if (AddMethod != null)
                AddMethod(Yaplacaklar);

        }
        else
        {
            // Update operation
            if (UpdateMethod != null)
                UpdateMethod(Yaplacaklar);
            Navigation.PopAsync();


        }

        Navigation.PopModalAsync();
        Navigation.PopAsync();
    }

    private async void Cancel(object sender, EventArgs e)
    {
        if (islem)
        {
            await Navigation.PopModalAsync();
        }
        else
        {
            Navigation.PopAsync();
        }
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();

        if (Yaplacaklar != null)
        {
            Bas.Text = Yaplacaklar.Head;
            Detay.Text = Yaplacaklar.Detail;
            Tarih.Date = DateTime.Parse(Yaplacaklar.Date);
            Saat.Time = TimeSpan.Parse(Yaplacaklar.Hour);
        }
    }
}