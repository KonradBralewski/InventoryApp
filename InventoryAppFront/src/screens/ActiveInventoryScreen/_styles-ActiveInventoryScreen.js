import { StyleSheet } from "react-native";

const styles = StyleSheet.create({
    container : {
        flex : 1,
        justifyContent : 'center',
        alignItems : 'center',
        marginBottom : 50
    },
    screenTitle : {
        fontWeight : "bold",
        fontSize : 30,
        marginBottom : 35 
    },

    inventoryInfoBox : {
        elevation : 5,
        borderWidth : 1,
        padding : 15
    },
    pressableContainer : {
        backgroundColor : 'red'
    },
        insideButtonBuildingText : {
            fontSize : 22,
            textAlign : 'center',
            color: 'black',
            padding: 8
        },
        insideButtonRoomText : {
            fontSize : 22,
            textAlign : 'center',
            color: 'black',
            padding: 8
        },
    insideButtonText : {
        fontSize : 22,
        textAlign : 'center',
        color: 'white',
        padding: 8
    }
  });
  
  export default styles;
