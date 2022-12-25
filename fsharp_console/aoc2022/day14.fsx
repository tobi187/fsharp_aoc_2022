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

let entry = (500, 0)

let con v offset l =
    match offset with
    | 1 -> Array.contains (fst v + 1, snd v) l 
    | -1 -> Array.contains (fst v - 1, snd v) l
    | _ -> Array.contains v l

let rec fall res point =
    let all = Array.append res rocks
    let next = (fst point, snd point + 1)
    match next with
    | p when con p 0 all -> 
        match p with
        | np when not (con np -1 all) -> fall res (fst np - 1, snd np) 
        | np when not (con np 1 all) -> fall res (fst np + 1, snd np)
        | _ -> fall (Array.append [| point |] res) entry
    | p when snd p > highest -> res
    | p -> fall res p
    
    
let r = fall Array.empty entry 

let d = List.init 10 (fun y -> List.init 20 (fun x -> (x + 490, y)))

d
|> List.map (fun l -> l |> List.map (fun e -> match e with
                                              | _ when Array.contains e rocks -> '#'
                                              | _ when Array.contains e r -> 'o'
                                              | _ -> '.'))
|> List.map (fun s -> String.Join("", s))
|> List.iter (printfn "%A\n")
