module Views

type Message = { Text: string }

open Giraffe.ViewEngine
open Giraffe.ViewEngine.Htmx

let autoload =
    tag "my-comp"  [ attr "x-init" "htmx.process($el); Alpine.initTree($el.shadowRoot);" ] [
        template [ attr "shadowroot" "open"] [
            div [] [
            div [ 
                _hxGet "/lazy-load-data"
                _hxTarget "#foo"
                HxTrigger.Load
                |> HxTrigger.Delay "3s"
                |> _hxTrigger ] [
                str "Loading..."
                div [ _id "foo" ; attr "@htmx:after-swap" "Alpine.initTree($el)" ] [
                  h1 [ attr "x-data" "{ message: 'I ❤️ Alpin2e' }"; attr "x-text" "message"] []
                ]
            ]
            ]
        ]
    ]

let without = 
     div [] [
            div [ 
                _hxGet "/lazy-load-data"
                _hxTarget "#foo2"
                HxTrigger.Load
                |> HxTrigger.Delay "3s"
                |> _hxTrigger ] [
                str "Loading..."
                div [ _id "foo2";attr "@htmx:after-swap" "console.log('a')" ] [

                  // h1 [ attr "x-data" "{ message: 'I ❤️ Alpin2e' }"; attr "x-text" "message"] []
                   div [attr "x-data" "{ open: true }"; attr "x-effect" "console.log(open)"] []
                ]
            ]
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
                   _href "https://unpkg.com/open-props" ]

            link [ _rel "stylesheet"
                   _href "https://unpkg.com/open-props/normalize.min.css" ]
            link [ _rel "stylesheet"
                   _href "https://unpkg.com/open-props/buttons.min.css" ]
            link [ _rel "stylesheet"
                   _type "text/css"
                   _href "/main.css" ]
            //Htmx.Script.minified
            link [ _rel "stylesheet"
                   _href "https://cdn.jsdelivr.net/npm/@shoelace-style/shoelace@2.0.0-beta.80/dist/themes/light.css" ]
            script [ _type "module"
                     _src "https://cdn.jsdelivr.net/npm/@shoelace-style/shoelace@2.0.0-beta.80/dist/shoelace.js" ] []
            script [ _src "https://unpkg.com/hyperscript.org@0.9.7" ] []

        ]
        body [] [
            yield! content
            script [ _type "module"; _src "index.js" ] []
            script [ _src "https://unpkg.com/@alpinejs/morph@3.x.x/dist/cdn.min.js" ; attr "defer" "" ] []
            script [ _src "https://unpkg.com/alpinejs@3.x.x/dist/cdn.min.js" ; attr "defer" "" ] []

        ]
    ]

let sl_button = tag "sl-button"
let sl_input = tag "sl-input"
let todo_hamburger = tag "todo-hamburger"
let sl_popup = tag "sl-popup"
let _placement = attr "placement"
let _active = attr "active" ""
let _slot = attr "slot"
let _h = attr "_"

let index (model: Message) =
    [ //partial ()
      autoload
      togglebutton
      without
      h1 [ attr "x-data" "{ message: 'I ❤️ Alpine' }"; attr "x-text" "message"] []
      div [ attr "x-init" "console.log('Count is ')" ] [ str "dHello Onur"]
      p [] [ encodedText model.Text ]
      header [] [
          sl_button [] [ encodedText "Login" ]
          sl_button [] [ encodedText "Register" ]

          sl_popup [ _id "hamburger-menu"; _placement "bottom"; ] [
              sl_button [ _slot "anchor" ; _onclick "document.getElementById('hamburger-menu').toggleAttribute('active')"; ] [
                  rawText
                      """<svg viewBox="0 0 100 80" width="40" height="40">
    <rect width="100" height="20" rx="8"></rect>
    <rect y="30" width="100" height="20" rx="8"></rect>
    <rect y="60" width="100" height="20" rx="8"></rect>
</svg>"""
              ]
              sl_button [] [ encodedText "Register2" ]
          ]


      ]
      main [] [
          h1 [] [ encodedText "Your Todo List" ]
          sl_input [ _type "text"
                     _placeholder "Enter New Task" ] []
          sl_button [] [
              encodedText "Create task"
          ]
      ]
      footer [ _class "footer" ] [
          p [] [ encodedText "GHTMX" ]
      ] ]
    |> layout
