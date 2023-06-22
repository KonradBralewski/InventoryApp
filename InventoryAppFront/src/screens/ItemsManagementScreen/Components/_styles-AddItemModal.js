import { StyleSheet } from "react-native";

const styles = StyleSheet.create({
  itemNameInput : {
    width : 200,
    height : 50,
    backgroundColor : 'black',
    borderWidth : 2,
    borderColor : 'gray',
    color : 'white',
    padding : 5
  },
  inputAndButtonContainer : 
  {
    flex : 1,
    flexDirection : "column",
    alignItems : "center",
    justifyContent : "center",
    gap : 5
  },
  errorMessage : {
    margin : 5,
    color : 'red',
    fontSize : 20,
    fontWeight : 'bold'
},
  addButton : {
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
        fontSize : 20,
        textAlign : 'center',
        margin : 5
    }
}
  });
  
  export default styles;
