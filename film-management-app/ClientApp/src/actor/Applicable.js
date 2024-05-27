import { hostname } from '../server';
import React, { Component } from 'react';
import { Navigate } from 'react-router-dom';

export class Applicable extends Component {
  static displayName = Applicable.name;

  constructor(props) {
    super(props);
    this.state = { movies: [], apply: false };
    this.applyClick = this.applyClick.bind(this);
    this.populateMoviesData = this.populateMoviesData.bind(this);
  }

  componentDidMount() {
    this.populateMoviesData();
  }

  applyClick(movieId) {
    this.setState({ movies: this.state.movies, apply: true });
  }

  async populateMoviesData() {
    fetch(`${hostname}/Actor/OtherMovies`, {
      headers: {
        "Authorization": sessionStorage.getItem("token")
      }
    })
    .then(resp => resp.json())
    .then(data => this.setState({ movies: data, apply: this.state.negotiate }));
  }

  render() {
    return (
      <div>
        {this.state.movies.map(m =>
          this.state.apply ? <Navigate to={`/negotiate/${m.id}`} replace="true" /> :
            <div key={m.id} style={{ borderRadius: "10px", backgroundColor: "#DDDDDD", padding: "15px", marginBottom: "15px" }}>
              <h2>{m.title}</h2>
              <blockquote>{m.tagLine}</blockquote>
              <p>Planned shooting: {m.plannedShootingStartDate} - {m.plannedShootingEndDate}</p>
              <p>Planned budget: {m.budget} EUR</p>
              <p>Actors: {m.actors.map(a => `${a.fullName} | `)}</p>
              <div style={{ display: "flex", flexDirection: "row" }}>
                <button onClick={() => this.applyClick(m.id)}>Apply</button>
              </div>
            </div>)}
      </div>
    );
  }
}