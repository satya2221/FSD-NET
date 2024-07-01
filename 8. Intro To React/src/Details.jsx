import { useQuery } from "@tanstack/react-query"
import { useParams } from "react-router-dom"
import { CircleLoader } from "react-spinners"
import React from "react"

import fetchPet from "./fetchPet"
import Carousel from "./Carousel"
import ErrorBoundary from "./ErrorBoundary"

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
      <Carousel images = {pet.images} />
      <div>
        <h1>{pet.name}</h1>
        <h2>{`${pet.animal} ${pet.breed} ${pet.city} ${pet.state}`}</h2>
        <button>Apakah mau mengadopsi {pet.name}</button>
        <p>{pet.description}</p>
      </div>
    </div>
  )
}

//export default Details
export default function DetailsErrorBoundary(props){
  return(
    <ErrorBoundary>
      <Details {...props} />
    </ErrorBoundary>
  )
}
