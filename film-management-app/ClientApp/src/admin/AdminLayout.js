import React, { Component } from 'react';
import { Container } from 'reactstrap';
import { AdminNavMenu } from './NavMenu';

export class AdminLayout extends Component {
  static displayName = AdminLayout.name;

  render() {
    return (
      <div>
        <AdminNavMenu />
        <Container>
          {this.props.children}
        </Container>
      </div>
    );
  }
}
