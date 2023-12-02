module Advent_of_code_2023.day2

let private processData (line: string) =
    //Game 1: 3 blue, 4 red; 1 red, 2 green, 6 blue; 2 green
    let gameId = (line.Split(":")[0]).Split(" ")[1] |> int
    
    let gameTurns =
        (line.Split(":")[1]).Split(";")
        |> Seq.map(fun s ->
            let mutable r, g, b = 0, 0, 0
            s.Split(",")
            |> Seq.iter(fun f ->
                let res=  f.Split(" ")
                let number = res[1] |> int
                match res[2] with
                | "red" -> r <- number
                | "green" -> g <- number
                | "blue" -> b <- number
                | _ -> () )
            gameId, r, g, b
            )
    
    gameTurns

let private isImpossible (_: int, r: int, g: int, b: int) =
    let redMax, greenMax, blueMax = 12, 13, 14    
    r > redMax || g > greenMax || b > blueMax
    

let firstPart(data: string seq) =
    let allTurns =
        data
        |> Seq.map processData
        |> Seq.concat
        |> Seq.toList
    
    let impossibleGames =
        allTurns
        |> Seq.where isImpossible
        |> Seq.map(fun (g, _, _, _) -> g)
        |> Seq.distinct
        
    let res =
        allTurns
        |> Seq.map(fun (g, _, _, _) -> g)
        |> Seq.distinct
        |> Seq.where(fun i -> impossibleGames |> Seq.contains i |> not )
        |> Seq.sum
        
    res
        
let secondPart(data: string seq) =
    let res =
        data
        |> Seq.map processData
        |> Seq.concat
        |> Seq.groupBy(fun (f, _, _, _) -> f)
        |> Seq.map(fun (_, f) -> f |> Seq.map(fun (_, r, g, b) -> r, g, b))
        |> Seq.map(fun f -> f |> Seq.toList |> List.unzip3)
        |> Seq.map(fun (rg, gs, bs) -> (rg |> List.max) * (gs |> List.max) * (bs |> List.max))
        |> Seq.sum
        
    res
        
    
