import { useEffect, useState } from "react"
import useBreedList from "./useBreeedList"
import Result from "./Results"

const ANIMALS = ["bird", "cat", "dog", "rabbit"]

const SearchParams = () => {
  const [location, setLocation] = useState("Seattle, WA")
  const [animal, setAnimal] = useState("")
  const [breed, setBreed] = useState("")
  const [breeds] = useBreedList(animal)
  const [pets, setPets] = useState([])

  useEffect(() => {
    fetchPets()
  }, [])

  async function fetchPets() {
    const res = await fetch(
      `http://pets-v2.dev-apis.com/pets?animal=${animal}&location=${location}&breed=${breed}`
    )
    const jsonRes = await res.json()
    setPets(jsonRes.pets)
  }
  console.log(breeds)
  return (
    <div className="search-params">
      <form
        onSubmit={(e) => {
          e.preventDefault() // Cegah halaman ke refresh
          fetchPets()
        }}
      >
        <label htmlFor="location">
          <input
            id="location"
            placeholder="Location"
            type="text"
            value={location}
            onChange={(e) => setLocation(e.target.value)}
          />
        </label>
        <label htmlFor="animal">
          Animal
          <select
            id="animal"
            value={animal}
            onChange={(e) => {
              setAnimal(e.target.value)
              setBreed("")
            }}
            onBlur={(e) => {
              setAnimal(e.target.value)
              setBreed("")
            }}
          >
            <option />
            {ANIMALS.map((animal) => (
              <option key={animal} value={animal}>
                {animal}
              </option>
            ))}
          </select>
        </label>
        <label htmlFor="breed">
          Breed
          <select
            disabled={!breeds.length}
            id="breed"
            value={breed}
            onChange={(e) => setBreed(e.target.value)}
            onBlur={(e) => setBreed(e.target.value)}
          >
            <option />
            {breeds.map((breed) => (
              <option key={breed} value={breed}>
                {breed}
              </option>
            ))}
          </select>
        </label>
        <button>Submit</button>
      </form>
      {/* {pets.map((pet) => (
        <Pet
          name={pet.name}
          animal={pet.animal}
          breed={pet.breed}
          key={pet.id}
        />
      ))} */}
      <Result pets={pets} />
    </div>
  )
}

export default SearchParams
