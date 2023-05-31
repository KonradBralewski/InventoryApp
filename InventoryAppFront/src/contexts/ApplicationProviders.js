import UserProvider from "./UserProvider";
export default function ApplicationProviders({children}){
    return(
    <UserProvider>
        {children}
    </UserProvider>)
}