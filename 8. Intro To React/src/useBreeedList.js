// import { useEffect, useState } from "react";

import { useQuery } from "@tanstack/react-query";
import fetchBreedList from "./fetchBreedList";

// const localCache = {}

export default function useBreedList(animal){
    // const [breedList, setBreedList] = useState([])
    // const [status, setStaus] = useState('unloaded')
    // useEffect(() => {
    //     if(!animal){
    //         setBreedList([]);
    //     }
    //     else if(localCache[animal]){
    //         setBreedList(localCache[animal])
    //     }
    //     else {
    //         requestBreedList()
    //     }
    //     async function requestBreedList(){
    //         const res = await fetch(`http://pets-v2.dev-apis.com/breeds?animal=${animal}`)
    //         const jsonRes = await res.json()
    //         localCache[animal] = jsonRes.breeds || []
    //         setBreedList(localCache[animal])
    //         setStaus('loaded')
    //     }
    // }, [animal])

    // return [breedList, status]
    const result = useQuery(['breeds', animal], fetchBreedList)
    return [result?.data?.breeds ?? [], result.status]
}

//export default useBreedList