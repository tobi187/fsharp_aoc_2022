module day3

open System.IO

let file = File.ReadAllLines "d3.in"

let sp d =
    d
    |> Array.indexed
    
