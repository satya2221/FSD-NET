async function fetchBreedList({queryKey}){
    const animal = queryKey[1]
    if(!animal)
        return []
    const res = await fetch(
        `http://pets-v2.dev-apis.com/breeds?animal=${animal}`
    )
    if (!res.ok){
        throw new Error(`gagal fetching data breedd hewan ${animal}`)
    }
    return res.json()
}
export default fetchBreedList