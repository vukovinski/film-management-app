import Select from 'react-select';
import { hostname } from '../server';
import { useParams, useNavigate } from 'react-router-dom';
import React, { useEffect, useState } from 'react';

export function MovieDetails() {
  const { id } = useParams();
  const navigate = useNavigate();
  const [genres, setGenres] = useState([]);
  const [actors, setActors] = useState([]);
  const [details, setDetails] = useState(null);

  const [title, setTitle] = useState("");
  const [tagLine, setTagLine] = useState("");
  const [budget, setBudget] = useState("");
  const [mGenres, setMGenres] = useState([]);
  const [plannedShootStart, setPlannedShootStart] = useState("");
  const [plannedShootEnd, setPlannedShootEnd] = useState("");
  const [mActors, setMActors] = useState([]);
  const [negotiations, setNegotiations] = useState([]);
  const [selectedActors, setSelectedActors] = useState([]);
  const [selectedGenres, setSelectedGenres] = useState([]);

  function submit() {
    fetch(`${hostname}/Director/EditMovie`, {
      method: "POST", headers: {
        "Content-Type": "application/json",
        "Authorization": sessionStorage.getItem("token")
      },
      body: JSON.stringify({
        Id: Number(id),
        Title: title,
        TagLine: tagLine,
        Budget: Number(budget),
        PlannedShootingStartDate: plannedShootStart,
        PlannedShootingEndDate: plannedShootEnd,
        Genres: mGenres.filter(mG => new Set(selectedGenres).has(mG.id)),
        Actors: mActors.filter(mA => new Set(selectedActors).has(mA.actorId))
      })
    })
    .then(resp => {
      if (resp.ok) {
        navigate(`/`)
      }
    })
  }

  function accept(actorId) {
    fetch(`${hostname}/Director/AcceptFee/${id}/${actorId}`, {
      method: "PUT", headers: {
        "Authorization": sessionStorage.getItem("token")
      }
    })
    .then(resp => {
      if (resp.ok) {
        navigate(`/`)
      }
    })
  }

  function decline(actorId) {
    fetch(`${hostname}/Director/DeclineFee/${id}/${actorId}`, {
      method: "PUT", headers: {
        "Authorization": sessionStorage.getItem("token")
      }
    })
    .then(resp => {
      if (resp.ok) {
        navigate(`/`)
      }
    })
  }

  useEffect(() => {
    fetch(`${hostname}/Director/MovieDetails/${id}`, {
      headers: {
        "Authorization": sessionStorage.getItem("token")
      }
    })
    .then(resp => resp.json())
    .then(data => {
      setTitle(data.title);
      setTagLine(data.tagLine);
      setBudget(data.budget);
      setMGenres(data.genres);
      setSelectedGenres(data.genres.map(g => g.id));
      setPlannedShootStart(data.plannedShootingStartDate);
      setPlannedShootEnd(data.plannedShootingEndDate);
      setMActors(data.actors);
      setSelectedActors(data.actors.map(a => a.actorId));
      setNegotiations(data.negotiations);
      setDetails(data);
    });

    fetch(`${hostname}/SuperUser/GetGenres`, {
      headers: {
        "Authorization": sessionStorage.getItem("token")
      }
    })
    .then(resp => resp.json())
    .then(data => setGenres(data))

    fetch(`${hostname}/Director/AllActors`, {
      headers: {
        "Authorization": sessionStorage.getItem("token")
      }
    })
    .then(resp => resp.json())
    .then(data => setActors(data));

  }, [id])


  return details && <div style={{ display: "flex", flexDirection: "column", maxWidth: "550px" }}>
    <h2>Edit movie</h2>
    <label>Title</label>
    <input type="text" value={title} onChange={(evt) => setTitle(evt.target.value)} />
    <label>Tag line</label>
    <input type="text" value={tagLine} onChange={(evt) => setTagLine(evt.target.value)} />
    <label>Budget</label>
    <input type="text" value={budget} onChange={(evt) => setBudget(evt.target.value)} />
    <label>Genres</label>
    <Select
      isMulti
      onChange={(items) => setSelectedGenres(items.map(i => i.value))}     
      defaultValue={mGenres.map(mg => { return { value: mg.id, label: mg.name }})}
      options={genres.map(g => { return { value: g.id, label: g.name } })}
    />
    <label>Director</label>
    <input type="text" value={details.director.fullName} disabled />
    <label>Planned shooting start</label>
    <input type="date" value={plannedShootStart} onChange={(evt) => setPlannedShootStart(evt.target.value)} />
    <label>Planned shooting end</label>
    <input type="date" value={plannedShootEnd} onChange={(evt) => setPlannedShootEnd(evt.target.value)} />
    <label>Actors</label>
    <Select
      isMulti
      isSearchable
      onChange={(items) => setSelectedActors(items.map(i => i.value))}
      defaultValue={mActors.map(ac => { return { value: ac.actorId, label: `${ac.fullName} - ${ac.fee}` }})}
      options={actors.map(a => { return { value: a.actorId, label: `${a.fullName} - ${a.fee}` } })}
    />
    <label>Fee negotiations</label>
    {negotiations.map(fn => {
      const actor = actors.find(a => fn.actorId === a.id);
      return actor && <div key={fn.id} style={{ display: "flex", flexDirection: "row"}}>
        <p>{actor.fullName} - {fn.oldFee} - {fn.newFee}</p>
        <button onClick={() => accept(fn.actorId)}>✅</button>
        <button onClick={() => decline(fn.actorId)}>❌</button>
      </div>
    })}
    {negotiations.length > 0
      ? <button disabled>Shoot</button>
      : <button>Shoot</button>
    }
    <button onClick={submit}>Save changes</button>
  </div>
}