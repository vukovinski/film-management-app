import { hostname } from '../server';
import React, { Component } from 'react';

export class CreateDirector extends Component {
  static displayName = CreateDirector.name;

  constructor(props) {
    super(props);
    this.tryCreate = this.tryCreate.bind(this);
  }

  tryCreate() {
    fetch(`${hostname}/SuperUser/CreateDirector`, {
      method: "POST", headers: {
        "Content-Type": "application/json",
        "Authorization": sessionStorage.getItem("token")
      },
      body: JSON.stringify({
        "FullName": document.getElementById("createDirector.fullName").value,
        "Email": document.getElementById("createDirector.emailEntry").value,
        "PasswordHash": document.getElementById("createDirector.passwordEntry").value
      })
    });
  }

  render() {
    return (
      <div style={{ display: "flex", flexDirection: "column", maxWidth: "250px" }}>
        <h2>Create actor</h2>
        <label>Fullname</label>
        <input id="createDirector.fullName" type="text"></input>
        <label>Email</label>
        <input id="createDirector.emailEntry" type="text"></input>
        <label>Password</label>
        <input id="createDirector.passwordEntry" type="password"></input>
        <button onClick={this.tryCreate}>Submit</button>
      </div>
    )
  }
}