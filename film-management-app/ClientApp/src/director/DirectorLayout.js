import React, { Component } from 'react';
import { Container } from 'reactstrap';
import { DirectorNavMenu } from './DirectorNavMenu';

export class DirectorLayout extends Component {
  static displayName = DirectorLayout.name;

  render() {
    return (
      <div>
        <DirectorNavMenu />
        <Container>
          {this.props.children}
        </Container>
      </div>
    );
  }
}
