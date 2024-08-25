﻿document.addEventListener("DOMContentLoaded", function () {
    // Function to handle adding the 'slide-up' class when the element is in view
    function handleIntersection(entries) {
        entries.forEach((entry) => {
            if (entry.isIntersecting) {
                entry.target.classList.add("slide-up");
                observer.unobserve(entry.target);
            }
        });
    }

    const observer = new IntersectionObserver(handleIntersection, {
        root: null,
        rootMargin: "0px",
        threshold: 0.4,
    });

    const columnElements = document.querySelectorAll(
        ".columnn-1, .member-1"
    );
    columnElements.forEach((element) => observer.observe(element));
});

let currentIndex = 0;
const slideWidth = 300; // Width of each slide
const slides = document.querySelector(".team-slides");
const totalItems = document.querySelectorAll(".team-member").length;

// Clone the first and last slides to handle seamless looping
function cloneItems() {
    const firstItem = slides.children[0].cloneNode(true);
    const lastItem =
        slides.children[slides.children.length - 1].cloneNode(true);

    slides.appendChild(firstItem); // Clone at the end
    slides.insertBefore(lastItem, slides.children[0]); // Clone at the beginning

    // Set initial transform to show the first item correctly
    slides.style.transform = `translateX(${-slideWidth}px)`;
}

// Initialize cloning
cloneItems();

function moveSlide(direction) {
    const slidesCount = totalItems - 2; // Total slides including clones

    // Move the slides
    currentIndex += direction;

    // Move the slides with a smooth transition
    slides.style.transition = "transform 0.5s ease-in-out";
    slides.style.transform = `translateX(${-currentIndex * slideWidth}px)`;

    // Check if we've reached the first or last slide and adjust the index and transform
    if (currentIndex < 0) {
        setTimeout(() => {
            slides.style.transition = "none"; // Remove transition for instant jump
            currentIndex = slidesCount - 3; // Move to the last real item
            slides.style.transform = `translateX(${-currentIndex * slideWidth
                }px)`;
        }, 500); // Match the transition duration
    } else if (currentIndex >= slidesCount - 1) {
        setTimeout(() => {
            slides.style.transition = "none"; // Remove transition for instant jump
            currentIndex = 1; // Move to the first real item
            slides.style.transform = `translateX(${-currentIndex * slideWidth
                }px)`;
        }, 500); // Match the transition duration
    }
}