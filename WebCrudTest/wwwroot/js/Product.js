uri = "/Product/getProducts";

const categoryModel = {
    id: 0,
    name: "",
    isActive: 0,
};

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
                $("<td>").text(item.precioProducto),
                $("<td>").text(item.nombreCategoria),
            ));
        });
    }).catch((error) => {
        console.error('Error fetching data:', error);
    });
}

function ShowModal(model) {

    $("#txtIdCategoria").val(model.idCategoria);
    $("#txtId").val(model.idCategoria);
    $("#txtNombreCategoria").val(model.nombreCategoria);
    $("#cbxIsActiva").val(model.isActiva ? "true" : "false");

    $('.modal').modal("show");
}

function Clean() {
    $("#txtIdCategoria").val('');
    $("#txtId").val('');
    $("#txtNombreCategoria").val('');
    $("#cbxIsActiva").val('');
}

$("#btnNew").click(() => {
    ShowModal({
        id: 0,
        idCategoria: 0,
        NombreCategoria: "",
        isActiva: 0,
    });
})

$("#btnSave").click(() => {

    if ($("#txtIdCategoria").val() == "") {
        return alert('ID Categoría es requerido!');
    }

    if ($("#txtNombreCategoria").val() == "") {
        return alert('Nombre Categoríá es requerido!');
    }


    let newModel = categoryModel;
    newModel["id"] = $("#txtIdCategoria").val();
    newModel["name"] = $("#txtNombreCategoria").val();
    newModel["isActive"] = $("#cbxIsActive").val() === "true";

    if ($("#txtIdCategoria").val() == "0") {

        fetch("/Product/Create", {
            method: "POST",
            headers: {
                'Content-type': 'application/json;charset=utf-8'
            },
            body: JSON.stringify(newModel)
        }).then((response) => {
            return response.ok ? response.json() : Promise.reject(response)
        }).then((dataJson) => {
            if (dataJson.valor) {
                alert('Item added successfully!');
                $('.modal').modal('hide');
                Clean();
                getItems();
            }
        })
    } else {
        fetch("/Product/Update", {
            method: "POST",
            headers: {
                'Content-Type': 'application/json;charset=utf-8'
            },
            body: JSON.stringify(newModel)
        }).then((response) => {
            return response.ok ? response.json() : Promise.reject(response)
        }).then((dataJson) => {
            if (dataJson) {
                alert('Item updated successfully!');
                $('.modal').modal('hide');
                Clean();
                getItems();
            }
        })
    }
})

$("#tbList tbody").on("click", ".btn-edit", function () {
    let item = $(this).data("model");
    ShowModal(item);
})

$("#tbList tbody").on("click", ".btn-delete", function () {
    let id = $(this).data("txtId");
    let result = window.confirm("Do you want delete this item?");
    if (result == true) {
        fetch("/Product/Delete?id=" + id, {
            method: 'DELETE'
        }).then((response) => {
            return response.ok ? response.json() : Promise.reject(response)
        }).then((dataJson) => {
            if (dataJson.valor) {
                Clean();
                getProducts();
            }
        })
    }
})