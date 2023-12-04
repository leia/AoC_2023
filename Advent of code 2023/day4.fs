module Advent_of_code_2023.day4
open System

let private processLine(line: string) =
    let colon = line.IndexOf(":")
    let splitLine =
        line.Substring(colon+1).Split("|", StringSplitOptions.RemoveEmptyEntries)
        |> Seq.map(fun f -> f.Split(" ", StringSplitOptions.RemoveEmptyEntries) |> Seq.map int)
        
    let winningNumbers, mine = splitLine |> Seq.head |> Set.ofSeq, splitLine |> Seq.last |> Set.ofSeq
    let mineWinning = Set.intersect winningNumbers mine |> Set.toList
    mineWinning

let firstPart(data: string seq) =
    let res =
        data
        |> Seq.map processLine
        |> Seq.map(fun f -> f.Length)
        |> Seq.map(fun f -> if f = 0 then 0.0
                            else
                                let e = f-1
                                Math.Pow(2, e))
        |> Seq.sum        
    res
    
let secondPart(data: string seq) =
    let winnings =
        data
        |> Seq.map processLine
        |> Seq.map(fun f -> f.Length)
        |> Seq.indexed 
        |> Map.ofSeq
    
    let getMoreCards (state: Map<int, int>) (index: int) =
        let eval = winnings |> Map.find index
        let copies = state |> Map.find index
        
        let indices = [index+1..index+eval]
        
        let addCopies(st: Map<int, int>) i =
            st |> Map.change i (fun (Some c) -> c+copies |> Some)
            
        let newState =
            indices
            |> Seq.fold addCopies state
        
        newState
        
    let initState =
        winnings |> Map.toSeq |> Seq.map(fun (i, _) -> i, 1) |> Map.ofSeq
        
    let res =
        winnings
        |> Map.toSeq
        |> Seq.map fst
        |> Seq.fold getMoreCards (initState)
        |> Map.toSeq
        |> Seq.sumBy snd
        
    res 