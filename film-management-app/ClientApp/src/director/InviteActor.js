import { hostname } from '../server';
import React, { useEffect, useState } from 'react';
import { useParams, useNavigate } from 'react-router-dom';

export function InviteActor() {
  const navigate = useNavigate();
  const { movieId } = useParams();
  const [actors, setActors] = useState([]);
  const [movieDetails, setMovieDetails] = useState(null);
  const [selectedAuthor, setSelectedAuthor] = useState(null);

  function sendInvite() {
    fetch(`${hostname}/Director/InviteActor/${movieId}/${selectedAuthor}`, {
      method: "PUT", headers: {
        "Authorization": sessionStorage.getItem("token")
      }
    })
    .then(resp => {
      if (resp.ok) {
        navigate("/");
      }
    });
  }

  useEffect(() => {
    fetch(`${hostname}/Director/MovieDetails/${movieId}`, {
      headers: {
        "Authorization": sessionStorage.getItem("token")
      }
    })
    .then(resp => resp.json())
    .then(data => setMovieDetails(data));

    fetch(`${hostname}/Director/AllActors`, {
      headers: {
        "Authorization": sessionStorage.getItem("token")
      }
    })
    .then(resp => resp.json())
    .then(data => setActors(data));

  }, [movieId]);

  return <div style={{ display: "flex", flexDirection: "column", maxWidth: "250px" }}>
    <h2>Invite actor</h2>
    <label>Select actor</label>
    {movieDetails && <select id="inviteActor.selectedAuthor" value={selectedAuthor} onChange={(evt) => setSelectedAuthor(evt.target.value)}>
      {actors.map(a => <option value={a.id}>{a.fullName} - {a.currentFee}</option>) }
    </select>}
    <button onClick={sendInvite}>Send invite</button>
  </div>
}