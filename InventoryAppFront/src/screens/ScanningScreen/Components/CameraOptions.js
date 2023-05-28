import { Text, View } from 'react-native';
import Button from '../../../components/Button';

// styles
import styles from "./_styles-CameraOptions.js"

export default function CodeInfo({cameraObject}){
    return (
        <View style={styles.container}>
            <Text style={styles.scannedCodeInfo}>Read Code: {cameraObject.readCode}</Text>
            <Button title="Skanuj ponownie" styles={styles.repeatScanButton}/>
        </View>
    );
}