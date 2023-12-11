module Advent_of_code_2023.day11
open System
open System.Text.RegularExpressions

let private isEmpty line =
    line
    |> Seq.toList
    |> Seq.map string
    |> Seq.forall(fun s -> s = ".")
    
let getGalaxies(line: string) =
    let indices = Regex.Matches(line, "[^.]") |> Seq.map(fun m -> m.Index)
    indices

let firstPart  (expandCoef: int64) (data: string seq) =
    let lineIndices =
        data
        |> Seq.indexed
        |> Seq.where(fun (i, f) -> f |> isEmpty)
        |> Seq.map fst
        |> Seq.toList
        
    let indices =
        data
        |> Seq.transpose
        |> Seq.map(fun f -> f |> Seq.map string |> Seq.fold(+) "")
        |> Seq.indexed
        |> Seq.where(fun (i, f) -> f |> isEmpty)
        |> Seq.map fst
        |> Seq.toList
    
    let getNewIndex (li, i) =
        let liMil = (lineIndices |> Seq.where(fun f -> f < li) |> Seq.length) |> int64
        let iMil = (indices |> Seq.where(fun f -> f < i) |> Seq.length) |> int64
        
        let nl: int64 = (li |> int64) + liMil*(expandCoef-1L)
        let ni: int64 = (i |> int64) + iMil*(expandCoef-1L)
        
        (nl, ni)
        
    let galaxiesMap =
        data
        |> Seq.mapi(fun i l -> l |> getGalaxies |> Seq.map(fun f -> i, f))
        |> Seq.concat
        |> Seq.map getNewIndex
        |> Seq.indexed
        |> Map.ofSeq
        
    let allPairs =
       galaxiesMap.Keys
       |> Seq.allPairs galaxiesMap.Keys
       |> Seq.map(fun (x, y) -> [x; y] |> Set.ofSeq)
       |> Seq.distinct
       |> Seq.where(fun s -> s.Count > 1)
       |> Seq.map(fun s -> s.MinimumElement, s.MaximumElement)
       
    let getDistance (f: int, s: int) =
        let a = galaxiesMap |> Map.find f
        let b = galaxiesMap |> Map.find s
        
        Math.Abs(fst a - fst b) + Math.Abs(snd a - snd b)
        
    let sum = allPairs |> Seq.map getDistance |> Seq.sum
        
    sum