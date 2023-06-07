import { useNavigation } from '@react-navigation/native';
import screens from '../../constants/screens';
import { View, Text, FlatList, DeviceEventEmitter} from 'react-native';
import { SafeAreaView } from 'react-native-safe-area-context';
import InventoryProcess from './Components/InventoryProcess';
import { useAxiosRequest } from '../../hooks/UseAxiosRequest';
import { MemoizedLoadingScreen } from '../LoadingScreen/LoadingScreen';
import ErrorScreen from '../ErrorScreen/ErrorScreen';
import Ionicons from 'react-native-vector-icons/Ionicons';
import { useComponentsUtils } from '../../contexts/ComponentsUtilsProvider';

// styles
import styles from "./_styles-ItemsScreen"
import { useEffect } from 'react';


export default function ItemsScreen({route}){

  const [utils, setUtils] = useComponentsUtils()

  const navigate = useNavigation()
  const inventoryTabConstants = screens.InventoryTab

  const {locationId} = route.params

  const[inventoryProcessResponse, inventoryProcessError] = useAxiosRequest(`api/Inventories/${locationId}`, "get")

  const[data, error, isLoading, resetHook] = useAxiosRequest(`api/StockItems/location/${locationId}`, "get")

  useEffect(()=>setUtils(prev => ({
    ...prev, "ItemsScreen" : {
      ...prev["ItemsScreen"],
      reseter : resetHook,
      locationId : locationId
    }
  })),[]) // set reseter & locationId for ItemsScreen

  if(isLoading || (!data && !error) || (!inventoryProcessResponse && !inventoryProcessError)){
    return <MemoizedLoadingScreen/>
  }

  if(!inventoryProcessResponse && inventoryProcessError){
    return <ErrorScreen errorTitle ="Błąd Aplikacji"
     errorDescription="InventoryApp nie był w stanie otrzymać informacji o przebiegających inwentaryzacjach." reseter={()=>{resetHook()}}/>
  }

  if(!data && error){
    return <ErrorScreen errorTitle ="Błąd Aplikacji"
     errorDescription="InventoryApp nie był w stanie otrzymać listy przedmiotów." reseter={()=>{resetHook()}}/>
  }

  const items = data.map(item => ({
    id : item.id,
    name : item.name,
    code : item.code,
    wasScanned : item.scannedItemEntryId != null,
    isArchive : item.isArchive
    }))

  const ListItem = ({item}) =>{
    return (
      <View style={styles.itemContainer}>
          <Text style={styles.itemText}>{item.name}</Text> 
          <Text style={styles.itemText}>{item.code}</Text>
          <View style={styles.checkboxView}>
            {item.wasScanned && <Ionicons name="checkmark-sharp" color={"green"} size={37}/>}
          </View>
        </View>
    );
  };

  const EmptyListView = () => {
    return (
      <View style={{ alignItems: "center" }}>
        <Text style={styles.itemText}>Lista przedmiotów jest pusta.</Text>
      </View>
    );
  };
  
  var processStarted = inventoryProcessResponse.inventories[0].statusName == "Started" 
  var processEnded = inventoryProcessResponse.inventories[0].statusName == "Ended"

  return(
    <SafeAreaView style={styles.safeAreaContainer}>
      <InventoryProcess processStarted={processStarted} processEnded={processEnded} itemsScreenReseter={resetHook}/>
      <FlatList
        data={items}
        renderItem={(props)=><ListItem {...props}/>}
        keyExtractor={(item) => item.id}
        ListEmptyComponent={EmptyListView}
        contentContainerStyle={styles.listContainer}
      />
    </SafeAreaView>
  )
}