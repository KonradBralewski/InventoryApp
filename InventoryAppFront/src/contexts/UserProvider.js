import { createContext, useContext, useState } from "react"


const UserContext = createContext()

export default function UserProvider({children}){

    const [user, setUser] = useState({
        isSigned : false,
        email : undefined,
        token : undefined
    })

    const setToken = (tokenValue) => setUser(prev => ({...prev, token : tokenValue}))

    return (
        <UserContext.Provider value={[user, setToken]}>
            {children}
        </UserContext.Provider>)
}

export const useUserContext = () => useContext(UserContext)