import cfg from "../configuration.js"
import axios from "axios"
import { useState, useEffect} from "react"
import { defaultUserObject, useUserContext } from '../contexts/UserProvider';
import { getErrorCode } from "../utils/errorUtils.js";
import errors from "../constants/errors.js";

export const useAxiosRequest = (noun, method, statefulRun = null, reqBody = null) => {

    const [data, setData] = useState(undefined)
    const [error, setError] = useState(undefined)
    const [isLoading, setIsLoading] = useState(false)
    const [wasReseted, setWasReseted] = useState(false)

    const [user, setUser] = useUserContext()

    const requestHeaders ={
        'Content-Type': 'application/json'
    }

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
        
        if(user.isSigned){
            requestHeaders["Authorization"] = user.token
        }

        axios(cfg.API_SERVER + noun, {
            method: method, 
            data: reqBody,
            withCredentials : true,
            headers : requestHeaders
        })
        .then(result => {
            setData(result.data)
            setError(undefined)
        })
        .catch((error)=> {
            setError(error)
            if(getErrorCode(error) == errors.NOT_AUTHORIZED){
                setUser(defaultUserObject)
            }
            setIsLoading(false)
        })
        .finally(()=>{
            setIsLoading(false)
            setWasReseted(false)
        })
    }, [noun, method, statefulRun, reqBody, wasReseted])

    return [data, error, isLoading, resetHook]
}