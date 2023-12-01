open System
open Advent_of_code_2023
open Advent_of_code_2023.Helpers

let fileNames =
    let d = DateTime.Now.Day
    $"day{d}a", $"day{d}b"

[<EntryPoint>]
let main argv =
    let filenameEasy, fileNameHard = fileNames
    
    let first =
        InputHandler.readFile filenameEasy 
        |> day1.secondPart
        
    let second =
        InputHandler.readFile fileNameHard 
        |> day1.secondPart
    printfn "%A, %A" first second
    0 // return an integer exit code