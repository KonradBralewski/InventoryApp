import React, { useState, useEffect } from 'react';
import { Text, View, Button } from 'react-native';
import { BarCodeScanner } from 'expo-barcode-scanner';

// styles
import styles from "./_styles-ScanningScreen.js"
import CameraOptions from './Components/CameraOptions.js';
import { Dimensions } from 'react-native';


export default function ScanningScreen() {
  const [hasPermission, setHasPermission] = useState(null);
  const [scanned, setScanned] = useState(false);
  const [readCode, setReadCode] = useState(null);

  useEffect(() => {
    const getBarCodeScannerPermissions = async () => {
      const { status } = await BarCodeScanner.requestPermissionsAsync();
      setHasPermission(status === 'granted');
    };

    getBarCodeScannerPermissions();
  }, []);

  const handleBarCodeScanned = ({ type, data }) => {
    setScanned(true);
    setReadCode(data);
  };

  if (hasPermission === null) {
    return <Text>Requesting for camera permission</Text>;
  }
  if (hasPermission === false) {
    return <Text>No access to camera</Text>;
  }

  return (
    <View style={styles.container}>
      <BarCodeScanner
        onBarCodeScanned={scanned ? undefined : handleBarCodeScanned}
        style={[styles.cameraContainer,{
          width : Dimensions.get('screen').width,
          height : Dimensions.get('screen').height
        }]}
      />
      <View style={{position : 'absolute', bottom:0, right:0, left:0, justifyContent:'center'}}>
        <CameraOptions code={readCode} scanned={scanned}/>
      </View>
    </View>
  );
}
