export const getErrorCode = (error) => {
    if(!error) return 500
    if(!error.response) return 500

    return error.response.status
}

export const getErrorMessage = (error) => {
    if(!error) return "Server error"
    if(!error.response) return "Server error"

    return error.response.data
}