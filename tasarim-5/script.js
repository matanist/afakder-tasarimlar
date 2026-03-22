/* =============================================
   AFAKDER — Warm, Heartfelt Interactions
   ============================================= */

document.addEventListener('DOMContentLoaded', () => {

    // --- Preloader ---
    const preloader = document.getElementById('preloader');
    window.addEventListener('load', () => {
        setTimeout(() => {
            preloader.classList.add('hidden');
            setTimeout(() => { preloader.style.display = 'none'; }, 500);
        }, 800);
    });

    // Fallback: hide preloader after 3 seconds regardless
    setTimeout(() => {
        preloader.classList.add('hidden');
        setTimeout(() => { preloader.style.display = 'none'; }, 500);
    }, 3000);

    // --- Navbar Scroll ---
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

    // --- Mobile Menu ---
    const hamburger = document.getElementById('hamburger');
    const navLinks = document.getElementById('navLinks');
    const mobileOverlay = document.getElementById('mobileOverlay');

    const toggleMenu = () => {
        hamburger.classList.toggle('active');
        navLinks.classList.toggle('mobile-open');
        mobileOverlay.classList.toggle('active');
        document.body.style.overflow = navLinks.classList.contains('mobile-open') ? 'hidden' : '';
    };

    const closeMenu = () => {
        hamburger.classList.remove('active');
        navLinks.classList.remove('mobile-open');
        mobileOverlay.classList.remove('active');
        document.body.style.overflow = '';
    };

    hamburger.addEventListener('click', toggleMenu);
    mobileOverlay.addEventListener('click', closeMenu);

    // Close menu on link click
    navLinks.querySelectorAll('a').forEach(link => {
        link.addEventListener('click', closeMenu);
    });

    // --- Smooth Scroll for anchor links ---
    document.querySelectorAll('a[href^="#"]').forEach(anchor => {
        anchor.addEventListener('click', (e) => {
            const target = document.querySelector(anchor.getAttribute('href'));
            if (target) {
                e.preventDefault();
                target.scrollIntoView({ behavior: 'smooth', block: 'start' });
            }
        });
    });

    // --- Intersection Observer for Animations ---
    const observerOptions = {
        threshold: 0.15,
        rootMargin: '0px 0px -50px 0px'
    };

    const animateOnScroll = new IntersectionObserver((entries) => {
        entries.forEach(entry => {
            if (entry.isIntersecting) {
                entry.target.classList.add('visible');
                animateOnScroll.unobserve(entry.target);
            }
        });
    }, observerOptions);

    // Story cards staggered
    document.querySelectorAll('.story-card').forEach((card, i) => {
        card.style.transitionDelay = `${i * 0.15}s`;
        animateOnScroll.observe(card);
    });

    // Service cards staggered
    document.querySelectorAll('.service-card').forEach((card, i) => {
        card.style.transitionDelay = `${i * 0.1}s`;
        animateOnScroll.observe(card);
    });

    // Generic fade-in elements
    document.querySelectorAll('.section-header, .about-grid, .trust-grid, .donate-cards, .donate-progress, .volunteer-content, .contact-grid').forEach(el => {
        el.classList.add('fade-in');
        animateOnScroll.observe(el);
    });

    // --- Counter Animation ---
    const counterObserver = new IntersectionObserver((entries) => {
        entries.forEach(entry => {
            if (entry.isIntersecting) {
                animateCounter(entry.target);
                counterObserver.unobserve(entry.target);
            }
        });
    }, { threshold: 0.5 });

    document.querySelectorAll('.counter').forEach(counter => {
        counterObserver.observe(counter);
    });

    function animateCounter(el) {
        const target = parseInt(el.dataset.target);
        const duration = 2000;
        const startTime = performance.now();

        function easeOutQuart(t) {
            return 1 - Math.pow(1 - t, 4);
        }

        function update(currentTime) {
            const elapsed = currentTime - startTime;
            const progress = Math.min(elapsed / duration, 1);
            const easedProgress = easeOutQuart(progress);
            const current = Math.round(easedProgress * target);

            el.textContent = formatNumber(current);

            if (progress < 1) {
                requestAnimationFrame(update);
            }
        }

        requestAnimationFrame(update);
    }

    function formatNumber(num) {
        return num.toLocaleString('tr-TR');
    }

    // --- Finance Bars Animation ---
    const financeObserver = new IntersectionObserver((entries) => {
        entries.forEach(entry => {
            if (entry.isIntersecting) {
                entry.target.querySelectorAll('.finance-bar-fill').forEach((bar, i) => {
                    setTimeout(() => {
                        bar.style.width = bar.dataset.width + '%';
                        bar.classList.add('animated');
                    }, i * 150);
                });
                financeObserver.unobserve(entry.target);
            }
        });
    }, { threshold: 0.3 });

    const financeBars = document.querySelector('.finance-bars');
    if (financeBars) financeObserver.observe(financeBars);

    // --- Donation Progress Bar ---
    const progressObserver = new IntersectionObserver((entries) => {
        entries.forEach(entry => {
            if (entry.isIntersecting) {
                const fill = entry.target.querySelector('.progress-fill');
                if (fill) {
                    setTimeout(() => {
                        fill.style.width = fill.dataset.width + '%';
                    }, 300);
                }
                progressObserver.unobserve(entry.target);
            }
        });
    }, { threshold: 0.3 });

    const progressBar = document.querySelector('.donate-progress');
    if (progressBar) progressObserver.observe(progressBar);

    // --- Donation Card Selection ---
    const donateCards = document.querySelectorAll('.donate-card');
    donateCards.forEach(card => {
        card.addEventListener('click', (e) => {
            if (e.target.closest('.btn-donate')) return; // let button handle its own click
            donateCards.forEach(c => c.classList.remove('selected'));
            card.classList.add('selected');
        });
    });

    // Donate buttons
    document.querySelectorAll('.btn-donate').forEach(btn => {
        btn.addEventListener('click', () => {
            const card = btn.closest('.donate-card');
            const amount = card.dataset.amount;
            const isMonthly = document.getElementById('monthlyToggle').checked;
            const type = isMonthly ? 'aylık' : 'tek seferlik';

            // In a real site, this would redirect to payment
            alert(`${amount} TL ${type} bağış için teşekkür ederiz! Ödeme sayfasına yönlendirileceksiniz.`);
        });
    });

    // --- Monthly Toggle ---
    const monthlyToggle = document.getElementById('monthlyToggle');
    const toggleLabelOnce = document.getElementById('toggleLabelOnce');
    const toggleLabelMonthly = document.getElementById('toggleLabelMonthly');

    toggleLabelOnce.classList.add('active');

    monthlyToggle.addEventListener('change', () => {
        if (monthlyToggle.checked) {
            toggleLabelMonthly.classList.add('active');
            toggleLabelOnce.classList.remove('active');
        } else {
            toggleLabelOnce.classList.add('active');
            toggleLabelMonthly.classList.remove('active');
        }
    });

    // --- Contact Form ---
    const contactForm = document.getElementById('contactForm');

    contactForm.addEventListener('submit', (e) => {
        e.preventDefault();

        let isValid = true;

        // Reset errors
        contactForm.querySelectorAll('.form-group').forEach(g => g.classList.remove('error'));

        // Validate name
        const name = document.getElementById('contactName');
        if (!name.value.trim()) {
            name.closest('.form-group').classList.add('error');
            isValid = false;
        }

        // Validate email
        const email = document.getElementById('contactEmail');
        const emailRegex = /^[^\s@]+@[^\s@]+\.[^\s@]+$/;
        if (!emailRegex.test(email.value.trim())) {
            email.closest('.form-group').classList.add('error');
            isValid = false;
        }

        // Validate subject
        const subject = document.getElementById('contactSubject');
        if (!subject.value) {
            subject.closest('.form-group').classList.add('error');
            isValid = false;
        }

        // Validate message
        const message = document.getElementById('contactMessage');
        if (!message.value.trim() || message.value.trim().length < 10) {
            message.closest('.form-group').classList.add('error');
            isValid = false;
        }

        if (isValid) {
            // Show loading
            const btnText = contactForm.querySelector('.btn-text');
            const btnLoading = contactForm.querySelector('.btn-loading');
            btnText.style.display = 'none';
            btnLoading.style.display = 'inline';

            // Simulate form submission
            setTimeout(() => {
                btnText.style.display = 'inline';
                btnLoading.style.display = 'none';
                contactForm.reset();
                document.getElementById('successModal').classList.add('active');
            }, 1500);
        } else {
            // Scroll to first error
            const firstError = contactForm.querySelector('.form-group.error');
            if (firstError) {
                firstError.scrollIntoView({ behavior: 'smooth', block: 'center' });
            }
        }
    });

    // Remove error on input
    contactForm.querySelectorAll('input, select, textarea').forEach(input => {
        input.addEventListener('input', () => {
            input.closest('.form-group').classList.remove('error');
        });
    });

    // --- Newsletter Form ---
    const newsletterForm = document.getElementById('newsletterForm');
    if (newsletterForm) {
        newsletterForm.addEventListener('submit', (e) => {
            e.preventDefault();
            const input = newsletterForm.querySelector('input');
            const emailRegex = /^[^\s@]+@[^\s@]+\.[^\s@]+$/;

            if (emailRegex.test(input.value.trim())) {
                alert('Bültenimize kaydoldunuz! Teşekkür ederiz.');
                input.value = '';
            } else {
                alert('Lütfen geçerli bir e-posta adresi girin.');
            }
        });
    }

    // --- Active nav link highlight ---
    const sections = document.querySelectorAll('section[id]');

    const highlightNav = () => {
        const scrollPos = window.scrollY + 150;

        sections.forEach(section => {
            const top = section.offsetTop;
            const height = section.offsetHeight;
            const id = section.getAttribute('id');

            if (scrollPos >= top && scrollPos < top + height) {
                navLinks.querySelectorAll('a').forEach(link => {
                    link.style.color = '';
                    link.style.background = '';
                    if (link.getAttribute('href') === '#' + id) {
                        link.style.color = '#e07c5a';
                    }
                });
            }
        });
    };

    window.addEventListener('scroll', highlightNav, { passive: true });

});

// --- Modal Close (global) ---
function closeModal() {
    document.getElementById('successModal').classList.remove('active');
}

// Close modal on overlay click
document.getElementById('successModal').addEventListener('click', (e) => {
    if (e.target === e.currentTarget) closeModal();
});

// Close modal on Escape key
document.addEventListener('keydown', (e) => {
    if (e.key === 'Escape') closeModal();
});
