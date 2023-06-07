import ComponentsResetersProvider from "./ComponentsUtilsProvider";
import UserProvider from "./UserProvider";
export default function ApplicationProviders({children}){
    return(
    <UserProvider>
        <ComponentsResetersProvider>
            {children}
        </ComponentsResetersProvider>
    </UserProvider>)
}