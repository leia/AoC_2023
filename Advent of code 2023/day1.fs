module Advent_of_code_2023.day1
open System
open Advent_of_code_2023.Helpers

let private extractLineDigit(line: string) =
    let res =
        line
        |> Seq.toList
        |> List.where(fun ch -> ch |> Char.IsDigit)
        |> List.map string
        
    let digit =
        if res.Length = 0 then 0
        else
            (res |> Seq.head)+(res |> Seq.last) |> int
        
    digit        
    
let firstPart(data: string seq) =
    let res =
        data
        |> Seq.map extractLineDigit
        |> Seq.sum        
    res
    
let wordlyDigits = ["one"; "two"; "three"; "four"; "five"; "six"; "seven"; "eight"; "nine"]

let getDigitIndices (line: string) (digitStr: string) =
    let indices = line |> String.IndicesOfAll digitStr
    let parsedDigit = (wordlyDigits |> Seq.findIndex(fun q -> q = digitStr)) + 1 |> string
    
    indices |> List.map(fun i -> i, parsedDigit)
        

let transferWordsIntoDigits (line: string) =    
    let wordsToNumbers =
        wordlyDigits
        |> Seq.where(fun s -> line.Contains(s, StringComparison.InvariantCultureIgnoreCase))
        |> Seq.map(fun str -> getDigitIndices line str)
        |> Seq.toList
        |> List.concat
    
    let digits =
        line
        |> Seq.toList
        |> Seq.indexed
        |> Seq.where(fun (i, ch) -> ch |> Char.IsDigit )
        |> Seq.map(fun (i, ch) -> i+1, ch |> string)
        |> Seq.toList
        
    let sorted =
       wordsToNumbers
       |> Seq.append digits
       |> Seq.sortBy fst
       |> Seq.map snd
       |> Seq.toList
       
    let digit =
        if sorted.Length = 0 then 0
        else
            (sorted |> Seq.head)+(sorted |> Seq.last) |> int
        
    digit
    
let secondPart (data: string seq) =
    let res =
        data
        |> Seq.map transferWordsIntoDigits
        |> Seq.sum
        
    res
        