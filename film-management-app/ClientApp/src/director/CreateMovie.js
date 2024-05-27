import { hostname } from '../server';
import React, { Component } from 'react';

export class CreateMovie extends Component {
  static displayName = CreateMovie.name;

  constructor(props) {
    super(props);
    this.state = { genres: [] };
    this.tryCreate = this.tryCreate.bind(this);
    this.populateGenreData = this.populateGenreData.bind(this);
  }

  tryCreate() {
    fetch(`${hostname}/Director/CreateMovie`, {
      method: "POST", headers: {
        "Content-Type": "application/json",
        "Authorization": sessionStorage.getItem("token")
      },
      body: JSON.stringify({
        "Title": document.getElementById("createMovie.title").value,
        "TagLine": document.getElementById("createMovie.tagLine").value,
        "Budget": Number(document.getElementById("createMovie.budget").value),
        "Genres": Array.from(document.getElementById("createMovie.genres").selectedOptions).map(o => {
          return {
            Id: Number(o.value),
            Name: o.innerText
          }
        }),
        "PlannedShootingStartDate": document.getElementById("createMovie.start").value,
        "PlannedShootingEndDate": document.getElementById("createMovie.end").value
      })
    });
  }

  componentDidMount() {
    this.populateGenreData();
  }

  render() {
    return (
      <div style={{ display: "flex", flexDirection: "column", maxWidth: "250px" }}>
        <h2>Create movie</h2>
        <label>Title</label>
        <input id="createMovie.title" type="text"></input>
        <label>Tag line</label>
        <input id="createMovie.tagLine" type="text"></input>
        <label>Budget</label>
        <input id="createMovie.budget" type="text"></input>
        <label>Genres</label>
        <select id="createMovie.genres" multiple>
          {this.state.genres.map(g => 
            <option key={g.id} value={g.id}>{g.name}</option>
          )}
        </select>
        <label>Shooting starts</label>
        <input id="createMovie.start" type="date"></input>
        <label>Shooting ends</label>
        <input id="createMovie.end" type="date"></input>
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