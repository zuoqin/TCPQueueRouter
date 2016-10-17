module Queue

type TCPMessage = 
    | RetrievePage of byte[]  * AsyncReplyChannel<byte[]>

let DownloadPage (bytes : byte[]) : byte[] =
    let written = TCPClient.asyncSendInput(TCPClient.ClientStream, bytes)
    let read = TCPClient.asyncGetResponse(TCPClient.ClientStream)    
    read
    

let crud = MailboxProcessor.Start(fun agent ->             
    let rec loop () : Async<unit> = async {
        let! msg = agent.Receive()
        match msg with 
        | RetrievePage ( bytes, reply ) ->
            let newpage = DownloadPage bytes
            reply.Reply newpage
                    


        return! loop () }
    loop () )     