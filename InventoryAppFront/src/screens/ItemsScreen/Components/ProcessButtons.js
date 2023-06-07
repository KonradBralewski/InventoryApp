import { View, TouchableOpacity, Text, Pressable} from 'react-native';
import Button from '../../../components/Button';
import { useNavigation } from '@react-navigation/native';
import screens from '../../../constants/screens';
import { LinearGradient } from 'expo-linear-gradient';

//styles
import styles from './_styles-ProcessButtons';

export default function ProcessButtons({processStarted, processEnded}){

    const navigation = useNavigation();
    const inventoryTabConstants = screens.InventoryTab;

    const handleStartProcess = () => {
        if (!processStarted && !processEnded) {
          return (
            <TouchableOpacity
              style={styles.startButton.pressableContainer}
              onPress={() => console.log('Start process')}
            >
              <LinearGradient
                colors={['#005e00', '#009200']}
                style={styles.startButton.gradientContainer}
                start={[0, 0.5]}
                end={[1, 0.5]}>
                <Text style={styles.startButton.insideButtonText}>Rozpocznij Inwentaryzację</Text>
              </LinearGradient>
            </TouchableOpacity>
          );
        }
    
        return null;
    }

    const handleEndProcess = () => {
        var disabled = processStarted ?  false : true

        if(!processEnded){
        
            return (
                <TouchableOpacity
                  style={styles.endButton.pressableContainer}
                  onPress={() => console.log('End process')}
                  disabled={disabled}
                >
                  <LinearGradient
                    colors={['#800000', '#c41414']}
                    style={styles.endButton.gradientContainer}
                    start={[0, 0.5]}
                    end={[1, 0.5]}
                  >
                    <Text style={styles.endButton.insideButtonText}>Zakończ Inwetaryzację</Text>
                  </LinearGradient>
                </TouchableOpacity>
              );


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
            onPress={()=>navigation.navigate(inventoryTabConstants.ScanningScreen.screenName)}/>
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