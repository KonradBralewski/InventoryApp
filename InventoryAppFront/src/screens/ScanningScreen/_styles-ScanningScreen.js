import { StyleSheet } from "react-native";

const styles = StyleSheet.create({
    container : {
        flex : 1,
        backgroundColor : 'black'
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
        justifyContent : 'center',
        alignItems : 'center'
    },
    maskView  : {
        width: 200,
        height: 200,
        backgroundColor: '#000'
    }
});
  
  export default styles;
