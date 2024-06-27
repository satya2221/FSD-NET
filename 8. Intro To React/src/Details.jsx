import { useQuery } from "@tanstack/react-query"
import React from "react"
import { useParams } from "react-router-dom"
import fetchPet from "./fetchPet"
import { CircleLoader } from "react-spinners"

function Details() {
  const params = useParams()
  const result = useQuery(["details", params.id], fetchPet)

  if (result.isLoading) {
    return (
      <div className="loading-pane">
        <h2 className="loader">
          <CircleLoader size={40} color="#0000ff" />
        </h2>
      </div>
    )
  }
  const pet = result.data.pets[0]
  return (
    <div className="details">
      <div>
        <h1>{pet.name}</h1>
        <h2>{`${pet.animal} ${pet.breed} ${pet.city} ${pet.state}`}</h2>
        <button>Apakah mau mengadopsi {pet.name}</button>
        <p>{pet.description}</p>
      </div>
    </div>
  )
}

export default Details
