import React, { Component } from 'react';
import { Route, Routes } from 'react-router-dom';
import { ActorLayout } from './actor/ActorLayout';
import { MyMovies } from './actor/MyMovies';
import { Applicable } from './actor/Applicable';
import { Negotiate } from './actor/Negotiate';
import { Apply } from './actor/Apply';
import './custom.css';

export default class ActorApp extends Component {
  static displayName = ActorApp.name;

  render() {
    return (
      <ActorLayout>
        <Routes>
          <Route index element={<MyMovies />} />
          <Route path="/apply/:movieId" element={<Apply />}/>
          <Route path="/applicable" element={<Applicable />} />
          <Route path="/negotiate/:movieId" element={<Negotiate />} />
        </Routes>
      </ActorLayout>
    );
  }
}
