import 'bootstrap/dist/css/bootstrap.css';
import React from 'react';
import { createRoot } from 'react-dom/client';
import { BrowserRouter } from 'react-router-dom';
import AdminApp from './AdminApp';
import ActorApp from './ActorApp';
import DirectorApp from './DirectorApp';
import Login from './components/Login';
import * as serviceWorkerRegistration from './serviceWorkerRegistration';
import reportWebVitals from './reportWebVitals';

const baseUrl = document.getElementsByTagName('base')[0].getAttribute('href');
const rootElement = document.getElementById('root');
const root = createRoot(rootElement);

var role = "";

function appForRole() {
  if (role === "admin")
    return <AdminApp />;

  if (role === "actor")
    return <ActorApp />;

  if (role === "director")
    return <DirectorApp />;
}

root.render(
  <BrowserRouter basename={baseUrl}>
    {role === "" || role === "none" ? <Login onAuthed={(userRole => role = userRole)} /> : appForRole()}
  </BrowserRouter>);

// If you want your app to work offline and load faster, you can change
// unregister() to register() below. Note this comes with some pitfalls.
// Learn more about service workers: https://cra.link/PWA
serviceWorkerRegistration.unregister();

// If you want to start measuring performance in your app, pass a function
// to log results (for example: reportWebVitals(console.log))
// or send to an analytics endpoint. Learn more: https://bit.ly/CRA-vitals
reportWebVitals();
