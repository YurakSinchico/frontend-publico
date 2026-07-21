using System.Text;
using System.Text.Json;

namespace Api.Consumer.Common;

public static class Crud<T>
{
    public static string Endpoint { get; set; } = "";

    public static async Task<T?> Create(T data)
    {
        using HttpClient client = new();

        var json = JsonSerializer.Serialize(data);

        var content = new StringContent(
            json,
            Encoding.UTF8,
            "application/json");

        var response = await client.PostAsync(Endpoint, content);

        response.EnsureSuccessStatusCode();

        var result = await response.Content.ReadAsStringAsync();

        return JsonSerializer.Deserialize<T>(result);
    }

    public static async Task<List<T>?> ReadAll()
    {
        using HttpClient client = new();

        var response = await client.GetAsync(Endpoint);

        response.EnsureSuccessStatusCode();

        var json = await response.Content.ReadAsStringAsync();

        return JsonSerializer.Deserialize<List<T>>(json);
    }

    public static async Task<T?> ReadById(string id)
    {
        using HttpClient client = new();

        var response = await client.GetAsync($"{Endpoint}/{id}");

        response.EnsureSuccessStatusCode();

        var json = await response.Content.ReadAsStringAsync();

        return JsonSerializer.Deserialize<T>(json);
    }

    public static async Task<bool> Update(string id, T data)
    {
        using HttpClient client = new();

        var json = JsonSerializer.Serialize(data);

        var content = new StringContent(
            json,
            Encoding.UTF8,
            "application/json");

        var response = await client.PutAsync($"{Endpoint}/{id}", content);

        return response.IsSuccessStatusCode;
    }

    public static async Task<bool> Delete(string id)
    {
        using HttpClient client = new();

        var response = await client.DeleteAsync($"{Endpoint}/{id}");

        return response.IsSuccessStatusCode;
    }
}