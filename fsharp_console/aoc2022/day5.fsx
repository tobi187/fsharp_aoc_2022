open System
open System.IO

let file = File.ReadAllText "in4.txt"

type stack = {
    num: int
    racks: string list
    tIn: int
}

let createStacks state (line: string) =
    state
    |> Array.map (fun s -> 
        if line.[s.tIn] = ' ' then s else {s with racks = string(line.[s.tIn]) :: s.racks })
    

let parse (f: string[]) =
    let stacks = 
        Array.last f
        |> Seq.toArray
        |> Array.indexed
        |> Array.filter (fun (_,x) -> x <> ' ')
        |> Array.map (fun (i,x) -> 
            { num=int(x); racks=[]; tIn=i })


    f
    |> Array.removeAt (f.Length - 1)
    |> Array.fold createStacks stacks
    //|> Array.map (fun s -> { s with racks = s.racks |> List.rev })


let add a f t (s: stack list) =
    let r = s.[f-1].racks
    0


let move (line: string) stacks =
    line.Split(' ')
    |> Array.indexed
    |> Array.filter (fun (i,_) -> i <> 0)
    |> Array.map snd
    |> Array.map int


let [|f; s|] = file.Split "\r\n\r\n" |> Array.map (fun e -> e.Split("\n"))

parse f |> printfn "hu: %A"

