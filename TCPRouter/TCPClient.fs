module TCPClient

open System
open System.Net.Sockets

let mutable ClientStream : NetworkStream = null

//let asyncGetInput = async { return BitConverter.GetBytes(Console.Read()) }

let rec asyncSendInput (stream : NetworkStream, asyncGetInput : byte[]) = //async {
    let input = asyncGetInput
    let out = stream.WriteByte |> Array.map <| input
    //do! asyncSendInput stream
    out
    //}


//let asyncGetResponse (stream : NetworkStream) = async { return Char.ConvertFromUtf32(stream.ReadByte()) }

let asyncGetResponse (stream : NetworkStream) = //async {
    let mutable buffer = Array.zeroCreate<byte>( 0xFFFFFF )
    let mutable tmpbuffer = Array.zeroCreate<byte>( 0xFFFFFF )
    let mutable index = (int) 0
    let mutable data = 0
    stream.ReadTimeout <- -1
    let mutable count = 1
    try
        while count > 0 do
            count <- stream.Read (tmpbuffer, 0, 0xFFFFFF)
            if count > 0 then
                let newbuffer = Array.zeroCreate<byte>( count )
                let newbuffer1 = newbuffer |> Array.mapi( fun i x -> tmpbuffer.[i])
                buffer <- buffer |> Array.mapi (fun i x -> if i < index then x elif i < (index + count) then newbuffer1.[i - index] else x)
                index <- index + count
                if count <= 65535 then
                    //Console.WriteLine(  count  ) 
                    count <- -1
                else
                    stream.ReadTimeout <- 10

    with
        | e -> Console.Write( ""  ) 
    let newArray = Array.zeroCreate<byte>( index )
    let result = newArray |> Array.mapi(fun i x -> buffer.[i])
    result
