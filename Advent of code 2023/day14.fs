module Advent_of_code_2023.day14

open System
open System.Text.RegularExpressions

let private processLine(line: string) =
    let i =
        Regex.Matches(line, "[#]") |> Seq.map(fun m -> m.Index)
        |> Seq.toArray
        |> Array.sort
    
    let startEnd =  [|0; line.Length|]
    let indices =
        if (i.Length =0) then startEnd
        else i
        |> Array.append startEnd
        |> Array.distinct
        |> Array.sort
        |> Array.windowed 2
    
    let parts = indices |> Array.map(fun i -> (i[1], line.Substring(i[0], i.[1]-i[0])))
    
    let getZerosIndices (endIndex: int) (zerosCount: int) =
        let k =
            [|0..zerosCount-1|] |> Array.map(fun k -> endIndex-k) |> Array.sum
        k
            
    let res =
        parts
        |> Array.map(fun (endPosition, substring) ->
                let zerosCount = substring |> Seq.filter(fun c -> c = 'O') |> Seq.length
                let r = getZerosIndices endPosition zerosCount
                r
                )
        |> Array.sum
    res 

let firstPart(data: string seq) =
    let res =
        data
        |> Seq.toArray
        |> Array.map(fun s -> s |> Seq.toArray |> Array.map string)
        |> Array.transpose
        |> Array.map(fun s -> s |> Array.map string |> Array.rev |> Array.fold(+) "")
        |> Array.map processLine
        |> Array.sum
        
    res 

let secondPart(data: string seq) = ()