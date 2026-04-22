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


//MENY FÖR TRAINING
const training = document.querySelector(".dropdown-training-container")
const trainingMenu = document.querySelector(".training-menu")

training.addEventListener("click", () => {
    training.classList.toggle("active");
    trainingMenu.classList.toggle("active");
})


// ACCORDION icon, pilen pekar upp och ner
    const accordionArrow = document.querySelectorAll(".accordion-question");

    accordionArrow.forEach(question => {
        question.addEventListener("click", () => {
            const item = question.parentElement;
            item.classList.toggle("active");
    });
});








        /*  ***FILUPPLADDNING PÅ MYACCOUNT SIDAN***
            Har fått lite hjälp med denna delen av CHATGPT. 
            Utan nedan javascript kod, skulle det inte framgå på hemsidan om användarens valda bild
            har sparats i systemet. Detta då placeholder-texten "Upload Profile Image" inte förändras.
            Med nedan Javascript kod har jag gjort så att placeholder texten faktiskt förändras när
            anvöndaren lägger in en bild.

            (Jag försökte se om detta gick att lösa med enbart html och css, men designen
            blev inte snygg, det blev för mycket kod och överlag inte en bra lösning.)
    
            Jag använder därför Javascript nedan, för att det ska bli tydligt för användaren
            att bilden HAR lagts till, genom att namnet på bilden som användaren väljer att lägga till
            skrivs ut i placeholdern. */


// ***FÖRKLARING AV KOD NEDAN***
// getElementById('real-file-input');   = Javascript söker upp IDt: real-file-input i html med hjälp av metoden getElementById. (MyAccount.cshtml)
// document                             = söker på hela sidan för att HITTA ID:t: real-file-input
// const fileInput                      = en ny sökväg skapas för "real-file-input" under namnet fileInput

               const fileInput = document.getElementById('real-file-input');
                // Javascript söker upp IDt: img-name-display i html. (MyAccount.cshtml)
                const imgNameDisplay = document.getElementById('img-name-display');







if (fileInput && imgNameDisplay) {              // VIKTIGT! Denna IF-satsen gör att Javascript håller sig till BecomeAMember sidan och inte buggar på andra sidor som den inte ens ska göra något i. 

// fileInput.addEventListener('change', ...)    = Denna delen står och väntar på att användaren har valt en fil och tryckt "OK" i filfönstret.
// Just "addEventListener"                      = är en metod som gör att din webbsida kan "lyssna" efter att något händer och sedan reagera på det.
// *** add: Lägg till.
// *** Event: Händelse (t.ex. ett klick, att man skriver något, eller att man väljer en fil).
// *** Listener: Lyssnar efter ändring.
fileInput.addEventListener('change', function () {
        
// this.files           = är själva LÅDAN.
// this.files.length    = är antal SAKER i lådan.
// Både det som står till vänster och det som står till höger om "&&"" måste vara sant för att vi ska gå vidare in i måsvingarna { }.       
        if (this.files && this.files.length > 0) 
            {
            imgNameDisplay.innerText = this.files[0].name;      /*  OM EN BILD VALTS, VISA BILDENS NAMN I PLACEHOLDERN.*/
            }                                                   /*  Utan denna, kommer placeholderns text inte ändras
                                                                    och det blir omöjligt att se om bilden har lagts in.*/
    });
} // < Avslutar Första IF satsen