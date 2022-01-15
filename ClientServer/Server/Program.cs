namespace Server;

public class Program
{
    public static async Task Main() {
        ServerSocket server = new ServerSocket();
        await server.OpenConnectionAsync();
    }
}