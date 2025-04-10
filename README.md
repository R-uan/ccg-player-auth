# Player Auth Server (Microservice)
This repository is part of a larger project of a game back-end.
# Endpoints
## Authentication

### 🔓 Player Login
    PUBLIC POST /api/auth/login {
        "email": string,
        "password": string
    }

#### Responses
    Ok {
        "token": string
    }

    Unauthorized {}
    

### 🔓 Player Registration
    PUBLIC POST /api/auth/register {
        "email": string,
        "username": string,
        "password": string
    }

#### Responses
    Ok {
        "player": {
            "guid": string,
            "email": string,
            "username":string
        }
    }

    BadRequest { }

## Player

### 🔒 Update information (TODO)
    GUARDED PATCH /api/player { }

### 🔒 Player Profile Information
    GUARDED GET /api/player/profile { }

#### Responses
    Ok {
        "id": string,
        "email": string,
        "username": string,
        "cardCollection": [string],
        "level": int,
        "experience": int,
        "wins": int,
        "losses": int,
        "lastLogin": date,
        "isBanned": bool
    }

    Unauthorized { }

### 🔓 Partial Player Profile Information
    PUBLIC GET /api/player/partial/{playerId}` { }

#### Responses
    Ok {
        "id": string,
        "email": string,
        "username": string,
        "level": int,
        "wins": int,
        "losses": int,
    }

    NotFound { }

## Card Collection

### 🔒 Player Card Collection (Collection Only) 
    GUARDED GET /api/collection { }

### 🔒 Add new card to Player's collection
    GUARDED POST /api/collection { }
