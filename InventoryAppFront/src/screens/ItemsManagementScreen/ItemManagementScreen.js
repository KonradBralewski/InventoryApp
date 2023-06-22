import { useCallback, useEffect, useState } from 'react';
import {View, Text, FlatList, TouchableOpacity, Modal, Alert} from 'react-native';
import { CommonActions, useFocusEffect, useNavigation} from '@react-navigation/native';
import { useUserContext } from '../../contexts/UserProvider';
import { LinearGradient } from 'expo-linear-gradient';
import screens from '../../constants/screens';
import { useAxiosRequest } from '../../hooks/UseAxiosRequest';
import Ionicons from 'react-native-vector-icons/Ionicons';
import ErrorScreen from '../ErrorScreen/ErrorScreen';
import List from '../../components/List';
import { MemoizedLoadingScreen } from '../LoadingScreen/LoadingScreen';

// styles
import styles from './_styles-ItemManagementScreen';
import { useComponentsUtils } from '../../contexts/ComponentsUtilsProvider';
import AddItemModal from './Components/AddItemModal';


export default function ItemManagementScreen({route}){

    const navigation = useNavigation()
    const [user, setUser] = useUserContext()
    const [utils, setUtils] = useComponentsUtils()

    const [isAddingModalVisible, setIsAddingModalVisible] = useState(false)

    /// building & room state needed for modification
    const [building, setBuilding] = useState(undefined)
    const [room, setRoom] = useState(undefined)
    const [selectedItem, setSelectedItem] = useState(undefined)

    const [moveInfo, setMoveInfo] = useState({
        itemToMove : undefined,
        desiredLocation : undefined
    })

    // setup lifecycle listener
    useFocusEffect(useCallback(()=>{
        return () => {         
          setBuilding(undefined)
          setRoom(undefined)
          setSelectedItem(undefined)
          setMoveInfo({
            itemToMove : undefined,
            desiredLocation : undefined
          })
        }
      }, []))
      // setup lifecycle listener
    
    const inventoryTabConstants = screens.InventoryTab;

    const {stockItemId} = route.params

    const[buildings, buildingsError, isLoadingBuildings, resetBuildingsHook] = useAxiosRequest("api/buildings", "get")
    const[rooms, roomsError, isLoadingRooms, resetRoomsHook] = useAxiosRequest(`api/Locations/building/${building}`, "get")
    const[stockItems, stockItemsError, isLoadingStockItems, resetItemsHook] = useAxiosRequest(`api/StockItems/location/${room}`, "get")

    //DELETE
    const [shouldDelete, setShouldDelete] = useState(false)

    const[deleteResponse, deleteError, isLoadingDelete] = useAxiosRequest(`api/StockItems/${selectedItem}`, "delete", {
        state : shouldDelete,
        modifierFunc : setShouldDelete
    })
    //

    // MOVE (UPDATE)
    const [shouldMove, setShouldMove] = useState(false)
    const [movePayload, setMovePayload] = useState({
        locationId : undefined
    })

    const [moveResponse, moveError, isLoadingMove] = useAxiosRequest(`api/StockItems/${moveInfo.itemToMove}`, 'patch', {
        state : shouldMove,
        modifierFunc : setShouldMove
    }, movePayload)
    //
    
    useEffect(()=>{
        if((!isLoadingDelete && deleteResponse && !deleteError) || (!isLoadingMove && moveResponse && !moveError)){
            navigation.dispatch(
                CommonActions.reset({
                  index: 0,
                  routes: [
                    {
                      name: inventoryTabConstants.displayedText
                    }
                  ],
                })
              );
        }

    }, [deleteResponse, deleteError, isLoadingDelete,
        moveResponse, moveError, isLoadingMove]) // useEffect handling action responses (add/delete)

   const returnBuildingsScrollable = () => {
    if(isLoadingBuildings){
        return <MemoizedLoadingScreen/>
    }
    
    if(!buildings){
        return null
    }

    const buildingsMapped = buildings.map(building => ({...building, 
        onItemPress: () => setBuilding(building.id)}))
    
      return(
        <List data={buildingsMapped} emptyListMessage="Brak budynków" style={styles}/>)
   }

   const returnRoomsScrollable = () => {
    if(isLoadingRooms){
        return <MemoizedLoadingScreen/>
    }
    
    if(!rooms){
        return null
    }

    const onRoomPressed = (id) => {
        if(moveInfo.itemToMove != undefined){
            Alert.alert(
                "Przenoszenie przedmiotu",
                "           Zatwierdź przenoszenie.",
                [{
                    text : "Zatwierdź",
                    onPress : ()=> {
                        setMovePayload({
                            locationId : id
                        })
                        setShouldMove(true)
                    },
                    style : "default"
                },
                {
                    text : "Anuluj",
                    style : "cancel"
                }
            ]
            )
            return
        }
        setRoom(id)
    }

    const roomsMapped = rooms.map(location => ({id : location.id, name : location.roomDescription, 
        onItemPress: () => onRoomPressed(location.id)}))
    
      return(
        <List data={roomsMapped} emptyListMessage="Brak pomieszczeń" style={styles}/>)
   }

   const returnItemsScrollable = () => {
    if(isLoadingStockItems){
        return <MemoizedLoadingScreen/>
    }
    
    if(!buildings || !room || !stockItems){
        return null
    }

    const itemsMapped = stockItems.map(item => ({
        id : item.id,
        name : item.name,
        code : item.code,
        isArchive : item.isArchive
    }))

    const ListItem = ({item}) => {
        return (
          <TouchableOpacity style={[styles.stockitemContainer, item.id === selectedItem ? {
            borderColor : 'green',
            borderWidth : 3,
            borderBottomWidth : 3,
            borderRadius : 10
          } : {}
        ]} onPress={()=>setSelectedItem(item.id)}>
              <Text style={styles.inListItemText}>{item.name}</Text> 
              <Text style={styles.inListItemText}>{item.code}</Text>
              {item.isArchive && 
              <View style={styles.archivedView}>
                <Ionicons name="trash-bin-sharp" color="black" size={37}/>
              </View>}
            </TouchableOpacity>
        );}

    const emptyItemListResponse = () => {
        return (
            <View style={{ alignItems: "center" }}>
                <Text style={{padding: 20, fontSize: 20, marginTop: 5}}>Brak przedmiotów</Text>
            </View>
            );
          }

      return(
        <FlatList
        data={itemsMapped}
        renderItem={(props)=><ListItem {...props}/>}
        keyExtractor={(item) => item.id}
        contentContainerStyle={styles.itemListContainer}
        ListEmptyComponent= {emptyItemListResponse}
      />)
   }

   const returnConditionalList = () =>{
    if(!building){
        return returnBuildingsScrollable()
    }
    
    if(!room){
        return returnRoomsScrollable()
    }

    return returnItemsScrollable()
   }

   if((!isLoadingBuildings && !buildings && buildingsError) ||
   (building && !isLoadingRooms && !rooms && roomsError)){
        return <ErrorScreen errorTitle ="Błąd Aplikacji"
     errorDescription="InventoryApp nie był skorzystać z usługi modyfikacji środków." reseter={()=>{resetBuildingsHook()}}/>
    }

    if(!isLoadingMove && !moveResponse && moveError){
         return <ErrorScreen errorTitle ="Błąd Aplikacji"
      errorDescription="InventoryApp nie był w stanie przenieść przedmiotu przez błąd serwera." reseter={()=>{resetBuildingsHook()}}/>
     }

    const backButtonHandler = () => {
        if(room){
            setRoom(undefined)
            return
        }

        if(building){
            setBuilding(undefined)
            return
        }
    }

    // action functions
    const deleteItem = () => {
        if(!selectedItem){
            return
        }
        
        Alert.alert(
            "Usuwanie przedmiotu",
            "           Zatwierdź usuwanie.",
            [{
                text : "Zatwierdź",
                onPress : ()=> {
                    setShouldDelete(true)
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
    
    const moveItem = () => {
        setMoveInfo({
            itemToMove : selectedItem,
            desiredLocation : undefined,
            originLocation : room
        })
        setSelectedItem(undefined)
        setRoom(undefined)
        setBuilding(undefined)
    }
    //

    if(isLoadingDelete){
        return <MemoizedLoadingScreen/>
    }

    return(
        <View style={styles.container}>
            {isAddingModalVisible && <AddItemModal locationId={room} hideModal={()=>setIsAddingModalVisible(false)}/>}
            {building && <Ionicons name='return-down-back' size={70} color="black" style={{marginBottom : - 30}} onPress={backButtonHandler}/>}
            <View style={styles.listsContainer}>
                {returnConditionalList()}
            </View>
            <TouchableOpacity
                    disabled={room === undefined}
                    style={styles.pressableContainer}
                    onPress={()=>setIsAddingModalVisible(true)}>
                    <LinearGradient
                        start={{ x: 0, y: 0 }}
                        end={{x: 1, y: 1 }}
                        colors={room === undefined ? ['gray', 'black'] : ['#90ba16', '#1fab0c', '#00ff66']}
                    >
                        <Text style={styles.insideButtonText}>Dodaj</Text>
                    </LinearGradient>
            </TouchableOpacity>
            <TouchableOpacity
                    disabled={selectedItem === undefined}
                    style={styles.pressableContainer}
                    onPress={deleteItem}>
                    <LinearGradient
                        start={{ x: 0, y: 0 }}
                        end={{x: 1, y: 1 }}
                        colors={selectedItem === undefined ? ['gray', 'black'] : ['#e0143b', '#de151c', '#ff0027']}
                    >
                        <Text style={styles.insideButtonText}>Usuń</Text>
                    </LinearGradient>
            </TouchableOpacity>
            <TouchableOpacity
                    disabled={selectedItem === undefined}
                    style={styles.pressableContainer}
                    onPress={moveItem}>
                    <LinearGradient
                        start={{ x: 0, y: 0 }}
                        end={{x: 1, y: 1 }}
                        colors={selectedItem === undefined ? ['gray', 'black'] : ['#262324', '#452829']}
                    >
                        <Text style={styles.insideButtonText}>Przenieś</Text>
                    </LinearGradient>
            </TouchableOpacity>
        </View>
    )
}