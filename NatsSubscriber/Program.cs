using NATS.Client;

namespace NatsSubscriber;

public class Program
{
    private const string Subject = "testSubject";

    public static void Main()
    {
        var factory = new ConnectionFactory();
        var connection = factory.CreateConnection();

        var subscription = connection.SubscribeAsync(Subject, MessageHandle);

        subscription.Start();

        Console.ReadLine();

        subscription.Unsubscribe();

        connection.Drain();
        connection.Close();
    }

    private static EventHandler<MsgHandlerEventArgs> MessageHandle
        => (_, args) =>
        {
            Console.WriteLine("Message received: {0}", args.Message);
        };
}