open System.IO
open System

let file = File.ReadAllLines "d3.in"

let calc (c: char) = 
    match Char.IsUpper c with
    | true -> (int)c - 65 + 27
    | false -> (int)c - 96 


let sp (d: string) =
    let [a; b] = Seq.splitInto 2 d |> Seq.toList
    
    b
    |> Array.filter (fun x -> Seq.contains x a)
    |> Array.head
    |> calc

let sec (d: string[]) =
    let [|a;b;c|] = d
    a
    |> Seq.filter (fun x-> Seq.contains x b)
    |> Seq.filter (fun x-> Seq.contains x c)
    |> Seq.head
    |> calc



file |> Array.map sp |> Array.sum |> printfn "Result: %i"
file |> Array.chunkBySize 3 |> Array.map sec |> Array.sum |> printfn "Result2: %A"

