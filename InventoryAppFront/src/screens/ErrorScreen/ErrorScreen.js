import {View, Text} from 'react-native';
import Button from '../../components/Button';
import styles from './_styles-ErrorScreen';
import { useNavigation } from '@react-navigation/native';

export default function ErrorScreen({errorTitle, errorDescription, reseter, reseterMessage}){

    const nav = useNavigation()

    return(
        <View style={styles.container}>
            <Text style={styles.errorTitle}>{errorTitle}</Text>
            <Text style={styles.errorDescription}>{errorDescription}</Text>
            <Button title={reseterMessage} onPress={reseter} styles={styles.resetButton}/>
        </View>
    )
}

ErrorScreen.defaultProps = {
    reseterMessage : "Resetuj"
}