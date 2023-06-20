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

    
    const [shouldInventory, setShouldInventory] = useState(false)
    const [inventoryPayload, setInventoryPayload] = useState({
        code : manualCodeInput,
        isArchive : false,
        locationId : utils["ItemsScreen"].locationId
    })

    useEffect(()=>{
        setManualCodeInput(cameraObject.readCode)
        setInventoryPayload((prevPayload) => ({
            ...prevPayload,
            code : cameraObject.readCode
        }))
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
                onPress : ()=> {
                    setInventoryPayload(prevPayload => ({
                        ...prevPayload,
                        isArchive : true
                    }))
                    inventoryProduct()
                },
                style : "default"
            },
            {
                text : "Anuluj",
                style : "cancel"
            }
        ]
        )
    }

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
            {shouldShowSuccess && <MemoizedSuccessPopup setVisibility={setShouldShowSucccess} style={{container : {
                 flex : 1,
                 top : hasInputFocus ? 0 : -100
            }}}/>}
            <TextInput
                        label="manualCode"
                        placeholder='Code...'
                        placeholderTextColor="gray"
                        style={styles.codeInput}
                        value={manualCodeInput}
                        onChangeText={newCode => {
                            setManualCodeInput(newCode)   
                            setInventoryPayload((prevPayload) => ({
                            ...prevPayload,
                            code : newCode
                        }))}}
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