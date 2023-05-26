import { NavigationContainer } from '@react-navigation/native';
import { createBottomTabNavigator } from '@react-navigation/bottom-tabs';
import Ionicons from 'react-native-vector-icons/Ionicons';
import screens from './constants/screens';

//Screens
import HomeScreen from './screens/HomeScreen/HomeScreen';
import RaportScreen from './screens/RaportScreen/RaportScreen';
import InventoryScreen from "./screens/InventoryScreen/InventoryScreen";

const Tab = createBottomTabNavigator();

export default function MainContainter({navigation}){
    return(
        <NavigationContainer>
                <Tab.Navigator
                    initialRouteName={screens.HomeTab.displayedText}
                    screenOptions={({ route }) => ({
                        tabBarIcon: ({ focused, color, size }) => {
                            let iconName;
                            let rn = route.name;

                            if (rn === screens.HomeTab.displayedText) {
                                iconName = focused ? 'home' : 'home-outline';
                            }else if (rn === screens.InventoryTab.displayedText) {
                                iconName = focused ? 'business' : 'business-outline';
                            }else if (rn === screens.RaportsTab.displayedText) {
                                iconName = focused ? 'reader' : 'reader-outline';
                            }

                            return <Ionicons name={iconName} size={size} color={color} />
                        },
                    })}
                >
                <Tab.Screen name={screens.HomeTab.displayedText} component={HomeScreen}/>    
                <Tab.Screen name={screens.InventoryTab.displayedText} component={InventoryScreen}/>    
                <Tab.Screen name={screens.RaportsTab.displayedText} component={RaportScreen}/>    

            </Tab.Navigator>

        </NavigationContainer>
    );
}