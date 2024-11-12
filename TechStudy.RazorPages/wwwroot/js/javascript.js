function myFunction(){
    let nav = document.getElementById("navlist");
    nav.classList.toggle("open")
}
window.addEventListener("scroll",function(){
    let header = document.querySelector(".container-fluid");
    header.classList.toggle("sticky",window.scrollY > 0)
})


// password show 

function showPassword(){
    const passwordField = document.getElementById("password");
    const toggler = document.getElementById("togglePassword")
  
    if(passwordField.type === "password"){
        passwordField.type ="text";
        toggler.classList.remove("fa-eye")
        toggler.classList.add("fa-eye-slash")
    }
    else{
        passwordField.type ="password"
        toggler.classList.remove ("fa-eye-slash")
        toggler.classList.add ("fa-eye")
    }
}


// scrollreveal 







