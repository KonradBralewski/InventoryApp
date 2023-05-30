import { useNavigation } from '@react-navigation/native';
import List from "../../components/List";
import screens from '../../constants/screens';
import { View, TouchableOpacity, Text} from 'react-native';
import { SafeAreaView } from 'react-native-safe-area-context';
import InventoryProcess from './Components/InventoryProcess';

// styles
import styles from "./_styles-ItemsScreen"

let items = [
  {
    id: '1',
    name: 'Klawiatura 1'
  },
  {
    id: '2',
    name: 'Biurko 1',
  },
  {
    id: '3',
    name: 'Przedmiot 3',
  },
  {
    id: '4',
    name: 'Przedmiot 4',
  },
  {
    id: '5',
    name: 'Przedmiot 5',
  },
  {
    id: '6',
    name: 'Przedmiot 6',
  },
  {
    id: '7',
    name: 'Przedmiot 7',
  },
  {
    id: '8',
    name: 'Przedmiot 8',
  },
];

export default function ItemsScreen(){
  const navigate = useNavigation();
  const homeTabConstants = screens.HomeTab;

  return(
    <SafeAreaView style={styles.safeAreaContainer}>
      <InventoryProcess/>
      <View style={styles.container}>
        <List data={items} emptyListMessage="Brak przedmiotów" iconless ={true} disabled={true}/>
      </View>
    </SafeAreaView>
  )
}