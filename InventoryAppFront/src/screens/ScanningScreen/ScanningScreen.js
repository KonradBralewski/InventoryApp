import React, { useState, useEffect } from 'react';
import { Text, View, BackHandler } from 'react-native';
import { Camera, CameraType } from 'expo-camera';
import CameraOptions from './Components/CameraOptions.js';
import { changeGivenProperty } from '../../utils/stateUtils.js';
import { useIsFocused, useNavigation } from '@react-navigation/native';
import MaskedView from '@react-native-masked-view/masked-view';
import { useComponentsUtils } from '../../contexts/ComponentsUtilsProvider.js';

// styles
import styles from "./_styles-ScanningScreen.js"


export default function ScanningScreen() {
  const isFocused = useIsFocused()
  const [hasInputBoxFocus, setHasInputBoxFocus] = useState(false)
  const [utils, setUtils] = useComponentsUtils()

  useEffect(()=>{ // Custom hardware back button
    const backHandler = BackHandler.addEventListener('hardwareBackPress', () => {
      if(!utils) return
      if(!utils["ItemsScreen"].reseter) return

      utils["ItemsScreen"].reseter()
    })
    return () => backHandler.remove()
  }, [])

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
            </View>
          </View>
          }>
          <Camera
            onBarCodeScanned={cameraObject.scanned ? undefined : handleBarCodeScanned}
            style={styles.cameraContainer} type={cameraObject.type} ratio="16:9"/>
        </MaskedView>
      }
      <View style={{...styles.scanningBorderView, top : hasInputBoxFocus ? 80 : 208}}></View>
      <CameraOptions cameraObject={cameraObject} reseter={resetScanning} setHasInputBoxFocus={setHasInputBoxFocus}/>
    </View>
  );
}
