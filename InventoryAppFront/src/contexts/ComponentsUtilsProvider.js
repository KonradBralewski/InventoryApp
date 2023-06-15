import { createContext, useContext, useState } from "react"


const ComponentsUtilsContext = createContext()

export default function ComponentsUtilsProvider({children}){

    const [utils, setUtils] = useState(defaultComponentsUtilsObject)

    return (
        <ComponentsUtilsContext.Provider value={[utils, setUtils]}>
            {children}
        </ComponentsUtilsContext.Provider>)
}

export const useComponentsUtils = () => useContext(ComponentsUtilsContext)
export const defaultComponentsUtilsObject = {
    ItemsScreen : {
        locationId : undefined,
        reseter : undefined
    },
    ActiveInventoryScreen : {
        hasAnyActiveInventory : undefined
    }
}