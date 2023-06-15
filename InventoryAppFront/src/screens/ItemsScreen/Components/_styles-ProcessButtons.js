import { StyleSheet } from "react-native";

const styles = StyleSheet.create({
    inventoryProcessButtonsContainer : {
        width : '100%',
        justifyContent : 'center',
        alignItems : 'center'
    },
    /// Nested styles for Button component
    startButton : {
       pressableContainer : {
            width : '90%'
        },
        gradientContainer : {
            borderRadius : 8,
            margin : 2,
            marginBottom : 0,
            padding : 8
        },
        insideButtonText : {
            fontSize : 22,
            textAlign : 'center',
            color: 'white',
            padding: 8
        }
    },
    endButton : {
        pressableContainer : {
            width : '90%',
        },
        gradientContainer : {
            borderRadius : 8,
            margin : 2,
            marginBottom : 0,
            padding : 8
        },
        insideButtonText : {
            fontSize : 22,
            textAlign : 'center',
            color: 'white',
            padding: 8
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
