using System.Text.Json;

namespace finalProject;

public partial class DovizKurlari : ContentPage
{
    public DovizKurlari()
    {
        InitializeComponent();
    }

    private static DovizKurlari instance;
    public static DovizKurlari Page
    {
        get
        {
            if (instance == null)
                instance = new DovizKurlari();
            return instance;
        }
    }

    protected async override void OnAppearing()
    {
        base.OnAppearing();

        await Load();
    }

    AltinDoviz kurlar;

    async Task Load()
    {
        string jsondata = await GetAltinDovizGuncelKurlar();

        kurlar = JsonSerializer.Deserialize<AltinDoviz>(jsondata);

        Sepet.ItemsSource = new List<Doviz>()
        {
            new Doviz()
            {
                doviz_adi = "Dolar",
                doviz_alis = kurlar.USD.alis,
                doviz_satis = kurlar.USD.satis,
                Fark = kurlar.USD.degisim,
                Yon = GetImage(kurlar.USD.d_yon),
            },

            new Doviz()
            {
                doviz_adi = "Euro",
                doviz_alis = kurlar.EUR.alis,
                doviz_satis = kurlar.EUR.satis,
                Fark = kurlar.EUR.degisim,
                Yon = GetImage(kurlar.EUR.d_yon),
            },

            new Doviz()
            {
                doviz_adi = "Sterlin",
                doviz_alis = kurlar.GBP.alis,
                doviz_satis = kurlar.GBP.satis,
                Fark = kurlar.GBP.degisim,
                Yon = GetImage(kurlar.GBP.d_yon),
            },

            new Doviz()
            {
                doviz_adi = "---",
                doviz_alis = "",
                doviz_satis = "",
                Fark = "",
                Yon = "",
            },


            new Doviz()
            {
                doviz_adi = "Gram Altýn",
                doviz_alis = kurlar.GA.alis,
                doviz_satis = kurlar.GA.satis,
                Fark = kurlar.GA.degisim,
                Yon = GetImage(kurlar.GA.d_yon),
            },

            new Doviz()
            {
                doviz_adi = "Çeyrek Altýn",
                doviz_alis = kurlar.C.alis,
                doviz_satis = kurlar.C.satis,
                Fark = kurlar.C.degisim,
                Yon = GetImage(kurlar.C.d_yon),
            },

            new Doviz()
            {
                doviz_adi = "Gümüþ",
                doviz_alis = kurlar.GAG.alis,
                doviz_satis = kurlar.GAG.satis,
                Fark = kurlar.GAG.degisim,
                Yon = GetImage(kurlar.GAG.d_yon),
            },

            new Doviz()
            {
                doviz_adi = "---",
                doviz_alis = "",
                doviz_satis = "",
                Fark = "",
                Yon = "",
            },


            new Doviz()
            {
                doviz_adi = "BTC",
                doviz_alis = kurlar.BTC.alis,
                doviz_satis = kurlar.BTC.satis,
                Fark = kurlar.BTC.degisim,
                Yon = GetImage(kurlar.BTC.d_yon),
            },

            new Doviz()
            {
                doviz_adi = "ETH",
                doviz_alis = kurlar.ETH.alis,
                doviz_satis = kurlar.ETH.satis,
                Fark = kurlar.ETH.degisim,
                Yon = GetImage(kurlar.ETH.d_yon),
            },

            new Doviz()
            {
                doviz_adi = "---",
                doviz_alis = "",
                doviz_satis = "",
                Fark = "",
                Yon = "",
            },

            new Doviz()
            {
                doviz_adi = "BIST100",
                doviz_alis = kurlar.XU100.alis,
                doviz_satis = kurlar.XU100.satis,
                Fark = kurlar.XU100.degisim,
                Yon = GetImage(""),
            },

        };
    }

    private string GetImage(string str)
    {
        if (str.Contains("up"))
            return "up.png";
        if (str.Contains("down"))
            return "down.png";
        if (str.Contains("minus"))
            return "minus.png";

        return "";
    }

    async Task<string> GetAltinDovizGuncelKurlar()
    {
        string url = "https://api.genelpara.com/embed/altin.json";
        HttpClient client = new HttpClient();
        HttpResponseMessage response = await client.GetAsync(url);
        response.EnsureSuccessStatusCode();

        string jsondata = await response.Content.ReadAsStringAsync();
        return jsondata;
    }

    private async void Button_Clicked(object sender, EventArgs e)
    {

        await Load();

        await Task.Delay(1000); 

    }
}

public class Doviz
{
    public string doviz_adi { get; set; }
    public string doviz_alis { get; set; }
    public string doviz_satis { get; set; }
    public string Fark { get; set; }
    public string Yon { get; set; }
}

public class AltinDoviz
{
    public USD USD { get; set; }
    public EUR EUR { get; set; }
    public GBP GBP { get; set; }
    public GA GA { get; set; }
    public C C { get; set; }
    public GAG GAG { get; set; }
    public BTC BTC { get; set; }
    public ETH ETH { get; set; }
    public XU100 XU100 { get; set; }
}

public class BTC
{
    public string satis { get; set; }
    public string alis { get; set; }
    public string degisim { get; set; }
    public string d_oran { get; set; }
    public string d_yon { get; set; }
}

public class C
{
    public string satis { get; set; }
    public string alis { get; set; }
    public string degisim { get; set; }
    public string d_oran { get; set; }
    public string d_yon { get; set; }
}

public class ETH
{
    public string satis { get; set; }
    public string alis { get; set; }
    public string degisim { get; set; }
    public string d_oran { get; set; }
    public string d_yon { get; set; }
}

public class EUR
{
    public string satis { get; set; }
    public string alis { get; set; }
    public string degisim { get; set; }
    public string d_oran { get; set; }
    public string d_yon { get; set; }
}

public class GA
{
    public string satis { get; set; }
    public string alis { get; set; }
    public string degisim { get; set; }
    public string d_oran { get; set; }
    public string d_yon { get; set; }
}

public class GAG
{
    public string satis { get; set; }
    public string alis { get; set; }
    public string degisim { get; set; }
    public string d_oran { get; set; }
    public string d_yon { get; set; }
}

public class GBP
{
    public string satis { get; set; }
    public string alis { get; set; }
    public string degisim { get; set; }
    public string d_oran { get; set; }
    public string d_yon { get; set; }
}



public class USD
{
    public string satis { get; set; }
    public string alis { get; set; }
    public string degisim { get; set; }
    public string d_oran { get; set; }
    public string d_yon { get; set; }
}

public class XU100
{
    public string satis { get; set; }
    public string alis { get; set; }
    public string degisim { get; set; }
}
