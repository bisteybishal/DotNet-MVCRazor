
var Dtable;
$(document).ready(function () {
    Dtable = $('#myTable').DataTable
    ({

        "ajax":
             {
                 "url": "/Admin/Product/AllProducts"
             },
        "columns":
        [
            { "data": "name" },
            { "data": 'description' },
            { "data": "price" },
            { "data": "category.name" },
               {
                    "data": "id",
                    "render": function (data) {
                    //    return `

                    //<a href="/Admin/Product/CreateUpadate?id=${data}"><i class="bi bi-pencil-square"></i></a>
                    // <a href="/Admin/Product/Delete?id?=${data}"><i class="bi bi-trash"></i></a>
                
                    //    `
                    }


                }
           
        ]
    });
});