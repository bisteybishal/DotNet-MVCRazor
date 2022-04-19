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
                    { "data": "productName" },
                    { "data": 'price' },
                    { "data": "description" },
                    { "data": "category.name" },
                    {
                        "data": "id",
                        "render": function (data) {
                            return `

                    <a href="/Admin/Product/CreateUpdate?id=${data}"><i class="bi bi-pencil-square"></i></a>
                     <a onClick=RemoveProduct("/Admin/Product/Delete/${data}")><i class="bi bi-trash"></i></a>
                
                        `
                        }


                    }

                ]
        });
});

//this function is used show the popup message of deleta and notification.
function RemoveProduct(url)
{
    Swal.fire({
        title: 'Are you sure?',
        text: "You won't be able to revert this!",
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Yes, delete it!'
    }).then((result) => {
        if (result.isConfirmed)
        {
            $.ajax({
                url: url,
                type: "Delete",
                success: function (data) {
                    if (data.success) {
                        Dtable.ajax.reload();
                        toastr.success(data.message);
                    }
                    else {
                        toastr.error(data.error);
                    }
                }


            })
        }
    })
}




//const { ajax, type } = require("jquery");
//var Dtable;
//$(document).ready(function () {
//    Dtable = $('#myTable').DataTable
//    ({

//        "ajax":
//             {
//                 "url": "/Admin/Product/AllProducts"
//             },
//        "columns":
//        [
//            { "data": "productName" },
//                { "data": "price" },
//                { "data": 'description' },
//            { "data": "category.name" },
//               {
//                    "data": "id",
//                    "render": function (data) {
//                        return `

//                    <a href="/Admin/Product/CreateUpdate?id=${data}"><i class="bi bi-pencil-square"></i></a>
//                    <a href="/Admin/Product/Delete?id=${data}")><i class="bi bi-trash"></i></a>

//                        `
//                    }


//                }

//        ]
//    });
