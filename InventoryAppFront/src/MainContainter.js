import * as React from 'react';
import {View, Text} from 'react-native';

import { NavigationContainer } from '@react-navigation/native';
import { createBottomTabNavigator } from '@react-navigation/bottom-tabs';
import Ionicons from 'react-native-vector-icons/Ionicons'

//Screens
import HomeScreen from './screens/HomeScreen/HomeScreen';
import RaportScreen from './screens/RaportScreen/RaportScreen';
import MainInventoryScreen from "./screens/MainInventoryScreen/MainInventoryScreen";
import InventoryItems from './screens/ItemsScreen/ItemsScreen';


//Screens names
const homeName = 'Home';
const raportName = 'Raport';
const inventoryName = 'Inventory';

const Tab = createBottomTabNavigator();

export default function MainContainter({navigation}){
    return(
        <NavigationContainer>
                <Tab.Navigator
                    initialRouteName={homeName}
                    screenOptions={({ route }) => ({
                        tabBarIcon: ({ focused, color, size }) => {
                            let iconName;
                            let rn = route.name;

                            if (rn === homeName) {
                                iconName = focused ? 'home' : 'home-outline';
                            }else if (rn === inventoryName) {
                                iconName = focused ? 'business' : 'business-outline';
                            }else if (rn === raportName) {
                                iconName = focused ? 'reader' : 'reader-outline';
                            }

                            return <Ionicons name={iconName} size={size} color={color} />
                        },
                    })}
                >
                <Tab.Screen name={homeName} component={HomeScreen}/>    
                <Tab.Screen name={inventoryName} component={MainInventoryScreen}/>    
                <Tab.Screen name={raportName} component={RaportScreen}/>    

            </Tab.Navigator>

        </NavigationContainer>
    );
}