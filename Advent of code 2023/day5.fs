module Advent_of_code_2023.day5

open System
open System.Security.Cryptography.X509Certificates
open Microsoft.FSharp.Reflection

let private getSeeds(lines: string seq) =
    let seeds =
        lines
        |> Seq.map(fun line ->
            (line.Split(":")
            |> Seq.last).Split(" ", StringSplitOptions.RemoveEmptyEntries)
            |> Seq.map int64
        )
        |> Seq.concat
        |> Seq.distinct
    seeds
    
type MapperRange =
    {
        startIndex: int64
        endIndex: int64
    }
    
module MapperRange =
    let create s e: MapperRange =
        {
          startIndex = s
          endIndex = e
        }
    
let private getMappings(lines: string seq) =
    let categoryName = lines |> Seq.head
    
    let createMapping (line: string)  =
        let split = line.Split(" ") |> Array.map int64
        let dest, source, len = split[0], split[1], split[2]
        let sourceMapper = MapperRange.create source (source+len-1L)
        let destMapper = MapperRange.create dest (dest+len-1L)
        sourceMapper, destMapper
    
    let mappings = lines |> Seq.tail |> Seq.map createMapping 
    
    categoryName, mappings
    
let mutable private mappers = Map.empty

let getDirection (seed: int64) (mapperName: string)  =
    let mapper = mappers |> Map.find(mapperName)
    
    let res =
        mapper
        |> Seq.tryFind(fun (s, d) ->
            s.startIndex <= seed && seed <= s.endIndex)
        |> Option.map(fun (s, d) -> seed - s.startIndex + d.startIndex)
        |> Option.defaultValue seed
        
    res

let firstPart(data: string seq) = 
    let endSeedLine = (data |> Seq.findIndex(fun s -> s = String.Empty))-1
    let seeds = (data |> Seq.toList)[0..endSeedLine] |> getSeeds
    
    let m =
        data
        |> Seq.indexed |> Seq.where(fun (_, f) -> f = String.Empty) |> Seq.map fst |> Seq.pairwise
        |> Seq.map(fun (s, e) -> (data |> Seq.toList)[s+1..e-1] )
        |> Seq.toList
        |> Seq.map getMappings
        |> Map.ofSeq
    mappers <- m
    
    let chain = seq {"seed-to-soil map:"; "soil-to-fertilizer map:"; "fertilizer-to-water map:"; "water-to-light map:"; "light-to-temperature map:"; "temperature-to-humidity map:"; "humidity-to-location map:"}
          
    let res =
        seeds
        |> Seq.map(fun seed ->
                chain |> Seq.fold getDirection seed 
            )
        |> Seq.toList
        |> Seq.min
        
    res //|> Seq.length




let private chooseFn (f:(int * (int64 * int64)) array) startSeed endSeed indexedMappedLength =
    let indx, (x1, x2) = f[0]
    let indy, (y1, y2) = f[1]
    
    if indx = 0 && x1 > startSeed then
        Some (startSeed, x1-1L)
    else if indy = (indexedMappedLength-1) && y2 < endSeed then
        Some(y2+1L, endSeed)
    else
        if y1-x2 = 1L then None            
        else
            Some (x2+1L, y1-1L)

let private processSeeds (seeds: seq<int64 * int64>) (mapperName: string) =
    let mapper = mappers |> Map.find(mapperName)
    
    let hasIntersection (startIndex: int64, endIndex: int64) (startSeed: int64, endSeed: int64) =
        let first = Math.Max(startSeed, startIndex)
        let second  = Math.Min(endSeed, endIndex)
        first <= second
     
    let newSeeds =
        seeds
        |> Seq.map(fun (startSeed, endSeed) ->
            let withMapping =
                mapper
                |> Seq.where(fun (f, _) -> hasIntersection (f.startIndex, f.endIndex) (startSeed, endSeed))
                |> Seq.map(fun (f, d) ->
                    let first = Math.Max(startSeed, f.startIndex)
                    let second  = Math.Min(endSeed, f.endIndex)
                    
                    (first, second), f, d
                    )
                
            let mappedToDestination =
                withMapping
                |> Seq.map(fun (interval, sourceMapper, destMapper) ->
                    let ns = Math.Abs(sourceMapper.startIndex - fst interval) + destMapper.startIndex
                    let ne = destMapper.endIndex - (sourceMapper.endIndex- snd interval)
                    ns, ne
                    )
            let indexedMapped =
                withMapping
                |> Seq.map(fun (i, _, _) -> i)
                |> Seq.sortBy fst
                |> Seq.indexed
                |> Seq.toList
                
            if withMapping |> Seq.isEmpty then
                seq {(startSeed, endSeed)}
            else 
                let unmapped =
                    if (indexedMapped |> Seq.length) = 1 then
                        let _, (x1, x2) = indexedMapped |> Seq.head
                        if (x1 = startSeed && x2 = endSeed) then Seq.empty
                        else if(x1 = x2) then
                            if( x1 = startSeed) then seq {(startSeed+1L, endSeed)}
                            else if (x1 = endSeed) then seq {startSeed, endSeed-1L}
                            else seq {(startSeed, x1-1L); (x1+1L, endSeed)}
                        else
                            if (x1 = startSeed) then ((x2+1L, endSeed) |> Seq.singleton)
                            else if (x2 = endSeed) then ((x1, x2-1L) |> Seq.singleton)
                            else (seq {(startSeed, x1-1L); (x2+1L, endSeed)})
                    else    
                        indexedMapped
                        |> Seq.windowed 2
                        |> Seq.choose(fun f -> chooseFn f startSeed endSeed (indexedMapped |> Seq.length))
                Seq.append mappedToDestination unmapped
        )
        |> Seq.concat
        |> Seq.toList
        
    newSeeds |> Seq.ofList

let secondPart(ds: string seq) =
        let data = ds |> Seq.toList
        let endSeedLine = (data |> Seq.findIndex(fun s -> s = String.Empty))-1
        
        let getMappers =
            data
            |> Seq.indexed |> Seq.where(fun (_, f) -> f = String.Empty) |> Seq.map fst |> Seq.pairwise
            |> Seq.map(fun (s, e) -> (data |> Seq.toList)[s+1..e-1] )
            |> Seq.map getMappings
            |> Map.ofSeq
            
        mappers <- getMappers
       
        let chain = seq {"seed-to-soil map:"; "soil-to-fertilizer map:"; "fertilizer-to-water map:"; "water-to-light map:"; "light-to-temperature map:"; "temperature-to-humidity map:"; "humidity-to-location map:"}
        let seedsRanges =
            (data |> Seq.toList)[0..endSeedLine]
            |> getSeeds
            |> Seq.chunkBySize 2
            |> Seq.sortBy(fun f -> f |> Seq.head)
            |> Seq.map(fun f ->
                let startIndex, len = f[0], f[1]
                startIndex, startIndex+len-1L
                )
            |> Seq.toList
            
        let intervals =
            seedsRanges
            |> Seq.map(fun seedRange ->
                let r = chain |> Seq.fold processSeeds (seedRange |> Seq.singleton)
                r
            )
            |> Seq.concat
            |> Seq.map fst
            |> Seq.min
            
        intervals