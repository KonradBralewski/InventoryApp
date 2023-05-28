import { StyleSheet } from "react-native";

const styles = StyleSheet.create({
    container : {
        flex:1,
        alignItems:'center',
        justifyContent:'center'
    },
    errorTitle : {
        color : 'black',
        fontSize : 35,
        fontWeight : 'bold'
    },
    errorDescription : {
        color : 'black',
        fontSize : 25,
        textAlign : 'center'
    },
    resetButton : {
        pressableContainer : {
            backgroundColor : 'green',
            borderRadius : 20,
            margin : 5,
            marginBottom : 0,
            padding : 5,
            elevation : 5,
            shadowColor : 'black'
        },
        insideButtonText : {
            fontSize : 25,
            textAlign : 'center',
            margin : 5
        }
    }
  });
  
  export default styles;
