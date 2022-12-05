open System

    
let highestRank (array: int[]) =
  array
  |> Array.groupBy (fun x -> x)
  |> Array.sortBy (fun x -> fst x)
  |> Array.maxBy (fun (_, x) -> Array.length(x))
  |> fun (a, _) -> a 
