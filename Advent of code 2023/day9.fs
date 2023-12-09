module Advent_of_code_2023.input.day9

let private processLine(line: string) =
    line.Split(" ") |> Seq.map int64
     
let rec private countHistory  (state: int64 seq)  =    
    match state |> Seq.toList with
    | [x] -> x
    | _ ->
        if (state |> Seq.forall(fun f -> f = 0L)) then 0L
        else
            let res =
                state
                |> Seq.windowed 2
                |> Seq.map (fun f -> f[1] - f[0])
            (state |> Seq.last) + (countHistory res)
            

let firstPart(data: string seq) = 
    let initStates =
        data
        |> Seq.map processLine
        |> Seq.toList
     
    let res =
        initStates
        |> Seq.map( fun f -> countHistory f)
        |> Seq.sum
        
    res
    
let secondPart(data: string seq) =
    let initStates =
        data
        |> Seq.map processLine
        |> Seq.toList
        
    let res =
        initStates
        |> Seq.map(fun f -> f |> Seq.rev)
        |> Seq.map( fun f -> countHistory f)
        |> Seq.sum
        
    res