# jsonrpc-repro

Reproduce StreamJsonRPC issue on dotnet6-ios running in aot-only mode

build:
```
dotnet build /p:BuildIpa=true /p:ArchiveOnBuild=true -c Debug /bl:msbuild.binlog
```

```
Error in rpc communication: Newtonsoft.Json.JsonSerializationException: Error writing JSON RPC Message: JsonSerializationException: Error getting value from 'RequestId' on 'StreamJsonRpc.JsonMessageFormatter+OutboundJsonRpcRequest'.
 ---> Newtonsoft.Json.JsonSerializationException: Error getting value from 'RequestId' on 'StreamJsonRpc.JsonMessageFormatter+OutboundJsonRpcRequest'.
 ---> System.ExecutionEngineException: Attempting to JIT compile method '(wrapper delegate-invoke) StreamJsonRpc.RequestId <Module>:invoke_callvirt_RequestId_JsonRpcRequest (StreamJsonRpc.Protocol.JsonRpcRequest)' while running in aot-only mode. See https://docs.microsoft.com/xamarin/ios/internals/limitations for more information.

   at System.Linq.Expressions.Interpreter.FuncCallInstruction`2[[StreamJsonRpc.Protocol.JsonRpcRequest, StreamJsonRpc, Version=2.12.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a],[StreamJsonRpc.RequestId, StreamJsonRpc, Version=2.12.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a]].<…>
   ```
