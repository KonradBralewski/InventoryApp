import { useEffect, useState } from 'react';
import {View, Text} from 'react-native';
import { useNavigation } from '@react-navigation/native';
import { useUserContext } from '../../contexts/UserProvider';
import { LinearGradient } from 'expo-linear-gradient';
import { TouchableOpacity } from 'react-native';
import screens from '../../constants/screens';
import { useAxiosRequest } from '../../hooks/UseAxiosRequest';
import ErrorScreen from '../ErrorScreen/ErrorScreen';
import { MemoizedLoadingScreen } from '../LoadingScreen/LoadingScreen';

// styles
import styles from './_styles-ActiveInventoryScreen';
import { useComponentsUtils } from '../../contexts/ComponentsUtilsProvider';


export default function ActiveInventoryScreen({inventory}){

    const navigation = useNavigation()
    const inventoryTabConstants = screens.InventoryTab;

    const[activeInventoryResponse, error, isLoading, resetHook] = useAxiosRequest("api/Inventories/currentUser/filter?isActive=true", "get")

    const [user, setUser] = useUserContext()
    const [utils, setUtils] = useComponentsUtils()

    useEffect(()=>{
        if(isLoading) return
        if(error) return
        if(!activeInventoryResponse) return
        
        if(Object.keys(activeInventoryResponse.inventories).length == 0){
            setUtils((prevComponentUtils) => ({
                ...prevComponentUtils,
                "ActiveInventoryScreen" : {
                    hasAnyActiveInventory : false
                }
            }))
        }
        
    }, [activeInventoryResponse, error, isLoading])
  
    if(isLoading || (!activeInventoryResponse && !error)){
      return <MemoizedLoadingScreen/>
    }
  
    if(!activeInventoryResponse && error){
      return <ErrorScreen errorTitle ="Błąd Aplikacji"
       errorDescription="InventoryApp nie był w stanie sprawdzić czy istnieje nie zakończona inwentaryzacja." reseter={()=>{resetHook()}}/>
    }

    if(Object.keys(activeInventoryResponse.inventories).length == 0){
        return null
    }

    const activeInventory = activeInventoryResponse.inventories[0]
    
    return(
        <View style={styles.container}>
            <Text style={styles.screenTitle}>Niezakończone inwentaryzacje</Text>
            <View style={styles.inventoryInfoBox}>
                    <Text style={styles.insideButtonBuildingText}>&#127970;    {activeInventory.buildingName}</Text>
                    <Text style={styles.insideButtonRoomText}>&#x1f6aa;    {activeInventory.roomDescription}</Text>
                <TouchableOpacity
                    style={styles.pressableContainer}
                    onPress={() => navigation.navigate(inventoryTabConstants.ItemsScreen.screenName, {locationId: activeInventory.locationId})}>
                    <LinearGradient
                        start={{ x: 0, y: 0 }}
                        end={{x: 1, y: 1 }}
                        colors={['#5851DB', '#C13584', '#E1306C', '#FD1D1D', '#F77737']}
                    >
                        <Text style={styles.insideButtonText}>Kontynuuj</Text>
                    </LinearGradient>
                </TouchableOpacity>
            </View>
        </View>
    )
}