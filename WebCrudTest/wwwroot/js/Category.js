uri = "/Category/getCategories";

const categoryModel = {
    id: 0,
    name: "",
    isActive: 0,
};

let isEditing = false;  // Flag to track if we're editing an existing item
let currentItem = null;

$(document).ready(() => {
    getCategories();
});
function getCategories() {
    fetch(uri).then((response) => {
        return response.ok ? response.json() : Promise.reject(response);
    }).then((dataJson) => {
        $("#tbList tbody").empty();
        dataJson.forEach((item) => {
            console.log(item);
            $("#tbList tbody").append($("<tr>").append(
                $("<td>").text(item.id),
                $("<td>").text(item.name),
                $("<td>").text(item.isActive),
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

$("#btnNew").click(() => {
    ShowModal({
        id: 0,
        idCategoria: 0,
        NombreCategoria: "",
        isActiva: 0,
    });
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

$("#btnSave2").click(() => {

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
    console.log(newModel);
    if ($("#txtIdCategoria").val() != "0") {

        fetch("/Category/Create", {
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
                getCategories();
            }
        })
    } else {
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
                alert('Item updated successfully!');
                $('.modal').modal('hide');
                Clean();
                getItems();
            }
        })
    }
})

// Example of handling form submission
$("#btnSave").on("click", function () {
    if (isEditing) {
        // This is an edit operation
        console.log("Editing item: ", currentItem);

        // You can now update the item with the current data from the modal
        // For example, send an API request to update the product/category
        UpdateItem(currentItem); // Custom function to handle updating
    } else {
        // This is an add operation
        console.log("Adding new item");

        // Handle new item creation here (e.g., call API to insert new item)
        CreateItem(); // Custom function to handle creating a new item
    }
});

// Example of update function
function UpdateItem(item) {
    let newModel = categoryModel;
    newModel["id"] = $("#txtIdCategoria").val();
    newModel["name"] = $("#txtNombreCategoria").val();
    newModel["isActive"] = $("#cbxIsActive").val() === "true";
    // Call an API or perform your update logic here
    console.log("Updated item:", item);
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
            alert('Item updated successfully!');
            $('.modal').modal('hide');
            Clean();
            getItems();
        }
    })
}

// Example of create function
function CreateItem() {
    let newModel = categoryModel;
    newModel["id"] = $("#txtIdCategoria").val();
    newModel["name"] = $("#txtNombreCategoria").val();
    newModel["isActive"] = $("#cbxIsActive").val() === "true";
    // Call an API or perform your create logic here
    console.log("New item created");
    fetch("/Category/Create", {
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
            getCategories();
        }
    })
}

$("#tbList tbody").on("click", ".btn-edit", function () {
    //let item = $(this).data("model");
    //ShowModal(item);
    console.log('editing')
    let item = $(this).data("model");

    // Set the flag to true for editing
    isEditing = true;
    currentItem = item; // Store the current item to edit

    // Show the modal and populate with the item data
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