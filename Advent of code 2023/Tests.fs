﻿module Advent_of_code_2023.Tests

open Advent_of_code_2023.input
open NUnit.Framework
open System
    
    
[<Test>]
let ``Day 1 first part`` () =
    let result =
        InputHandler.readTestFile "day1a" 
        |> day1.firstPart
    
    Assert.AreEqual(142, result)

[<Test>]    
let ``Day 1 second part`` () =
    let result =
        InputHandler.readTestFile "day1a2" 
        |> day1.secondPart
    
    Assert.AreEqual(281, result)
    
[<Test>]
let ``Day 2 first part`` () =
    let result =
        InputHandler.readTestFile "day2a" 
        |> day2.firstPart
    
    Assert.AreEqual(8, result)

[<Test>]    
let ``Day 2 second part`` () =
    let result =
        InputHandler.readTestFile "day2a" 
        |> day2.secondPart
    
    Assert.AreEqual(2286, result)
    
[<Test>]
let ``Day 3 first part`` () =
    let result =
        InputHandler.readTestFile "day3a" 
        |> day3.firstPart
    
    Assert.AreEqual(4361, result)
    
[<Test>]
let ``Day 3 second part`` () =
    let result =
        InputHandler.readTestFile "day3a" 
        |> day3.secondPart
    
    Assert.AreEqual(467835, result)
    
[<Test>]
let ``Day 4 first part`` () =
    let result =
        InputHandler.readTestFile "day4a" 
        |> day4.firstPart
    
    Assert.AreEqual(13, result)
    
[<Test>]
let ``Day 4 second part`` () =
    let result =
        InputHandler.readTestFile "day4a" 
        |> day4.secondPart
    
    Assert.AreEqual(30, result)
    
[<Test>]
let ``Day 5 first part`` () =
    let result =
        InputHandler.readTestFile "day5a" 
        |> day5.firstPart 
    
    Assert.AreEqual(35, result)
    
[<Test>]
let ``Day 5 second part`` () =
    let result =
        InputHandler.readTestFile "day5a" 
        |> day5.secondPart 
    
    Assert.AreEqual(46, result)


[<Test>]    
let ``Day 6 first part`` () =
    let result =
     InputHandler.readTestFile "day6a" 
     |> day6.firstPart 

    Assert.AreEqual(288, result)
    
[<Test>]
let ``Day 6 second part`` () =
    let result =
        InputHandler.readTestFile "day6a" 
        |> day6.secondPart 
    
    Assert.AreEqual(71503, result)
    
[<Test>]    
let ``Day 7 first part`` () =
    let result =
     InputHandler.readTestFile "day7a" 
     |> day7.firstPart 
    
    Assert.AreEqual(6440, result)
    
[<Test>]
let ``Day 7 second part`` () =
    let result =
     InputHandler.readTestFile "day7a" 
     |> day7.secondPart 

    Assert.AreEqual(5905, result)

// [<Test>]    
// let ``Day 8 first part`` () =
//     let result =
//          InputHandler.readTestFile "day8a" 
//          |> day8.firstPart 
//     
//     Assert.AreEqual(result, 2) 
    
// [<Test>]
// let ``Day 8 second part`` () =
//     let result =
//          InputHandler.readTestFile "day8a" 
//          |> day8.secondPart 
//     
//     Assert.AreEqual(result, ())
    
[<Test>]    
let ``Day 9 first part`` () =
    let result =
         InputHandler.readTestFile "day9a" 
         |> day9.firstPart 
    
    Assert.AreEqual(114, result)
    
[<Test>]
let ``Day 9 second part`` () =
    let result =
         InputHandler.readTestFile "day9a" 
         |> day9.secondPart 
    
    Assert.AreEqual(2, result)
 
[<Test>]   
let ``Day 10 first part`` () =
    let result =
         InputHandler.readTestFile "day10a" 
         |> day10.firstPart 
    
    Assert.AreEqual(8, result) 
    
[<Test>]
let ``Day 10 second part`` () =
    let result =
         InputHandler.readTestFile "day10a2" 
         |> day10.secondPart 
    
    Assert.AreEqual(4, result) 