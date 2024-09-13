namespace ECommerce.Web;

public class ApiClient(HttpClient client)
{
    public async Task<T> GetAsync<T>(string url)
    {
        var response = await client.GetAsync(url);
        response.EnsureSuccessStatusCode();
        
        return await response.Content.ReadFromJsonAsync<T>();
    }

    public async Task<T> GetByIdAsync<T>(string url, int id)
    {
        var response = await client.GetAsync($"{url}/{id}");
        response.EnsureSuccessStatusCode();
        
        return await response.Content.ReadFromJsonAsync<T>();
    }

    public async Task<T> PostAsync<T>(string url, T data)
    {
        var response = await client.PostAsJsonAsync(url, data);
        response.EnsureSuccessStatusCode();
        
        return await response.Content.ReadFromJsonAsync<T>();
    }

    public async Task<T> PutAsync<T>(string url, int id, T data)
    {
        var response = await client.PutAsJsonAsync($"{url}/{id}", data);
        response.EnsureSuccessStatusCode();
        
        return await response.Content.ReadFromJsonAsync<T>();
    }

    public async Task DeleteAsync(string url, int id)
    {
        var response = await client.DeleteAsync($"{url}/{id}");
        response.EnsureSuccessStatusCode();
    }
}