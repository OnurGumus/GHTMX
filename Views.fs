module Views

type Message = { Text: string }

open Giraffe.ViewEngine
open Giraffe.ViewEngine.Htmx

let autoload =
    div [ _hxGet "/lazy-load-data"
          HxTrigger.Load
          |> HxTrigger.Delay "3s"
          |> _hxTrigger ] [
        str "Loading..."
    ]

let togglebutton =
    button [ attr "_" "on click toggle .hidden on me" ] [
        str "Toggle"
    ]


let layout (content: XmlNode list) =
    html [] [
        head [] [
            title [] [ encodedText "GHTMX" ]
            link [ _rel "stylesheet"
                   _type "text/css"
                   _href "/main.css" ]
            Htmx.Script.minified
            script [ _src "https://unpkg.com/hyperscript.org@0.9.7" ] []
        ]
        body [] content
    ]

let partial () = h1 [] [ encodedText "GHTMX" ]

let index (model: Message) =
    [ partial ()
      autoload
      togglebutton
      p [] [ encodedText model.Text ] ]
    |> layout
