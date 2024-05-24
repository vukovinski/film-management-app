import React, { Component } from 'react';
import { Route, Routes } from 'react-router-dom';
import { Layout } from './components/Layout';
import './custom.css';
import { AdminHome } from './admin/AdminHome';

export default class AdminApp extends Component {
  static displayName = AdminApp.name;

  render() {
    return (
      <Layout>
        <Routes>
          <Route index element={<AdminHome />} />
        </Routes>
      </Layout>
    );
  }
}
