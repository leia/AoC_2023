module Advent_of_code_2023.day7

open Microsoft.FSharp.Core

type Ranks =
    | HighCards = 1
    | OnePair = 2
    | TwoPair = 3
    | ThreeOfAKind = 4
    | FullHouse = 5
    | FourOfAKind = 6
    | FiveOfAKind = 7

let private processLine(line: string) =
    let split = line.Split(" ")
    split[0], split[1] |> int
    
let private rankCards(hand: string, win: int) (withJoker: bool) =
    let groups = hand |> Seq.toList |> Seq.map string |> Seq.groupBy id |> Seq.map(fun (x, y) -> x, y |> Seq.length)
    
    let handType =
        match groups |> Seq.length with
        | 5 -> //12344
            let withoutJoker = Ranks.HighCards
            if withJoker = false then withoutJoker
            else
                groups
                |> Seq.tryFind(fun (s, l) -> s.Contains("J"))
                |> Option.map(fun _ -> Ranks.OnePair)
                |> Option.defaultValue withoutJoker 
        | 1 -> Ranks.FiveOfAKind
        | 2 -> //11222 //12222
            let withoutJoker =
                groups
                |> Seq.tryFind(fun (_, l) -> l = 2)
                |> Option.map (fun _ -> Ranks.FullHouse)
                |> Option.defaultValue Ranks.FourOfAKind
            if withJoker = false then withoutJoker
            else
                groups
                    |> Seq.tryFind(fun (s, l) -> s.Contains("J"))
                    |> Option.map (fun _ -> Ranks.FiveOfAKind)
                    |> Option.defaultValue withoutJoker
        | 4 ->
            //12J44 -> 1 2 444, 1 22 44
            //123JJ -> 1 2 333, 1 22 33     
            let withoutJoker = Ranks.OnePair
            if withJoker = false then withoutJoker
            else
                groups
                    |> Seq.tryFind(fun (s, l) -> s.Contains("J"))
                    |> Option.map(fun _ -> Ranks.ThreeOfAKind)
                    |> Option.defaultValue withoutJoker
        | 3 ->
            //1122J -> 11222 | 11122 -> fullHouse
            //11JJ3 -> 11113 | 11133 | 11333 -> fourOfAkind
            //1112J -> 11112 | 11122 -> fourOfAKind
            //JJJ23 -> fourOfAKind
            let withoutJoker =
                groups
                |> Seq.tryFind(fun (_, l) -> l = 2)
                |> Option.map (fun _ -> Ranks.TwoPair)
                |> Option.defaultValue Ranks.ThreeOfAKind
            if withJoker = false then withoutJoker
            else
                match withoutJoker with
                | Ranks.TwoPair ->
                    groups
                    |> Seq.tryFind(fun (s, l) -> s.Contains("J"))
                    |> Option.map(fun (_, l) -> if l = 1 then Ranks.FullHouse else Ranks.FourOfAKind) 
                    |> Option.defaultValue withoutJoker
                | Ranks.ThreeOfAKind ->                    
                    if withJoker = false then withoutJoker
                    else
                        groups
                        |> Seq.tryFind(fun (s, l) -> s.Contains("J"))
                        |> Option.map(fun _ -> Ranks.FourOfAKind)
                        |> Option.defaultValue withoutJoker 
                | _ -> withoutJoker
        | _ -> failwith "Unsupported hand length"
        
    handType, hand, win

let cardsMap =
    [2..14]
    |> Seq.zip (Seq.append ([2..9] |> Seq.map string) ["T"; "J"; "Q"; "K"; "A"])
    |> Map.ofSeq
        
 
        
let firstPart(data: string seq) =
    let res =
        data
        |> Seq.map processLine
        |> Seq.map (fun f -> rankCards f false)
        |> Seq.groupBy(fun (r,_,_) -> r)
        |> Seq.sortBy fst
        |> Seq.map(fun (r, hands) ->
                hands
                |> Seq.map(fun (_, a, b) -> a,b)
                |> Seq.sortWith(fun x y ->
                    let s =
                        Seq.zip (x |> fst |> Seq.toList |> Seq.map string) (y |> fst |> Seq.toList |> Seq.map string)
                        |> Seq.where(fun (f, s) -> f <> s)
                        |> Seq.tryHead
                        |> Option.map(fun s -> if (cardsMap |> Map.find (fst s)) > (cardsMap |> Map.find (snd s)) then 1 else -1)
                        |> Option.defaultValue 1
                    s
                    )
             )
        |> Seq.concat
        |> Seq.map snd
        |> Seq.indexed
        |> Seq.map(fun (i, j) -> i+1, j)
        |> Seq.map( fun (r, w) -> r * w)
        |> Seq.sum
 
    res 

let cardsWithJokerMap =
    [2..13]
    |> Seq.zip (Seq.append ([2..9] |> Seq.map string) ["T";  "Q"; "K"; "A"])
    |> Seq.append [("J", 1)]
    |> Map.ofSeq

let secondPart(data: string seq) =
    cardsWithJokerMap |> ignore
    0
    let res =
        data
        |> Seq.map processLine
        |> Seq.map (fun f -> rankCards f true)
        |> Seq.groupBy(fun (r,_,_) -> r)
        |> Seq.sortBy fst
        |> Seq.map(fun (r, hands) ->
                hands
                |> Seq.map(fun (_, a, b) -> a,b)
                |> Seq.sortWith(fun x y ->
                    let s =
                        Seq.zip (x |> fst |> Seq.toList |> Seq.map string) (y |> fst |> Seq.toList |> Seq.map string)
                        |> Seq.where(fun (f, s) -> f <> s)
                        |> Seq.tryHead
                        |> Option.map(fun s -> if (cardsWithJokerMap |> Map.find (fst s)) > (cardsWithJokerMap |> Map.find (snd s)) then 1 else -1)
                        |> Option.defaultValue 1
                    s
                    )
             )
        |> Seq.concat
        |> Seq.map snd
        |> Seq.indexed
        |> Seq.map(fun (i, j) -> i+1, j)
        |> Seq.map( fun (r, w) -> r * w)
        |> Seq.sum
 
    res 
