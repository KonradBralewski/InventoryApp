import { useNavigation } from '@react-navigation/native';
import { useEffect, useState } from 'react';
import List from "../../components/List";
import screens from '../../constants/screens';
import { useAxiosRequest } from '../../hooks/UseAxiosRequest';
import ErrorScreen from '../ErrorScreen/ErrorScreen';
import { MemoizedLoadingScreen } from '../LoadingScreen/LoadingScreen';


export default function BuildingsScreen(){
  const navigate = useNavigation();
  const homeTabConstants = screens.HomeTab;

  const[data, error, isLoading, resetHook] = useAxiosRequest("api/buildings", "get")

  if(isLoading || (!data && !error)){
    return <MemoizedLoadingScreen/>
  }

  if(!data && error){
    return <ErrorScreen errorTitle ="Błąd Aplikacji"
     errorDescription="InventoryApp nie był w stanie otrzymać listy budynków." reseter={()=>{resetHook()}}/>
  }

  const buildings = data.map(building => ({...building, 
    onItemPress: () => navigate.navigate(homeTabConstants.RoomsScreen.screenName, {buildingId: building.id})}))

  return(
    <List data={buildings} headerTitle = "Wybierz Budynki" emptyListMessage="Brak budynków" iconName={"chevron-forward-outline"}/>
  )
}