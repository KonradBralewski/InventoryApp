import * as React from 'react';
import { createStackNavigator } from '@react-navigation/stack';
import BuildingsScreen from '../BuildingsScreen/BuildingsScreen';
import RoomsScreen from '../RoomsScreen/RoomsScreen';
import ItemsScreen from '../ItemsScreen/ItemsScreen';
import screens from '../../constants/screens';
import ScanningScreen from '../ScanningScreen/ScanningScreen';
import {returnObjectWithExactPropertyValue} from "../../utils/jsonUtils"

export default function InventoryScreen() {
  const Stack = createStackNavigator();

  const homeTabConstants = screens.HomeTab;
  
  return (
    <Stack.Navigator screenOptions={(route)=> {
      var foundSettings = returnObjectWithExactPropertyValue(homeTabConstants,"screenName",route.route.name)

      if(!foundSettings) {
        return {}
      }
      
      return {
        headerShown : foundSettings.isScreenHeaderVisible
      }
    }}>
      <Stack.Screen name={homeTabConstants.ItemsScreen.screenName} component={ItemsScreen} />
      <Stack.Screen name={homeTabConstants.BuildingsScreen.screenName} component={BuildingsScreen} />
      <Stack.Screen name={homeTabConstants.RoomsScreen.screenName} component={RoomsScreen} />
      <Stack.Screen name={homeTabConstants.ScanningScreen.screenName} component={ScanningScreen} />
    </Stack.Navigator>
  );
}
