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
    scannedCodeInfo : {
        fontSize : 25,
        color : 'white'
    },
    inventoryButton : {
        pressableContainer : {
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
        }
    }
});
  
  export default styles;
