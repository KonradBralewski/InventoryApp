import { View } from 'react-native';
import ProcessButtons from './ProcessButtons';

//styles
import styles from './_styles-InventoryProcess';

export default function InventoryProcess(){
    return (
        <View style={styles.buttonsContainer}>
            <ProcessButtons processStarted={true} processEnded={false}/>
        </View>
    )
}