using System.IO.Ports;

namespace NatsPublisher;

public class ComPort : IDisposable
{
    public Stack<string> Messages { get; set; }

    private readonly SerialPort _serialPort;

    public ComPort(int portNumber)
    {
        Messages = new Stack<string>();

        try
        {
            _serialPort = new SerialPort($"COM{portNumber}")
            {
                BaudRate = 9600,
                Parity = Parity.None,
                StopBits = StopBits.One,
                DataBits = 8,
                Handshake = Handshake.None
            };

            _serialPort.DataReceived += DataReceivedHandler;

            _serialPort.Open();
        }
        catch (FileNotFoundException)
        {
            Console.WriteLine("Serial port number not found!");
        }
        catch (Exception ex)
        {
            Console.WriteLine("Unexpected exception: {0}", ex.Message);
        }
    }

    private void DataReceivedHandler(object sender, SerialDataReceivedEventArgs e)
    {
        var port = (SerialPort)sender;

        Console.WriteLine("Message: {0}", port.ReadExisting());
        Messages.Push(port.ReadExisting());
    }

    public void Dispose()
    {
        _serialPort.Close();
    }
}
