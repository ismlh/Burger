const ulElements = document.querySelectorAll(".filters_menu li");


for (var i = 0; i < ulElements.length; i++) {
    ulElements[i].addEventListener('click', (e) => {
        let targetEle = e.target.innerHTML;
        localStorage.setItem("targetUl", targetEle)
    })
}

   for (var i = 0; i < ulElements.length; i++) {

        if (ulElements[i].innerHTML == localStorage.getItem("targetUl")) {
            ulElements[i].classList.add("active");

            let scrollY = localStorage.getItem("ScrollY");
            let scroll = Number(scrollY) - 200;
            window.scrollTo(0, scroll);


        }
        else {
            ulElements[i].classList.remove("active");

        }
    }


function Hi(element) {
    const rect = element.getBoundingClientRect();

  
    // Y-position relative to the viewport
    const yPositionViewport = rect.top;

    // Y-position relative to the document (including scroll offset)
    const yPositionDocument = window.scrollY + yPositionViewport;

  

    localStorage.setItem("ScrollY", yPositionDocument);

}

function resetLocalStorage() {
    localStorage.setItem("ScrollY", 0);

}


// Get the button
const scrollToTopBtn = document.getElementById("scrollToTopBtn");

// When the user scrolls down 20px from the top of the document, show the button
window.onscroll = function () { scrollFunction() };

function scrollFunction() {
    if (document.body.scrollTop > 20 || document.documentElement.scrollTop > 20) {
        scrollToTopBtn.style.display = "block";
    } else {
        scrollToTopBtn.style.display = "none";
    }
}

// When the user clicks on the button, scroll to the top of the document
scrollToTopBtn.addEventListener("click", function () {
    document.body.scrollTop = 0; // For Safari
    document.documentElement.scrollTop = 0; // For Chrome, Firefox, IE, and Opera
});


