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
            link [_rel "stylesheet"; _href "https://cdn.jsdelivr.net/npm/@shoelace-style/shoelace@2.0.0-beta.80/dist/themes/light.css"]
            script [_type "module"; _src "https://cdn.jsdelivr.net/npm/@shoelace-style/shoelace@2.0.0-beta.80/dist/shoelace.js" ] []
            script [ _src "https://unpkg.com/hyperscript.org@0.9.7" ] []
        ]
        body [] content
    ]

let sl_button = tag "sl-button"
let sl_input = tag "sl-input"

let index (model: Message) =
    [ //partial ()
      //autoload
      //togglebutton
      // p [] [ encodedText model.Text ]
      header [] [
          sl_button [] [ encodedText "Login"]
          sl_button [] [ encodedText "Register" ]
      ]
      main [] [
          h1 [] [ encodedText "Your Todo List" ]
          sl_input [ _type "text"; _placeholder "Enter New Task" ] []
          sl_button [] [ encodedText "Create task"]
      ]
      footer [ _class "footer" ] [
          p [] [ encodedText "GHTMX" ]
      ] ]
    |> layout
