import { hostname } from '../server';
import React, { useState, useEffect } from 'react';
import { useNavigate, useParams } from 'react-router-dom';

export function Negotiate() {
  const navigate = useNavigate();
  const { movieId } = useParams();
  const [oldFee, setOldFee] = useState(null);
  const [newFee, setNewFee] = useState(null);

  function submit() {
    fetch(`${hostname}/Actor/CreateNegotiation/${movieId}/${Number(newFee)}`, {
      method: "PUT", headers: {
        "Authorization": sessionStorage.getItem("token")
      }
    })
    .then(resp => {
      if (resp.ok) {
        navigate("/");
      }
    })
  }

  useEffect(() => {
    fetch(`${hostname}/Actor/GetStarring/${movieId}`, {
      headers: {
        "Authorization": sessionStorage.getItem("token")
      }
    })
    .then(resp => resp.json())
    .then(data => setOldFee(data.fee));

  }, [movieId]);

  return <div style={{ display: "flex", flexDirection: "column", maxWidth: "250px" }}>
    <label>Old fee</label>
    <input type="text" value={oldFee} onChange={(evt) => setOldFee(evt.target.value)} disabled/>
    <label>New fee</label>
    <input type="text" value={newFee} onChange={(evt) => setNewFee(evt.target.value)} />
    <button onClick={submit}>Submit offer</button>
  </div>
}