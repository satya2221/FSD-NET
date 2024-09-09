import { useContext, useState } from "react"
import useBreedList from "./useBreeedList"
import Result from "./Results"
import { useQuery } from "@tanstack/react-query"
import fetchSearch from "./fetchSearch"
import AdoptedPetContext from "./PetContext"

const ANIMALS = ["bird", "cat", "dog", "rabbit", 'reptile']

const SearchParams = () => {
  const [adoptedPet] = useContext(AdoptedPetContext)
  // const [location, setLocation] = useState("Seattle, WA")
  const [animal, setAnimal] = useState("")
  //const [breed, setBreed] = useState("")
  const [reqParams, setReqParams] = useState({
    location:'',
    animal: '',
    breed: ''
  })
  const [breeds] = useBreedList(animal)
  //const [pets, setPets] = useState([])
  const result = useQuery(['search', reqParams], fetchSearch)
  const pets = result?.data?.pets ?? []

  // useEffect(() => {
  //   fetchPets()
  // }, [])

  // async function fetchPets() {
  //   const res = await fetch(
  //     `http://pets-v2.dev-apis.com/pets?animal=${animal}&location=${location}&breed=${breed}`
  //   )
  //   const jsonRes = await res.json()
  //   setPets(jsonRes.pets)
  // }
  return (
    <div className="search-params">
      <form
        //className="p-10 mb-10 rounded-lg bg-gray-300 shadow-lg flex flex-col justify-center items-center"
        onSubmit={(e) => {
          e.preventDefault() // Cegah halaman ke refresh
          // fetchPets()
          const formData = new FormData(e.target)
          const obj = {
            animal: formData.get('animal') ?? '',
            location: formData.get('location') ?? '',
            breed: formData.get('breed') ?? ''
          }
          setReqParams(obj)
        }}
      >
        {adoptedPet ? (
          <div className="pet image-container">
            <img src={adoptedPet.images[0]} alt={adoptedPet.name} />
          </div>
        ) : null}
        <label htmlFor="location">
          <input
            id="location"
            placeholder="Location"
            type="text"
            name = "location"
            className="search-input"
            // value={location}
            // onChange={(e) => setLocation(e.target.value)}
          />
        </label>
        <label htmlFor="animal">
          Animal
          <select
            id="animal"
            name="animal"
            //value={animal}
            onChange={(e) => {
              setAnimal(e.target.value)
            }}
            onBlur={(e) => {
              setAnimal(e.target.value)
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
            name = "breed"
            // onChange={(e) => setBreed(e.target.value)}
            // onBlur={(e) => setBreed(e.target.value)}
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
