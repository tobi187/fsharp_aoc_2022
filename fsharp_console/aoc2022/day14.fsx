open System
open System.IO

let path = @"C:\Users\fisch\Desktop\projects\tests\fsharp_console\fsharp_console\aoc2022\d14.txt"
let file = File.ReadAllLines path |> Array.toList

let genRange st =
    let [|sx; sy|] = fst st |> Array.map int
    let [|ex; ey|] = snd st |> Array.map int

    match sx = ex with
    | true -> List.init (abs(sy-ey)) (fun i -> (sx, i * sign (ey-sy) + sy))
    | false -> List.init (abs(sx-ex)) (fun i -> (i * sign (ex-sx) + sx), sy)
    

let rec prepare (content: string list) res = 
    match content with
    | [] -> res |> List.rev
    | h :: t -> 
        let line = 
            h.Split " -> " 
            |> Array.map (fun x -> x.Split "," ) 
            |> Array.pairwise
            |> Array.map genRange
            |> Array.toList
            |> List.concat
            
        prepare t (List.append line res)

type pos = {
    symbol: string
    x: int
    y: int
}

let rocks = prepare file List.empty |> List.toArray

let lowest = rocks |> Array.minBy snd |> snd
let highest = rocks |> Array.maxBy snd |> snd

//let grid = 
//    List.init 200 (fun y -> y) 
//    |> List.map (fun i -> List.init 200 (fun x -> match i with
//                                                  | i when Array.contains (x+400,i) rocks -> { x=x+400; y=i; symbol = "#" }
//                                                  | _ -> { x=x+400; y=i; symbol="." }))


//let printer = grid |> List.take 10 |> List.map (fun x -> x |> List.fold (fun s a -> s + a.symbol) "") |> List.iter (printfn "%s\n")

let entry = (500, highest + 1)

let con v offset l =
    match offset with
    | 1 -> Array.contains (fst v + 1, snd v) l 
    | -1 -> Array.contains (fst v - 1, snd v) l
    | _ -> Array.contains v l

let rec fall res point =
    let all = Array.append res rocks
    match point with
    | p when con p 0 all -> 
        match p with
        | np when not (con p -1 all) -> fall res (fst p - 1, snd p - 1) 
        | np when not (con p 1 all) -> fall res (fst p + 1, snd p - 1)
        | np -> fall (Array.append [|(fst np, snd np + 1)|] res) entry
    | p when snd p < lowest -> res |> Array.length
    | p -> fall res (fst p, snd p - 1)
    
    
fall Array.empty entry |> printfn "result: %i"
