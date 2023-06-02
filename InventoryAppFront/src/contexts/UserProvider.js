import { createContext, useContext, useState } from "react"


const UserContext = createContext()

export default function UserProvider({children}){

    const [user, setUser] = useState({
        isSigned : false,
        email : undefined,
        token : undefined
    })

    const updateUser = (response) => setUser({
        email : response.email,
        isSigned : true,
        token : "Bearer " + response.token
    })

    return (
        <UserContext.Provider value={[user, updateUser]}>
            {children}
        </UserContext.Provider>)
}

export const useUserContext = () => useContext(UserContext)