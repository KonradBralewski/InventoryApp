import { NavigationContainer } from '@react-navigation/native';
import { createBottomTabNavigator } from '@react-navigation/bottom-tabs';
import Ionicons from 'react-native-vector-icons/Ionicons';
import screens from './constants/screens';

//Screens
import HomeScreen from './screens/HomeScreen/HomeScreen';
import RaportScreen from './screens/RaportScreen/RaportScreen';
import InventoryScreen from "./screens/InventoryScreen/InventoryScreen";
import LoginScreen from './screens/LoginScreen/LoginScreen';
import { useUserContext } from './contexts/UserProvider';

const Tab = createBottomTabNavigator();

export default function MainContainter({navigation}){
    const [user] = useUserContext()

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
                                else if (rn === screens.LoginTab.displayedText){
                                    iconName = 'log-in-outline';
                                }

                                return <Ionicons name={iconName} size={size} color={color} />
                            },
                        })}
                    >
                    {user.token != undefined ? 
                    <>
                    <Tab.Screen name={screens.HomeTab.displayedText} component={HomeScreen}/>    
                    <Tab.Screen name={screens.InventoryTab.displayedText} component={InventoryScreen}/>    
                    <Tab.Screen name={screens.RaportsTab.displayedText} component={RaportScreen}/> 
                    </>
                    : 
                    <>
                    <Tab.Screen name={screens.LoginTab.displayedText} component={LoginScreen}/>
                    </>}
                </Tab.Navigator>

            </NavigationContainer>
    );
}