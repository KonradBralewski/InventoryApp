import { useNavigation } from '@react-navigation/native';
import List from "../../components/List";
import screens from '../../constants/screens';

let rooms = [
  {
    id: '1',
    name: 'Pomieszczenie 1'
  },
  {
    id: '2',
    name: 'Pomieszczenie 2',
  },
  {
    id: '3',
    name: 'Pomieszczenie 3',
  },
  {
    id: '4',
    name: 'Pomieszczenie 4',
  },
  {
    id: '5',
    name: 'Pomieszczenie 5',
  },
  {
    id: '6',
    name: 'Pomieszczenie 6',
  },
  {
    id: '7',
    name: 'Pomieszczenie 7',
  },
  {
    id: '8',
    name: 'Pomieszczenie 8',
  },
];

export default function BuildingsScreen(){
  const navigate = useNavigation();
  const homeTabConstants = screens.HomeTab;

  rooms = rooms.map(room => ({...room, 
    onItemPress: () => navigate.navigate(homeTabConstants.ItemsScreen.screenName, {roomsId: room.id})}))

  return(
    <List data={rooms} headerTitle = "Wybierz Pomieszczenie" emptyListMessage="Brak Pomieszczeń" iconName={"chevron-forward-outline"}/>
  )
}