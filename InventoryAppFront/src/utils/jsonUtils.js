export const returnObjectWithExactPropertyValue = (object, propertyName, value) => {
    let foundObject;
    Object.keys(object).forEach(key => {
        if(object[key][propertyName] === value){
            foundObject = object[key];
        }
    });
    return foundObject;
}