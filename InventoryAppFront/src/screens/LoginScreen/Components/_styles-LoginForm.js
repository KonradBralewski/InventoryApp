import { StyleSheet } from "react-native";

const styles = StyleSheet.create({
    container: {
        flex : 1,
        justifyContent : 'center',
        alignItems : 'center'
    },
    errorMessage : {
        margin : 5,
        color : 'red',
        fontSize : 20,
        fontWeight : 'bold'
    },
    emailInput : {
        width : 200,
        height : 50,
        backgroundColor : 'black',
        borderWidth : 2,
        borderColor : 'gray',
        color : 'white',
        padding : 5
    },
    passwordInput : {
        width : 200,
        height : 50,
        backgroundColor : 'black',
        borderWidth : 2,
        borderTopWidth : 0,
        borderColor : 'gray',
        color : 'white',
        padding : 5
    },
    loginButton : {
        pressableContainer : {
            borderRadius : 8,
            margin : 5,
            marginBottom : 0,
            padding : 8,
            elevation : 10,
            backgroundColor : 'green',
            shadowColor : 'black'
            
        },
        insideButtonText : {
            fontSize : 22,
            textAlign : 'center',
            color: 'white',
            padding: 8
        }
    },
  });
  
  export default styles;
