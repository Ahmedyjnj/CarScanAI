# CarScan AI – Authentication API

API documentation for the **Authentication** endpoints. Use this when integrating the frontend or other clients.

---

## Base URL

```
https://your-api-host.com/api/Authentication
```

For local development:

```
https://localhost:PORT/api/Authentication
```

Replace `PORT` with your API port (e.g. `7104`).

---

## General

### Request format

- **Content-Type:** `application/json` for POST bodies.
- **Authorization (protected routes):**  
  `Authorization: Bearer <JWT_TOKEN>`

### Error response format

All errors return JSON with a single message:

```json
{
  "error": "User-friendly error message here."
}
```

| Status | Meaning |
|--------|--------|
| **400** | Bad Request – validation or invalid input (e.g. register validation). |
| **401** | Unauthorized – wrong password or missing/invalid token. |
| **404** | Not Found – resource (e.g. user) not found. |
| **500** | Server error – generic message: *"An error occurred. Please try again."* |

---

## Endpoints

---

### 1. Register

Create a new user account.

| | |
|---|---|
| **Method** | `POST` |
| **URL** | `/api/Authentication/Register` |
| **Auth** | Not required |

**Request body**

```json
{
  "email": "user@example.com",
  "password": "YourPassword123",
  "userName": "johndoe",
  "displayName": "John Doe",
  "phoneNumber": "+201234567890"
}
```

| Field | Type | Required | Rules |
|-------|------|----------|--------|
| `email` | string | Yes | Valid email format. |
| `password` | string | Yes | 6–100 characters. |
| `userName` | string | Yes | Max 64 characters. |
| `displayName` | string | Yes | Max 100 characters. |
| `phoneNumber` | string | No | Valid phone format, max 20 characters. |

**Success (200)**

Returns the created user profile (e.g. `UserProfileDto`-like shape):

```json
{
  "id": "guid-string",
  "userName": "johndoe",
  "email": "user@example.com",
  "phoneNumber": "+201234567890",
  "name": "John Doe",
  "profileImage": "blank.png",
  "status": "Active",
  "createdAt": "2025-02-15T12:00:00Z"
}
```

**Error (400)**

Validation or Identity errors (e.g. duplicate email) in one line:

```json
{
  "error": "Registration failed: Email 'user@example.com' is already taken."
}
```

---

### 2. Login

Sign in and get a JWT and user info.

| | |
|---|---|
| **Method** | `POST` |
| **URL** | `/api/Authentication/Login` |
| **Auth** | Not required |

**Request body**

```json
{
  "email": "user@example.com",
  "password": "YourPassword123"
}
```

| Field | Type | Required |
|-------|------|----------|
| `email` | string | Yes, valid email. |
| `password` | string | Yes. |

**Success (200)**

```json
{
  "email": "user@example.com",
  "displayName": "John Doe",
  "token": "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9..."
}
```

Use the `token` in the **Authorization** header for protected endpoints:

```
Authorization: Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9...
```

**Errors**

- **401** – Wrong password or user not found:  
  `{ "error": "Invalid  password" }` or similar.
- **404** – Email not found:  
  `{ "error": "User with email user@example.com is not found !!" }`

---

### 3. Check email

Check if an email is already registered.

| | |
|---|---|
| **Method** | `GET` |
| **URL** | `/api/Authentication/CheckEmail` |
| **Auth** | Not required |

**Query parameters**

| Name | Type | Required |
|------|------|----------|
| `email` | string | Yes |

**Example**

```
GET /api/Authentication/CheckEmail?email=user@example.com
```

**Success (200)**

```json
true
```
or
```json
false
```

- `true` = email exists  
- `false` = email does not exist  

---

### 4. Get current user

Get the profile of the logged-in user. Requires a valid JWT.

| | |
|---|---|
| **Method** | `GET` |
| **URL** | `/api/Authentication/GetCurrentUser` |
| **Auth** | **Required** – `Authorization: Bearer <token>` |

**Request headers**

```
Authorization: Bearer <your_jwt_token>
```

**Success (200)**

```json
{
  "id": "user-guid",
  "userName": "johndoe",
  "email": "user@example.com",
  "phoneNumber": "+201234567890",
  "name": "John Doe",
  "profileImage": "blank.png",
  "status": "Active",
  "createdAt": "2025-02-15T12:00:00Z"
}
```

**Errors**

- **401** – Missing or invalid token (no body or `"error"` message).
- **404** – User not found.

---

### 5. Forgot password

Request a password reset. Sends a reset link to the user’s email (or returns a token in development).

| | |
|---|---|
| **Method** | `POST` |
| **URL** | `/api/Authentication/ForgotPassword` |
| **Auth** | Not required |

**Request body**

```json
{
  "email": "user@example.com"
}
```

| Field | Type | Required |
|-------|------|----------|
| `email` | string | Yes, valid email. |

**Success (200)**

```json
{
  "success": true,
  "message": "If an account exists for this email, you will receive a password reset link.",
  "token": null
}
```

- In **development**, `token` may be set so the client can use it for **Reset password** without email.
- In **production**, `token` is usually `null`; the user gets the reset link by email.

---

### 6. Reset password

Set a new password using the token from **Forgot password** (or from the reset link).

| | |
|---|---|
| **Method** | `POST` |
| **URL** | `/api/Authentication/ResetPassword` |
| **Auth** | Not required |

**Request body**

```json
{
  "email": "user@example.com",
  "token": "reset-token-from-email-or-forgot-password-response",
  "newPassword": "NewSecurePassword123"
}
```

| Field | Type | Required | Rules |
|-------|------|----------|--------|
| `email` | string | Yes | Must match the account. |
| `token` | string | Yes | Token from Forgot password (or link). |
| `newPassword` | string | Yes | 6–100 characters. |

**Success (200)**

```json
{
  "success": true,
  "message": "Your password has been reset successfully.",
  "errors": null
}
```

**Error (400)**

```json
{
  "success": false,
  "message": "Failed to reset password.",
  "errors": ["Invalid token."]
}
```

---

## Quick reference

| Action | Method | Endpoint | Auth |
|--------|--------|----------|------|
| Register | POST | `/api/Authentication/Register` | No |
| Login | POST | `/api/Authentication/Login` | No |
| Check email | GET | `/api/Authentication/CheckEmail?email=` | No |
| Get current user | GET | `/api/Authentication/GetCurrentUser` | **Bearer** |
| Forgot password | POST | `/api/Authentication/ForgotPassword` | No |
| Reset password | POST | `/api/Authentication/ResetPassword` | No |

---

## Notes for frontend

1. **Login** → store `token` (e.g. in memory or secure storage) and send it as `Authorization: Bearer <token>` on every request to protected routes.
2. **Register** and **Login** errors use the same format: `{ "error": "message" }`. Show `error` to the user.
3. **Reset password** flow: call **Forgot password** first; in dev you may get `token` in the response; in production the user gets the link by email. Then call **Reset password** with `email`, `token`, and `newPassword`.

If you need the same documentation for other controllers (e.g. Cars, Analysis), say which ones and we can add them in the same style.




Toyota
Hyundai
Kia
Nissan
Chevrolet
BMW
Mercedes-Benz
Audi
Volkswagen
Renault
Peugeot
MG
Chery
Skoda
Fiat

analysis 
getall reports