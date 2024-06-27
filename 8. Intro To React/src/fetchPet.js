import React from 'react'

async function fetchPet({queryKey}) {
    const id = queryKey[1]
    const res = await fetch(`http://pets-v2.dev-apis.com/pets?id=${id}`)
    if(!res.ok){
        throw new Error(`Gagal fetching data dengan id ${id}`)
    }
    return res.json()
}

export default fetchPet