# BeeCreak Content Admin

A lightweight Electron desktop helper that makes it easy to browse and edit the text based assets that live in `BeeCreak.Game/Content`.

## Getting started

```bash
cd Tooling/BeeCreak.Tooling.ContentAdmin
npm install
npm start
```

The app automatically points at `../../Game/BeeCreak.Game/Content` relative to this folder. No extra configuration is required as long as the Tooling project stays inside the BeeCreak repo.

## Features

- Tree browser for every asset under the content root with directories collapsed/expanded inline.
- Text editor for the supported file extensions (`json`, `as`, `asc`, `bbs`, `bts`, `game`, `mgcb`, `erec`, `eref`, `crec`, `cref`, `spritefont`, `spritesheet`, `tref`, etc).
- Dirty-state tracking with navigation warning so you do not lose edits accidentally.
- Quick refresh button to pick up out-of-band edits.
- Simple new-file workflow that drops starter templates based on the chosen extension.

Binary files (images, fonts, dlls, ogg, etc.) are listed but intentionally read-only so they are not corrupted by accident.

## Notes

- All filesystem access is funneled through the Electron main process (`main.js`) and is restricted to the content root.
- If you need to add additional editable extensions, extend the `TEXT_EXTENSIONS` list in `main.js` and optionally add a template in `renderer/renderer.js`.
