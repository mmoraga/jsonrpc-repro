using CoreFoundation;
using System.Net;
using System.Net.Sockets;
using System.Text;
using jsonrpc_repro;
using StreamJsonRpc;

static async Task RpcConnectionAsync()
{
    try
    {
        var tcpServer = new TcpListener(IPAddress.Loopback, 29000);

        CoreFoundation.OSLog.Default.Log("Starting server.");
        tcpServer.Start();
        var tcpClient = new TcpClient("localhost", 29000);

        var clientConnection = tcpServer.AcceptTcpClient();
        var clientConnectionStream = clientConnection.GetStream();
        var serverFormatter = new JsonMessageFormatter(Encoding.UTF8);
        var clientFormatter = new JsonMessageFormatter(Encoding.UTF8);

        var serverHandler = new LengthHeaderMessageHandler(clientConnectionStream, clientConnectionStream, serverFormatter);
        using (var jsonRpc = new JsonRpc(serverHandler, new GreeterServer()))
        {
            jsonRpc.StartListening();
            CoreFoundation.OSLog.Default.Log(OSLogLevel.Info, "start listening to rpc stuff");

            var stream = tcpClient.GetStream();
            var jsonMessageHandler = new LengthHeaderMessageHandler(stream, stream, clientFormatter);

            var greeterClient = new JsonRpc(jsonMessageHandler);
            greeterClient.StartListening();
            CoreFoundation.OSLog.Default.Log(OSLogLevel.Info, "Try sending message.");
            var helloReply = await greeterClient.InvokeAsync<string>("Name", "Little Bobby Tables");
            CoreFoundation.OSLog.Default.Log(OSLogLevel.Info, helloReply);

            await jsonRpc.Completion;
        }
    }
    catch (Exception e)
    {
        CoreFoundation.OSLog.Default.Log(OSLogLevel.Error, $"Error in rpc communication: {e}");
    }
}

_ = Task.Factory.StartNew(RpcConnectionAsync, CancellationToken.None,
    TaskCreationOptions.DenyChildAttach | TaskCreationOptions.LongRunning, TaskScheduler.Default);

UIApplication.Main (args, null, typeof (AppDelegate));
