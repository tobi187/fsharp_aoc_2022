//namespace MeteoriteBackEnd
open System
open FSharp.Control
open System.Net.Http
open System.Net.Http.Json
open day3

day3.file |> day3.sp |> printfn "%A"
Environment.Exit 0

[<CLIMutable>]
type GeoLocation = {
    latitude: string
    longitude: string
}


[<CLIMutable>]
type APIModel= {
    name: string
    id: string
    nametype: string
    recclass: string
    mass: string
    fall: string
    year: string
    reclat: string
    reclong: string
    geoLocation: GeoLocation
}


let client = new HttpClient()
    
let getRequest () = 
    task {
        let! res = client.GetAsync("https://data.nasa.gov/resource/gh4g-9sfh.json")
        let! data = res.Content.ReadFromJsonAsync<APIModel[]>()
        (*match res.IsSuccessStatusCode with 
        | true -> return Ok data
        | false -> return Error data*)
        printfn "%A" (data |> Seq.ofArray)
        //return data 
    }
    |> Async.AwaitTask
    |> Async.RunSynchronously

getRequest()
        