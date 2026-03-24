/* VANILLA JAVASCRIPTS KOD HÄR 

darkmode, lightmode, on toggle, */

/* 
    Tog lite hjälp av W3 schools för javascript delen
    samt kollade jag på lite olika videos för att förstå,
    där jag tyckte denna videos:
    https://youtu.be/flItyHiDm7E?si=HzozqQfUniHHfXsY
    var bra då jag kunde hänga med i det han gjorde
    och förstod. 
*/

const hamburger = document.querySelector(".dropdown")
const navMenu = document.querySelector(".hamburger-menu")

hamburger.addEventListener("click", () => {
    hamburger.classList.toggle("active");
    navMenu.classList.toggle("active");
})






// ACCORDION icon, pilen pekar upp och ner
    const accordionArrow = document.querySelectorAll(".accordion-question");

    accordionArrow.forEach(question => {
        question.addEventListener("click", () => {
            const item = question.parentElement;
            item.classList.toggle("active");
    });
});