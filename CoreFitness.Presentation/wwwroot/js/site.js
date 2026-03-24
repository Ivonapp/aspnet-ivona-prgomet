/* VANILLA JAVASCRIPTS KOD HÄR 

darkmode, lightmode, on toggle, */


const hamburger = document.querySelector(".dropdown")
const navMenu = document.querySelector(".hamburger-links")

hamburger.addEventListener("click", () => {
    hamburger.classList.toggle("active");
    navMenu.classList.toggle("active");
})