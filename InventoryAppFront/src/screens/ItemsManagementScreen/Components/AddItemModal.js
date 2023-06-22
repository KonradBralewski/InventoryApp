import { Modal, Text, View} from "react-native"
import { useCallback, useEffect, useState } from "react"
import List from "../../../components/List"
import { TextInput } from "react-native"
import ErrorScreen from "../../ErrorScreen/ErrorScreen"
import styles from "./_styles-AddItemModal"
import Button from "../../../components/Button"
import { useFocusEffect, useNavigation } from "@react-navigation/native"
import { useAxiosRequest } from "../../../hooks/UseAxiosRequest"
import { MemoizedLoadingScreen } from "../../LoadingScreen/LoadingScreen"
import ErrorMessage from "../../LoginScreen/Components/ErrorMessage"
import screens from "../../../constants/screens"

export default function AddItemModal({locationId, hideModal}) {
    const navigation = useNavigation()
    const [productType, setProductType] = useState(undefined)

    const [newItemCode, setNewItemCode] = useState('')
    const [hasTriedToAdd, setHasTriedToAdd] = useState(false)
    
    // add functionality
    const [shouldAdd, setShouldAdd] = useState(false)

    const [addPayload, setAddPayload] = useState({
      code: undefined,
      productId : undefined,
      locationId : locationId,
      isArchive: false
    })

    const[addResponse, addError, isLoadingAdd, addResetHook] = useAxiosRequest("api/StockItems", "post", {
      state : shouldAdd,
      modifierFunc : setShouldAdd
    }, addPayload)

    const handleAdd = () => {
      if(newItemCode === undefined){
        setHasTriedToAdd(true)
        return
      }
      setAddPayload({
        code : newItemCode,
        productId : productType,
        locationId : locationId,
        isArchive : false
      })
      setShouldAdd(true)
    }

    // add functionality

    useFocusEffect(useCallback(()=>{
      return () => {
        setHasTriedToAdd(false)
        setProductType(undefined)
        setNewItemCode(undefined)
        hideModal()
      }
    }, []))
    // setup lifecycle listener

    useEffect(()=>{
      if(!isLoadingAdd && addResponse && !addError){
        hideModal()
        navigation.navigate(screens.InventoryTab.BuildingsScreen.screenName)
      }
    }, [addResponse, addError, isLoadingAdd, addResetHook]) // handle OK response and navigate after adding
    console.log(addResponse, addError, isLoadingAdd, addResetHook)
    const types = [{
        id : 1,
        name : 'Biurko'
      },
      {
          id : 2,
          name : 'Klawiatura'
      },
      {
        id : 3,
        name : 'Myszka'
      },
      {
        id : 4,
        name : 'Monitor'
      },
      {
        id : 5,
        name : 'Projektor'
      },
  ]

  const mappedTypes = types.map(type => ({...type, onItemPress : () => setProductType(type.id)}))
  
  const renderStepByStepAdding = () => {
    if(productType === undefined){
      return <List data={mappedTypes} headerTitle = "Wybierz typ produktu"
       emptyListMessage="Brak budynków" iconName={"chevron-forward-outline"}/>
    }

    if(isLoadingAdd){
      return <MemoizedLoadingScreen/>
    }

    if(!isLoadingAdd && !addResponse && addError){
        
        return <ErrorScreen errorTitle ="Błąd Aplikacji"
        errorDescription="InventoryApp nie był w stanie dodać nowego przedmiotu przez błąd serwera."
        reseterMessage="Wróć"
        reseter={()=>{
          hideModal()
          navigation.navigate(screens.InventoryTab.displayedText)
        }}/>
    }

    return (
      <View style={styles.inputAndButtonContainer}>
        {hasTriedToAdd && <ErrorMessage customMessage={"Pola nie mogą być puste!"} styles={styles}/>}
        <TextInput
            label="ProductCode"
            value={newItemCode}
            onChangeText={newText => setNewItemCode(newText)}
            style={styles.itemNameInput}
            placeholder='Kod przedmiotu'
            placeholderTextColor="gray">
        </TextInput>
        <Button title="Dodaj przedmiot" onPress={handleAdd} styles={styles.addButton}/>
      </View>
    )
  }

  return (
  <Modal>
    {renderStepByStepAdding()}
  </Modal>)
}
