import React, { useState, useEffect } from 'react';
import { Text, View, Button } from 'react-native';
import { Camera, CameraType } from 'expo-camera';
import CameraOptions from './Components/CameraOptions.js';
import { changeGivenProperty } from '../../utils/stateUtils.js';

// styles
import styles from "./_styles-ScanningScreen.js"


export default function ScanningScreen() {

  const [cameraObject, setCameraObject] = useState({
    type: CameraType.back,
    hasPermission: false,
    scanned: false,
    readCode: null
  });

  useEffect(() => {
    const getBarCodeScannerPermissions = async () => {
      const { status } = await Camera.requestCameraPermissionsAsync();
      changeGivenProperty(setCameraObject, 'hasPermission', status === 'granted')
    };

    getBarCodeScannerPermissions();
  }, []);

  const handleBarCodeScanned = ({ type, data }) => {
    changeGivenProperty(setCameraObject, 'scanned', true)
    changeGivenProperty(setCameraObject, 'readCode', data)
  };

  if (cameraObject.hasPermission === null) {
    return <Text>Requesting for camera permission</Text>;
  }
  if (cameraObject.hasPermission === false) {
    return <Text>No access to camera</Text>;
  }

  return (
    <View style={styles.container}>
      <Camera
        onBarCodeScanned={cameraObject.scanned ? undefined : handleBarCodeScanned}
        style={styles.cameraContainer} type={CameraType.front} ratio="16:9"
      />
      <CameraOptions cameraObject={cameraObject}/>
    </View>
  );
}
