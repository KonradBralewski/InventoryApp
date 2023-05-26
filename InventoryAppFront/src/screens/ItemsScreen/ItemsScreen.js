import { useNavigation } from '@react-navigation/native';
import List from "../../components/List";
import screens from '../../constants/screens';
import { View, TouchableOpacity, Text} from 'react-native';
import { SafeAreaView } from 'react-native-safe-area-context';
import Ionicons from 'react-native-vector-icons/Ionicons';

// styles
import styles from "./_styles-ItemsScreen"

let items = [
  {
    id: '1',
    name: 'Przedmiot 1'
  },
  {
    id: '2',
    name: 'Przedmiot 2',
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
      <View style={styles.buttonsContainer}>
        <TouchableOpacity style={styles.scanButtonContainer}>
          <Ionicons name="scan-circle-sharp" size={60} color="black" style={styles.scanButtonIcon}/>
          <Text style={styles.underButtonText}>Skanuj</Text>
        </TouchableOpacity>
        <TouchableOpacity style={styles.generateButtonContainer}>
          <Ionicons name="document-sharp" size={60} color="black" style={styles.generateButtonIcon}/>
          <Text style={styles.underButtonText}>Generuj</Text>
        </TouchableOpacity>
      </View>
      <View style={styles.container}>
        <List data={items} emptyListMessage="Brak przedmiotów" iconless ={true}/>
      </View>
    </SafeAreaView>
  )
}