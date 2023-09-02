//API ROUTE FOR SUPPLIER API
const supplierApiUrl = 'http://localhost:5000/api/Supplier';


//VALIDATE EMAIL
const validateEmail = () => {

    const email = document.getElementById('email').value;
    const emailRegex = /^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$/;

    if (emailRegex.test(email)) {
        // NOTHING
    } else {
        document.getElementById('email').value = '';
        document.getElementById('email').focus();
        Swal.fire({
            icon: 'error',
            title: 'Lo sentimos',
            text: 'El correo electrónico no es válido',
            footer: 'Si el problema persiste, informa al administrador',
            confirmButtonText: 'Entendido'
        });
    }
}

//VALIDATE DPI
const validateCui = () => {

    const dpi = document.getElementById('dpi').value;
    const dpiRegex = /^\d{13}$/;

    if (dpiRegex.test(dpi)) {
        // NOTHING
    } else {
        document.getElementById('dpi').value = '';
        document.getElementById('dpi').focus();
        Swal.fire({
            icon: 'error',
            title: 'Lo sentimos',
            text: 'El número de CUI / DPI debe contener 13 caracteres',
            footer: 'Si el problema persiste, informa al administrador',
            confirmButtonText: 'Entendido'
        });
    }
}

//VALIDATE CELLPHONE
const validateCellPhone = () => {

    const cellPhone = document.getElementById('phone').value;
    const cellPhoneRegex = /^\d{8}$/;

    if (cellPhoneRegex.test(cellPhone)) {
        // NOTHING
    } else {
        document.getElementById('phone').value = '';
        document.getElementById('phone').focus();
        Swal.fire({
            icon: 'error',
            title: 'Lo sentimos',
            text: 'El número de teléfono debe contener 8 caracteres',
            footer: 'Si el problema persiste, informa al administrador',
            confirmButtonText: 'Entendido'
        });
    }
}

//CREATE A NEW SUPPLIER
const createNewSupplier = async () => {

    // Define an object to store all the input values
    const supplierData = {
        supplierSocialReason: document.getElementById('supplierSocialReason').value,
        supplierBusinessName: document.getElementById('supplierBusinessName').value,
        supplierfirstName: document.getElementById('supplierfirstName').value,
        supplierMiddleName: document.getElementById('supplierMiddleName').value,
        supplierThirdName: document.getElementById('supplierThirdName').value,
        supplierLastName: document.getElementById('supplierLastName').value,
        supplierMarriedName: document.getElementById('supplierMarriedName').value,
        dpi: document.getElementById('dpi').value,
        nit: document.getElementById('nit').value,
        email: document.getElementById('email').value,
        address: document.getElementById('address').value,
        phone: document.getElementById('phone').value,
        iconUrl: document.getElementById('iconUrl').value
    };

    // Validate that none of the required fields are empty
    for (const key in supplierData) {
        if (!supplierData[key]) {
            Swal.fire({
                icon: 'error',
                title: 'Lo sentimos',
                text: 'Llena todo el formulario para crear tu usuario',
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
        const response = await fetch(supplierApiUrl, requestOptions);
        const data = await response.json();

        // Handle the response data here
        if (data.statusCode === 200) {
            Swal.fire({
                icon: 'success',
                title: '¡Correcto!',
                text: 'Usuario registrado con éxito',
                footer: 'Si el problema persiste, informa al administrador',
                confirmButtonText: 'Entendido',
                showDenyButton: false,
                showCancelButton: false,
            }).then((result) => {
                if (result.isConfirmed) {
                    window.location.href = '../../adapters/view/login';
                } else if (result.isDenied) {
                    window.location.href = '../../adapters/view/login';
                }
            });
        }
        else {
            Swal.fire({
                icon: 'error',
                title: 'Lo sentimos',
                text: 'El usuario no pudo ser creado',
                footer: 'Si el problema persiste, informa al administrador',
                confirmButtonText: 'Entendido'
            });
        }
    } catch (error) {
        Swal.fire({
            icon: 'error',
            title: 'Lo sentimos',
            text: 'Surgió un error interno',
            footer: 'Si el problema persiste, informa al administrador',
            confirmButtonText: 'Entendido'
        });
        console.error(error); // Handle any errors
    }
};

