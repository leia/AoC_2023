module Advent_of_code_2023.InputHandler

open System.IO

let readFile (filename: string) =
    let path = $"../../../input/{filename}.txt"
    File.ReadLines(path) |> Seq.cast<string>
    