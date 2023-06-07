import {View, Text} from 'react-native';
import { FontAwesome5 } from '@expo/vector-icons';
import { Animated, Easing} from 'react-native';
import { memo } from 'react';

function SuccessPopup({setVisibility, style}){
    const scaleValue = new Animated.Value(0)

    Animated.timing(scaleValue,{
      toValue : 1.0,
      duration : 300,
      easing : Easing.linear,
      useNativeDriver : true
  }).start(()=> {setTimeout(()=>setVisibility(false), 250)})

    return(
        <View style={style.container}>
            <Animated.View style={{transform : [{scale : scaleValue}]}}>
                <FontAwesome5 name="check" size={150} color="green" />
            </Animated.View>
        </View>
    )
}

export const MemoizedSuccessPopup = memo(SuccessPopup)