import {Text, View, TouchableOpacity, SafeAreaView, FlatList} from "react-native";
import Ionicons from 'react-native-vector-icons/Ionicons';

//styles
import defaultStyles from './_styles-List.js'

const ListItem = ({item, iconName, disabled, style}) =>{
    return (
      <TouchableOpacity onPress={item.onItemPress} disabled={disabled}>
        <View style={[defaultStyles.itemContainer, style.itemContainer]}>
          <Text style={[defaultStyles.itemText, style.itemText]}>{item.name}</Text>
          {iconName && <Ionicons name={iconName} size={24} color="black" style={[defaultStyles.chevronIcon, style.chevronIcon]}/>} 
        </View>
      </TouchableOpacity>
    );
};

const EmptyListView = ({emptyListMessage, style}) => {
  return (
    <View style={{ alignItems: "center" }}>
      <Text style={[defaultStyles.itemText, style.itemText]}>{emptyListMessage}</Text>
    </View>
  );
};

const ListHeader = ({headerTitle, style}) => {
  if(!headerTitle) return null
  return(
    <View style={defaultStyles.listHeaderContainer}>
      <Text style={[defaultStyles.listHeaderText, style.listHeaderText]}>{headerTitle}</Text>
    </View>
  )
}

export default function List({style, data, emptyListMessage, headerTitle, iconName, disabled}){
  return (
    <SafeAreaView style={[defaultStyles.container, style]}>
      <FlatList
        data={data}
        renderItem={(props)=><ListItem {...props} iconName={iconName} disabled={disabled} style={style}/>}
        keyExtractor={(item) => item.id}
        ListEmptyComponent={()=><EmptyListView emptyListMessage={emptyListMessage} style={style}/>}
        ListHeaderComponent={()=><ListHeader headerTitle={headerTitle} style={style}/>}
      />
    </SafeAreaView>
  )
}

List.defaultProps = {
  style : {},
  emptyListMessage : "Lista jest pusta.",
  disabled : false
}