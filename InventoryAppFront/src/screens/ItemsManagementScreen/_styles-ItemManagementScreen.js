import { StyleSheet } from "react-native";

const styles = StyleSheet.create({
    container : {
        flex : 1,
        gap : 15,
        justifyContent : "center",
        alignItems : 'center'
    },
    listsContainer : {
        height : 300,
        justifyContent : "center",
        alignItems : "center",
        flexDirection : "column",
        borderWidth : 2,
        borderRadius : 15,
        margin : 20
    },
    itemListContainer : {
        width : 300
    },
    itemContainer : {
      borderRadius: 10,
      marginHorizontal: 10,
      marginBottom: 10,
      flexDirection: "row",
      alignItems: "center",
      justifyContent: "space-between",
      paddingRight: 20,
    },
    stockitemContainer : {
        flexDirection : 'row',
        borderBottomWidth : 1,
        width : '100%',
        height : 50,
        alignItems : "center",
        flex : 1,
        padding : 10
    },
    inListItemText : {
        width : 100,
        fontSize : 18
    },
    pressableContainer : {
        width : 200
    },
    insideButtonText : {
        fontSize : 22,
        textAlign : 'center',
        color: 'white',
        padding: 8
    },
    itemText : {
        fontSize : 20,
        textAlign : 'center'
    }
  });
  
  export default styles;
