// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
function deleteClaim(userId, claimType, claimValue) {
    console.log(userId)
    fetch(`/claims?userId=${userId}&type=${claimType}&value=${claimValue}`, {
        method: "DELETE"
    })
        .then(res => {
            if (res.ok) {
                alert("The claim was deleted")
            }
            else {
                console.error("Claim was not deleted");
            }
        })
        .then(c => location.reload())

}