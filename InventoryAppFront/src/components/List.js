import {Text, View, TouchableOpacity, SafeAreaView, FlatList} from "react-native";
import Ionicons from 'react-native-vector-icons/Ionicons';

//styles
import styles from './_styles-List';

const ListItem = ({item, iconName}) =>{
    return (
      <TouchableOpacity onPress={item.onItemPress}>
        <View style={styles.itemContainer}>
          <Text style={styles.itemText}>{item.name}</Text>
          {iconName && <Ionicons name={iconName} size={24} color="black" style={styles.chevronIcon}/>} 
        </View>
      </TouchableOpacity>
    );
};

const EmptyListView = ({emptyListMessage}) => {
  return (
    <View style={{ alignItems: "center" }}>
      <Text style={styles.itemText}>{emptyListMessage}</Text>
    </View>
  );
};

const ListHeader = ({headerTitle}) => {
  if(!headerTitle) return null
  return(
    <View style={styles.listHeaderContainer}>
      <Text style={styles.listHeaderText}>{headerTitle}</Text>
    </View>
  )
}

export default function List({data, emptyListMessage, headerTitle, iconName}){
  return (
    <SafeAreaView style={styles.container}>
      <FlatList
        data={data}
        renderItem={(props)=><ListItem {...props} iconName={iconName}/>}
        keyExtractor={(item) => item.id}
        ListEmptyComponent={()=><EmptyListView emptyListMessage={emptyListMessage}/>}
        ListHeaderComponent={()=><ListHeader headerTitle={headerTitle}/>}
      />
    </SafeAreaView>
  )
}