using Firebase.Database;
using Firebase.Database.Query;
using Microsoft.Maui.Controls;
using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows.Input;

namespace finalProject
{
     
    public partial class Yapilacaklar : ContentPage
    {
        //public enum OperationType
        //{
        //    Add,
        //    Update
        //}
        //public OperationType CurrentOperation { get; set; }
        public static Boolean islem = true;

        ObservableCollection<Missions> missions;
        string dosyaadı = Path.Combine(FileSystem.AppDataDirectory, "data.json");
        static FirebaseClient client = new FirebaseClient("https://yapilacaklar-dc33a-default-rtdb.firebaseio.com");

        public Yapilacaklar()
        {
            InitializeComponent();
            todos.ItemsSource = Missions;
            if (File.Exists(dosyaadı))
            {
                string data = File.ReadAllText(dosyaadı);
                missions = JsonSerializer.Deserialize<ObservableCollection<Missions>>(data);
            }
        }

        public ObservableCollection<Missions> Missions
        {
            get
            {
                if (missions == null)
                {
                    missions = new ObservableCollection<Missions>();
                }
                return missions;
            }
        }

        private async void YeniGorev(object sender, EventArgs e)
        {
            islem = true;
            Planlama planlama = new Planlama() { Title = "EKLEME", AddMethod = EkleGorev };
            await Navigation.PushModalAsync(planlama);
        }

        private async void GorevGuncelle(object sender, EventArgs e)
        {
            islem = false;
            var duzenle = sender as MenuItem;
            if (duzenle != null)
            {
                var id = duzenle.CommandParameter.ToString();
                var sondurum = Missions.Single(o => o.ID == id);

                Planlama planlama = new Planlama()
                {
                    Title = "Title",
                    Yaplacaklar = sondurum,
                    UpdateMethod = GuncelleGorev  
                };
                await Navigation.PushAsync(planlama);
            }
        }

        public async void GuncelleGorev(Missions updatedMission)
        {
            var existingMission = missions.FirstOrDefault(m => m.ID == updatedMission.ID);

            if (existingMission != null)
            {
                existingMission.Head = updatedMission.Head;
                existingMission.Detail = updatedMission.Detail;
                existingMission.Date = updatedMission.Date;
                existingMission.Hour = updatedMission.Hour;
                await client.Child("Yapilacaklar").PutAsync(missions);

                // Notify any UI changes
                todos.ItemsSource = missions;
            }
        }

        public async void EkleGorev(Missions mission)
            
        {
            islem = true;
            await client.Child("Yapilacaklar").PostAsync(mission);
            missions.Add(mission);
        }

        private async void Delete(object sender, EventArgs e)
        {
            var delete = sender as MenuItem;
            var sil = await DisplayAlert("Sil", "Emin Misiniz?", "Evet", "Hayır");
            if (sil)
            {
                var temzile = delete.BindingContext as Missions;
                if (temzile != null)
                {
                    missions.Remove(temzile);
                    await client.Child("Yapilacaklar").DeleteAsync();
                }
            }
        }
    }

    public class Missions : System.ComponentModel.INotifyPropertyChanged
    {
        public string ID
        {
            get
            {
                if (id == null)
                    id = Guid.NewGuid().ToString();

                return id;
            }
            set { id = value; }
        }

        string id, head, detail, date, hour;

        public string Head
        {
            get => head;
            set { head = value; NotifyPropertyChanged(); }
        }

        public string Detail
        {
            get => detail;
            set { detail = value; NotifyPropertyChanged(); }
        }

        public string Date
        {
            get => date;
            set { date = value; NotifyPropertyChanged(); }
        }

        public string Hour
        {
            get => hour;
            set { hour = value; NotifyPropertyChanged(); }
        }

        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;

        public void NotifyPropertyChanged([System.Runtime.CompilerServices.CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
        }
    }
}