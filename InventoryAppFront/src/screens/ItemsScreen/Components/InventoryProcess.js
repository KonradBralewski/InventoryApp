import { View } from 'react-native';
import ProcessButtons from './ProcessButtons';

//styles
import styles from './_styles-InventoryProcess';

export default function InventoryProcess({processStarted, processEnded}){
    return (
        <View style={styles.buttonsContainer}>
            <ProcessButtons processStarted={processStarted} processEnded={processEnded}/>
        </View>
    )
}