/* VANILLA JAVASCRIPTS KOD HÄR 

darkmode, lightmode, on toggle, */


const hamburger = document.querySelector(".dropdown")
const navMenu = document.querySelector(".hamburger-menu")

hamburger.addEventListener("click", () => {
    hamburger.classList.toggle("active");
    navMenu.classList.toggle("active");
})