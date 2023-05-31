import { StyleSheet } from "react-native";

const styles = StyleSheet.create({
    container : {
        flex : 1,
        backgroundColor : 'grey'
    },
    cameraContainer : {
        flex : 1
    },
    maskContainer : {
        flex : 1,
    },
    maskWrapper : {
        flex : 1,
        backgroundColor: 'rgba(0, 0, 0, 0.15)',
        position : 'absolute',
        top : 0, right : 0, bottom : 0, left : 0,
        justifyContent : 'center',
        alignItems : 'center'
    },
    maskView  : {
        width: 200,
        height: 200,
        backgroundColor: '#000',
        borderRadius : 30
    },
    scanningBorderView : {
        position : 'absolute',
        alignSelf : 'center',
        justifyContent : 'center',
        width : 200,
        height : 200,
        top : 208,
        borderWidth : 5,
        borderRadius : 30,
        borderStyle : "dotted"
    }
});
  
  export default styles;
