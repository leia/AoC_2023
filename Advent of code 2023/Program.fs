open System
open Advent_of_code_2023


[<EntryPoint>]
let main argv =
    let filename =
        let d = DateTime.Now.Day
        $"day{d}b"
        
    let first =
        InputHandler.readFile filename 
        |> day7.firstPart
        
    let second =
        InputHandler.readFile filename 
        |> day7.secondPart
    printfn "%A, %A" first second
    0 // return an integer exit code