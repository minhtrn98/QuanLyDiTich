# QLDT WEB FE

## Quick Start

Clone the repo:

Install the dependencies:

```bash
cd src/frontend/qldtweb/
yarn install
```

## Table of Contents

- [Commands](#commands)
- [Project Structure](#project-structure)

## Commands

Running locally:

```bash
yarn dev
```

## Project Structure

```
src\
 |--assets\         # Contains all images, css files, font files, etc. for project
 |--components\     # Contain every single component used commonly in your entire application
 |--constants\      # Contains all constants used globally within project
 |--hooks\          # Contains every single custom hook in your entire project
 |--lib\            # Contains facades for the various different libraries or common utilities functions used in project
 |--config\         # Contains library config (ex: axios,...) used in project
 |--layout\         # Contains commonly used layout for pages or other repeated components
 |--pages\          # Contains all pages (or modules) used in project
 |--routes\         # Contains routes config for project
 |--models\         # Contains typescript definitions (types, interface, enums,...) used in project
 |--stores\         # Contains redux config (reducers, stores, selectors,...) for project
 |--validations\    # Contains form validation using zod combined with react-hook-form
 |--services\       # Contains asynchronous services (api call, handle error, token, working with DOM,...) used in project
 |--hocs\           # Contains all higher order components used in project
```
