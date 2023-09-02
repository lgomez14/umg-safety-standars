//VALIDATE ACTIVE SESSION, VALID JWT TOKEN AND MORE SECURITY VALIDATIONS
const sessionValidate = () => {

    const jwt = sessionStorage.getItem('supplierTknSession');
    const flag = sessionStorage.getItem('key');
    const data = localStorage.getItem('supplierData');

    if (jwt === '' || jwt === null && flag === '' || flag === null && data === '' || data === null) {
        sessionStorage.removeItem('supplierTknSession');
        sessionStorage.removeItem('key');
        localStorage.removeItem('supplierData');
        window.location.href = '../../view/unauthorized';
    }
    else {
        console.log('sesion activa');
    }
}
sessionValidate();


//EXECUTE VALIDATION SESSION VALIDATE LOOP 1 MINUTE
setInterval(sessionValidate, 10000);



//CLOSE SESSION
const closeSupplierSession = () => {
    sessionStorage.removeItem('supplierTknSession');
    sessionStorage.removeItem('key');
    localStorage.removeItem('supplierData');
    window.location.href = '../../../../index';
}