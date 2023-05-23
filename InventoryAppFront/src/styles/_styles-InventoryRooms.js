import { StyleSheet } from "react-native";

const styles = StyleSheet.create({
    container: {
      padding: 5,
      flex: 1,
    },
    item: {
      padding: 20,
      fontSize: 20,
      marginTop: 5,
    },
    headerContainer: {
      backgroundColor: "#1470d9",
      borderRadius: 10,
      padding: 10,
      marginLeft: 10,
      marginRight: 10,
      marginTop: 0,
      marginBottom: 40,
    },
    headerText: {
      fontSize: 30,
      color: "white",
      fontWeight: "bold",
    },
    buildingContainer: {
      backgroundColor: "#D3D3D3",
      borderRadius: 10,
      marginHorizontal: 10,
      marginBottom: 10,
      flexDirection: "row",
      alignItems: "center",
      justifyContent: "space-between",
      paddingRight: 20,
    },
    chevronIcon: {
      marginLeft: 10,
    },
  });
  export default styles;
