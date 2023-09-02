//VALIDATE ACTIVE SESSION, VALID JWT TOKEN AND MORE SECURITY VALIDATIONS
const sessionValidate = () => {

    const jwt = sessionStorage.getItem('supplierTknSession');
    const flag = sessionStorage.getItem('key');
    const data = localStorage.getItem('supplierData');

    if (jwt === '' || jwt === null && flag === '' || flag === null && data === '' || data === null) {
        sessionStorage.removeItem('supplierTknSession');
        sessionStorage.removeItem('key');
        localStorage.removeItem('supplierData');
    }
    else {
        window.location.href = '../../adapters/view/u/products';
    }
}
sessionValidate();