import { hostname } from '../server';
import React, { Component } from 'react';
import { Navigate } from 'react-router-dom';

export class MyMovies extends Component {
  static displayName = MyMovies.name;

  constructor(props) {
    super(props);
    this.actorsClick = this.actorsClick.bind(this);
    this.detailsClick = this.detailsClick.bind(this);
    this.state = { movies: [], detail: -1, actors: false };
    this.populateMoviesData = this.populateMoviesData.bind(this);
  }

  componentDidMount() {
    this.populateMoviesData();
  }

  detailsClick(id) {
    this.setState({ movies: this.state.movies, detail: id, actors: this.state.actors });
  }

  actorsClick(id) {
    this.setState({ movies: this.state.movies, detail: this.state.detail, actors: id });
  }

  async populateMoviesData() {
    fetch(`${hostname}/Director/MyMovies`, {
      headers: {
        "Authorization": sessionStorage.getItem("token")
      }
    })
    .then(resp => resp.json())
    .then(data => this.setState({ movies: data, detail: this.state.detail, actors: this.state.actors }));
  }

  render() {
    return (
      <div>
        {this.state.movies.map(m =>
          this.state.detail === m.id ? <Navigate to={`/movie-details/${m.id}`} replace="true" /> :
          this.state.actors === m.id ? <Navigate to={`/invite-actor/${m.id}`} replace="true" />:
          <div key={m.id} style={{ borderRadius: "10px", backgroundColor: "#DDDDDD", padding: "15px", marginBottom: "15px" }}>
            <h2>{m.title}</h2>
            <blockquote>{m.tagLine}</blockquote>
            <p>Planned shooting: {m.plannedShootingStartDate} - {m.plannedShootingEndDate}</p>
            <p>Planned budget: {m.budget} EUR</p>
            <p>Actors: {m.actors.map(a => `${a.fullName} | `)}</p>
            <div style={{ display: "flex", flexDirection: "row" }}>
              <button onClick={() => this.actorsClick(m.id)}>Invite actors</button>
              <button onClick={() => this.detailsClick(m.id)}>Edit</button>
              <button disabled>Delete</button>
              <button disabled>Shoot</button>
            </div>
          </div>)}
      </div>
    );
  }
}