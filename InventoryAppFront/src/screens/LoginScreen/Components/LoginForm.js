import { Formik} from 'formik';
import { View, TextInput, Text} from 'react-native';
import Button from '../../../components/Button';
import * as Yup from 'yup';
import { useAxiosRequest } from '../../../hooks/UseAxiosRequest';
import { MemoizedLoadingScreen } from '../../LoadingScreen/LoadingScreen';
import { useUserContext } from '../../../contexts/UserProvider';
import { useEffect, useState } from 'react';
import ErrorMessage from './ErrorMessage';
import ErrorScreen from '../../ErrorScreen/ErrorScreen';

// styles
import styles from './_styles-LoginForm';
import { getErrorCode } from '../../../utils/errorUtils';
import errors from '../../../constants/errors';


const userObjectRules = {
    email : Yup.string().email(),
    password : Yup.string().min(8, "Hasło musi zawierać minimum 8 znaków.")
}

const initialValues = {
    email : "Test_user@sggw.edu.pl",
    password : "Sggw@sggw_PASSWORD1"
}

export default function LoginForm(){

    const [user, setUser] = useUserContext()
    
    const [credentials, setCredentials] = useState({})
    const [shouldLogin, setShouldLogin] = useState(false)
    const restfulRun = {
        state : shouldLogin,
        modifierFunc : setShouldLogin
    }

    const[response, error, isLoading, resetHook] = useAxiosRequest("api/Authentication/login", "post", restfulRun, credentials)
    
    useEffect(()=>{
        if(response != undefined && !isLoading && !error){
            setUser((prevUser => ({...prevUser, 
                token : 'Bearer ' + response.token,
                email : response.email,
                isSigned : true})))
        }
    }, [response, error, isLoading])

    if(isLoading){
        return <MemoizedLoadingScreen/>
    }
    
    if(!response && error){
        var errorCode = getErrorCode(error)

        if(errorCode == errors.INTERNAL_SERVER_ERROR
            || errorCode == errors.BAD_REQUEST
            || errorCode == errors.NOT_FOUND){
                return <ErrorScreen errorTitle ="Błąd Aplikacji"
                errorDescription="InventoryApp nie było w stanie wykonać próby logowania." reseter={()=>{resetHook()}}/>
            }
      }
    
    const submitLoginForm = (values, {setSubmitting}, passedValidation) => {
        if(!passedValidation){
            return
        }

        setSubmitting(true)

        setCredentials(values)

        setShouldLogin(true)

        setSubmitting(false)
    }


    return (
        <Formik enableReinitialize={true} initialValues={initialValues} validationSchema={Yup.object(userObjectRules)}
        onSubmit={submitLoginForm}>
            {({isSubmitting, handleChange, values, setSubmitting, errors, isValid, isInitialValid}) => { 
                const passedValidation = (isValid && values.email.length > 0)
                return (
                <View style={styles.container}>
                    <ErrorMessage error={error} styles={styles}/>
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
                        onChangeText={handleChange('password')}
                        style={styles.passwordInput}
                        value={values.password}
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