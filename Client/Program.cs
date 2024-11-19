using System;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

public class LinkClient
{
    private static readonly HttpClient _client = new();

    public LinkClient(string baseAddress)
    {
        _client.BaseAddress = new Uri(baseAddress);
        _client.DefaultRequestHeaders.Add("Accept", "application/json");
    }

    public async Task Login(string username, string password)
    {
        var credentials = new
        {
            Username = username,
            Password = password
        };

        var json = JsonSerializer.Serialize(credentials);
        var content = new StringContent(json, Encoding.UTF8, "application/json");

        var response = await _client.PostAsync("api/authenticator/login", content);

        if (response.IsSuccessStatusCode)
        {
            Console.WriteLine($"Login successful: {await response.Content.ReadAsStringAsync()}");
        }
        else
        {
            Console.WriteLine($"Login failed: {response.StatusCode} - {await response.Content.ReadAsStringAsync()}");
        }
    }

    public async Task SendUplink(string packet)
    {
        var content = new StringContent($"\"{packet}\"", Encoding.UTF8, "application/json");
        var response = await _client.PostAsync("api/uplink/send", content);

        if (response.IsSuccessStatusCode)
        {
            Console.WriteLine($"Uplink sent successfully: {await response.Content.ReadAsStringAsync()}");
        }
        else
        {
            Console.WriteLine($"Failed to send uplink: {response.StatusCode} - {await response.Content.ReadAsStringAsync()}");
        }
    }

    public async Task Logout(string username, string password)
    {
        var credentials = new
        {
            Username = username,
            Password = password
        };

        var json = JsonSerializer.Serialize(credentials);
        var content = new StringContent(json, Encoding.UTF8, "application/json");

        var response = await _client.PostAsync("api/authenticator/logout", content);

        if (response.IsSuccessStatusCode)
        {
            Console.WriteLine($"Logout successful: {await response.Content.ReadAsStringAsync()}");
        }
        else
        {
            Console.WriteLine($"Logout failed: {response.StatusCode} - {await response.Content.ReadAsStringAsync()}");
        }
    }
}

public class Program
{
    public static async Task Main(string[] args)
    {
        Console.WriteLine("Enter the base address of the server (e.g., https://localhost:5001):");
        var baseAddress = Console.ReadLine();
        var client = new LinkClient(baseAddress);

        Console.WriteLine("\n---- LOGIN ----");
        Console.Write("Username: ");
        var username = Console.ReadLine();
        Console.Write("Password: ");
        var password = Console.ReadLine();

        await client.Login(username, password);

        Console.WriteLine("\n---- SEND UPLINK ----");
        Console.Write("Enter packet data: ");
        var packet = Console.ReadLine();
        await client.SendUplink(packet);

        Console.WriteLine("\n---- LOGOUT ----");
        await client.Logout(username, password);

        Console.WriteLine("Client operations completed.");
    }
}
