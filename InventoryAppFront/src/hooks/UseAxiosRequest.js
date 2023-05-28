import cfg from "../configuration.js"
import axios from "axios"
import { useState, useEffect} from "react"

export const useAxiosRequest = (noun, method, statefulRun = null, reqBody = null) => {

    const [data, setData] = useState(undefined)
    const [error, setError] = useState(undefined)
    const [isLoading, setIsLoading] = useState(false)
    const [wasReseted, setWasReseted] = useState(false)
    console.log(error)
    const resetHook = () => {
        setData(undefined)
        setError(undefined)
        setIsLoading(false)
        setWasReseted(true)
    }

    useEffect(()=>{
        if(statefulRun != null){
            if(statefulRun.state === true){
                statefulRun.modifierFunc(false)
            }
            else{
                return
            }
        }

        if(noun === null || noun === undefined) return

        setIsLoading(true)

        axios(cfg.API_SERVER + noun, {
            method: method, 
            data: reqBody,
            withCredentials : true,
            headers: {'Content-Type': 'application/json'}
        })
        .then(result => {
            setData(result.data) 
        })
        .catch((error)=> {
            setError(error)
            setIsLoading(false)
        })
        .finally(()=>{
            setIsLoading(false)
            setWasReseted(false)
        })
    }, [noun, method, statefulRun, reqBody, wasReseted])

    return [data, error, isLoading, resetHook]
}