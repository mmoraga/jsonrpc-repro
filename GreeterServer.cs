using System;
using StreamJsonRpc;
using System.IO;
using CoreFoundation;

namespace jsonrpc_repro
{
    public class HelloRequest
    {
        public string? Name { get; set; }
    }

    public class HelloReply
    {
        public string? Message { get; set; }
    }

    public interface IGreeter
    {
        Task<HelloReply> SayHelloAsync(HelloRequest request);
    }

    public class GreeterServer : IGreeter
    {
        public Task<HelloReply> SayHelloAsync(HelloRequest request)
        {
            return Task.FromResult(new HelloReply
            {
                Message = "Hello " + request.Name
            });
        }
    }
}

