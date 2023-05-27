import { Text, View } from 'react-native';
import Button from '../../../components/Button';

// styles
import styles from "./_styles-CameraOptions.js"

export default function CodeInfo({code, scanned}){
    return (
        <View style={styles.container}>
            <Text style={styles.scannedCodeInfo}>Read Code: {code}</Text>
            <Button title="Skanuj ponownie" styles={styles.repeatScanButton}/>
        </View>
    );
}