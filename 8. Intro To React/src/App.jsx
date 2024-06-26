import { createRoot } from "react-dom/client"
import { BrowserRouter, Routes, Route, Link } from "react-router-dom"
import { QueryClient, QueryClientProvider } from "@tanstack/react-query"
import { useState } from "react"

import SearchParams from "./SearchParams"
import Details from "./Details"
import AdoptedPetContext from "./PetContext"


const queryClient = new QueryClient({
  defaultOptions: {
    queries: {
      staleTime: Infinity,
      cacheTime: Infinity,
    },
  },
})

const App = () => {
  const adoptedPet = useState(null)
  // return React.createElement("div", {}, [
  //   React.createElement("h1", {}, "Adopsi saya"),
  //   React.createElement(Pet, {
  //     name: "Luna",
  //     animal: "Dog",
  //     breed: "Havanese",
  //   }),
  //   React.createElement(Pet, { name: "Benu", animal: "Cat", breed: "Siamese" }),
  // ])

  return (
    <BrowserRouter>
      <AdoptedPetContext.Provider value={adoptedPet}>
        <QueryClientProvider client={queryClient}>
          <header>
            <Link to="/">Adopsi Saya!!</Link>
          </header>
          <Routes>
            <Route path="/" element={<SearchParams />} />
            <Route path="/details/:id" element={<Details />} />
          </Routes>
          {/* <Pet name="Luna" animal="Dog" breed="Havanese" />
        <Pet name="Benu" animal="Cat" breed="Siamese" /> */}
        </QueryClientProvider>
      </AdoptedPetContext.Provider>
    </BrowserRouter>
  )
}

const container = document.getElementById("root")
const root = createRoot(container)
//root.render(createElement(App))
root.render(<App />)
