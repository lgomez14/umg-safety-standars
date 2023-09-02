//API ROUTE FOR SUPPLIER API
const authApiUrl = 'http://localhost:5000/api/auth';


//CREATE SUPPLIER SESION
const loginNewSupplier = async () => {

    // Define an object to store all the input values
    const supplierData = {
        nit: document.getElementById('nit').value,
        psw: document.getElementById('psw').value
    };

    // Validate that none of the required fields are empty
    for (const key in supplierData) {
        if (!supplierData[key]) {
            Swal.fire({
                icon: 'error',
                title: 'Lo sentimos',
                text: 'Llena el NIT y la contraseña para continuar',
                footer: 'Si el problema persiste, informa al administrador',
                confirmButtonText: 'Entendido'
            });
            return; // Stop execution if a required field is empty
        }
    }

    // Create the request body using the supplierData object
    const requestBody = JSON.stringify(supplierData);

    // Set up the request headers
    const myHeaders = new Headers();
    myHeaders.append('Content-Type', 'application/json');

    // Set up the request options
    const requestOptions = {
        method: 'POST',
        headers: myHeaders,
        body: requestBody,
        redirect: 'follow'
    };

    // Now you can use requestOptions to make the API request
    // For example:
    try {
        const response = await fetch(authApiUrl, requestOptions);
        const data = await response.json();

        // Handle the response data here
        if (data.data.isAuthenticated === true) {
            Swal.fire({
                icon: 'success',
                title: '¡Correcto!',
                text: 'Usuario logueado, bienvenido',
                footer: 'Si el problema persiste, informa al administrador',
                confirmButtonText: 'Entendido',
                showDenyButton: false,
                showCancelButton: false,
            }).then((result) => {
                if (result.isConfirmed) {
                    setTokenInSessionStorage(data.data.jwt, data.data.isAuthenticated);
                } else if (result.isDenied) {
                    setTokenInSessionStorage(data.data.jwt, data.data.isAuthenticated);
                }
            });
        }
        else {
            console.clear();
            Swal.fire({
                icon: 'error',
                title: 'Lo sentimos',
                text: 'Usuario y/o contraseña incorrecta',
                footer: 'Si el problema persiste, informa al administrador',
                confirmButtonText: 'Entendido'
            });
            sessionStorage.removeItem('supplierTknSession');
            sessionStorage.removeItem('key');
            localStorage.removeItem('supplierData');
        }
    } catch (error) {
        console.clear();
        Swal.fire({
            icon: 'error',
            title: 'Lo sentimos',
            text: 'Usuario y/o contraseña incorrecta',
            footer: 'Si el problema persiste, informa al administrador',
            confirmButtonText: 'Entendido'
        });
        sessionStorage.removeItem('supplierTknSession');
        sessionStorage.removeItem('key');
        localStorage.removeItem('supplierData');
    }
};


//Set token in session Storage JS
const setTokenInSessionStorage = (jwt, flag) => {
    sessionStorage.setItem('supplierTknSession', jwt);
    sessionStorage.setItem('key', flag);
    localStorage.setItem('supplierData', 1);
    window.location.href = '../../adapters/view/u/products';
}
