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

    var inventoryPayload = {
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

    const utilizeProduct = () => {
        Alert.alert(
            "Utylizacja przedmiotu",
            "           Zatwierdź utylizację.",
            [{
                text : "Zatwierdź",
                style : "default"
            },
            {
                text : "Anuluj",
                style : "cancel"
            }
        ]
        )
    }

    console.log(response, getErrorMessage(error), inventoryPayload)
    if(!response && !isLoading && error){
        Alert.alert("Błąd", getErrorMessage(error))
        resetHook()
    }

    if(response && !isLoading && !error){
        resetHook() 
        setShouldShowSucccess(true)
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
            <View style={styles.InventoryUtilizeContainer}>
                <Button title="Inwentaryzuj" styles={styles.inventoryButton} disabled={!manualCodeInput} onPress={inventoryProduct}/>
                <Button title="Utylizuj" styles={styles.utilizeButton} disabled={!manualCodeInput}  onPress={utilizeProduct}/>
            </View>
            <Button title="Skanuj ponownie" styles={styles.repeatScanButton} onPress={reseter}/>
        </View>
    );
}