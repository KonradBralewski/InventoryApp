import * as React from 'react';
import { SafeAreaView, FlatList, StyleSheet, Text, View, TouchableOpacity } from "react-native";
import Ionicons from 'react-native-vector-icons/Ionicons';
import { useNavigation } from '@react-navigation/native';

//styles
import styles from '../../styles/_styles-InventoryRooms';

const buildings = [
  {
    id: '1',
    name: 'Biurko 1',
  },
  {
    id: '2',
    name: 'Klawiatura 1',
  },
  {
    id: '3',
    name: 'Biurko 2',
  },
];

export default function InventoryItem() {
  const navigation = useNavigation();
  const myListEmpty = () => {
    return (
      <View style={{ alignItems: "center" }}>
        <Text style={styles.item}>Brak sal</Text>
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
            <Text style={styles.headerText}>Przedmioty</Text>
          </View>
        )}
      />
    </SafeAreaView>
  );
}