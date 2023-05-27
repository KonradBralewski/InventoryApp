import { StyleSheet } from "react-native";

const styles = StyleSheet.create({
    inventoryProcessButtonsContainer : {
        width : '100%',
        flexDirection : 'column',
        justifyContent : 'center',
    },
    /// Nested styles for Button component
    startButton : {
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
            textAlign : 'center'
        }
    },
    endButton : {
        pressableContainer : {
            backgroundColor : 'red',
            borderRadius : 20,
            margin : 5,
            marginBottom : 2,
            padding : 5,
            elevation : 5,
            shadowColor : 'black',
        },
        insideButtonText : {
            fontSize : 25,
            textAlign : 'center'
        }
    },
    scanButton : {
        pressableContainer : {
            margin : 5,
            marginBottom : 2,
            padding : 5,
        },
        insideButtonText : {
            fontSize : 25,
            marginTop : -5,
            textAlign : 'center',
        },
        insideButtonIcon : {
            fontSize : 55,
            alignSelf : 'center'
        }
    },
    generateButton : {
        pressableContainer : {
            margin : 5,
            marginBottom : 2,
            padding : 5
        },
        insideButtonText : {
            fontSize : 25,
            marginTop : -5,
            textAlign : 'center',
        },
        insideButtonIcon : {
            fontSize : 55,
            alignSelf : 'center'
        }
    }

  });
  
  export default styles;
