using BackendWebApi.Models;
using Microsoft.VisualStudio.Web.CodeGeneration.Design;
using Newtonsoft.Json;
using System.Text;

namespace BackendWebApi.Test
{
    [TestClass]
    public sealed class Test1
    {
        [TestMethod]
        public async Task TestMethod1Async()
        {
            WebApplicationFactory<Program> webAppFactory = new WebApplicationFactory<Program>();
            HttpClient client = webAppFactory.CreateDefaultClient();

            Osto uusiOsto = new Osto();
            uusiOsto.Tuote = "Testi Tuote";
            uusiOsto.Hinta = 1;
            uusiOsto.Päivä = DateOnly.FromDateTime(DateTime.Now);

            // Muutetaan edellä luotu objekti Jsoniksi
            string input = JsonConvert.SerializeObject(uusiOsto);
            StringContent content = new StringContent(input, Encoding.UTF8, "application/json");

            // Lähetetään muodostettu data testattavalle api:lle post pyyntönä
            var responsePost = await client.PostAsync("/api/Ostot", content);

            // Tarkistetaan onko vastauksen statuskoodi ok
            Assert.AreEqual(responsePost.StatusCode.ToString().ToLower(), "ok");

            // Koitetaan hakea saman niminen kurssi
            var responseGet = await client.GetAsync("/api/Ostot");
            var json = await responseGet.Content.ReadAsStringAsync();

            var ostot = JsonConvert.DeserializeObject<Osto[]>(json).ToList();

            var matchedOsto = ostot.Where(k => k.Tuote == "Testi Tuote").FirstOrDefault();

            Assert.AreEqual("Testi Tuote", matchedOsto.Tuote);

            var responseDelete = await client.DeleteAsync("/api/Ostot/" + matchedOsto.Id);

            Assert.AreEqual("ok", responseDelete.StatusCode.ToString().ToLower());

            var response = await client.GetAsync("/api/Ostot/" + matchedOsto.Id);

            Assert.AreEqual("nocontent", response.StatusCode.ToString().ToLower());
        }
    }
}
