module Advent_of_code_2023.Helpers

module String =
    let IndicesOfAll (substring : string) (str : string): int list =
        let rec loop (indices: int list) (index : int) =
            if index >= String.length str then indices
            else
                match str.IndexOf(substring, index) with
                | -1 -> indices
                | idx -> loop (indices @ [idx+1]) (idx + 1)
        if String.length substring = 0 then [] else loop [] 0
        

