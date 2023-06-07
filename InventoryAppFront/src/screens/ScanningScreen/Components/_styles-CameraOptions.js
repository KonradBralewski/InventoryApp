import { StyleSheet } from "react-native";

const styles = StyleSheet.create({
    container : {
        flex : 1,
        flexDirection : 'column',
        position : 'absolute',
        bottom : 15,
        left : 0,
        right : 0,
        justifyContent : 'center',
        alignItems : 'center',
    },
    successPopup : {
        container : {
            flex : 1,
            top : -100
        }
    },
    codeInput : {
        backgroundColor : "white",
        width : 200,
        textAlign : 'center',
        height : 30,
        borderWidth : 2,
        fontSize : 20
    },
    inventoryButton : {
        pressableContainer : {
            width : 200,
            margin : 5,
            marginBottom : 2,
            padding : 5,
            backgroundColor : 'orange'
        },
        insideButtonText : {
            fontSize : 25,
            marginTop : -5,
            textAlign : 'center',
            color : 'white'
        },
        insideButtonIcon : {
            fontSize : 35,
            alignSelf : 'center'
        }
    },
    repeatScanButton : {
        pressableContainer : {
            width : 200,
            margin : 5,
            marginBottom : 2,
            padding : 5,
            backgroundColor : 'green'
        },
        insideButtonText : {
            fontSize : 25,
            marginTop : -5,
            textAlign : 'center',
            color : 'white'
        },
        insideButtonIcon : {
            fontSize : 35,
            alignSelf : 'center'
        },
    }
});
  
  export default styles;
