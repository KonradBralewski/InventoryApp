import { Pressable, Text } from "react-native"
import Ionicons from 'react-native-vector-icons/Ionicons';

export default function Button({styles, title, onPress, iconName, disabled, linearGradient}){
    return(
        <Pressable disabled={disabled} onPress={onPress} style={({pressed})=>[
            styles.pressableContainer,
            pressed ? {
                opacity : 0.5
            } : {},
            disabled ? {
                backgroundColor : 'grey'
            } : {}
        ]}>
            {iconName && <Ionicons name={iconName} style={styles.insideButtonIcon}/>}
            {linearGradient != undefined && linearGradient}
            <Text style={styles.insideButtonText}>{title}</Text>
        </Pressable>
    )
}

Button.defaultProps = {
    styles : {},
    title : "DefaultTitle",
    onPress : ()=>{},
    iconName : undefined,
    linearGradient : undefined,
    disabled : false
}
