import React, { Component } from 'react';
import { Route, Routes } from 'react-router-dom';
import AppRoutes from './AppRoutes';
import { AdminLayout } from './admin/AdminLayout';
import './custom.css';

export default class ActorApp extends Component {
  static displayName = ActorApp.name;

  render() {
    return (
      <AdminLayout>
        <Routes>
          {AppRoutes.map((route, index) => {
            const { element, ...rest } = route;
            return <Route key={index} {...rest} element={element} />;
          })}
        </Routes>
      </AdminLayout>
    );
  }
}
