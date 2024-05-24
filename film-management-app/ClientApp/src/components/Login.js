import { hostname } from '../server';
import React, { Component } from 'react';

export default class Login extends Component {
  static displayName = Login.name;

  constructor(props) {
    super(props);
    this.state = { role: "" };
    this.tryLogin = this.tryLogin.bind(this);
    this.appForRole = this.appForRole.bind(this);
  }

  tryLogin() {
    const email = document.getElementById("login.emailEntry").value;
    const password = document.getElementById("login.passwordEntry").value;
    fetch(`${hostname}/Auth/Login?email=${encodeURIComponent(email)}&passwordHash=${encodeURIComponent(password)}`).then(async resp => {
      if (resp.ok) {
        const data = await resp.json();
        const userRole = data.role;
        sessionStorage.setItem("token", `Bearer ${data.token}`);

        this.setState({ role: userRole });
      }
    });
  }

  appForRole() {
    if (this.state.role === "admin")
      return this.props.admin;
    if (this.state.role === "actor")
      return this.props.actor;
    if (this.state.role === "director")
      return this.props.director;
  }

  render() {
    return (
      this.state.role === "" || this.state.role === "none"
        ? <div style={{ display: "flex", flexDirection: "column", maxWidth: "250px" }}>
          <h2>Login</h2>
          <label>Email</label>
          <input id="login.emailEntry" type="text"></input>
          <label>Password</label>
          <input id="login.passwordEntry" type="password"></input>
          <button onClick={this.tryLogin}>Submit</button>
        </div>
        : this.appForRole()
    )
  }
}