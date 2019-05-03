open FSharp.Data.GraphQL

type MyProvider = GraphQLProvider<"sample_schema.json">
//type MyProvider = GraphQLProvider<"http://192.168.56.1:8084">

[<EntryPoint>]
let main args =
    match args with
    | [|serverUrl|] ->
        let ball = MyProvider.Types.Ball(form = "Spheric", format = "Spheric", id = "1")
        let box = MyProvider.Types.Box(form = "Cubic", format = "Cubic", id = "2")
        
        let things : MyProvider.Types.IThing list = [ball; box]
        
        printfn "Things: %A\n" things

        let operation = 
            MyProvider.Operation<"""query q {
              hero (id: "1000") {
                name
                appearsIn
                homePlanet
                friends {
                  ... on Human {
                    name
                    homePlanet
                  }
                  ... on Droid {
                    name
                    primaryFunction
                  }
                }
              }
            }""">()
        
        let runtimeContext = MyProvider.GetContext(serverUrl = serverUrl)
        
        let result = operation.Run(runtimeContext)
        //let result = operation.AsyncRun(runtimeContext) |> Async.RunSynchronously
        
        let data = result.Data
        
        let errors = result.Errors
        
        let customData = result.CustomData
        
        printfn "Data: %A\n" data
        printfn "Errors: %A\n" errors
        printfn "Custom data: %A\n" customData
        
        let hero = data.Value.Hero.Value
        
        if hero.AppearsIn |> Array.exists (fun x -> x = MyProvider.Types.Episode.Empire)
        then printfn "Hero appears in Empire episode!\n"
        else printfn "Hero does not appear in Empire episode!\n"
        
        let friends = hero.Friends |> Array.choose id
        
        let humanFriends = friends |> Array.choose (fun x -> x.TryAsHuman())
        let droidFriends = friends |> Array.choose (fun x -> x.TryAsDroid())
        
        let humanFriendsCount = friends |> Array.map (fun x -> if x.IsHuman() then 1 else 0) |> Array.reduce (+)
        let droidFriendsCount = friends |> Array.map (fun x -> if x.IsDroid() then 1 else 0) |> Array.reduce (+)
        
        printfn "Hero friends (%i): %A\n" friends.Length friends
        printfn "Hero human friends (%i): %A\n" humanFriendsCount humanFriends
        printfn "Hero droid friends (%i): %A\n" droidFriendsCount droidFriends
        
        let parsed = operation.ParseResult("""{
          "documentId": -1401953899,
          "data": {
            "hero": {
              "name": "Luke Skywalker",
              "appearsIn": [
                "NewHope",
                "Empire",
                "Jedi"
              ],
              "homePlanet": "Tatooine",
              "friends": [
                {
                  "name": "Han Solo",
                  "homePlanet": null,
                  "__typename": "Human"
                },
                {
                  "name": "Leia Organa",
                  "homePlanet": "Alderaan",
                  "__typename": "Human"
                },
                {
                  "name": "C-3PO",
                  "primaryFunction": "Protocol",
                  "__typename": "Droid"
                },
                {
                  "name": "R2-D2",
                  "primaryFunction": "Astromech",
                  "__typename": "Droid"
                }
              ],
              "__typename": "Human"
            },
            "__typename": "Query"
          }
        }""")
        
        printfn "Parsed result data: %A" parsed.Data
        printfn "Parsed result custom data: %A" parsed.CustomData
        printfn "Parsed result errors: %A" parsed.Errors
        0
    | args -> 
      printfn "Invalid command usage. Needs server URL as the only argument. Passed args: %A" args
      1