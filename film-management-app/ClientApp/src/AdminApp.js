import React, { Component } from 'react';
import { Route, Routes } from 'react-router-dom';
import { AdminLayout } from './admin/AdminLayout';
import './custom.css';
import { AdminHome } from './admin/AdminHome';
import { CreateActor } from './admin/CreateActor';
import { CreateDirector } from './admin/CreateDirector';
import { Genres } from './admin/Genres';

export default class AdminApp extends Component {
  static displayName = AdminApp.name;

  render() {
    return (
      <AdminLayout>
        <Routes>
          <Route index element={<AdminHome />} />
          <Route path="/create-actor" element={<CreateActor />} />
          <Route path="/create-director" element={<CreateDirector />} />
          <Route path="/genres" element={<Genres />} />
        </Routes>
      </AdminLayout>
    );
  }
}
