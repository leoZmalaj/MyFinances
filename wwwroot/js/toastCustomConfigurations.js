function getGenericConfigurations() {
    var myToast = $.toast({
        showHideTransition: 'fade',
        position: 'bottom-right',
        hideAfter: 5000
    });
    return myToast;
}

function returnError(message) {
    var myToast = getGenericConfigurations(); 
    myToast.update({
        text: message,
        heading: 'Error',
        icon: 'error'
    });
    return myToast;
}

function returnSuccess(message) {
    var myToast = getGenericConfigurations(); 
    myToast.update({
        text: message,
        heading: 'Success',
        icon: 'success'
    });
    return myToast;
}

function returnWarning(message) {
    var myToast = getGenericConfigurations();
    myToast.update({
        text: message,
        heading: 'Warning',
        icon: 'warning'
    });
    return myToast;
}