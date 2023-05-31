import React, { useState, useEffect } from 'react';
import { Text, View, Button } from 'react-native';
import { Camera, CameraType } from 'expo-camera';
import CameraOptions from './Components/CameraOptions.js';
import { changeGivenProperty } from '../../utils/stateUtils.js';
import { useIsFocused } from '@react-navigation/native';
import MaskedView from '@react-native-masked-view/masked-view';

// styles
import styles from "./_styles-ScanningScreen.js"


export default function ScanningScreen() {
  const isFocused = useIsFocused()

  const [cameraObject, setCameraObject] = useState({
    type: CameraType.back,
    hasPermission: false,
    scanned: false,
    readCode: undefined
  });

  useEffect(() => {
    const getBarCodeScannerPermissions = async () => {
      const { status } = await Camera.requestCameraPermissionsAsync();
      changeGivenProperty(setCameraObject, 'hasPermission', status === 'granted')
    };

    getBarCodeScannerPermissions();
  }, []);

  const resetScanning = () => {
    changeGivenProperty(setCameraObject, 'scanned', false)
    changeGivenProperty(setCameraObject, 'readCode', undefined)
  }

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
      {isFocused &&
        <MaskedView style={styles.maskContainer}
        maskElement={
          <View style={styles.maskWrapper}>
            <View style={styles.maskView}>
              <View style={styles.maskViewBorder}></View>
            </View>
          </View>
          }>
          <Camera
            onBarCodeScanned={cameraObject.scanned ? undefined : handleBarCodeScanned} autoFocus = {true}
            barCodeScannerSettings={{barcodeSize:{width: 200, height : 200}}}
            style={styles.cameraContainer} type={cameraObject.type} ratio="16:9"/>
        </MaskedView>
      }
      <View style={styles.scanningBorderView}></View>
      <CameraOptions cameraObject={cameraObject} reseter={resetScanning}/>
    </View>
  );
}
