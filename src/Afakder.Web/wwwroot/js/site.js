/* =============================================
   AFAKDER — Script
   Warm, emotional, performance-focused
   ============================================= */

document.addEventListener('DOMContentLoaded', () => {
  // --- PRELOADER ---
  const preloader = document.getElementById('preloader');
  window.addEventListener('load', () => {
    setTimeout(() => {
      preloader.classList.add('hidden');
      document.querySelector('.hero').classList.add('loaded');
    }, 1200);
  });

  // Fallback: hide preloader after 3s even if load doesn't fire
  setTimeout(() => {
    preloader.classList.add('hidden');
    document.querySelector('.hero').classList.add('loaded');
  }, 3000);

  // --- NAVBAR SCROLL ---
  const navbar = document.getElementById('navbar');
  let lastScroll = 0;

  function handleNavScroll() {
    const scrollY = window.scrollY;
    if (scrollY > 80) {
      navbar.classList.add('scrolled');
    } else {
      navbar.classList.remove('scrolled');
    }
    lastScroll = scrollY;
  }

  window.addEventListener('scroll', handleNavScroll, { passive: true });

  // --- MOBILE MENU ---
  const mobileToggle = document.getElementById('mobileToggle');
  const mobileMenu = document.getElementById('mobileMenu');
  let overlay = null;

  function openMobile() {
    mobileMenu.classList.add('open');
    mobileToggle.classList.add('active');
    if (!overlay) {
      overlay = document.createElement('div');
      overlay.className = 'mobile-overlay';
      document.body.appendChild(overlay);
      overlay.addEventListener('click', closeMobile);
    }
    requestAnimationFrame(() => overlay.classList.add('active'));
    document.body.style.overflow = 'hidden';
  }

  function closeMobile() {
    mobileMenu.classList.remove('open');
    mobileToggle.classList.remove('active');
    if (overlay) {
      overlay.classList.remove('active');
    }
    document.body.style.overflow = '';
  }

  mobileToggle.addEventListener('click', () => {
    if (mobileMenu.classList.contains('open')) {
      closeMobile();
    } else {
      openMobile();
    }
  });

  // Close mobile menu on link click
  document.querySelectorAll('.mobile-links a, .mobile-cta').forEach(link => {
    link.addEventListener('click', closeMobile);
  });

  // --- SMOOTH SCROLL for anchor links ---
  document.querySelectorAll('a[href^="#"]').forEach(link => {
    link.addEventListener('click', (e) => {
      const targetId = link.getAttribute('href');
      if (targetId === '#') return;
      const target = document.querySelector(targetId);
      if (target) {
        e.preventDefault();
        const offset = navbar.offsetHeight + 10;
        const top = target.getBoundingClientRect().top + window.scrollY - offset;
        window.scrollTo({ top, behavior: 'smooth' });
      }
    });
  });

  // --- SCROLL ANIMATIONS (Intersection Observer) ---
  const animateElements = document.querySelectorAll('[data-animate]');
  const observerOptions = {
    root: null,
    rootMargin: '0px 0px -60px 0px',
    threshold: 0.1
  };

  const animateObserver = new IntersectionObserver((entries) => {
    entries.forEach((entry, index) => {
      if (entry.isIntersecting) {
        // Stagger animation for siblings
        const parent = entry.target.parentElement;
        const siblings = parent.querySelectorAll('[data-animate]');
        let delay = 0;
        siblings.forEach((sib, i) => {
          if (sib === entry.target) delay = i * 120;
        });
        setTimeout(() => {
          entry.target.classList.add('visible');
        }, delay);
        animateObserver.unobserve(entry.target);
      }
    });
  }, observerOptions);

  animateElements.forEach(el => animateObserver.observe(el));

  // --- COUNTER ANIMATION ---
  const counterElements = document.querySelectorAll('.number-value[data-target]');
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

  counterElements.forEach(el => counterObserver.observe(el));

  function animateCounters() {
    counterElements.forEach(counter => {
      const target = parseInt(counter.dataset.target);
      const duration = 2000;
      const startTime = performance.now();

      function updateCounter(currentTime) {
        const elapsed = currentTime - startTime;
        const progress = Math.min(elapsed / duration, 1);
        // Ease out cubic
        const eased = 1 - Math.pow(1 - progress, 3);
        const current = Math.floor(eased * target);

        counter.textContent = formatNumber(current);

        if (progress < 1) {
          requestAnimationFrame(updateCounter);
        } else {
          counter.textContent = formatNumber(target);
        }
      }

      requestAnimationFrame(updateCounter);
    });
  }

  function formatNumber(num) {
    return num.toLocaleString('tr-TR');
  }

  // --- DONATION PROGRESS BAR ---
  const progressBar = document.querySelector('.progress-bar');
  if (progressBar) {
    const progressObserver = new IntersectionObserver((entries) => {
      entries.forEach(entry => {
        if (entry.isIntersecting) {
          const progress = progressBar.dataset.progress;
          const fill = progressBar.querySelector('.progress-fill');
          setTimeout(() => {
            fill.style.width = progress + '%';
          }, 300);
          progressObserver.unobserve(entry.target);
        }
      });
    }, { threshold: 0.3 });

    progressObserver.observe(progressBar);
  }

  // --- DONATION TOGGLE ---
  const toggleBtns = document.querySelectorAll('.toggle-btn');
  toggleBtns.forEach(btn => {
    btn.addEventListener('click', () => {
      toggleBtns.forEach(b => b.classList.remove('active'));
      btn.classList.add('active');
      // Could toggle between monthly/one-time display
      const type = btn.dataset.type;
      const impactCards = document.querySelectorAll('.impact-card');
      impactCards.forEach(card => {
        card.style.transform = 'scale(0.95)';
        card.style.opacity = '0.7';
        setTimeout(() => {
          card.style.transform = '';
          card.style.opacity = '';
        }, 200);
      });
    });
  });

  // --- IMPACT BUTTON CLICK (Donation simulation) ---
  document.querySelectorAll('.impact-btn').forEach(btn => {
    btn.addEventListener('click', () => {
      const card = btn.closest('.impact-card');
      const amount = card.dataset.amount;
      const isMonthly = document.querySelector('.toggle-btn[data-type="aylik"]').classList.contains('active');
      const typeText = isMonthly ? 'aylık' : 'tek seferlik';

      showModal(
        'Teşekkür Ederiz!',
        `${amount}₺ ${typeText} bağışınız için minnettarız. Her kuruş hayat kurtarır. Ödeme sayfasına yönlendirileceksiniz.`
      );
    });
  });

  // --- FORM VALIDATION ---
  function validateForm(form) {
    let valid = true;
    const groups = form.querySelectorAll('.form-group');

    groups.forEach(group => {
      const input = group.querySelector('input, textarea');
      const error = group.querySelector('.form-error');
      if (!input) return;

      group.classList.remove('error');
      if (error) error.textContent = '';

      if (input.hasAttribute('required') && !input.value.trim()) {
        group.classList.add('error');
        if (error) error.textContent = 'Bu alan zorunludur';
        valid = false;
      } else if (input.type === 'email' && input.value.trim()) {
        const emailRegex = /^[^\s@]+@[^\s@]+\.[^\s@]+$/;
        if (!emailRegex.test(input.value.trim())) {
          group.classList.add('error');
          if (error) error.textContent = 'Geçerli bir e-posta adresi giriniz';
          valid = false;
        }
      } else if (input.type === 'tel' && input.value.trim()) {
        const phoneClean = input.value.replace(/\s/g, '');
        if (phoneClean.length < 10) {
          group.classList.add('error');
          if (error) error.textContent = 'Geçerli bir telefon numarası giriniz';
          valid = false;
        }
      }
    });

    return valid;
  }

  // Real-time validation: clear errors on input
  document.querySelectorAll('.warm-form input, .warm-form textarea').forEach(input => {
    input.addEventListener('input', () => {
      const group = input.closest('.form-group');
      if (group) {
        group.classList.remove('error');
        const error = group.querySelector('.form-error');
        if (error) error.textContent = '';
      }
    });
  });

  // --- AJAX FORM SUBMIT HELPER ---
  async function submitFormAjax(form) {
    const formData = new FormData(form);
    try {
      const response = await fetch(form.action, {
        method: 'POST',
        body: formData
      });
      const result = await response.json();
      if (result.success) {
        showModal(result.title, result.message);
        form.reset();
      } else if (result.errors) {
        result.errors.forEach(err => console.warn('Validation:', err));
      }
    } catch (err) {
      showModal('Hata', 'Bir hata oluştu. Lütfen tekrar deneyin.');
    }
  }

  // Volunteer form
  const volunteerForm = document.getElementById('volunteerForm');
  if (volunteerForm) {
    volunteerForm.addEventListener('submit', (e) => {
      e.preventDefault();
      if (validateForm(volunteerForm)) {
        submitFormAjax(volunteerForm);
      }
    });
  }

  // Contact form
  const contactForm = document.getElementById('contactForm');
  if (contactForm) {
    contactForm.addEventListener('submit', (e) => {
      e.preventDefault();
      if (validateForm(contactForm)) {
        submitFormAjax(contactForm);
      }
    });
  }

  // --- MODAL ---
  window.showModal = function(title, text) {
    const modal = document.getElementById('successModal');
    modal.querySelector('.modal-title').textContent = title;
    modal.querySelector('.modal-text').textContent = text;
    modal.classList.add('active');
    document.body.style.overflow = 'hidden';
  };

  window.closeModal = function() {
    const modal = document.getElementById('successModal');
    modal.classList.remove('active');
    document.body.style.overflow = '';
  };

  // Close modal on backdrop click
  document.getElementById('successModal').addEventListener('click', (e) => {
    if (e.target === e.currentTarget) {
      closeModal();
    }
  });

  // Close modal on Escape
  document.addEventListener('keydown', (e) => {
    if (e.key === 'Escape') {
      closeModal();
      closeMobile();
    }
  });

  // --- ACTIVE NAV LINK HIGHLIGHT ON SCROLL ---
  const sections = document.querySelectorAll('section[id]');
  const navLinks = document.querySelectorAll('.nav-links a');

  function highlightNav() {
    const scrollPos = window.scrollY + 200;
    sections.forEach(section => {
      const top = section.offsetTop;
      const height = section.offsetHeight;
      const id = section.getAttribute('id');
      if (scrollPos >= top && scrollPos < top + height) {
        navLinks.forEach(link => {
          link.style.color = '';
          if (link.getAttribute('href') === '#' + id) {
            link.style.color = '#e8734a';
          }
        });
      }
    });
  }

  window.addEventListener('scroll', highlightNav, { passive: true });
});
