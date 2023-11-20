using System.Text;
using NATS.Client;

namespace NatsPublisher;

public class Program
{
    private const string Subject = "testSubject";

    public static void Main()
    {
        var connection = NatsConnection;

        Console.Write("Enter port number: ");

        int portNumber;

        while (!int.TryParse(Console.ReadLine(), out portNumber))
        {
            Console.WriteLine("Invalid port number!");
            Console.Write("Enter port number:");
        }

        using var port = new ComPort(portNumber);

        while (true)
        {
            if (port.Messages.Count == 0)
            {
                return;
            }

            var message = port.Messages.Pop();

            Console.Write($"Message sent: {message}");
            connection.Publish(Subject, Encoding.UTF8.GetBytes(message));
        }
    }

    private static IConnection NatsConnection
        => new ConnectionFactory().CreateConnection();
}
