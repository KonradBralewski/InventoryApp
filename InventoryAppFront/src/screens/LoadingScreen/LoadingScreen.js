import {View, Text} from 'react-native';
import styles from './_styles-LoadingScreen';
import { AntDesign } from '@expo/vector-icons';
import { Animated, Easing} from 'react-native';
import { memo } from 'react';

function LoadingScreen(){
    const spinValue = new Animated.Value(0)

    Animated.loop(
        Animated.timing(spinValue,{
            toValue : 360,
            duration : 400000,
            easing : Easing.linear,
            useNativeDriver : true
        })
    ).start()

    const rotateData = spinValue.interpolate({
        inputRange : [0,1],
        outputRange : ['0deg', '360deg']
    })

    return(
        <View style={styles.container}>
            <Text style={styles.overSpinnerText}>Loading</Text>
            <Animated.View style={{transform : [{rotate : rotateData}]}}>
                <AntDesign name="loading1" size={90} color="green" />
            </Animated.View>
        </View>
    )
}

export const MemoizedLoadingScreen = memo(LoadingScreen)