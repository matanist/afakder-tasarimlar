// ============================================
// AFAKDER - Afet Arama Kurtarma Derneği
// Interactive Scripts
// ============================================

document.addEventListener('DOMContentLoaded', () => {

    // --- Preloader ---
    const preloader = document.getElementById('preloader');
    window.addEventListener('load', () => {
        setTimeout(() => {
            preloader.classList.add('hidden');
        }, 800);
    });

    // Fallback: hide preloader after 3s max
    setTimeout(() => {
        preloader.classList.add('hidden');
    }, 3000);

    // --- Navigation Scroll Effect ---
    const nav = document.getElementById('nav');
    let lastScroll = 0;

    window.addEventListener('scroll', () => {
        const currentScroll = window.scrollY;

        if (currentScroll > 80) {
            nav.classList.add('scrolled');
        } else {
            nav.classList.remove('scrolled');
        }

        lastScroll = currentScroll;
    }, { passive: true });

    // --- Mobile Navigation Toggle ---
    const navToggle = document.getElementById('navToggle');
    const navLinks = document.getElementById('navLinks');

    navToggle.addEventListener('click', () => {
        navToggle.classList.toggle('active');
        navLinks.classList.toggle('open');
        document.body.style.overflow = navLinks.classList.contains('open') ? 'hidden' : '';
    });

    // Close mobile nav on link click
    navLinks.querySelectorAll('.nav-link').forEach(link => {
        link.addEventListener('click', () => {
            navToggle.classList.remove('active');
            navLinks.classList.remove('open');
            document.body.style.overflow = '';
        });
    });

    // --- Counter Animation ---
    const counters = document.querySelectorAll('.hero-stat-number[data-target]');

    function animateCounter(el) {
        const target = parseInt(el.dataset.target);
        const duration = 2000;
        const startTime = performance.now();

        function update(currentTime) {
            const elapsed = currentTime - startTime;
            const progress = Math.min(elapsed / duration, 1);

            // Ease out cubic
            const eased = 1 - Math.pow(1 - progress, 3);
            const current = Math.floor(eased * target);

            el.textContent = current.toLocaleString('tr-TR');

            if (progress < 1) {
                requestAnimationFrame(update);
            } else {
                el.textContent = target.toLocaleString('tr-TR');
            }
        }

        requestAnimationFrame(update);
    }

    // Start counters when hero is visible
    const heroObserver = new IntersectionObserver((entries) => {
        entries.forEach(entry => {
            if (entry.isIntersecting) {
                counters.forEach(counter => animateCounter(counter));
                heroObserver.disconnect();
            }
        });
    }, { threshold: 0.3 });

    const heroStats = document.querySelector('.hero-stats');
    if (heroStats) {
        heroObserver.observe(heroStats);
    }

    // --- Scroll Reveal Animations ---
    const revealElements = document.querySelectorAll(
        '.about-card, .activity-card, .team-card, .timeline-item, ' +
        '.impact-number, .section-header, .contact-detail, .contact-form-wrapper, ' +
        '.training-program, .training-cta-left, .donation-content, .impact-text'
    );

    const revealObserver = new IntersectionObserver((entries) => {
        entries.forEach(entry => {
            if (entry.isIntersecting) {
                entry.target.classList.add('revealed');
                revealObserver.unobserve(entry.target);
            }
        });
    }, {
        threshold: 0.1,
        rootMargin: '0px 0px -60px 0px'
    });

    revealElements.forEach((el, i) => {
        el.style.opacity = '0';
        el.style.transform = 'translateY(30px)';
        el.style.transition = `opacity 0.6s ease ${i % 4 * 0.1}s, transform 0.6s ease ${i % 4 * 0.1}s`;
        revealObserver.observe(el);
    });

    // Add the revealed class styles
    const style = document.createElement('style');
    style.textContent = `.revealed { opacity: 1 !important; transform: translateY(0) !important; }`;
    document.head.appendChild(style);

    // --- Donation Amount Selection ---
    const donationAmounts = document.querySelectorAll('.donation-amount:not(.donation-amount--custom)');

    donationAmounts.forEach(btn => {
        btn.addEventListener('click', () => {
            donationAmounts.forEach(b => b.classList.remove('active'));
            btn.classList.add('active');
        });
    });

    const customInput = document.querySelector('.donation-amount--custom input');
    if (customInput) {
        customInput.addEventListener('focus', () => {
            donationAmounts.forEach(b => b.classList.remove('active'));
            customInput.closest('.donation-amount').classList.add('active');
        });
    }

    // --- Contact Form ---
    const contactForm = document.getElementById('contactForm');
    if (contactForm) {
        contactForm.addEventListener('submit', (e) => {
            e.preventDefault();

            const btn = contactForm.querySelector('button[type="submit"]');
            const originalText = btn.querySelector('span').textContent;

            btn.querySelector('span').textContent = 'Gönderiliyor...';
            btn.disabled = true;

            // Simulate submission
            setTimeout(() => {
                btn.querySelector('span').textContent = 'Gönderildi!';
                btn.style.background = '#10b981';
                btn.style.borderColor = '#10b981';

                setTimeout(() => {
                    btn.querySelector('span').textContent = originalText;
                    btn.style.background = '';
                    btn.style.borderColor = '';
                    btn.disabled = false;
                    contactForm.reset();
                }, 2000);
            }, 1500);
        });
    }

    // --- Smooth Scroll for Anchor Links ---
    document.querySelectorAll('a[href^="#"]').forEach(anchor => {
        anchor.addEventListener('click', function (e) {
            const targetId = this.getAttribute('href');
            if (targetId === '#') return;

            const target = document.querySelector(targetId);
            if (target) {
                e.preventDefault();
                const navHeight = nav.offsetHeight;
                const targetPosition = target.getBoundingClientRect().top + window.scrollY - navHeight - 20;

                window.scrollTo({
                    top: targetPosition,
                    behavior: 'smooth'
                });
            }
        });
    });

    // --- Parallax Effect on Hero ---
    const heroContent = document.querySelector('.hero-content');
    const heroGrid = document.querySelector('.hero-grid');

    window.addEventListener('scroll', () => {
        const scrolled = window.scrollY;
        if (scrolled < window.innerHeight) {
            const opacity = 1 - (scrolled / (window.innerHeight * 0.8));
            const translateY = scrolled * 0.3;

            if (heroContent) {
                heroContent.style.transform = `translateY(${translateY}px)`;
                heroContent.style.opacity = Math.max(opacity, 0);
            }

            if (heroGrid) {
                heroGrid.style.transform = `translateY(${scrolled * 0.1}px)`;
            }
        }
    }, { passive: true });

    // --- Active Navigation Link Highlight ---
    const sections = document.querySelectorAll('section[id]');

    window.addEventListener('scroll', () => {
        const scrollY = window.scrollY + 100;

        sections.forEach(section => {
            const sectionTop = section.offsetTop;
            const sectionHeight = section.offsetHeight;
            const sectionId = section.getAttribute('id');

            if (scrollY >= sectionTop && scrollY < sectionTop + sectionHeight) {
                document.querySelectorAll('.nav-link').forEach(link => {
                    link.classList.remove('active');
                    if (link.getAttribute('href') === `#${sectionId}`) {
                        link.classList.add('active');
                        link.style.color = 'var(--color-text)';
                    } else {
                        link.style.color = '';
                    }
                });
            }
        });
    }, { passive: true });

});
