import React, { Component } from 'react';
import { Collapse, Navbar, NavbarBrand, NavbarToggler, NavLink, NavItem } from 'reactstrap';
import { Link } from 'react-router-dom';
import './NavMenu.css';

export class ActorNavMenu extends Component {
  static displayName = ActorNavMenu.name;

  constructor (props) {
    super(props);

    this.toggleNavbar = this.toggleNavbar.bind(this);
    this.state = {
      collapsed: true
    };
  }

  toggleNavbar () {
    this.setState({
      collapsed: !this.state.collapsed
    });
  }

  render() {
    return (
      <header>
        <Navbar className="navbar-expand-sm navbar-toggleable-sm ng-white border-bottom box-shadow mb-3" container light>
          <NavbarBrand tag={Link} to="/">film_management_app</NavbarBrand>
          <NavbarToggler onClick={this.toggleNavbar} className="mr-2" />
          <Collapse className="d-sm-inline-flex flex-sm-row-reverse" isOpen={!this.state.collapsed} navbar>
            <ul className="navbar-nav flex-grow">
              <NavItem>
                <NavLink tag={Link} to="/" className="text-dark">My Movies</NavLink>
              </NavItem>
              <NavItem>
                <NavLink tag={Link} to="/invites" className="text-dark">My Invites</NavLink>
              </NavItem>
              <NavItem>
                <NavLink tag={Link} to="/applicable" className="text-dark">Applicable</NavLink>
              </NavItem>
              <NavItem>
                <a href="/Auth/Logout" className="text-dark">Logout</a>
              </NavItem>
            </ul>
          </Collapse>
        </Navbar>
      </header>
    );
  }
}
