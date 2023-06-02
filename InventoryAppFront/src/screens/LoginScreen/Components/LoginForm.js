import { Formik} from 'formik';
import { View, TextInput, Text} from 'react-native';
import Button from '../../../components/Button';
import * as Yup from 'yup';
import { useAxiosRequest } from '../../../hooks/UseAxiosRequest';
import { MemoizedLoadingScreen } from '../../LoadingScreen/LoadingScreen';
import { useUserContext } from '../../../contexts/UserProvider';
import { useEffect, useState } from 'react';
import ErrorMessage from './ErrorMessage';

// styles
import styles from './_styles-LoginForm';


const userObjectRules = {
    email : Yup.string().email(),
    password : Yup.string().min(8, "Hasło musi zawierać minimum 8 znaków.")
}

const initialValues = {
    email : "test_api@sggw.edu.pl",
    password : "test_AsPI1"
}

export default function LoginForm(){

    const [user, updateUser] = useUserContext()
    
    const [credentials, setCredentials] = useState({})
    const [shouldLogin, setShouldLogin] = useState(false)
    const restfulRun = {
        state : shouldLogin,
        modifierFunc : setShouldLogin
    }

    const[response, error, isLoading] = useAxiosRequest("auth/login", "post", restfulRun, credentials)
    
    useEffect(()=>{
        if(response != undefined && !isLoading && !error){
            updateUser(response)
        }
    }, [response, error, isLoading])

    if(isLoading){
        return <MemoizedLoadingScreen/>
    }
    
    const submitLoginForm = (values, {setSubmitting}, passedValidation) => {
        if(!passedValidation){
            console.log('didnt pass')
            return
        }

        setSubmitting(true)

        setCredentials(values)

        setShouldLogin(true)

        setSubmitting(false)
    }


    return (
        <Formik initialValues={initialValues} validationSchema={Yup.object(userObjectRules)}
        onSubmit={submitLoginForm}>
            {({isSubmitting, handleChange, values, setSubmitting, errors, isValid, isInitialValid}) => { 
                const passedValidation = (isValid && values.email.length > 0)
                return (
                <View style={styles.container}>
                    <ErrorMessage error={error}/>
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
                    styles={styles.loginButton} /* disabled={!passedValidation} */
                     onPress={()=>submitLoginForm(values, {setSubmitting}, passedValidation)}/>
                </View>
            )}}
        </Formik>
    )
  
}