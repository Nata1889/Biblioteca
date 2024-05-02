using EstanteDigital.Modelos;
using Newtonsoft.Json;
using System.Collections.ObjectModel;
using System.Text;

namespace EstanteDigital.Repositories
{
    internal class RepositoryLibros
    {
        string urlApi = "https://biblioteca-dd19.restdb.io/rest/biblioteca"; 
        HttpClient client = new HttpClient();

        public RepositoryLibros()
        {
            //configuramos que trabajará con respuestas JSON
            client.DefaultRequestHeaders.Add("Accept", "application/json");
            client.DefaultRequestHeaders.Add("apikey", "bd6c1040e607cbd44e6d9ca2cee828010368a");
        }
        public async Task<ObservableCollection<Libros>> GetAllAsync()
        {
            try
            {

                var response = await client.GetStringAsync(urlApi);
                return
                    JsonConvert.DeserializeObject<ObservableCollection<Libros>>(response);
            }
            catch (Exception error)
            {
                await Application.Current.MainPage.DisplayAlert("Error", "Hubo un error" + error.Message, "OK");
                return null;
            }
        }
        public async Task<bool> RemoveAsync(string id)
        {
            var response = await client.DeleteAsync($"{urlApi}/{id}");
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> AddAsync(Libros libros)
        {
            var resonse = await client.PostAsync(urlApi,
                new StringContent(JsonConvert.SerializeObject(libros), Encoding.UTF8, "application/json"));
            return resonse.IsSuccessStatusCode;
        }

        public async Task<bool> UpdateAsync(Libros libros)
        {
            var resonse = await client.PutAsync($"{urlApi}/{libros._id}",
                new StringContent(JsonConvert.SerializeObject(libros), Encoding.UTF8, "application/json"));
            return resonse.IsSuccessStatusCode;
        }

    }
}

