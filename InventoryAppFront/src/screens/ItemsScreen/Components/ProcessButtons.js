import { View, TouchableOpacity, Text, Pressable} from 'react-native';
import Button from '../../../components/Button';
import { useNavigation } from '@react-navigation/native';
import screens from '../../../constants/screens';
import { LinearGradient } from 'expo-linear-gradient';
import { useEffect, useState } from 'react';
import { useAxiosRequest } from '../../../hooks/UseAxiosRequest';
import { useComponentsUtils } from '../../../contexts/ComponentsUtilsProvider';

//styles
import styles from './_styles-ProcessButtons';
import { MemoizedLoadingScreen } from '../../LoadingScreen/LoadingScreen';

export default function ProcessButtons({processStarted, processEnded}){

    const navigation = useNavigation();
    const inventoryTabConstants = screens.InventoryTab;

    const [utils, setUtils] = useComponentsUtils()

    const [shouldStart, setShouldStart] = useState(false)
    const [shouldEnd, setShouldEnd] = useState(false)

    const startInventoryPayload = {
      locationId: utils["ItemsScreen"].locationId,
      description : undefined
    }

    const endInventoryPayload = {
      locationId: utils["ItemsScreen"].locationId
    }

    const[startResponse, startError] = useAxiosRequest("api/Inventories/start", "post", {
        state : shouldStart,
        modifierFunc : setShouldStart
    }, startInventoryPayload)

    const[endResponse, endError, isLoading, resetHook] = useAxiosRequest("api/Inventories/end", "post", {
      state : shouldEnd,
      modifierFunc : setShouldEnd
  }, endInventoryPayload)

  useEffect(()=>{
    if(startResponse && !isLoading && !startError){
      setUtils((prevComponentUtils) => ({
        ...prevComponentUtils,
        "ActiveInventoryScreen" : {
            hasAnyActiveInventory : true
        }
    }))
      utils["ItemsScreen"].reseter()
    }

    if(endResponse && !isLoading && !endError){
      setUtils((prevComponentUtils) => ({
        ...prevComponentUtils,
        "ActiveInventoryScreen" : {
            hasAnyActiveInventory : false
        }
    }))
      navigation.popToTop()
      navigation.navigate(screens.RaportsTab.displayedText, {shouldDisplayLatest : true})
    }
  },[startResponse, endResponse, startError, endError, isLoading])

    const handleStartProcess = () => {
        if (!processStarted && !processEnded) {
          return (
            <TouchableOpacity
              style={styles.startButton.pressableContainer}
              onPress={() => {
                setShouldStart(true)              
              }}
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
        if(isLoading){
          return <MemoizedLoadingScreen/>
        }
        if(!processEnded){
        
            return (
                <TouchableOpacity
                  style={styles.endButton.pressableContainer}
                  onPress={() => {
                    setShouldEnd(true)
                  }}
                  disabled={disabled}
                >
                  <LinearGradient
                    colors={disabled ? ['#808080', '#808080'] : ['#800000', '#c41414']}
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

    const handleScan = () => {
        if(processStarted && !processEnded && !isLoading){
            return <Button title="Skanuj" styles={styles.scanButton} iconName="scan-circle-sharp"
            onPress={()=>navigation.navigate(inventoryTabConstants.ScanningScreen.screenName)}/>
        }

        return null
    }

    return (
        <View style={{...styles.inventoryProcessButtonsContainer, flexDirection : isLoading ? "row" : "column"}}>
            {handleScan()}
            {handleStartProcess()}
            {handleEndProcess()}
        </View>
    )
}

ProcessButtons.defaultProps = {
    processStarted : false,
    processEnded : false
}