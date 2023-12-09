open System
open Advent_of_code_2023
open Advent_of_code_2023.input


[<EntryPoint>]
let main argv =
    let filename =
        let d = DateTime.Now.Day
        $"day{d}b"
        
    let first =
        InputHandler.readFile filename 
        |> day9.firstPart
        
    let second =
        InputHandler.readFile filename 
        |> day9.secondPart
    printfn "%A, %A" first second
    0 // return an integer exit code