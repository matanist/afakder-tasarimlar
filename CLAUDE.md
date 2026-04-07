# CLAUDE.md

This file provides guidance to Claude Code (claude.ai/code) when working with code in this repository.

## Project Overview

AFAKDER (Afet Arama Kurtarma Derneği) - A disaster search and rescue organization website for Turkey. The site is in Turkish.

## Architecture

Static website with no build system or framework:
- `tasarim-6/` — Active design directory (Warm Emotional Design — "Dark → Light emotional arc")
  - `index.html` — Single-page site with sections: hero, hikaye (story/response), etki (impact), rakamlar (numbers), faaliyetler (activities), bağış (donation), gönüllü (volunteer), iletişim (contact), footer
  - `style.css` — Pure CSS with custom properties (CSS variables) design system, no preprocessor
  - `script.js` — Vanilla JS for interactions (preloader, navbar scroll, mobile menu, scroll animations via IntersectionObserver, counter animations, donation toggle, form validation, modal)
- `eskiTasarimlar/` — Archived previous design variants

## Yazılım Alt yapısı
- Uygulama bir asp.net 10 mvc uygulamasıdır. 
- Arka planda entityframework kullanılacak ve veritabanı olarak postgres 18 kullanılacak (connectionstring de bağlantı bilgilerini vereceğim)
- Görsel kısımlar zaten tasarim-6 dan gelecek genel yapısı böyle olacak. 
- Admin kısmı da olacak. Admin tarafta kullanıcı yönetimi, içeriklerin yönetimi (tüm anasayfanın yönetimi) kısımları yapılacak. Rol bazlı bir kullanıcı yönetimi olacak. 
- Logo, sloganlar, hero section gibi tüm alanlar değiştirilebilir olmalı. 
- SEO uyumlu bir yapı olmalı (sayfalar slug ile kontrol edilmeli)
- Blog kısmına da ihtiyacımız olacak. 




## Design System

- **Theme**: "Warm Emotional" — dark-to-light emotional arc, glass-morphism effects
- **Color palette**: Dark navy (`#1e2040`) base, sunset orange (`#e8734a`) accent, warm rose (`#d4626a`), soft lavender (`#9b8ec4`), gold (`#daa520`), cream (`#f5e6d3`)
- **Fonts** (Google Fonts): Quicksand (headings, 700), Poppins (body, 300–700), Libre Baskerville (serif accents)
- **CSS variables**: All design tokens defined in `:root` in `style.css` — includes `--radius`, `--shadow-warm`, `--glass-warm`, `--transition`
- **Animations**: `data-animate` attribute + IntersectionObserver for scroll-triggered animations, ECG heartbeat line in hero, floating ember particles
- **Language**: All UI text is in Turkish
