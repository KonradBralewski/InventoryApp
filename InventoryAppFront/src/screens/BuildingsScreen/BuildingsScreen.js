import { useNavigation } from '@react-navigation/native';
import List from "../../components/List";
import screens from '../../constants/screens';

let buildings = [
  {
    id: '1',
    name: 'Budynek 1'
  },
  {
    id: '2',
    name: 'Budynek 2',
  },
  {
    id: '3',
    name: 'Budynek 3',
  },
  {
    id: '4',
    name: 'Budynek 4',
  },
  {
    id: '5',
    name: 'Budynek 5',
  },
  {
    id: '6',
    name: 'Budynek 6',
  },
  {
    id: '7',
    name: 'Budynek 7',
  },
  {
    id: '8',
    name: 'Budynek 8',
  },
];

export default function BuildingsScreen(){
  const navigate = useNavigation();
  const homeTabConstants = screens.HomeTab;

  buildings = buildings.map(building => ({...building, 
    onItemPress: () => navigate.navigate(homeTabConstants.RoomsScreen.screenName, {buildingId: building.id})}))

  return(
    <List data={buildings} headerTitle = "Wybierz Budynki" emptyListMessage="Brak budynków" iconName={"chevron-forward-outline"}/>
  )
}