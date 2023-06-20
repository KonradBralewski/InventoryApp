import { SafeAreaView, FlatList, StyleSheet, Text, View, TouchableOpacity, Linking } from "react-native";
import Ionicons from 'react-native-vector-icons/Ionicons';
import { useFocusEffect, useNavigation } from '@react-navigation/native';
import { useAxiosRequest } from '../../hooks/UseAxiosRequest';
import { MemoizedLoadingScreen } from '../LoadingScreen/LoadingScreen';
import { useState, useEffect, useCallback } from "react";
import { downloadPdf } from "../../utils/fileUtils";
import ErrorScreen from "../ErrorScreen/ErrorScreen";

//styles
import styles from '../../styles/_styles-RaportScreen';


export default function RaportScreen({navigation, route}) {
  const nav = useNavigation();

  const[data, error, isLoading, resetHook] = useAxiosRequest("api/Reports", "get")

  const [shouldDownloadFile, setShouldDownloadFile] = useState(false)
  const [fileId, setFileId] = useState(undefined)

  const [downloadedLatest, setDownloadedLatest] = useState(false)
  const {shouldDisplayLatest} = route.params
  
  useFocusEffect(useCallback(()=>{
    return () => {
      resetHook()
      navigation.setParams({
        shouldDisplayLatest: false,
     });
     
      setDownloadedLatest(false)
      setShouldDownloadFile(false)
      setFileId(undefined)
    }
  }, []))
  // setup lifecycle listener

  if(isLoading || (!data && !error)){
    return <MemoizedLoadingScreen/>
  }

  if(!data && error){
    return <ErrorScreen errorTitle ="Błąd Aplikacji"
     errorDescription="InventoryApp nie był w stanie otrzymać listy raportów." reseter={()=>{resetHook()}}/>
  }

  if((shouldDownloadFile && fileId != undefined) || (shouldDisplayLatest && !downloadedLatest)){
    const url = shouldDisplayLatest ? 'api/Reports/file/latest' : `api/Reports/file/${fileId}`
    downloadPdf(url, 'report_' + fileId)
    .then(result => console.log('File_result = ',result))
    .catch(error => console.log('File_error = ', error))
    .finally(()=>{
      setShouldDownloadFile(false)
      if(shouldDisplayLatest && !downloadedLatest){
        setDownloadedLatest(true)
        navigation.setParams({
          shouldDisplayLatest: false
       })
       resetHook()
      }
    })
  }

  const reports = data.map((report, index) => {
    var reportTime = report.reportDate.replace("T", " ")
    reportTime = reportTime.slice(0, reportTime.lastIndexOf(":"))
    return {
    id : report.id,
    name: 'Raport nr. ' + (index + 1),
    data : reportTime,
    buildingName : report.buildingName,
    roomDescription : report.roomDescription
  }
})

  const myListEmpty = () => {
    return (
      <View style={{ alignItems: "center" }}>
        <Text style={styles.item}>Brak sal</Text>
      </View>
    );
  };
  
  const renderReportItem = ({ item }) => {
    const handlePrintIconPress = (id) => {
      setFileId(id)
      setShouldDownloadFile(true)
    };
  
    return (
      <View style={styles.reportContainer}>
        <View style={styles.leftContainer}>
          <View style={styles.reportInfoContainer}>
            <Ionicons name="newspaper-outline" size={30} color="black" style={styles.icon} />
            <Text style={styles.reportNumber}>{item.name}</Text>
          </View>
          <View style={styles.buildingRoomInfoContainer}>
            <Text style={styles.reportBuilding}>{item.buildingName}</Text>
            <Text style={styles.reportRoom}>{item.roomDescription}</Text>
          </View>
          <Text style={styles.reportDate}>{item.data}</Text>
        </View>
        <TouchableOpacity onPress={()=>handlePrintIconPress(item.id)}>
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
        data={reports}
        renderItem={renderReportItem}
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