import { hostname } from '../server';
import React, { useState, useEffect } from 'react';
import { useNavigate, useParams } from 'react-router-dom';

export function Apply() {
  const navigate = useNavigate();
  const { movieId } = useParams();
  const [fee, setFee] = useState(null);
  const [details, setDetails] = useState(null);

  useEffect(() => {
    fetch(`${hostname}/Actor/MovieDetails/${movieId}`, {
      headers: {
        "Authorization": sessionStorage.getItem("token")
      }
    })
    .then(resp => resp.json())
    .then(data => setDetails(data));

  }, [movieId]);

  function confirmClick() {
    fetch(`${hostname}/Actor/ApplyForMovie/${movieId}/${fee}`, {
      method: "PUT", headers: {
        "Authorization": sessionStorage.getItem("token")
      }
    })
    .then(resp => {
      if (resp.ok) {
        navigate("/")
      }
    });
  }

  return details && <div key={details.id} style={{ borderRadius: "10px", backgroundColor: "#DDDDDD", padding: "15px", marginBottom: "15px" }}>
    <h2>{details.title}</h2>
    <blockquote>{details.tagLine}</blockquote>
    <p>Planned shooting: {details.plannedShootingStartDate} - {details.plannedShootingEndDate}</p>
    <p>Planned budget: {details.budget} EUR</p>
    <p>Actors: {details.actors.map(a => `${a.fullName} | `)}</p>
    <div style={{ display: "flex", flexDirection: "column", maxWidth: "250px" }}>
      <label>Desired fee</label>
      <input type="text" value={fee} onChange={(evt) => setFee(evt.target.value)} />
      <button onClick={confirmClick}>Confirm</button>
    </div>
  </div>
}