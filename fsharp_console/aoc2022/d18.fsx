open System
open System.IO

type Point = {
    x: int
    y: int
    z: int
    sides: int
}

let file = 
    File.ReadAllLines "d3.in" 
    |> Array.map (fun x -> x.Split "," |> Array.map int)
    |> Array.map (fun [|a; b; c|] -> {x=a; y=b; z=c; sides=6 }) 


let findNeighs 
