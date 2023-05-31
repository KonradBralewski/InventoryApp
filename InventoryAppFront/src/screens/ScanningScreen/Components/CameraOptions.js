import { Text, View } from 'react-native';
import Button from '../../../components/Button';

// styles
import styles from "./_styles-CameraOptions.js"

export default function CameraOptions({cameraObject, reseter}){
    return (
        <View style={styles.container}>
            {cameraObject.readCode && <Text style={styles.scannedCodeInfo}>{cameraObject.readCode}</Text>}
            <Button title="Inwentaryzuj" styles={styles.inventoryButton}/>
            <Button title="Skanuj ponownie" styles={styles.repeatScanButton} onPress={reseter}/>
        </View>
    );
}