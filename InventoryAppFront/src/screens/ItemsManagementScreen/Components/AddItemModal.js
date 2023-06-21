import { Modal, Text} from "react-native"
import List from "../../../components/List"

export default function AddItemModal() {
    const products = {
        id : 1,
        name : 'Myszka',
        onItemPress : () => {}
    }
    
  return (
  <Modal>
    <List data={products} iconName={"chevron-forward-outline"}/>
  </Modal>)
}
