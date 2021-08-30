import React, { useState, useEffect } from "react";
import logo from "./logo.svg";
import "./App.css";

const { HelloRequest } = require("./public/greet_pb.js");
const { GreeterClient } = require("./public/greet_grpc_web_pb.js");


function App() {
  const [helloResponse, setHelloResponse] = useState({
    text: "Waiting response from server...",
  });
 
  function refresh() {
    let unaryRequest = new HelloRequest();
    unaryRequest.setName("Turkish Airlines Technology");
    new GreeterClient("https://localhost:5001").sayHello(
      unaryRequest,
      {
        // metadata
      },
      function (error, response) {
        if (error) {
          console.log(`ERROR-(${error.code}): ${error.message}`);
        } else {
          setHelloResponse({ text: response.getMessage() + " " +new Date() });
        }
      }
    );

   
  }

  useEffect(() => refresh(), []);

  return (
    <div className="App">
      <header className="App-header">
        <p>{helloResponse.text}</p>&nbsp;
        <button onClick={refresh}>Call Again!</button>
      </header>
    </div>
  );
}

export default App;
