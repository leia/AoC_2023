module Advent_of_code_2023.Tests

open NUnit.Framework
open System
    
    
[<Test>]
let ``Day 1 first part`` () =
    let result =
        InputHandler.readTestFile "day1a" 
        |> day1.firstPart
    
    Assert.AreEqual(result, 142)

[<Test>]    
let ``Day 1 second part`` () =
    let result =
        InputHandler.readTestFile "day1a2" 
        |> day1.secondPart
    
    Assert.AreEqual(result, 281)
    
[<Test>]
let ``Day 2 first part`` () =
    let result =
        InputHandler.readTestFile "day2a" 
        |> day2.firstPart
    
    Assert.AreEqual(result, 8)

[<Test>]    
let ``Day 2 second part`` () =
    let result =
        InputHandler.readTestFile "day2a" 
        |> day2.secondPart
    
    Assert.AreEqual(result, 2286)
    
[<Test>]
let ``Day 3 first part`` () =
    let result =
        InputHandler.readTestFile "day3a" 
        |> day3.firstPart
    
    Assert.AreEqual(result, 4361)
    
[<Test>]
let ``Day 3 second part`` () =
    let result =
        InputHandler.readTestFile "day3a" 
        |> day3.secondPart
    
    Assert.AreEqual(result, 467835)
    
[<Test>]
let ``Day 4 first part`` () =
    let result =
        InputHandler.readTestFile "day4a" 
        |> day4.firstPart
    
    Assert.AreEqual(result, 13)
    
[<Test>]
let ``Day 4 second part`` () =
    let result =
        InputHandler.readTestFile "day4a" 
        |> day4.secondPart
    
    Assert.AreEqual(result, 30)
    
[<Test>]
let ``Day 5 first part`` () =
    let result =
        InputHandler.readTestFile "day5a" 
        |> day5.firstPart 
    
    Assert.AreEqual(result, 35)
    
[<Test>]
let ``Day 5 second part`` () =
    let result =
        InputHandler.readTestFile "day5a" 
        |> day5.secondPart 
    
    Assert.AreEqual(result, 46)


[<Test>]    
let ``Day 6 first part`` () =
    let result =
     InputHandler.readTestFile "day6a" 
     |> day6.firstPart 

    Assert.AreEqual(result, 288)
    
[<Test>]
let ``Day 6 second part`` () =
    let result =
        InputHandler.readTestFile "day6a" 
        |> day6.secondPart 
    
    Assert.AreEqual(result, 71503)
    
[<Test>]    
let ``Day 7 first part`` () =
    let result =
     InputHandler.readTestFile "day7a" 
     |> day7.firstPart 
    
    Assert.AreEqual(result, 6440)
    
[<Test>]
let ``Day 7 second part`` () =
    let result =
     InputHandler.readTestFile "day7a" 
     |> day7.secondPart 

    Assert.AreEqual(result, 5905)

[<Test>]    
let ``Day 8 first part`` () =
    //let result =
    //     InputHandler.readTestFile "day8a" 
    //     |> day8.firstPart 
    
    //Assert.AreEqual(result, ()) //TODO
    Assert.Inconclusive("Not implemented")
    
[<Test>]
let ``Day 8 second part`` () =
    // let result =
    //     InputHandler.readTestFile "day8a" 
    //     |> day8.secondPart 
    
    //Assert.AreEqual(result, ()) //TODO
    Assert.Inconclusive("Not implemented")
    
[<Test>]    
let ``Day 9 first part`` () =
    //let result =
    //     InputHandler.readTestFile "day8a" 
    //     |> day8.firstPart 
    
    //Assert.AreEqual(result, ()) //TODO
    Assert.Inconclusive("Not implemented")
    
[<Test>]
let ``Day 9 second part`` () =
    // let result =
    //     InputHandler.readTestFile "day8a" 
    //     |> day9.secondPart 
    
    //Assert.AreEqual(result, ()) //TODO
    Assert.Inconclusive("Not implemented")