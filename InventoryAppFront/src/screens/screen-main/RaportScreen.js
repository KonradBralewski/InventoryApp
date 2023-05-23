import * as React from 'react';
import { SafeAreaView, FlatList, StyleSheet, Text, View, TouchableOpacity } from "react-native";
import Ionicons from 'react-native-vector-icons/Ionicons';
import { useNavigation } from '@react-navigation/native';
//styles
import styles from '../../styles/_styles-RaportScreen';

const raport = [
  {
    id: '1',
    name: 'Raport 1',
    data: '12/04/2023',
  },
  {
    id: '2',
    name: 'Raport 2',
    data: '12/04/2023',
  },
  {
    id: '3',
    name: 'Raport 3',
    data: '12/04/2023',
  },
];

export default function RaportScreen() {
  const navigation = useNavigation();
  const myListEmpty = () => {
    return (
      <View style={{ alignItems: "center" }}>
        <Text style={styles.item}>Brak sal</Text>
      </View>
    );
  };
  const renderBuildingItem = ({ item }) => {
    const handlePrintIconPress = () => {
      navigation.navigate('RaportScreen');
    };
  
    return (
      <View style={styles.buildingContainer}>
        <View style={styles.leftContainer}>
          <View style={styles.reportInfoContainer}>
            <Ionicons name="newspaper-outline" size={30} color="black" style={styles.icon} />
            <Text style={styles.reportNumber}>{item.name}</Text>
          </View>
          <Text style={styles.reportDate}>{item.data}</Text>
        </View>
        <TouchableOpacity onPress={handlePrintIconPress}>
          <View style={styles.rightContainer}>
            <Ionicons name="print-outline" size={42} color="black" style={styles.iconPrint} />
          </View>
        </TouchableOpacity>
      </View>
    );
  };
  return (
    <SafeAreaView style={styles.container}>
      <FlatList
        data={raport}
        renderItem={renderBuildingItem}
        keyExtractor={(item) => item.id}
        ListEmptyComponent={myListEmpty}
        ListHeaderComponent={() => (
          <View style={styles.headerContainer}>
            <Text style={styles.headerText}>Ostatnie raporty</Text>
          </View>
        )}
      />
    </SafeAreaView>
  );
}