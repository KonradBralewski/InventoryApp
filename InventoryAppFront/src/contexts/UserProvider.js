import { createContext, useContext, useState } from "react"


const UserContext = createContext()

export default function UserProvider({children}){

    const [user, setUser] = useState(defaultUserObject)

    return (
        <UserContext.Provider value={[user, setUser]}>
            {children}
        </UserContext.Provider>)
}

export const useUserContext = () => useContext(UserContext)
export const defaultUserObject = {
    isSigned : false,
    email : undefined,
    token : undefined
}