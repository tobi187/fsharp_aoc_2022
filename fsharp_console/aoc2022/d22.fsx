open System
open System.IO

let [| g; i |] = File.ReadAllText("22.in").Split "\r\n\r\n"

type Point = {
    x: int
    y: int
    sym: string
}

let rec parseInstructions mess res = 
    match mess with
    | [] -> 
        res 
        |> List.rev
        |> List.chunkBySize 2
        |> List.map (fun [d;n] -> d, int n)
    | head :: tail -> 
        match head with
        | x when Char.IsLetter x -> parseInstructions tail (string x :: res)
        | x when Char.IsLetter res.Head.[0] -> parseInstructions tail (string x :: res)
        | x -> parseInstructions tail (res.Head + (string x) :: res.Tail)
        
let gline = g.Split "\r\n" |> Array.map (Seq.toArray)

let xLen = gline.[0].Length
let yLen = gline.Length

let instructions = parseInstructions (i.Trim() |> Seq.toList) ["R"]

let grid = 
    [
        for y = 0 to (gline.Length - 1) do
            for x = 0 to (gline.[0].Length - 1) do
                match gline.[y].[x] with
                | ' ' -> yield {x=x; y=y; sym=" "}
                | '.' -> yield {x=x; y=y; sym="."}
                | '#' -> yield {x=x; y=y; sym="#"}
                | e -> failwith $"wtf is that char {e}"
    ]


type Pos = {
    x: int
    y: int
    dir: string
}


let rec walk pos comm =
    let dir, len = comm
    match dir, pos.dir with
    | ("R", "N")|("L", "S") -> 0
    | ("R", "E")|("L", "W") -> 0
    | ("R", "S")|("L", "N") -> 0
    | ("R", "W")|("L", "E") -> 0
    | u -> failwith $"dir unkown: {u}"


let rec step pos steps =
    
