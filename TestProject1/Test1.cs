using Newtonsoft.Json;
using System.Text;
using BackendWebApi.Models;
using Microsoft.AspNetCore.Mvc.Testing;

namespace TestProject1
{
    [TestClass]
    public sealed class OstoTest1
    {
            [TestMethod]
            public async Task Test1()
            {

                WebApplicationFactory<Program> webAppFactory = new WebApplicationFactory<Program>();
                HttpClient client = webAppFactory.CreateDefaultClient();

                Osto uusiOsto = new Osto();
                uusiOsto.Tuote = "TestiTuote";
                uusiOsto.Kauppa = "Testi kauppa";
                uusiOsto.Hinta = 9.99M;
                uusiOsto.Päivä = DateOnly.FromDateTime(DateTime.Now);
                uusiOsto.Osoite = "Testi osoite";
                uusiOsto.Tietoja = "Testi tietoja";

            // Muutetaan edellä luotu objekti Jsoniksi
            string input = JsonConvert.SerializeObject(uusiOsto);
                StringContent content = new StringContent(input, Encoding.UTF8, "application/json");

                // Lähetetään muodostettu data testattavalle api:lle post pyyntönä
                var responsePost = await client.PostAsync("api/ostot", content);

                // Tarkistetaan onko vastauksen statuskoodi ok
                Assert.AreEqual("created", responsePost.StatusCode.ToString().ToLower());

                // Koitetaan hakea saman niminen kurssi
                var responseGet = await client.GetAsync("api/ostot");
                var json = await responseGet.Content.ReadAsStringAsync();

                var ostot = JsonConvert.DeserializeObject<Osto[]>(json).ToList();

                var osto = ostot.Where(k => k.Kauppa == "Testi kauppa").FirstOrDefault();

                Assert.AreEqual("Testi kauppa", osto.Kauppa);

                var responseDelete = await client.DeleteAsync("api/ostot/" + osto.Id);

                Assert.AreEqual("nocontent", responseDelete.StatusCode.ToString().ToLower());

                var response = await client.GetAsync("api/ostot/" + osto.Id);

                Assert.AreEqual("notfound", response.StatusCode.ToString().ToLower());

            }
        
    }
}
