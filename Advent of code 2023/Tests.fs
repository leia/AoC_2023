module Advent_of_code_2023.Tests

open NUnit.Framework
open System
    
    
[<Test>]
let ``Day 1 first part`` () =
    let fileName = "day1a"
    let result =
        InputHandler.readFile fileName 
        |> day1.firstPart
    
    Assert.AreEqual(result, 142)

[<Test>]    
let ``Day 1 second part`` () =
    let fileName = "day1a2"
    let result =
        InputHandler.readFile fileName 
        |> day1.secondPart
    
    Assert.AreEqual(result, 281)
    
[<Test>]
let ``Day 2 first part`` () =
    let fileName = "day2a"
    let result =
        InputHandler.readFile fileName 
        |> day2.firstPart
    
    Assert.AreEqual(result, 8)

[<Test>]    
let ``Day 2 second part`` () =
    let fileName = "day2a"
    let result =
        InputHandler.readFile fileName 
        |> day2.secondPart
    
    Assert.AreEqual(result, 2286)
    
[<Test>]
let ``Day 3 first part`` () =
    let fileName = "day3a"
    let result =
        InputHandler.readFile fileName 
        |> day3.firstPart
    
    Assert.AreEqual(result, 4361)
    
[<Test>]
let ``Day 3 second part`` () =
    let fileName = "day3a"
    let result =
        InputHandler.readFile fileName 
        |> day3.secondPart
    
    Assert.AreEqual(result, 467835)
    
[<Test>]
let ``Day 4 first part`` () =
    let fileName = "day4a"
    let result =
        InputHandler.readFile fileName 
        |> day4.firstPart
    
    Assert.AreEqual(result, 13)
    
[<Test>]
let ``Day 4 second part`` () =
    let fileName = "day4a"
    let result =
        InputHandler.readFile fileName 
        |> day4.secondPart
    
    Assert.AreEqual(result, 30)
    
[<Test>]
let ``Day 5 first part`` () =
    let fileName = "day5a"
    let result =
        InputHandler.readFile fileName 
        |> day5.firstPart 
    
    Assert.AreEqual(result, 35)
    
[<Test>]
let ``Day 5 second part`` () =
    let fileName = "day5a"
    let result =
        InputHandler.readFile fileName 
        |> day5.secondPart 
    
    Assert.AreEqual(result, 46)


[<Test>]    
let ``Day 6 first part`` () =
    let fileName = "day6a"
    let result =
     InputHandler.readFile fileName 
     |> day6.firstPart 

    Assert.AreEqual(result, 288)
    
[<Test>]
let ``Day 6 second part`` () =
    let fileName = "day6a"
    let result =
        InputHandler.readFile fileName 
        |> day6.secondPart 
    
    Assert.AreEqual(result, 71503)
    
[<Test>]    
let ``Day 7 first part`` () =
    let fileName = "day7a"
    // let result =
    //     InputHandler.readFile fileName 
    //     |> day5.firstPart 
    
    //Assert.AreEqual(result, ()) //TODO
    Assert.Fail("Not implemented")
    
[<Test>]
let ``Day 7 second part`` () =
    let fileName = "day7a"
    // let result =
    //     InputHandler.readFile fileName 
    //     |> day5.secondPart 
    
    //Assert.AreEqual(result, ()) //TODO
    Assert.Fail("Not implemented")
    
[<Test>]    
let ``Day 8 first part`` () =
    let fileName = "day8a"
    // let result =
    //     InputHandler.readFile fileName 
    //     |> day5.firstPart 
    
    //Assert.AreEqual(result, ()) //TODO
    Assert.Fail("Not implemented")
    
[<Test>]
let ``Day 8 second part`` () =
    let fileName = "day8a"
    // let result =
    //     InputHandler.readFile fileName 
    //     |> day5.secondPart 
    
    //Assert.AreEqual(result, ()) //TODO
    Assert.Fail("Not implemented")