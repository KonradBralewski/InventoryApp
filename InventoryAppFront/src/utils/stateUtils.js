export function changeGivenProperty(setterFunction, propertyName, value){
    setterFunction((prevState) => {
        return {...prevState, [propertyName] : value}
    });
}