$(document).ready(function () {
    $('#CargarCupones').click(function () {

        console.log("LLego")

        var id = $("#IdInversion");

        var calcularCuotasRequest = { IdInversion: id }
        
        //CalcularCuotas(calcularCuotasRequest)
        //    .then(() => {
              
        //        return fetchInversionesData(id);
        //    })
        //    .then((data) => {
                
        //        poblarEncabezados(data.Encabezado);
        //        poblarDetalles(data.Detalles);
        //    })
        //    .catch((error) => {
        //        console.error("An error occurred:", error);
        //    });
    });
});

function CalcularCuotas(calcularCuotasRequest) {
    return new Promise((resolve, reject) => {
        $.ajax({
            url: 'Inversiones/api/calcular', 
            type: 'POST',
            contentType: 'application/json', 
            data: JSON.stringify(calcularCuotasRequest), 
            success: function () {
                resolve();
            },
            error: function (xhr, status, error) {
                console.log(error)
                reject(error);
            }
        });
    });
}





function fetchInversionesData(id) {
    return new Promise((resolve, reject) => {
        $.ajax({
            url: `/Inversiones/api/${id}/detalles`, 
            type: 'GET',
            success: function (data) {
                console.log(data)
                resolve(data);
            },
            error: function (xhr, status, error) {
                console.log(error)
                reject(error);
            }
        });
    });
}



function poblarEncabezados(encavezado) {

    var summaryTableBody = $('#tableEncabezado tbody');
    summaryTableBody.empty();

    var row = $('<tr>');
    row.append($('<td>').text(encavezado.InteresTotalc));
    row.append($('<td>').text(encavezado.SaldoCapitalizado));
 
    summaryTableBody.append(row);

    $('#summaryTable').show();
}

function poblarDetalles(detalles) {
    var tableBody = $('#tableEncabezado tbody');
    tableBody.empty(); 

    $.each(detalles,  function (index, detalle ) {
        var row = $('<tr>');
        row.append($('<td>').text(detalle.Año));
        row.append($('<td>').text(detalle.Cupon));
        row.append($('<td>').text(detalle.Saldo));
        row.append($('<td>').text(detalle.SaldoCapitalizado));
    
        tableBody.append(row);
    });

    $('#inversionesTable').show(); // Show the table
}
