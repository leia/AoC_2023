module Advent_of_code_2023.day6

open System

let private parseGames(lines: int64 seq seq) =
    Seq.zip (lines |> Seq.head) (lines |> Seq.last)
    
let private processGame(time: int64, currentRecord: int64) =
    let startIndex, endIndex = (time / 2L), time-1L
    
    let calculateDistance (holdingButtonMs: int64) = (time - holdingButtonMs) * holdingButtonMs
    
    let scores =
        seq {1L..endIndex}
        |> Seq.map calculateDistance
        |> Seq.where(fun f -> f > currentRecord)
        |> Seq.length
        
    scores
    
let firstPart(data: string seq) =
    let games =
        data
        |> Seq.map(fun line -> (line.Split(":")[1]).Split(" ", StringSplitOptions.RemoveEmptyEntries) |> Seq.map int64)
        |> parseGames
        |> Seq.map processGame
        //|> Seq.toList
        |> Seq.fold(*) 1
        
    games

let secondPart(data: string seq) =
    let game =
        data
        |> Seq.map(fun line -> (line.Split(":")[1]).Replace(" ", "") |> int64)
        |> Seq.toList
        
    let res = processGame (game[0], game[1])
        
    res