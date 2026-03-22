# CLAUDE.md

This file provides guidance to Claude Code (claude.ai/code) when working with code in this repository.

## Project Overview

AFAKDER (Afet Arama Kurtarma Derneği) - A disaster search and rescue organization website for Turkey. The site is in Turkish.

## Architecture

Static website with no build system or framework:
- `tasarim/` — Main design/site directory containing the landing page
  - `index.html` — Single-page site with all sections (hero, about, activities, team, training, timeline, contact, donation, footer)
  - `style.css` — Pure CSS with custom properties (CSS variables) design system, no preprocessor
  - `script.js` — Vanilla JS for interactions (preloader, scroll effects, counter animations, mobile nav, form handling)

## Development

Open `tasarim/index.html` directly in a browser — no server or build step required. For live reload, use any static server (e.g., `npx serve tasarim` or `python3 -m http.server -d tasarim`).

## Design System

- **Color palette**: Dark navy (`#0a0e17`) base + emergency orange (`#e85d26`) accent
- **Fonts** (Google Fonts): Bebas Neue (display), Barlow Condensed (headings), Barlow (body)
- **CSS variables**: All design tokens defined in `:root` in `style.css`
- **Language**: All UI text is in Turkish
