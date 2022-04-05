// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
$('.toast').toast('show');


function loadComent(id, event){
    $('.modal').modal('show');

    event.preventDefault();

    fetch('https://localhost:5001/Comments/Index/' + id)
    .then(Response => Response.text())
    .then(html => {
        document.getElementById("div_comment").innerHTML = html;
    });
}

function deleteMovie(id) {
    Swal.fire({
        title: 'Eleminar Producto!',
        text: 'Desea Eliminar este Articulo?',
        icon: 'warning',
        showDenyButton: true,
        confirmButtonText: 'Eliminar',  
        denyButtonText: 'Cancelar',
    }).then((result) => {  
        /* Read more about isConfirmed, isDenied below */  
        if (result.isConfirmed) {    
            window.location.href = "https://localhost:5001/Movie/Delete/" + id;
        } else {
            swal.fire({
                title: "No se ha eliminado!",
                icon:"success"
            });
          }
    });
}
