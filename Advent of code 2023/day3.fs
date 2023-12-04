module Advent_of_code_2023.day3

open System
open System.Text.RegularExpressions

let private isSymbol(str: string) =
    let ch = str |> char
    (ch |> Char.IsDigit |> not) && (ch |> Char.IsLetter |> not) && (ch <> '.')
    
type NumberValue =
    {
        lineIndex: int
        startIndex: int
        endIndex: int
        value: int
    }

module NumberValue =
    let create(lineIndex: int) (startIndex: int) (endIndex: int) (value: int): NumberValue =
        {
          lineIndex = lineIndex
          startIndex = startIndex
          endIndex = endIndex
          value = value
        }
    
type Symbol =
    {
        lineIndex: int
        index: int
        value: string
    }
    
module Symbol =
    let create(lineIndex: int) (index: int) (str: string): Symbol =
        {
            lineIndex = lineIndex
            value = str
            index = index
        }
        
let private getSymbolsAndValues (lineIndex: int, accsymbols: Symbol list, accvalues: NumberValue list) (line: string) =
    let numberValues =
        Regex.Matches(line, "(\d+)")
        |> Seq.map(fun m -> NumberValue.create lineIndex m.Index (m.Index+m.Value.Length-1) (m.Value |> int))
        |> Seq.toList
        
    let symbols =
         Regex.Matches(line, "[^\.^\d]")
        |> Seq.map(fun m -> Symbol.create lineIndex m.Index m.Value)
        |> Seq.toList
    
    lineIndex+1, (accsymbols @ symbols), (accvalues @ numberValues)
        
let private isAdjacent  (symbols: Symbol seq) (number: NumberValue) =
    let min = number.startIndex - 1
    let max = number.endIndex + 1
    
    let r =
        symbols
        |> Seq.where(fun sym -> sym.index >= min && sym.index <= max && (sym.lineIndex >= number.lineIndex-1 && sym.lineIndex <= number.lineIndex+1))
    (r |> Seq.length) > 0
    
let firstPart(data: string seq) =
    let _, symbols, values =
        data
        |> Seq.fold getSymbolsAndValues (0, List.empty, List.empty)
    
    let r =
      values
      |> Seq.where(fun f -> f |> isAdjacent symbols)
      |> Seq.toList
      |> Seq.map(fun f -> f.value)
      |> Seq.sum      
    r

let private getSymbolsAndValues2 (lineIndex: int, accsymbols: Symbol list, accvalues: NumberValue list) (line: string) =
    let numberValues =
        Regex.Matches(line, "(\d+)")
        |> Seq.map(fun m -> NumberValue.create lineIndex m.Index (m.Index+m.Value.Length-1) (m.Value |> int))
        |> Seq.toList
        
    let symbols =
         Regex.Matches(line, "[*]")
        |> Seq.map(fun m -> Symbol.create lineIndex m.Index m.Value)
        |> Seq.toList
    
    lineIndex+1, (accsymbols @ symbols), (accvalues @ numberValues)

let private getGears (numbers: NumberValue seq) (symbol: Symbol)=
    let symbolBoundaries = [symbol.index - 1..symbol.index + 1] |> Set.ofList
    
    let r =
        numbers
        |> Seq.where(fun num ->
            let numberBoundaries = [num.startIndex..num.endIndex] |> Set.ofList
            let areAdjacent = Set.intersect symbolBoundaries numberBoundaries |> Set.isEmpty |> not
            
            areAdjacent && num.lineIndex >= symbol.lineIndex-1 && num.lineIndex <= symbol.lineIndex+1
            )
        |> Seq.toList
    
    if r.Length = 2 then (r |> List.head).value * (r |> List.last).value else 0
    
    
let secondPart(data: string seq) =
    let _, symbols, values =
        data
        |> Seq.fold getSymbolsAndValues2 (0, List.empty, List.empty)
    
    let r =
      symbols
      |> Seq.map(fun f -> f |> getGears values)
      |> Seq.toList
      |> Seq.sum
      
    r