open System
open Advent_of_code_2023

let fileNames =
    let d = DateTime.Now.Day
    $"day{d}a", $"day{d}b" 
    //"day3a", "day3b"

[<EntryPoint>]
let main argv =
    let filenameEasy, fileNameHard = fileNames
    
    // let first =
    //     InputHandler.readFile fileNameHard 
    //     |> day4.firstPart
        
    let second =
        InputHandler.readFile fileNameHard 
        |> day4.secondPart
    printfn "%A, %A" 0 second
    0 // return an integer exit code