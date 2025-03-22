// src/animations.ts
import { gsap } from 'gsap';
import { ScrollTrigger } from 'gsap/ScrollTrigger';

gsap.registerPlugin(ScrollTrigger);

interface AnimationOptions {
  delay?: number;
  duration?: number;
  y?: number;
  x?: number;
  stagger?: number;
}

// Scroll-based fade-in animation
export function scrollFadeIn(element: HTMLElement, options: AnimationOptions = {}) {
  const { delay = 0, duration = 1, y = 20 } = options;
  
  gsap.from(element, {
    opacity: 0,
    y: y,
    duration: duration,
    delay: delay,
    scrollTrigger: {
      trigger: element,
      start: "top 80%",
      toggleActions: "play none none reverse",
    },
  });
}

// Scroll-based slide-in animation
export function scrollSlideIn(element: HTMLElement, options: AnimationOptions = {}) {
  const { delay = 0, duration = 1, x = -100 } = options;
  
  gsap.from(element, {
    opacity: 0,
    x: x,
    duration: duration,
    delay: delay,
    scrollTrigger: {
      trigger: element,
      start: "top 90%",
      toggleActions: "play none none reverse",
    },
  });
}

// Pin an element in place as you scroll past it
export function pinElement(element: HTMLElement) {
  ScrollTrigger.create({
    trigger: element,
    start: "top top",
    end: "+=500",
    pin: true,
  });
}