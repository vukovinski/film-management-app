import { hostname } from '../server';
import React, { Component } from 'react';
import { Navigate } from 'react-router-dom';

export class MyMovies extends Component {
  static displayName = MyMovies.name;

  constructor(props) {
    super(props);
    this.state = { movies: [], negotiate: false };
    this.negotiateClick = this.negotiateClick.bind(this);
    this.populateMoviesData = this.populateMoviesData.bind(this);
  }

  componentDidMount() {
    this.populateMoviesData();
  }

  negotiateClick(movieId) {
    this.setState({ movies: this.state.movies, negotiate: true });
  }

  async populateMoviesData() {
    fetch(`${hostname}/Actor/MyMovies`, {
      headers: {
        "Authorization": sessionStorage.getItem("token")
      }
    })
    .then(resp => resp.json())
    .then(data => this.setState({ movies: data, negotiate: this.state.negotiate }));
  }

  render() {
    return (
      <div>
        {this.state.movies.map(m =>
          this.state.negotiate ? <Navigate to={`/negotiate/${m.id}`} replace="true" /> :
          <div key={m.id} style={{ borderRadius: "10px", backgroundColor: "#DDDDDD", padding: "15px", marginBottom: "15px" }}>
            <h2>{m.title}</h2>
            <blockquote>{m.tagLine}</blockquote>
            <p>Planned shooting: {m.plannedShootingStartDate} - {m.plannedShootingEndDate}</p>
            <p>Planned budget: {m.budget} EUR</p>
            <p>Actors: {m.actors.map(a => `${a.fullName} | `)}</p>
            <div style={{ display: "flex", flexDirection: "row" }}>
              <button onClick={() => this.negotiateClick(m.id)}>Negotiate</button>
            </div>
          </div>)}
      </div>
    );
  }
}