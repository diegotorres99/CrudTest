uri = "/Category/";

let isEditing = false;
let currentItem = null;
const categoryModel = {
    id: 0,
    name: "",
    isActive: 0,
};

$(document).ready(() => {
    getCategories();
});
function getCategories() {
    fetch(uri + 'getCategories').then((response) => {
        return response.ok ? response.json() : Promise.reject(response);
    }).then((dataJson) => {
        $("#tbList tbody").empty();
        dataJson.forEach((item) => {
            $("#tbList tbody").append($("<tr>").append(
                $("<td>").text(item.id),
                $("<td>").text(item.name),
                $("<td>").text(item.isActive ? "Si" : "No"),
                $("<td>").append(
                    $("<button>").addClass("btn btn-primary btn-sm me-2 btn-edit")
                        .data("model", item).text("Edit"),
                    $("<button>").addClass("btn btn-danger btn-sm btn-delete")
                        .data("txtId", item.id).text("Delete")
                )
            ));
        });
    }).catch((error) => {
        console.error('Error fetching data:', error);
    });
}
function ShowModal(model) {

    $("#txtIdCategoria").val(model.id);
    $("#txtId").val(model.id);
    $("#txtIsUpdate").val(model.isUpdate);
    $("#txtNombreCategoria").val(model.name);
    $("#cbxIsActiva").val(model.isActive ? "true" : "false");

    $('.modal').modal("show");
}
function Clean() {
    $("#txtIdCategoria").val('');
    $("#txtId").val('');
    $("#txtNombreCategoria").val('');
    $("#cbxIsActiva").val('');
}
function UpdateItem() {
    if (!validateFields()) {
        return; 
    }
    let newModel = categoryModel;
    newModel["id"] = $("#txtIdCategoria").val();
    newModel["name"] = $("#txtNombreCategoria").val();
    newModel["isActive"] = $("#cbxIsActive").val() === "true";
 
    fetch("/Category/Update", {
        method: "PUT",
        headers: {
            'Content-Type': 'application/json;charset=utf-8'
        },
        body: JSON.stringify(newModel)
    }).then((response) => {
        return response.ok ? response.json() : Promise.reject(response)
    }).then((dataJson) => {
        if (dataJson) {
            alert('Categoria actualizada correctamente!');
            $('.modal').modal('hide');
            Clean();
            getCategories();
        }
    })
}
function CreateItem() {

    if (!validateFields()) {
        return;
    }

    let newModel = categoryModel;
    newModel["id"] = $("#txtIdCategoria").val();
    newModel["name"] = $("#txtNombreCategoria").val();
    newModel["isActive"] = $("#cbxIsActive").val() === "true";

    fetch("/Category/Create", {
        method: "POST",
        headers: {
            'Content-type': 'application/json;charset=utf-8'
        },
        body: JSON.stringify(newModel)
    })
        .then((response) => {
            if (!response.ok) {
                return Promise.reject({
                    status: response.status,
                    statusText: response.statusText,
                    url: response.url
                });
            }
            return response.json();
        })
        .then((dataJson) => {
            if (dataJson.valor) {
                alert('Categoria agregada correctamente!');
                $('.modal').modal('hide');
                Clean();
                getCategories();
            }
        })
        .catch((error) => {

            alert(`Por favor ingresa un ID mayor al anterior, Ej: ${parseInt(newModel["id"]) + 1}`);
        });
}
function GetNextId() {
    const txtIdCategory = $("#txtIdCategoria");
    txtIdCategory.prop("disabled", true);

    fetch(uri + 'GetNextId')
        .then(response => {
            txtIdCategory.prop("disabled", false);
            if (!response.ok) {
                throw new Error(`HTTP error! status: ${response.status}`);
            }
            return response.json();
        })
        .then(data => {
            txtIdCategory.val(data.nextId);
        })
        .catch(error => {
            txtIdCategory.prop("disabled", false);
        });
}
function validateFields() {
    if ($("#txtIdCategoria").val() == "" || $("#txtIdCategoria").val() <= 0) {
        alert('ID is required!');
        $("#txtIdCategoria").focus();
        return false;
    }
    if ($("#txtNombreCategoria").val() == "") {
        alert('Category name is required!');
        $("#txtNombreCategoria").focus();
        return false;
    }
    return true;
}

$("#tbList tbody").on("click", ".btn-edit", function () {
    let item = $(this).data("model");
    isEditing = true;
    currentItem = item; 

    ShowModal(item);
})

$("#tbList tbody").on("click", ".btn-delete", function () {
    let id = $(this).data("txtId");
    let result = window.confirm("Desea eliminar la categoria?");
    if (result == true) {
        fetch("/Category/Delete?id=" + id, {
            method: 'DELETE'
        }).then((response) => {
            return response.ok ? response.json() : Promise.reject(response);
        }).then((dataJson) => {
            if (dataJson.message) {
                alert('Categoria eliminada correctamente!');
                Clean();
                getCategories();
            }
        }).catch((error) => {
            alert("Error al eliminar.");
        });
    }
});

$("#btnNew").click(() => {
    ShowModal({
        id: 0,
        idCategoria: 0,
        NombreCategoria: "",
        isActiva: 0,
    });

    GetNextId();
})

$("#btnUpdate").click(() => {
    ShowModal({
        id: 0,
        idCategoria: 0,
        NombreCategoria: "",
        isActive: 0,
        isUpdate: true
    });
})

$("#btnSave").on("click", function () {
    if (isEditing) {
        UpdateItem(currentItem);
    } else {

        CreateItem();
    }
});
