import React, { Component } from 'react';
import { Container } from 'reactstrap';
import { ActorNavMenu } from './ActorNavMenu';

export class ActorLayout extends Component {
  static displayName = ActorLayout.name;

  render() {
    return (
      <div>
        <ActorNavMenu />
        <Container>
          {this.props.children}
        </Container>
      </div>
    );
  }
}
