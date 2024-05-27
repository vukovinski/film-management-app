import React, { Component } from 'react';
import { Route, Routes } from 'react-router-dom';
import { MyMovies } from './director/MyMovies';
import { CreateMovie } from './director/CreateMovie';
import { InviteActor } from './director/InviteActor';
import { MovieDetails } from './director/MovieDetails';
import { DirectorLayout } from './director/DirectorLayout';
import './custom.css';

export default class DirectorApp extends Component {
  static displayName = DirectorApp.name;

  render() {
    return (
      <DirectorLayout>
        <Routes>
          <Route index element={<MyMovies />} />
          <Route path="/create-movie" element={<CreateMovie />} />
          <Route path="/movie-details/:id" element={<MovieDetails />} />
          <Route path="/invite-actor/:movieId" element={<InviteActor />} />
        </Routes>
      </DirectorLayout>
    );
  }
}
