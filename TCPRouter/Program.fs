
open System
open System.Net
open System.Text
open System.IO

open System
open System.IO
open System.Net
open System.Net.Sockets
open System.Threading

[<EntryPoint>]
let main argv = 
    printfn "%A" argv
    let address = IPAddress.Parse("127.0.0.1");
    let disposable = TCPServer.Server.Start( ipAddress=address,port = 8091)
    


    let client = new System.Net.Sockets.TcpClient()
    client.Connect("127.0.0.1", 80)
    //printfn "Connected to %A %A..." args.[0] args.[1]
    TCPClient.ClientStream <- client.GetStream()
    printfn "Got stream, starting two way asynchronous communication."
    //Async.Parallel [TCPClient.asyncSendInput stream; TCPClient.asyncPrintResponse stream] |> Async.RunSynchronously |> ignore
    
    

    let res = Console.ReadLine()
    disposable.Dispose()
    0 // return an integer exit code
