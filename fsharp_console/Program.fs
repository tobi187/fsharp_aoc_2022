open System
open System.IO

let [| g; i |] = File.ReadAllText(@"C:\Users\fisch\Desktop\projects\tests\fsharp_console\fsharp_console\aoc2022\22.in").Split "\r\n\r\n"

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
        
let gl = g.Split "\r\n"

let xLen = gl |> Array.maxBy String.length |> String.length
let yLen = gl.Length

let gline = gl |> Array.map (fun s -> s.PadRight xLen) |> Array.map Seq.toArray 

let instructions = parseInstructions (i.Trim() |> Seq.toList) ["R"]

(*let grid = 
    [
        for y = 0 to (gline.Length - 1) do
            for x = 0 to (gline.[0].Length - 1) do
                match gline.[y].[x] with
                | ' ' -> yield {x=x; y=y; sym=" "}
                | '.' -> yield {x=x; y=y; sym="."}
                | '#' -> yield {x=x; y=y; sym="#"}
                | e -> failwith $"wtf is that char {e}"
    ]*)


type Pos = {
    x: int
    y: int
    dir: string
}

let getRow y = gline |> Array.map (fun x -> x.[y]) |> Array.indexed |> Array.filter (fun e -> snd e <> ' ') |> Array.map fst
let getXRow y = gline.[y] |> Array.indexed |> Array.filter (fun e -> snd e <> ' ') |> Array.map fst

let getNewPos pos =
    match pos.dir with
    | "N" -> if pos.y > 0 then { pos with y = pos.y - 1 } 
             else { pos with y = getRow pos.x |> Array.last }
    | "S" -> if pos.y + 1 < yLen then { pos with y = pos.y + 1 }
             else { pos with y = getRow pos.x |> Array.head }
    | "E" -> if pos.x + 1 < xLen then { pos with x = pos.x + 1 } 
             else { pos with x = getXRow pos.y |> Array.head }
    | "W" -> if pos.x > 0 then { pos with x = pos.x - 1 }
             else { pos with x = getXRow pos.y |> Array.last }
    | _ -> failwith "aa"

let rec step pos steps =
    match steps with
    | 0 -> pos
    | _ -> 
        let newPos = getNewPos pos
        match gline.[newPos.y].[newPos.x] with
        | '#' -> step pos 0
        | ' ' ->  failwith "A"
        | _ -> step newPos (steps - 1)


let walk pos comm =
    let dir, len = comm
    match dir, pos.dir with
    | ("R", "N")|("L", "S") -> step { pos with dir = "E" } len 
    | ("R", "E")|("L", "W") -> step { pos with dir = "S" } len
    | ("R", "S")|("L", "N") -> step { pos with dir = "W" } len
    | ("R", "W")|("L", "E") -> step { pos with dir = "N" } len
    | u -> failwith $"dir unkown: {u}"

let gd d =
    match d with
    | "E" -> 0
    | "S" -> 1
    | "W" -> 2
    | "N" -> 3
    | _ -> failwith ""

instructions
|> List.fold walk {x = getXRow 0 |> Array.head; y = 0; dir = "N"}
|> printfn "res: %A"

//|> (fun r -> (r.y-1) * 1000 + (r.x-1) * 4 + gd r.dir)