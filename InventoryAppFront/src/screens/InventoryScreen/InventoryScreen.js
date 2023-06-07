import { useEffect } from 'react';
import { createStackNavigator } from '@react-navigation/stack';
import BuildingsScreen from '../BuildingsScreen/BuildingsScreen';
import RoomsScreen from '../RoomsScreen/RoomsScreen';
import ItemsScreen from '../ItemsScreen/ItemsScreen';
import screens from '../../constants/screens';
import ScanningScreen from '../ScanningScreen/ScanningScreen';
import {returnObjectWithExactPropertyValue} from "../../utils/jsonUtils"
import ActiveInventoryScreen from '../ActiveInventoryScreen/ActiveInventoryScreen';

export default function InventoryScreen() {
  const Stack = createStackNavigator();

  const inventoryTabConstants = screens.InventoryTab;

  return (
    <Stack.Navigator screenOptions={(route)=> {
      var foundSettings = returnObjectWithExactPropertyValue(inventoryTabConstants,"screenName",route.route.name)

      if(!foundSettings) {
        return {}
      }
      
      return {
        headerShown : foundSettings.isScreenHeaderVisible,
        headerTitleAlign : foundSettings.headerTitleAlign
      }
    }}>
      <Stack.Screen name={inventoryTabConstants.ActiveInventoryScreen.screenName} component={ActiveInventoryScreen}/>
      <Stack.Screen name={inventoryTabConstants.BuildingsScreen.screenName} component={BuildingsScreen} />
      <Stack.Screen name={inventoryTabConstants.RoomsScreen.screenName} component={RoomsScreen} />
      <Stack.Screen name={inventoryTabConstants.ItemsScreen.screenName} component={ItemsScreen} />
      <Stack.Screen name={inventoryTabConstants.ScanningScreen.screenName} component={ScanningScreen} />
    </Stack.Navigator>
  );
}
