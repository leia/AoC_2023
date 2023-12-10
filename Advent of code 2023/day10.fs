module Advent_of_code_2023.day10

open System.Collections.Generic
open System.Text.RegularExpressions
open System
open Microsoft.FSharp.Core

type TileType =
    | TopLeftCorner
    | TopRightCorner
    | BottomLeftCorner
    | BottomRightCorner
    | Horizontal
    | Vertical
    | Start
    
module TileType =
    let ofValue (str: string): TileType =
        match str with
        | "S" -> Start
        | "7" -> TopRightCorner
        | "L" -> BottomLeftCorner
        | "J" -> BottomRightCorner
        | "F" -> TopLeftCorner
        | "|" -> Vertical
        | "-" -> Horizontal
        | x -> failwith $"Unsupported tile type {x}"
    
    
type Tile =
    {
        tileType: TileType
        lineIndex: int
        index: int
    }
    
module Tile =
    let create (tile: string) lineIndex index: Tile =
        {
          tileType = tile |> TileType.ofValue
          lineIndex = lineIndex
          index = index
        }
        

let private processLine (lineIndex: int, line: string) =
    let relevantTiles =
        Regex.Matches(line, "[^.]")
        |> Seq.map(fun m -> Tile.create m.Value lineIndex m.Index )
        
    relevantTiles

let mutable newPath = new List<Tile*int>() 

let processTiles  (tiles: Tile seq) =
    let getDirection first second =
        if second.lineIndex > first.lineIndex && second.index = first.index then 1
        else if second.lineIndex < first.lineIndex && second.index = first.index then -1
        else if second.lineIndex = first.lineIndex && second.index < first.index then -1
        else 1
        
    let rec getNextTileDest (previousTile: Tile) (currentTile: Tile) (direction: int) (stepsCount: int) =
        newPath.Add(currentTile, direction)
        let nt =
            if previousTile.tileType = Start && previousTile.lineIndex = currentTile.lineIndex then Horizontal
            else if previousTile.tileType = Start && previousTile.index = currentTile.index then Vertical
            else Start
        
        let nPrev = {previousTile with tileType = nt }    
            
        let li, i =
            match nPrev.tileType, currentTile.tileType with
            | Vertical, TopLeftCorner ->                
                    currentTile.lineIndex, currentTile.index+1
            | Horizontal, TopLeftCorner ->
                    currentTile.lineIndex+1, currentTile.index
            | _, TopLeftCorner ->
                if previousTile.lineIndex = currentTile.lineIndex then
                    if previousTile.index > currentTile.index then                        
                        currentTile.lineIndex+1, currentTile.index
                    else
                        currentTile.lineIndex, currentTile.index+1
                else if previousTile.lineIndex > currentTile.lineIndex then
                        currentTile.lineIndex, currentTile.index+1
                else
                    currentTile.lineIndex, currentTile.index
            | _, Horizontal  ->
                currentTile.lineIndex, currentTile.index+direction
            | _, Vertical ->
                currentTile.lineIndex+direction, currentTile.index
            | Vertical, TopRightCorner ->
                    currentTile.lineIndex, currentTile.index-1
            | Horizontal, TopRightCorner ->
                    currentTile.lineIndex+1, currentTile.index
            | _, TopRightCorner ->
                if previousTile.index < currentTile.index then
                    currentTile.lineIndex+1, currentTile.index
                else
                    currentTile.lineIndex, currentTile.index-1
            | Vertical, BottomRightCorner ->
                    currentTile.lineIndex, currentTile.index-1
            | Horizontal, BottomRightCorner ->
                    currentTile.lineIndex-1, currentTile.index
            | _, BottomRightCorner ->
                if previousTile.lineIndex < currentTile.lineIndex then
                    currentTile.lineIndex, currentTile.index-1
                else
                    currentTile.lineIndex-1, currentTile.index
            | Vertical, BottomLeftCorner ->
                    currentTile.lineIndex, currentTile.index+1
            | Horizontal, BottomLeftCorner ->
                    currentTile.lineIndex-1, currentTile.index
            | _, BottomLeftCorner ->
                if previousTile.lineIndex < currentTile.lineIndex then
                    currentTile.lineIndex, currentTile.index+1
                else
                    currentTile.lineIndex-1, currentTile.index
                
            
        let next = tiles |> Seq.find(fun t -> t.lineIndex = li && t.index = i) 
        
        match next.tileType with
        | Start -> stepsCount+1
        | _ ->
            let dir = getDirection currentTile next
            getNextTileDest currentTile next dir (stepsCount+1) 
        
        
    let start = tiles |> Seq.find(fun t -> t.tileType = TileType.Start)
    let lineIndices = [start.lineIndex+1; start.lineIndex-1; start.lineIndex]
    let indices = [start.index+1; start.index-1; start.index]
    
    let startingPoint =
        tiles
        |> Seq.where(fun t -> lineIndices |> Seq.contains(t.lineIndex) && indices |> Seq.contains(t.index))
        |> Seq.where(fun t ->
                     t.lineIndex = start.lineIndex && t.index = start.index+1 && (t.tileType = BottomRightCorner || t.tileType = TopRightCorner || t.tileType = Horizontal)
                     || t.lineIndex = start.lineIndex && t.index = start.index-1 && (t.tileType = BottomLeftCorner || t.tileType = TopLeftCorner || t.tileType = Horizontal)
                     || t.lineIndex = start.lineIndex+1 && t.index = start.index && (t.tileType = TopLeftCorner || t.tileType = TopRightCorner || t.tileType = Vertical)
                     || t.lineIndex = start.lineIndex-1 && t.index = start.index && (t.tileType = BottomLeftCorner || t.tileType = BottomRightCorner || t.tileType = Vertical)
                     )
        |> Seq.head //doesn't matter which one I take
    let startingDir = (getDirection start startingPoint)
    let res = getNextTileDest start startingPoint startingDir  1 
        
    res

//create list of objects - or maybe map with key [pathTile][lineIndex][columnIndex]
//find S
//find two adjascent path tiles
//pick one
//travel through path based on path tile type
//get path length / 2

let firstPart(data: string seq) =
    let res =
        data
        |> Seq.indexed
        |> Seq.map processLine
        |> Seq.concat
        |> Seq.toList
        |> processTiles
        
    res / 2

  
let mutable trappedTiles = new List<int*int>()

let t = -1
let private getTrappedTiles () =                      
    let proceed (tile: Tile, dir: int)  =
        let rec check (lineIndex: int, index: int) =            
            if (newPath.FindAll(fun (t, _) -> t.lineIndex = lineIndex && t.index = index).Count > 0) then ()
            else
                if (trappedTiles.Contains(lineIndex, index)) then ()
                else
                    //Console.WriteLine(trappedTiles.Count.ToString())
                    trappedTiles.Add(lineIndex, index)
                    seq {(lineIndex+1, index); (lineIndex-1, index); (lineIndex, index+1); (lineIndex, index-1)}
                    |> Seq.iter check
                    
        let li, i =
            match tile.tileType with
            | Horizontal ->
                if dir = 1 then //from left to right
                    tile.lineIndex+t, tile.index
                else
                    tile.lineIndex-t, tile.index
            | Vertical ->
                if dir = 1 then //from top to bottom
                    tile.lineIndex, tile.index-t
                else
                    tile.lineIndex, tile.index+t
                    
        check (li, i)
        
   
    newPath.FindAll(fun (t, _) -> t.tileType = Horizontal || t.tileType = Vertical)
    |> Seq.iter proceed
        
    trappedTiles.Count
        
    

let secondPart(data: string seq) =
    let path =
        data
        |> Seq.indexed
        |> Seq.map processLine
        |> Seq.concat
        |> Seq.toList
        |> processTiles
        
    path |> ignore
    
    let res = getTrappedTiles()
    res 
        
   

