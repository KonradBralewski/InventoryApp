import { useNavigation } from '@react-navigation/native';
import List from "../../components/List";
import screens from '../../constants/screens';
import { useState, useEffect } from 'react';
import { useAxiosRequest } from '../../hooks/UseAxiosRequest';
import { MemoizedLoadingScreen } from '../LoadingScreen/LoadingScreen';
import ErrorScreen from '../ErrorScreen/ErrorScreen';

export default function RoomsScreen({route}){
  const navigate = useNavigation();
  const {buildingId} = route.params
  const inventoryTabConstants = screens.InventoryTab;

  const[data, error, isLoading, resetHook] = useAxiosRequest(`api/Locations/building/${buildingId}`, "get")

  if(isLoading || (!data && !error)){
    return <MemoizedLoadingScreen/>
  }

  if(!data && error){
    return <ErrorScreen errorTitle ="Błąd Aplikacji"
     errorDescription="InventoryApp nie był w stanie otrzymać listy pomieszczeń." reseter={()=>{resetHook()}}/>
  }
  
  rooms = data.map(location => ({id : location.roomId, name : location.roomDescription, 
    onItemPress: () => navigate.navigate(inventoryTabConstants.ItemsScreen.screenName, {locationId: location.id})}))

  return(
    <List data={rooms} headerTitle = "Wybierz Pomieszczenie" emptyListMessage="Brak Pomieszczeń" iconName={"chevron-forward-outline"}/>
  )
}