import { useNavigation } from "@react-navigation/native"
import { Text } from "react-native"
import { getErrorCode } from "../../../utils/errorUtils"

export default function ErrorMessage({error, styles}){

    const navigation = useNavigation()

    var errorMessage = ''

    switch(getErrorCode(error)){
        case 422:
            errorMessage = "Email lub hasło jest nieprawidłowe."
            break;
        default:
            break;
    }

    return (
    <Text style={styles.errorMessage}>
        {errorMessage}
    </Text>)
}