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
import styles from './_styles-ItemManagementScreen';
import { useComponentsUtils } from '../../contexts/ComponentsUtilsProvider';


export default function ItemManagementScreen({route}){

    const navigation = useNavigation()
    const [user, setUser] = useUserContext()
    const [utils, setUtils] = useComponentsUtils()
    
    const inventoryTabConstants = screens.InventoryTab;

    const {stockItemId} = route.params

    const[buildings, buildingsError, isLoadingBuildings, resetBuildingsHook] = useAxiosRequest("api/buildings", "get")
    const[deleteResponse, deleteError, isLoadingDelete] = useAxiosRequest(`api/StockItems/${stockItemId}`, "get")
    //const[buildings, buildingsError, isLoadingBuildings, resetBuildingsHook] = useAxiosRequest("api/buildings", "get")
    //const[buildings, buildingsError, isLoadingBuildings, resetBuildingsHook] = useAxiosRequest("api/buildings", "get")
   //const[buildings, buildingsError, isLoadingBuildings, resetBuildingsHook] = useAxiosRequest("api/buildings", "get")
    
    return(
        <View style={styles.container}>
        </View>
    )
}