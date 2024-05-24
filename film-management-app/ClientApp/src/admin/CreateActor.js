import { hostname } from '../server';
import React, { Component } from 'react';

export class CreateActor extends Component {
  static displayName = CreateActor.name;

  constructor(props) {
    super(props);
    this.tryCreate = this.tryCreate.bind(this);
  }

  tryCreate() {
    fetch(`${hostname}/SuperUser/CreateActor`, {
      method: "POST", headers: {
        "Content-Type": "application/json",
        "Authorization": sessionStorage.getItem("token")
      },
      body: JSON.stringify({
        "FullName": document.getElementById("createActor.fullName").value,
        "Email": document.getElementById("createActor.emailEntry").value,
        "PasswordHash": document.getElementById("createActor.passwordEntry").value,
        "ExpectedFee": Number(document.getElementById("createActor.expectedFee").value)
      })
    });
  }

  render() {
    return (
      <div style={{ display: "flex", flexDirection: "column", maxWidth: "250px" }}>
        <h2>Create actor</h2>
        <label>Fullname</label>
        <input id="createActor.fullName" type="text"></input>
        <label>Email</label>
        <input id="createActor.emailEntry" type="text"></input>
        <label>Password</label>
        <input id="createActor.passwordEntry" type="password"></input>
        <label>Expected fee</label>
        <input id="createActor.expectedFee" type="text"></input>
        <button onClick={this.tryCreate}>Submit</button>
      </div>
    )
  }
}