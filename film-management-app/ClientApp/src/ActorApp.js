import React, { Component } from 'react';
import { Route, Routes } from 'react-router-dom';
import { ActorLayout } from './actor/ActorLayout';
import { MyMovies } from './actor/MyMovies';
import { Applicable } from './actor/Applicable';
import { Invitations } from './actor/Invitations';
import './custom.css';

export default class ActorApp extends Component {
  static displayName = ActorApp.name;

  render() {
    return (
      <ActorLayout>
        <Routes>
          <Route index element={<MyMovies />} />
          <Route path="/invites" element={<Invitations />} />
          <Route path="/applicable" element={<Applicable />} />
        </Routes>
      </ActorLayout>
    );
  }
}
