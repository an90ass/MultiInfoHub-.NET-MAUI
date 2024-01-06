using System.Collections.ObjectModel;
using System.Text.Json;
namespace finalProject;

public partial class HavaDurumu : ContentPage
{
	public HavaDurumu()
	{
		InitializeComponent();

        if (File.Exists(dosyaismi))
        {
            string data = File.ReadAllText(dosyaismi);
        }
        Grad.ItemsSource = Sehirler;
    }
    static string dosyaismi = Path.Combine(FileSystem.Current.AppDataDirectory, "hdata.json");
    static ObservableCollection<SehirHavaDurumu> Sehirler = new ObservableCollection<SehirHavaDurumu>();

    private async void add_sehir(Object sender, EventArgs e)
    {
        string sehir = await DisplayPromptAsync("Şehir :", "Şehir ismi giriniz :", "Devam", "İptal");
        sehir = sehir.ToUpper(System.Globalization.CultureInfo.CurrentCulture);
        sehir = sehir.Replace('Ç', 'C');
        sehir = sehir.Replace('Ğ', 'G');
        sehir = sehir.Replace('İ', 'I');
        sehir = sehir.Replace('Ö', 'O');
        sehir = sehir.Replace('Ü', 'U');
        sehir = sehir.Replace('Ş', 'S');

        Sehirler.Add(new SehirHavaDurumu() { Name = sehir });
        string data = JsonSerializer.Serialize(Sehirler);
        File.WriteAllText(dosyaismi, data);

    }

    private async void Sil(Object sender, EventArgs e)
    {
        var sil = sender as ImageButton;
        if (sil != null)
		{
            var pop = Sehirler.First(o => o.Name == sil.CommandParameter.ToString());


            Sehirler.Remove(pop);


        }

    }


}
public class SehirHavaDurumu
{
    public string Name { get; set; }
    public string Source => $"https://www.mgm.gov.tr/sunum/tahmin-klasik-5070.aspx?m={Name}&basla=1&bitir=5&rC=111&rZ=fff";
}

