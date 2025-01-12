uri = "/Product/getProducts";

$(document).ready(() => {
    getProducts();
});
function getProducts() {
    fetch(uri).then((response) => {
        return response.ok ? response.json() : Promise.reject(response);
    }).then((dataJson) => {
        $("#tbList tbody").empty();
        dataJson.forEach((item) => {
            $("#tbList tbody").append($("<tr>").append(
                $("<td>").text(item.idProducto),
                $("<td>").text(item.nombreProducto),
                $("<td>").text(parseFloat(item.precioProducto).toFixed(2)),
                $("<td>").text(item.nombreCategoria),
            ));
        });
    }).catch((error) => {
        console.error('Error fetching data:', error);
    });
}

