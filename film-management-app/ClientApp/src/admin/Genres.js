import { hostname } from '../server';
import React, { Component } from 'react';

export class Genres extends Component {
  static displayName = Genres.name;

  constructor(props) {
    super(props);
    this.state = { genres: [] };
    this.tryCreate = this.tryCreate.bind(this);
    this.tryDelete = this.tryDelete.bind(this);
    this.populateGenreData = this.populateGenreData.bind(this);
  }

  tryCreate() {
    fetch(`${hostname}/SuperUser/CreateGenre`, {
      method: "POST",
      headers: {
        "Content-Type": "application/json",
        "Authorization": sessionStorage.getItem("token")
      },
      body: JSON.stringify({
        name: document.getElementById("genres.newGenre").value
      })
    })
  }

  tryDelete(id) {
    fetch(`${hostname}/SuperUser/DeleteGenre/${id}`, {
      method: "DELETE",
      headers: {
        "Authorization": sessionStorage.getItem("token")
      }
    })
  }

  componentDidMount() {
    this.populateGenreData();
  }

  render() {
    return (
      <div style={{ display: "flex", flexDirection: "column", maxWidth: "250px" }}>
        <h2>Genres</h2>
        {this.state.genres.map(g =>
          <div style={{ display: "flex", flexDirection: "row" }} key={g.id}>
            <p>{g.name}</p>
            <button onClick={() => this.tryDelete(g.id)}>X</button>
          </div>)}
        <label>New genre</label>
        <input id="genres.newGenre" type="text"></input>
        <button onClick={this.tryCreate}>Submit</button>
      </div>
    )
  }

  async populateGenreData() {
    await fetch(`${hostname}/SuperUser/GetGenres`, {
      headers: {
        "Authorization": sessionStorage.getItem("token")
      }
    })
    .then(resp => resp.json())
    .then(data => this.setState({ genres: data }))
  }
}