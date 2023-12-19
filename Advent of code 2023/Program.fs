open System
open Advent_of_code_2023


[<EntryPoint>]
let main argv =
    let stopWatch = System.Diagnostics.Stopwatch.StartNew()
    
    let filename =
        let d = DateTime.Now.Day
        //$"day{d}b"
        "day15b"
        
    let first =
        InputHandler.readFile filename 
        |> day15.firstPart 
        
    // let second =
    //     InputHandler.readFile filename 
    //     |> day18.secondPart
        
    printfn "%A, %A" first ()
    
    stopWatch.Stop()
    printfn "%f" stopWatch.Elapsed.TotalSeconds
    0 // return an integer exit code