namespace Client;

public class Program
{
    public static async Task Main() {
        var client = new ClientSocket();
        await client.ConnectToServer();
    }
}