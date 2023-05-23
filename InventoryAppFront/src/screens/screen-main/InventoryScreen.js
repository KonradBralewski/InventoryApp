import * as React from 'react';
import { SafeAreaView, FlatList, StyleSheet, Text, View, TouchableOpacity } from "react-native";
import Ionicons from 'react-native-vector-icons/Ionicons';
import { useNavigation } from '@react-navigation/native';

//styles
import styles from '../../styles/_styles-InventoryScreen';

const buildings = [
  {
    id: '1',
    name: 'Budynek 1',
  },
  {
    id: '2',
    name: 'Budynek 2',
  },
  {
    id: '3',
    name: 'Budynek 3',
  },
  {
    id: '4',
    name: 'Budynek 4',
  },
  {
    id: '5',
    name: 'Budynek 5',
  },
  {
    id: '6',
    name: 'Budynek 6',
  },
  {
    id: '7',
    name: 'Budynek 7',
  },
  {
    id: '8',
    name: 'Budynek 8',
  },
];

export default function InventoryScreen() {
  const navigation = useNavigation();
  const myListEmpty = () => {
    return (
      <View style={{ alignItems: "center" }}>
        <Text style={styles.item}>Brak budynków</Text>
      </View>
    );
  };

const renderBuildingItem = ({ item }) => {
  const handleItemPress = () => {
    navigation.navigate('RaportScreen', { buildingId: item.id });
  };

  return (
    <TouchableOpacity onPress={handleItemPress}>
      <View style={styles.buildingContainer}>
        <Text style={styles.item}>{item.name}</Text>
        <Ionicons name="chevron-forward-outline" size={24} color="black" style={styles.chevronIcon} />
      </View>
    </TouchableOpacity>
  );
};

  return (
    <SafeAreaView style={styles.container}>
      <FlatList
        data={buildings}
        renderItem={renderBuildingItem}
        keyExtractor={(item) => item.id}
        ListEmptyComponent={myListEmpty}
        ListHeaderComponent={() => (
          <View style={styles.headerContainer}>
            <Text style={styles.headerText}>Wybierz budynek</Text>
          </View>
        )}
      />
    </SafeAreaView>
  );
}
