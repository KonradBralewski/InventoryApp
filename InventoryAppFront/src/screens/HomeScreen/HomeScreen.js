import {View, Text} from 'react-native';
import { useUserContext } from '../../contexts/UserProvider';

export default function HomeScreen({navigation}){
    const [user] = useUserContext()
    return(
        <View style={{flex:1,alignItems:'center',justifyContent:'center'}}>
            <Text onPress={()=>alert('This is the Home screen')}
                    style={{fontSize:26, fontWeight:'bold'}}> Witaj, {user.email}
            </Text>
        </View>
    )
}