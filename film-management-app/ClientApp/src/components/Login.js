import { hostname } from '../server';
import React, { Component } from 'react';

export default class Login extends Component {
  static displayName = Login.name;

  constructor(props) {
    super(props);
    this.onAuthed = this.props.onAuthed;
    this.tryLogin = this.tryLogin.bind(this);
  }

  tryLogin() {
    const email = document.getElementById("emailEntry").value;
    const password = document.getElementById("passwordEntry").value;
    fetch(`${hostname}/Auth/Login?email=${encodeURIComponent(email)}&passwordHash=${encodeURIComponent(password)}`).then(resp => {
      if (resp.ok) {
        const data = resp.json();
        const userRole = data.role;
        sessionStorage.setItem("token", `Bearer ${data.token}`);

        this.onAuthed(userRole);
      }
    });
  }

  render() {
    return (
      <div>
        <h2>Login</h2>
        <label>Email</label>
        <input id="emailEntry" type="text"></input>
        <label>Password</label>
        <input id="passwordEntry" type="password"></input>
        <button onClick={this.tryLogin}>Submit</button>
      </div>
    )
  }
}