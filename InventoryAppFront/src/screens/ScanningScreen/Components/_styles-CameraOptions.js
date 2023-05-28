import { StyleSheet } from "react-native";

const styles = StyleSheet.create({
    container : {
        flex : 1,
        flexDirection : 'row',
        position : 'absolute'
    },
    scannedCodeInfo : {
        fontSize : 25,
        color : 'white'
    },
    repeatScanButton : {
        pressableContainer : {
            margin : 5,
            marginBottom : 2,
            padding : 5,
            backgroundColor : 'green',
            width : '33%'
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
