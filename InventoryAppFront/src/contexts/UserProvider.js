import { createContext, useContext, useEffect, useState } from "react"
import jwt from 'jwt-decode'
import jwtDecode from "jwt-decode"

const UserContext = createContext()

export default function UserProvider({children}){

    const [user, setUser] = useState(defaultUserObject)

    useEffect(()=>{
        if(user.token){
            const decodedUser = jwtDecode(user.token)
            if(decodedUser){
                if(decodedUser["http://schemas.microsoft.com/ws/2008/06/identity/claims/role"] === "Admin"){
                    setUser(prevUserObject => ({...prevUserObject, isAdmin : true}))
                }
            }
        }

    }, [user.token])

    return (
        <UserContext.Provider value={[user, setUser]}>
            {children}
        </UserContext.Provider>)
}

export const useUserContext = () => useContext(UserContext)
export const defaultUserObject = {
    isSigned : false,
    email : undefined,
    token : undefined,
    isAdmin : false
}