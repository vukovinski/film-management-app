import { hostname } from '../server';
import { useParams } from 'react-router-dom';
import React, { useEffect, useState } from 'react';

export function InviteActor() {
  const { movieId } = useParams();
  const [actors, setActors] = useState([]);
  const [movieDetails, setMovieDetails] = useState(null);
  const [selectedAuthor, setSelectedAuthor] = useState(-1);

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
    {movieDetails && <select value={selectedAuthor} onChange={(evt) => setSelectedAuthor(evt.target.value)}>
      {actors.map(a => <option value={a.id}>{a.fullName} - {a.currentFee}</option>) }
    </select>}
    <button>Invite author</button>
  </div>
}