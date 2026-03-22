/* ============================================
   AFAKDER — Script
   ============================================ */

document.addEventListener('DOMContentLoaded', () => {

    /* --- Preloader --- */
    const preloader = document.getElementById('preloader');
    window.addEventListener('load', () => {
        setTimeout(() => {
            preloader.classList.add('hidden');
        }, 1200);
    });
    // Fallback if load already fired
    if (document.readyState === 'complete') {
        setTimeout(() => {
            preloader.classList.add('hidden');
        }, 1200);
    }

    /* --- Navbar Scroll --- */
    const navbar = document.getElementById('navbar');
    const handleScroll = () => {
        if (window.scrollY > 50) {
            navbar.classList.add('scrolled');
        } else {
            navbar.classList.remove('scrolled');
        }
    };
    window.addEventListener('scroll', handleScroll, { passive: true });
    handleScroll();

    /* --- Mobile Navigation --- */
    const navToggle = document.getElementById('navToggle');
    const navCloseBtn = document.getElementById('navCloseBtn');
    const navLinks = document.getElementById('navLinks');
    const navOverlay = document.getElementById('navOverlay');

    function openNav() {
        navLinks.classList.add('open');
        navOverlay.classList.add('active');
        document.body.style.overflow = 'hidden';
    }

    function closeNav() {
        navLinks.classList.remove('open');
        navOverlay.classList.remove('active');
        document.body.style.overflow = '';
    }

    navToggle.addEventListener('click', openNav);
    navCloseBtn.addEventListener('click', closeNav);
    navOverlay.addEventListener('click', closeNav);

    // Close nav on link click
    navLinks.querySelectorAll('a').forEach(link => {
        link.addEventListener('click', closeNav);
    });

    /* --- Smooth Scroll --- */
    document.querySelectorAll('a[href^="#"]').forEach(anchor => {
        anchor.addEventListener('click', (e) => {
            const targetId = anchor.getAttribute('href');
            if (targetId === '#') return;
            const target = document.querySelector(targetId);
            if (target) {
                e.preventDefault();
                const offset = navbar.offsetHeight + 20;
                const top = target.getBoundingClientRect().top + window.pageYOffset - offset;
                window.scrollTo({ top, behavior: 'smooth' });
            }
        });
    });

    /* --- Scroll Animations --- */
    const animateElements = document.querySelectorAll('.animate-in');

    const observerOptions = {
        root: null,
        rootMargin: '0px 0px -60px 0px',
        threshold: 0.1
    };

    const observer = new IntersectionObserver((entries) => {
        entries.forEach(entry => {
            if (entry.isIntersecting) {
                entry.target.classList.add('visible');
                observer.unobserve(entry.target);
            }
        });
    }, observerOptions);

    animateElements.forEach(el => observer.observe(el));

    /* --- Counter Animation --- */
    const statNumbers = document.querySelectorAll('.stat-number[data-target]');
    let countersAnimated = false;

    const counterObserver = new IntersectionObserver((entries) => {
        entries.forEach(entry => {
            if (entry.isIntersecting && !countersAnimated) {
                countersAnimated = true;
                animateCounters();
                counterObserver.unobserve(entry.target);
            }
        });
    }, { threshold: 0.3 });

    const statsSection = document.getElementById('rakamlar');
    if (statsSection) {
        counterObserver.observe(statsSection);
    }

    function animateCounters() {
        statNumbers.forEach(el => {
            const target = parseInt(el.getAttribute('data-target'), 10);
            const duration = 2000;
            const startTime = performance.now();

            function easeOutQuart(t) {
                return 1 - Math.pow(1 - t, 4);
            }

            function updateCounter(currentTime) {
                const elapsed = currentTime - startTime;
                const progress = Math.min(elapsed / duration, 1);
                const easedProgress = easeOutQuart(progress);
                const current = Math.round(easedProgress * target);
                el.textContent = current.toLocaleString('tr-TR');

                if (progress < 1) {
                    requestAnimationFrame(updateCounter);
                } else {
                    // Add + suffix for large numbers
                    if (target >= 1000) {
                        el.textContent = target.toLocaleString('tr-TR') + '+';
                    }
                }
            }

            requestAnimationFrame(updateCounter);
        });
    }

    /* --- Contact Form --- */
    const contactForm = document.getElementById('contactForm');
    const formSuccess = document.getElementById('formSuccess');

    if (contactForm) {
        contactForm.addEventListener('submit', (e) => {
            e.preventDefault();

            // Simple validation visual feedback
            const inputs = contactForm.querySelectorAll('input[required], select[required], textarea[required]');
            let valid = true;

            inputs.forEach(input => {
                if (!input.value.trim()) {
                    valid = false;
                    input.style.borderColor = '#c4553a';
                    setTimeout(() => {
                        input.style.borderColor = '';
                    }, 2000);
                }
            });

            if (!valid) return;

            // Simulate submission
            const submitBtn = contactForm.querySelector('button[type="submit"]');
            submitBtn.textContent = 'Gönderiliyor...';
            submitBtn.disabled = true;

            setTimeout(() => {
                contactForm.style.display = 'none';
                formSuccess.style.display = 'block';
            }, 1200);
        });

        // Clear error styling on input
        contactForm.querySelectorAll('input, select, textarea').forEach(input => {
            input.addEventListener('input', () => {
                input.style.borderColor = '';
            });
        });
    }

    /* --- Active nav link highlighting --- */
    const sections = document.querySelectorAll('section[id]');

    function updateActiveNav() {
        const scrollPos = window.scrollY + navbar.offsetHeight + 100;

        sections.forEach(section => {
            const top = section.offsetTop;
            const height = section.offsetHeight;
            const id = section.getAttribute('id');

            if (scrollPos >= top && scrollPos < top + height) {
                navLinks.querySelectorAll('a').forEach(link => {
                    link.style.color = '';
                    if (link.getAttribute('href') === '#' + id) {
                        link.style.color = '#c4553a';
                    }
                });
            }
        });
    }

    window.addEventListener('scroll', updateActiveNav, { passive: true });

    /* --- Parallax on hero shapes (desktop only) --- */
    if (window.innerWidth > 768) {
        const shapes = document.querySelectorAll('.hero-shape');
        window.addEventListener('scroll', () => {
            const scrollY = window.scrollY;
            shapes.forEach((shape, i) => {
                const speed = 0.1 + (i * 0.05);
                shape.style.transform += '';
                shape.style.marginTop = (scrollY * speed) + 'px';
            });
        }, { passive: true });
    }

    /* --- Training decoration rotation --- */
    const decoCircles = document.querySelectorAll('.deco-circle');
    if (decoCircles.length) {
        let rotation = 0;
        function rotateDeco() {
            rotation += 0.15;
            decoCircles.forEach((circle, i) => {
                const dir = i % 2 === 0 ? 1 : -1;
                circle.style.transform = `rotate(${rotation * dir}deg)`;
            });
            requestAnimationFrame(rotateDeco);
        }
        rotateDeco();
    }

});
