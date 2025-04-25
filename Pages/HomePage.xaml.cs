using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Storage;

namespace CanineConnect.Pages
{
    public partial class HomePage : ContentPage
    {
        public HomePage()
        {
            InitializeComponent();
            LoadPupsFromJson();
        }

        async void LoadPupsFromJson()
        {
            using var stream = await FileSystem.OpenAppPackageFileAsync("pets.json");
            using var reader = new StreamReader(stream);
            var json = await reader.ReadToEndAsync();

            var opts = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };
            var list = JsonSerializer.Deserialize<List<Pup>>(json, opts)
                    ?? new List<Pup>();

            PetsCollection.ItemsSource = list;
        }


        void OnAddPupClicked(object sender, EventArgs e)
        {
            // no-op
        }

        class Pup
        {
            public string Id { get; set; }
            public string Image { get; set; }
            public string Name { get; set; }
            public string Age { get; set; }
            public string Gender { get; set; }
            public string Size { get; set; }
            public string Breed { get; set; }
        }
    }
}
