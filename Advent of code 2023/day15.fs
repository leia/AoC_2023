module Advent_of_code_2023.day15

open System.Text.RegularExpressions


let private evalString(str: string) =
    let chars = str.ToCharArray() |> Array.map int
    let res  =
        chars |> Array.fold (fun acc ch -> (acc + ch) * 17 % 256) 0
    res
    
let firstPart(data: string seq) =
    let res =
        data
        |> Seq.map(fun f -> f.Split(','))
        |> Seq.head
        |> Seq.map evalString
        |> Seq.toArray
        |> Array.sum
    res
    
    
let getLabel(str: string) =
    let res = Regex.Matches(str, "^\w.") |> Seq.map(fun f -> f.Value) |> Seq.head
    res
  
let mutable labelMap = Map.empty

let private processCommand (boxes: string array array) (command: string) =
    let label = command |> getLabel
    let index = labelMap[command]
    
    let newBox =
        if command.Contains("-") then
            match boxes[index] with
            | [||] -> [||]
            | x ->
                let withLabel = x |> Array.where(fun f -> f.Contains(label))
                let res =
                    boxes[index] |> Array.except withLabel
                res
        else if command.Contains("=") then            
            let res =
                boxes[index] |> Array.map(fun f -> if f.Contains(label) then command else f)
                
            if (res |> Array.exists(fun f -> f.Contains(label))|> not) then
                Array.append res [|command|]
            else
                res
        else
            failwith "Unknown command"
    
    boxes |> Array.mapi(fun i f -> if i = index then newBox else f)
    
let secondPart(data: string seq) =
    let commands = (data |> Seq.head).Split(",")
    let labels =
        commands
        |> Seq.map(fun s ->
            let label = s |> getLabel
            let b = label |> evalString
            s, b
        )
        
    labelMap <- labels |> Map.ofSeq
    let max = labels |> Seq.maxBy snd |> snd
    let boxes = Array.init (max+1) (fun i -> [||])
    
    let res =
        commands
        |> Seq.fold processCommand boxes
        |> Seq.map(fun f -> f |> Array.map(fun s -> s.Split("=") |> Array.last |> int))
        |> Seq.mapi(fun boxIndex lenses ->
                lenses
                |> Seq.mapi(fun slot fp -> (boxIndex+1)*(slot+1)*fp) )
        |> Seq.concat
        |> Seq.sum
    
    res 