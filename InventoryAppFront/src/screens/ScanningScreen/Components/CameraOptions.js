import { useEffect, useState } from 'react';
import { View, TextInput, Keyboard, Alert} from 'react-native';
import Button from '../../../components/Button';
import { MemoizedSuccessPopup } from '../../../components/SuccessPopup';
import { useComponentsUtils } from '../../../contexts/ComponentsUtilsProvider';
import { useAxiosRequest } from '../../../hooks/UseAxiosRequest';
import { getErrorMessage } from '../../../utils/errorUtils';

// styles
import styles from "./_styles-CameraOptions.js"

export default function CameraOptions({cameraObject, reseter, setHasInputBoxFocus}){

    const [shouldShowSuccess, setShouldShowSucccess] = useState(false)
    const [manualCodeInput, setManualCodeInput] = useState(cameraObject.readCode)
    const [hasInputFocus, setHasInputFocus] = useState(false)
    const [utils, setUtils] = useComponentsUtils()

    useEffect(()=>{
        setManualCodeInput(cameraObject.readCode)
    }, [cameraObject.readCode])
    
    useEffect(()=>{
        const keyboardHideListener = Keyboard.addListener('keyboardDidHide', ()=>{
            Keyboard.dismiss()
        })

        return () => Keyboard.removeAllListeners('keyboardDidHide')
    }, [])

    useEffect(()=>{
        setHasInputBoxFocus(hasInputFocus)
    }, [hasInputFocus])

    const [shouldInventory, setShouldInventory] = useState(false)

    const inventoryPayload = {
        code : manualCodeInput,
        isArchive : false,
        locationId : utils["ItemsScreen"].locationId
    }

    const[response, error, isLoading, resetHook] = useAxiosRequest("api/Inventories/scan", "post", {
        state : shouldInventory,
        modifierFunc : setShouldInventory
    }, inventoryPayload)

    const inventoryProduct = () => {
        if(!inventoryPayload.code){
            return
        }
        setShouldInventory(true)
        reseter()
    }

    console.log(response, error)
    if(!response && !isLoading && error){
        Alert.alert("Błąd", getErrorMessage(error))
    }    

    return (
        <View style={styles.container}>
            {shouldShowSuccess && <MemoizedSuccessPopup setVisibility={setShouldShowSucccess} style={styles.successPopup}/>}
            <TextInput
                        label="manualCode"
                        placeholder='Code...'
                        placeholderTextColor="gray"
                        style={styles.codeInput}
                        value={manualCodeInput}
                        onChangeText={newCode => setManualCodeInput(newCode)}
                        onBlur={()=>setHasInputFocus(false)}
                        onFocus={()=>setHasInputFocus(true)}>
            </TextInput>
            <Button title="Inwentaryzuj" styles={styles.inventoryButton} disabled={!manualCodeInput} onPress={inventoryProduct}/>
            <Button title="Skanuj ponownie" styles={styles.repeatScanButton} onPress={()=>setShouldShowSucccess(true)}/>
        </View>
    );
}