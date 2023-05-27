import { View, TouchableOpacity, Text, Pressable} from 'react-native';
import Button from '../../../components/Button';
import { useNavigation } from '@react-navigation/native';
import screens from '../../../constants/screens';

//styles
import styles from './_styles-ProcessButtons';

export default function ProcessButtons({processStarted, processEnded}){

    const navigation = useNavigation();
    const homeTabConstants = screens.HomeTab;

    const handleStartProcess = () => {
        if(!processStarted && !processEnded){
            return <Button title="Rozpocznij Inwentaryzację" styles={styles.startButton}/>
        }

        return null
    }

    const handleEndProcess = () => {
        var disabled = processStarted ?  false : true

        if(!processEnded){
            return <Button title="Zakończ Inwetaryzację" styles={styles.endButton} disabled={disabled}/>
        }

       return null
    }

    const handleGenerate = () => {
        if(!processStarted && processEnded){
            return <Button title="Generuj" styles={styles.generateButton} iconName="document-sharp"/>
        }

        return null
    }

    const handleScan = () => {
        if(processStarted && !processEnded){
            return <Button title="Skanuj" styles={styles.scanButton} iconName="scan-circle-sharp"
            onPress={()=>navigation.navigate(homeTabConstants.ScanningScreen.screenName)}/>
        }

        return null
    }

    return (
        <View style={styles.inventoryProcessButtonsContainer}>
            {handleScan()}
            {handleStartProcess()}
            {handleEndProcess()}
            {handleGenerate()}
        </View>
    )
}

ProcessButtons.defaultProps = {
    processStarted : false,
    processEnded : false
}