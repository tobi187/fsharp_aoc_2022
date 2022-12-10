open System
open System.IO

let file = File.ReadAllLines "d3.in" |> Array.map (fun x -> x.Split ()) |> Array.toList

let genRes c cc cv res =
    let next = List.length res * 40 + 20
    match c <= next && next < cc with
    | true -> 
        printfn "cyc: %i; val: %i" c cv
        cv * next :: res
    | false -> res


let rec doLine (lines: string[] list) cycle cVal res =
    printfn "cyc: %i; val: %i" cycle cVal
    match lines with
    | [] -> res
    | head :: tail ->
        match head.[0] with
        | "noop" -> 
            let nRes = genRes cycle (cycle+1) cVal res
            doLine tail (cycle+1) cVal nRes 
        | "addx" ->
            let num = cVal + int head.[1]
            let nRes = genRes cycle (cycle+2) cVal res
            doLine tail (cycle+2) num nRes
        | _ -> failwith "a"


doLine file 1 1 []
|> printfn "%A"
