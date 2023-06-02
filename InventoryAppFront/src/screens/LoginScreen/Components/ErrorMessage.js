import { Text } from "react-native"

export default function ErrorMessage({error, styles}){

    const getErrorCode = ()=> {
        if(!error) return 500
        if(!error.response) return 500

        return error.response.status
    }


    var errorMessage = ''

    switch(getErrorCode()){
        case 500:
            break;
        case 422:
            errorMessage = "Email lub hasło jest nieprawidłowe."
            break;
        default:
            break;
    }

    return (<Text styles={styles}>
        {errorMessage}
           </Text>)
}