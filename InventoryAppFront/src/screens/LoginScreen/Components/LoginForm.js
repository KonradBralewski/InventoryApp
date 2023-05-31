import { Formik} from 'formik';
import { View, TextInput } from 'react-native';
import Button from '../../../components/Button';
import * as Yup from 'yup';
import { useAxiosRequest } from '../../../hooks/UseAxiosRequest';

// styles
import styles from './_styles-LoginForm';
import { useUserContext } from '../../../contexts/UserProvider';

export default function LoginForm(){

    /* const[data, error, isLoading, resetHook] = useAxiosRequest("auth/login", "post") */
    const [user, setToken] = useUserContext()

    const userObjectRules = {
        email : Yup.string().email(),
        password : Yup.string().min(8, "Hasło musi zawierać minimum 8 znaków.")
    }

    const initialValues = {
        email : "",
        password : ""
    }
    
    const submitLoginForm = (values, {setSubmitting}) => {
        setSubmitting(true)
        setToken("1233333")
        setSubmitting(false)
    }
    console.log(user)
    return (
        <Formik initialValues={initialValues} validationSchema={Yup.object(userObjectRules)} onSubmit={submitLoginForm}>
            {({isSubmitting, handleChange, values, setSubmitting, errors, isValid}) => {
  
                return (
                <View style={styles.container}>
                    <TextInput
                        label="Email"
                        keyboardType="email-address"
                        value={values.email}
                        onChangeText={handleChange('email')}
                        style={styles.emailInput}
                        placeholder='Email'
                        placeholderTextColor="gray">
                    </TextInput>
                    <TextInput
                        label="Password"
                        secureTextEntry={true}
                        onChangeText={()=>handleChange('password')}
                        style={styles.passwordInput}
                        placeholder='Hasło'
                        placeholderTextColor="gray">
                    </TextInput>
                    <Button title="Login" 
                    styles={styles.loginButton} disabled={!isValid} onPress={()=>submitLoginForm(values, {setSubmitting})}/>
                </View>
            )}}
        </Formik>
    )
  
}