module Advent_of_code_2023.Helpers

module String =
    let IndicesOfAll (needle : string) (haystack : string): int list =
        let rec loop (indices: int list) (index : int) =
            if index >= String.length haystack then indices
            else
                match haystack.IndexOf(needle, index) with
                | -1 -> indices
                | idx -> loop (indices @ [idx+1]) (idx + 1)
        if String.length needle = 0 then [] else loop [] 0
        

